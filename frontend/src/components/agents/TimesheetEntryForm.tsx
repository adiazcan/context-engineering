import { useState, type FormEvent } from 'react'
import './TimesheetEntryForm.css'

interface TimesheetFormData {
  date: string
  hours: string
  projectCode: string
  description: string
}

interface TimesheetEntryFormProps {
  onSubmit: (data: TimesheetFormData) => void
  disabled?: boolean
}

export function TimesheetEntryForm({ onSubmit, disabled = false }: TimesheetEntryFormProps) {
  const [formData, setFormData] = useState<TimesheetFormData>({
    date: new Date().toISOString().split('T')[0],
    hours: '',
    projectCode: '',
    description: '',
  })

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    if (formData.date && formData.hours && formData.projectCode) {
      onSubmit(formData)
      setFormData({
        date: new Date().toISOString().split('T')[0],
        hours: '',
        projectCode: '',
        description: '',
      })
    }
  }

  return (
    <form className="timesheet-form" onSubmit={handleSubmit}>
      <h3 className="form-title">⏰ Timesheet Entry</h3>

      <div className="form-row">
        <div className="form-field">
          <label htmlFor="date">Date</label>
          <input
            type="date"
            id="date"
            value={formData.date}
            onChange={e => setFormData({ ...formData, date: e.target.value })}
            disabled={disabled}
            required
          />
        </div>

        <div className="form-field">
          <label htmlFor="hours">Hours</label>
          <input
            type="number"
            id="hours"
            min="0"
            max="24"
            step="0.5"
            value={formData.hours}
            onChange={e => setFormData({ ...formData, hours: e.target.value })}
            disabled={disabled}
            required
            placeholder="8.0"
          />
        </div>
      </div>

      <div className="form-field">
        <label htmlFor="project-code">Project Code</label>
        <input
          type="text"
          id="project-code"
          value={formData.projectCode}
          onChange={e => setFormData({ ...formData, projectCode: e.target.value })}
          disabled={disabled}
          required
          placeholder="e.g., PROJ-001"
        />
      </div>

      <div className="form-field">
        <label htmlFor="description">Description (Optional)</label>
        <textarea
          id="description"
          value={formData.description}
          onChange={e => setFormData({ ...formData, description: e.target.value })}
          placeholder="Describe your work..."
          disabled={disabled}
          rows={3}
        />
      </div>

      <button type="submit" className="form-submit-button" disabled={disabled}>
        Submit Entry
      </button>
    </form>
  )
}
