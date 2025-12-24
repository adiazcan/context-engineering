import { useCallback, useEffect } from 'react'
import { useChatContext } from '@/store/ChatContext'
import { agentService } from '@/services/agentService'
import type { ChatMessage } from '@/types/agent'

export function useChat() {
  const { state, addMessage, setMessages, setLoading, setError } = useChatContext()

  // Load chat history on mount
  useEffect(() => {
    const loadHistory = async () => {
      try {
        setLoading(true)
        const response = await agentService.getChatHistory(state.sessionId)
        if (response.success && response.data) {
          setMessages(response.data.messages)
        }
      } catch (error) {
        console.error('Failed to load chat history:', error)
      } finally {
        setLoading(false)
      }
    }

    loadHistory()
  }, [state.sessionId, setMessages, setLoading])

  const sendMessage = useCallback(
    async (content: string) => {
      if (!content.trim()) return

      const userMessage: ChatMessage = {
        role: 'user',
        content: content.trim(),
        timestamp: new Date(),
      }

      addMessage(userMessage)
      setLoading(true)
      setError(null)

      try {
        const response = await agentService.sendMessage({
          message: content.trim(),
          agentType: state.selectedAgent,
          sessionId: state.sessionId,
          userId: state.userId,
        })

        if (response.success && response.data) {
          const assistantMessage: ChatMessage = {
            role: 'assistant',
            content: response.data.response,
            timestamp: new Date(response.data.timestamp),
          }
          addMessage(assistantMessage)
        } else {
          setError(response.error || 'Failed to send message')
        }
      } catch (error) {
        setError(error instanceof Error ? error.message : 'An error occurred')
      } finally {
        setLoading(false)
      }
    },
    [state.selectedAgent, state.sessionId, state.userId, addMessage, setLoading, setError]
  )

  const sendMessageStreaming = useCallback(
    (content: string) => {
      if (!content.trim()) return () => {}

      const userMessage: ChatMessage = {
        role: 'user',
        content: content.trim(),
        timestamp: new Date(),
      }

      addMessage(userMessage)
      setLoading(true)
      setError(null)

      let assistantMessageContent = ''

      const cleanup = agentService.streamMessage(
        {
          message: content.trim(),
          agentType: state.selectedAgent,
          sessionId: state.sessionId,
          userId: state.userId,
        },
        chunk => {
          assistantMessageContent += chunk
          // Update the last message if it's from assistant, otherwise add new message
          const lastMessage = state.messages[state.messages.length - 1]
          if (lastMessage && lastMessage.role === 'assistant') {
            setMessages([
              ...state.messages.slice(0, -1),
              { ...lastMessage, content: assistantMessageContent },
            ])
          } else {
            addMessage({
              role: 'assistant',
              content: assistantMessageContent,
              timestamp: new Date(),
            })
          }
        },
        () => {
          setLoading(false)
        },
        error => {
          setError(error.message)
          setLoading(false)
        }
      )

      return cleanup
    },
    [
      state.selectedAgent,
      state.sessionId,
      state.userId,
      state.messages,
      addMessage,
      setMessages,
      setLoading,
      setError,
    ]
  )

  return {
    messages: state.messages,
    selectedAgent: state.selectedAgent,
    isLoading: state.isLoading,
    error: state.error,
    sendMessage,
    sendMessageStreaming,
  }
}
