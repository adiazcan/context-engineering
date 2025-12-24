import { useState, type FormEvent } from 'react'
import './VacationRequestForm.css'

interface VacationFormData {
  startDate: string
  endDate: string
  reason: string
}

interface VacationRequestFormProps {
  onSubmit: (data: VacationFormData) => void
  disabled?: boolean
}

export function VacationRequestForm({ onSubmit, disabled = false }: VacationRequestFormProps) {
  const [formData, setFormData] = useState<VacationFormData>({
    startDate: '',
    endDate: '',
    reason: '',
  })

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    if (formData.startDate && formData.endDate && formData.reason.trim()) {
      onSubmit(formData)
      setFormData({ startDate: '', endDate: '', reason: '' })
    }
  }

  return (
    <form className="vacation-form" onSubmit={handleSubmit}>
      <h3 className="form-title">📝 Vacation Request</h3>

      <div className="form-field">
        <label htmlFor="start-date">Start Date</label>
        <input
          type="date"
          id="start-date"
          value={formData.startDate}
          onChange={e => setFormData({ ...formData, startDate: e.target.value })}
          disabled={disabled}
          required
        />
      </div>

      <div className="form-field">
        <label htmlFor="end-date">End Date</label>
        <input
          type="date"
          id="end-date"
          value={formData.endDate}
          onChange={e => setFormData({ ...formData, endDate: e.target.value })}
          disabled={disabled}
          required
        />
      </div>

      <div className="form-field">
        <label htmlFor="reason">Reason</label>
        <textarea
          id="reason"
          value={formData.reason}
          onChange={e => setFormData({ ...formData, reason: e.target.value })}
          placeholder="Reason for vacation request..."
          disabled={disabled}
          required
          rows={3}
        />
      </div>

      <button type="submit" className="form-submit-button" disabled={disabled}>
        Submit Request
      </button>
    </form>
  )
}
