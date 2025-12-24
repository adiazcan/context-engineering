import { useChatContext } from '@/store/ChatContext'
import type { AgentType } from '@/types/agent'
import './AgentSelector.css'

const agents: Array<{ type: AgentType; name: string; icon: string; description: string }> = [
  { type: 'vacation', name: 'Vacation Agent', icon: '🏖️', description: 'Vacation requests' },
  { type: 'procedure', name: 'Procedure Agent', icon: '📋', description: 'HR procedures' },
  { type: 'timesheet', name: 'Timesheet Agent', icon: '⏰', description: 'Timesheet help' },
]

export function AgentSelector() {
  const { state, selectAgent } = useChatContext()

  return (
    <div className="agent-selector">
      <label htmlFor="agent-select" className="agent-selector-label">
        Select Agent:
      </label>
      <select
        id="agent-select"
        className="agent-selector-dropdown"
        value={state.selectedAgent}
        onChange={e => selectAgent(e.target.value as AgentType)}
      >
        {agents.map(agent => (
          <option key={agent.type} value={agent.type}>
            {agent.icon} {agent.name} - {agent.description}
          </option>
        ))}
      </select>
    </div>
  )
}
