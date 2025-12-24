export type AgentType = 'vacation' | 'procedure' | 'timesheet'

export interface ChatMessage {
  role: 'user' | 'assistant'
  content: string
  timestamp: Date
}

export interface ChatMessageRequest {
  message: string
  agentType: AgentType
  sessionId: string
  userId: string
}

export interface ChatMessageResponse {
  response: string
  sessionId: string
  timestamp: Date
}

export interface ChatHistoryResponse {
  sessionId: string
  messages: ChatMessage[]
}

export interface ApiResponse<T> {
  success: boolean
  data?: T
  error?: string
  errors?: Record<string, string[]>
  statusCode: number
}
