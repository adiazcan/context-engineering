import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MessageBubble } from '@/components/chat/MessageBubble'

describe('MessageBubble', () => {
  it('renders user message with correct styling', () => {
    const message = {
      role: 'user' as const,
      content: 'Hello there',
      timestamp: new Date('2025-12-24T10:00:00'),
    }

    const { container } = render(<MessageBubble message={message} />)
    expect(screen.getByText('Hello there')).toBeInTheDocument()
    expect(screen.getByText('You')).toBeInTheDocument()
    expect(container.querySelector('.message-user')).toBeInTheDocument()
  })

  it('renders assistant message with correct styling', () => {
    const message = {
      role: 'assistant' as const,
      content: 'How can I help?',
      timestamp: new Date('2025-12-24T10:00:00'),
    }

    const { container } = render(<MessageBubble message={message} />)
    expect(screen.getByText('How can I help?')).toBeInTheDocument()
    expect(screen.getByText('AI Agent')).toBeInTheDocument()
    expect(container.querySelector('.message-assistant')).toBeInTheDocument()
  })

  it('displays formatted timestamp', () => {
    const message = {
      role: 'user' as const,
      content: 'Test',
      timestamp: new Date('2025-12-24T14:30:00'),
    }

    render(<MessageBubble message={message} />)
    expect(screen.getByText(/2:30 PM|14:30/i)).toBeInTheDocument()
  })
})
