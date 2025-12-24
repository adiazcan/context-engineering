import { AgentSelector } from './AgentSelector'
import { MessageList } from './MessageList'
import { MessageInput } from './MessageInput'
import { useChat } from '@/hooks/useChat'
import './ChatContainer.css'

export function ChatContainer() {
  const { messages, isLoading, error, sendMessage } = useChat()

  return (
    <div className="chat-container">
      <AgentSelector />
      {error && (
        <div className="chat-error">
          <span className="error-icon">⚠️</span>
          <span className="error-message">{error}</span>
        </div>
      )}
      <MessageList messages={messages} isLoading={isLoading} />
      <MessageInput onSendMessage={sendMessage} disabled={isLoading} />
    </div>
  )
}
