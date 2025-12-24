import { Layout } from '@/components/layout/Layout'
import { ChatProvider } from '@/store/ChatContext'
import { ChatContainer } from '@/components/chat/ChatContainer'
import './App.css'

function App() {
  return (
    <ChatProvider>
      <Layout>
        <div className="app-container">
          <ChatContainer />
        </div>
      </Layout>
    </ChatProvider>
  )
}

export default App
