# HR Agents Frontend

A React + TypeScript frontend for the HR Agents multi-agent chat application.

## Tech Stack

- **Framework**: React 19.2.0 with Vite 7.2.4
- **Language**: TypeScript 5.9.3 (strict mode)
- **UI Library**: CopilotKit (AG-UI implementation)
- **Testing**: Vitest + Testing Library
- **Code Quality**: ESLint + Prettier
- **Styling**: CSS with custom components

## Project Structure

```
src/
├── components/      # React components
│   └── layout/      # Layout components (Header, Footer)
├── hooks/           # Custom React hooks
├── services/        # API service layer
│   └── agentService.ts  # Backend communication
├── store/           # State management (if needed)
├── types/           # TypeScript type definitions
│   └── agent.ts     # Agent and API types
├── utils/           # Utility functions
│   └── sessionStorage.ts  # Session management
└── test/            # Test configuration and setup
```

## Available Scripts

- `npm run dev` - Start development server (port 5173)
- `npm run build` - Build for production
- `npm run preview` - Preview production build
- `npm run lint` - Run ESLint
- `npm run test` - Run tests in watch mode
- `npm run test:ui` - Run tests with UI
- `npm run test:coverage` - Generate coverage report
- `npm run format` - Format code with Prettier
- `npm run format:check` - Check code formatting

## Development

### Prerequisites

- Node.js 20+
- npm or pnpm

### Getting Started

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the development server:
   ```bash
   npm run dev
   ```

3. Open [http://localhost:5173](http://localhost:5173)

### Environment Variables

Create a `.env` file in the frontend directory:

```
VITE_API_BASE_URL=http://localhost:5288/api
```

## Architecture

The frontend follows these design principles:

- **Component-Based**: Modular, reusable components
- **Type Safety**: Full TypeScript with strict mode
- **Service Layer**: Centralized API communication
- **Path Aliases**: Clean imports with `@/` prefix
- **Test Coverage**: Minimum 80% coverage requirement

## API Integration

The frontend communicates with the .NET backend through:

- REST API for standard requests
- Server-Sent Events (SSE) for streaming responses
- Session management with localStorage
- Automatic user ID generation

## Testing

Tests are written using Vitest and Testing Library:

```bash
# Run all tests
npm test

# Run with coverage
npm run test:coverage

# Run with UI
npm run test:ui
```

## Code Style

- **Formatter**: Prettier (100 char line length)
- **Linter**: ESLint with TypeScript rules
- **Conventions**: camelCase for variables/functions, PascalCase for components

## Next Steps

- [ ] Implement chat interface components
- [ ] Add AG-UI integration with backend
- [ ] Create agent-specific chat views
- [ ] Add authentication with Azure Entra ID
- [ ] Implement state management
- [ ] Add E2E tests

## Contributing

See the root [CONTRIBUTING.md](../CONTRIBUTING.md) for guidelines.

## License

Copyright © 2025 HR Agents. All rights reserved.
