const SESSION_KEY = 'hr-agent-session-id'
const USER_KEY = 'hr-agent-user-id'

export const sessionStorage = {
  getSessionId(): string {
    let sessionId = localStorage.getItem(SESSION_KEY)
    if (!sessionId) {
      sessionId = crypto.randomUUID()
      localStorage.setItem(SESSION_KEY, sessionId)
    }
    return sessionId
  },

  getUserId(): string {
    let userId = localStorage.getItem(USER_KEY)
    if (!userId) {
      userId = `user-${crypto.randomUUID()}`
      localStorage.setItem(USER_KEY, userId)
    }
    return userId
  },

  clearSession(): void {
    localStorage.removeItem(SESSION_KEY)
    localStorage.removeItem(USER_KEY)
  },

  resetSession(): void {
    this.clearSession()
    return this.getSessionId()
  },
}
