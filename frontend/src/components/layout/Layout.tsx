import type { ReactNode } from 'react'
import './Layout.css'

interface LayoutProps {
  children: ReactNode
}

export function Layout({ children }: LayoutProps) {
  return (
    <div className="layout">
      <header className="layout-header">
        <h1>HR Agents</h1>
        <p>Your AI-powered HR assistant</p>
      </header>
      <main className="layout-main">{children}</main>
      <footer className="layout-footer">
        <p>© 2025 HR Agents. All rights reserved.</p>
      </footer>
    </div>
  )
}
