import { describe, it, expect, vi, beforeEach } from 'vitest'
import { render, screen, waitFor } from '@testing-library/react'
import App from '@/App'

// Mock the agent service
vi.mock('@/services/agentService', () => ({
  agentService: {
    getChatHistory: vi.fn().mockResolvedValue({
      success: true,
      data: { sessionId: 'test-session', messages: [] },
    }),
    sendMessage: vi.fn(),
    streamMessage: vi.fn(),
  },
}))

describe('App', () => {
  beforeEach(() => {
    vi.clearAllMocks()
  })

  it('renders HR Agents header', () => {
    render(<App />)
    expect(screen.getByText('HR Agents')).toBeInTheDocument()
  })

  it('renders agent selector', () => {
    render(<App />)
    expect(screen.getByLabelText('Select Agent:')).toBeInTheDocument()
  })

  it('renders empty state message after loading', async () => {
    render(<App />)
    await waitFor(() => {
      expect(screen.getByText('💬 Start a conversation')).toBeInTheDocument()
    })
  })
})
