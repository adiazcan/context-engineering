# System Architecture

## Overview
This is a fullstack agent chat application with a .NET minimal API backend and a TypeScript frontend. The system is designed to facilitate real-time communication between users and agents, leveraging modern web technologies for scalability and maintainability. 

## Technology Stack
- **Backend**: .NET, Microsoft Agent Framework
- **Frontend**: TypeScript, AG-UI, CopilotKit
- **Architecture**: real-time streaming, state management, and interactive UI components

## Design Patterns
- **Microservices**: The backend is structured as a collection of microservices, each responsible for a specific domain of functionality.
- **Dependency Injection**: Services and repositories are injected into controllers to enhance testability and maintainability.
- **Event-Driven Architecture**: The system utilizes events to facilitate communication between services, enhancing scalability and responsiveness.

## Folder structure
```
/backend
/frontend
/docs
```