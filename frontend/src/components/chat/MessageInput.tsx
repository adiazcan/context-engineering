import { useState, type FormEvent, type KeyboardEvent } from 'react'
import './MessageInput.css'

interface MessageInputProps {
  onSendMessage: (message: string) => void
  disabled: boolean
}

export function MessageInput({ onSendMessage, disabled }: MessageInputProps) {
  const [message, setMessage] = useState('')

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    if (message.trim() && !disabled) {
      onSendMessage(message)
      setMessage('')
    }
  }

  const handleKeyDown = (e: KeyboardEvent<HTMLTextAreaElement>) => {
    if (e.key === 'Enter' && !e.shiftKey) {
      e.preventDefault()
      handleSubmit(e)
    }
  }

  return (
    <form className="message-input-container" onSubmit={handleSubmit}>
      <textarea
        className="message-input-field"
        value={message}
        onChange={e => setMessage(e.target.value)}
        onKeyDown={handleKeyDown}
        placeholder="Type your message... (Shift+Enter for new line)"
        disabled={disabled}
        rows={1}
      />
      <button type="submit" className="message-send-button" disabled={disabled || !message.trim()}>
        <span className="send-icon">➤</span>
      </button>
    </form>
  )
}
