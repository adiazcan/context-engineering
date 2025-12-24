import { describe, it, expect } from 'vitest'
import { render, screen } from '@testing-library/react'
import { MessageList } from '@/components/chat/MessageList'

describe('MessageList', () => {
  it('renders empty state when no messages', () => {
    render(<MessageList messages={[]} isLoading={false} />)
    expect(screen.getByText('💬 Start a conversation')).toBeInTheDocument()
  })

  it('renders loading indicator when loading with no messages', () => {
    const { container } = render(<MessageList messages={[]} isLoading={true} />)
    expect(container.querySelector('.loading-indicator')).toBeInTheDocument()
  })

  it('renders messages', () => {
    const messages = [
      { role: 'user' as const, content: 'Hello', timestamp: new Date() },
      { role: 'assistant' as const, content: 'Hi there', timestamp: new Date() },
    ]

    render(<MessageList messages={messages} isLoading={false} />)
    expect(screen.getByText('Hello')).toBeInTheDocument()
    expect(screen.getByText('Hi there')).toBeInTheDocument()
  })

  it('shows loading indicator with messages', () => {
    const messages = [{ role: 'user' as const, content: 'Hello', timestamp: new Date() }]

    const { container } = render(<MessageList messages={messages} isLoading={true} />)
    expect(screen.getByText('Hello')).toBeInTheDocument()
    expect(container.querySelector('.loading-indicator')).toBeInTheDocument()
  })
})
