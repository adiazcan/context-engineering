import { createContext, useContext, useReducer, useCallback, type ReactNode } from 'react'
import type { ChatMessage, AgentType } from '@/types/agent'
import { sessionStorage } from '@/utils/sessionStorage'

interface ChatState {
  messages: ChatMessage[]
  selectedAgent: AgentType
  sessionId: string
  userId: string
  isLoading: boolean
  error: string | null
}

type ChatAction =
  | { type: 'ADD_MESSAGE'; payload: ChatMessage }
  | { type: 'SET_MESSAGES'; payload: ChatMessage[] }
  | { type: 'SELECT_AGENT'; payload: AgentType }
  | { type: 'SET_LOADING'; payload: boolean }
  | { type: 'SET_ERROR'; payload: string | null }
  | { type: 'CLEAR_MESSAGES' }
  | { type: 'RESET_SESSION' }

interface ChatContextValue {
  state: ChatState
  addMessage: (message: ChatMessage) => void
  setMessages: (messages: ChatMessage[]) => void
  selectAgent: (agent: AgentType) => void
  setLoading: (isLoading: boolean) => void
  setError: (error: string | null) => void
  clearMessages: () => void
  resetSession: () => void
}

const ChatContext = createContext<ChatContextValue | undefined>(undefined)

function chatReducer(state: ChatState, action: ChatAction): ChatState {
  switch (action.type) {
    case 'ADD_MESSAGE':
      return {
        ...state,
        messages: [...state.messages, action.payload],
      }
    case 'SET_MESSAGES':
      return {
        ...state,
        messages: action.payload,
      }
    case 'SELECT_AGENT':
      return {
        ...state,
        selectedAgent: action.payload,
      }
    case 'SET_LOADING':
      return {
        ...state,
        isLoading: action.payload,
      }
    case 'SET_ERROR':
      return {
        ...state,
        error: action.payload,
      }
    case 'CLEAR_MESSAGES':
      return {
        ...state,
        messages: [],
      }
    case 'RESET_SESSION':
      return {
        ...state,
        messages: [],
        sessionId: crypto.randomUUID(),
        error: null,
      }
    default:
      return state
  }
}

export function ChatProvider({ children }: { children: ReactNode }) {
  const [state, dispatch] = useReducer(chatReducer, {
    messages: [],
    selectedAgent: 'vacation',
    sessionId: sessionStorage.getSessionId(),
    userId: sessionStorage.getUserId(),
    isLoading: false,
    error: null,
  })

  const addMessage = useCallback((message: ChatMessage) => {
    dispatch({ type: 'ADD_MESSAGE', payload: message })
  }, [])

  const setMessages = useCallback((messages: ChatMessage[]) => {
    dispatch({ type: 'SET_MESSAGES', payload: messages })
  }, [])

  const selectAgent = useCallback((agent: AgentType) => {
    dispatch({ type: 'SELECT_AGENT', payload: agent })
  }, [])

  const setLoading = useCallback((isLoading: boolean) => {
    dispatch({ type: 'SET_LOADING', payload: isLoading })
  }, [])

  const setError = useCallback((error: string | null) => {
    dispatch({ type: 'SET_ERROR', payload: error })
  }, [])

  const clearMessages = useCallback(() => {
    dispatch({ type: 'CLEAR_MESSAGES' })
  }, [])

  const resetSession = useCallback(() => {
    sessionStorage.resetSession()
    dispatch({ type: 'RESET_SESSION' })
  }, [])

  return (
    <ChatContext.Provider
      value={{
        state,
        addMessage,
        setMessages,
        selectAgent,
        setLoading,
        setError,
        clearMessages,
        resetSession,
      }}
    >
      {children}
    </ChatContext.Provider>
  )
}

export function useChatContext() {
  const context = useContext(ChatContext)
  if (context === undefined) {
    throw new Error('useChatContext must be used within a ChatProvider')
  }
  return context
}
