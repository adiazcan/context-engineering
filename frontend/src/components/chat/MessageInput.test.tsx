import { describe, it, expect, vi } from 'vitest'
import { render, screen, fireEvent } from '@testing-library/react'
import { MessageInput } from '@/components/chat/MessageInput'

describe('MessageInput', () => {
  it('renders textarea and send button', () => {
    const mockSend = vi.fn()
    render(<MessageInput onSendMessage={mockSend} disabled={false} />)

    expect(screen.getByPlaceholderText(/Type your message/i)).toBeInTheDocument()
    expect(screen.getByRole('button', { name: /➤/i })).toBeInTheDocument()
  })

  it('calls onSendMessage when form is submitted', () => {
    const mockSend = vi.fn()
    render(<MessageInput onSendMessage={mockSend} disabled={false} />)

    const textarea = screen.getByPlaceholderText(/Type your message/i)
    fireEvent.change(textarea, { target: { value: 'Hello agent' } })
    fireEvent.submit(screen.getByRole('button').closest('form')!)

    expect(mockSend).toHaveBeenCalledWith('Hello agent')
  })

  it('clears input after sending', () => {
    const mockSend = vi.fn()
    render(<MessageInput onSendMessage={mockSend} disabled={false} />)

    const textarea = screen.getByPlaceholderText(/Type your message/i) as HTMLTextAreaElement
    fireEvent.change(textarea, { target: { value: 'Hello' } })
    fireEvent.submit(screen.getByRole('button').closest('form')!)

    expect(textarea.value).toBe('')
  })

  it('disables input when disabled prop is true', () => {
    const mockSend = vi.fn()
    render(<MessageInput onSendMessage={mockSend} disabled={true} />)

    const textarea = screen.getByPlaceholderText(/Type your message/i)
    const button = screen.getByRole('button')

    expect(textarea).toBeDisabled()
    expect(button).toBeDisabled()
  })

  it('does not send empty messages', () => {
    const mockSend = vi.fn()
    render(<MessageInput onSendMessage={mockSend} disabled={false} />)

    const textarea = screen.getByPlaceholderText(/Type your message/i)
    fireEvent.change(textarea, { target: { value: '   ' } })
    fireEvent.submit(screen.getByRole('button').closest('form')!)

    expect(mockSend).not.toHaveBeenCalled()
  })
})
