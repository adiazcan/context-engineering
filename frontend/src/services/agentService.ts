import type {
  ChatMessageRequest,
  ChatMessageResponse,
  ChatHistoryResponse,
  ApiResponse,
} from '@/types/agent'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || '/api'

export const agentService = {
  async sendMessage(request: ChatMessageRequest): Promise<ApiResponse<ChatMessageResponse>> {
    const response = await fetch(`${API_BASE_URL}/chat/message`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    })

    return response.json()
  },

  async getChatHistory(sessionId: string): Promise<ApiResponse<ChatHistoryResponse>> {
    const response = await fetch(`${API_BASE_URL}/chat/history/${sessionId}`)
    return response.json()
  },

  streamMessage(
    request: ChatMessageRequest,
    onMessage: (chunk: string) => void,
    onComplete: () => void,
    onError: (error: Error) => void
  ): () => void {
    const eventSource = new EventSource(
      `${API_BASE_URL}/chat/stream?` +
        new URLSearchParams({
          message: request.message,
          agentType: request.agentType,
          sessionId: request.sessionId,
          userId: request.userId,
        })
    )

    eventSource.onmessage = event => {
      if (event.data === '[DONE]') {
        eventSource.close()
        onComplete()
      } else {
        onMessage(event.data)
      }
    }

    eventSource.onerror = error => {
      eventSource.close()
      onError(new Error('Stream connection error'))
    }

    return () => eventSource.close()
  },
}
