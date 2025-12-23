# System Architecture

## Overview
Our application follows a microservices architecture with three main services...

## Technology Stack
- Frontend: React 18 with TypeScript
- Backend: Node.js with Express
- Database: PostgreSQL with Prisma ORM
- Cache: Redis

## Design Patterns
- Repository pattern for data access
- Factory pattern for service instantiation
- Observer pattern for event handling

## Data Flow
1. Client requests hit the API Gateway
2. Gateway routes to appropriate microservice
3. Service layer handles business logic
4. Repository layer manages data persistence