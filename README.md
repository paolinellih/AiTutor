# AiTutor Project

!['AiTutor, Henrique Paolinelli'](https://github.com/paolinellih/AiTutor/blob/main/AiTutor.png)

Welcome to AiTutor, an AI-powered tutoring system built with the latest technologies and following clean architecture principles, Domain-Driven Design (DDD), SOLID principles, and utilizing both Ollama AI and Redis for efficient data processing.

## Overview

AiTutor is designed to help users with educational content generation and explanations. The project is built using Docker, making it easy to set up and run all the necessary services with minimal configuration. The system integrates several technologies:

- **Clean Architecture**: Ensures the separation of concerns, scalability, and testability.
- **DDD (Domain-Driven Design)**: Focuses on the core business logic and domain modeling, providing a robust foundation for the system.
- **SOLID Principles**: Ensures maintainable, flexible, and scalable code with well-defined responsibilities.

## Prerequisites

Before running AiTutor, make sure you have the following prerequisites installed on your machine:

1. **Docker**: AiTutor uses Docker containers to manage services. Ensure Docker is installed and running on your machine.
2. **Docker Compose**: Docker Compose is used to orchestrate the services for the AiTutor project.

You can install these tools from the following links:
- [Install Docker](https://www.docker.com/get-started)
- [Install Docker Compose](https://docs.docker.com/compose/install/)

## Getting Started

### 1. Clone the Repository

First, clone the repository to your local machine:

```bash
git clone https://github.com/your-username/AiTutor.git
cd AiTutor
```

### 2. Build and Start the Services

Once you have Docker and Docker Compose installed, you can easily start AiTutor by running the following command:

```bash
docker-compose up --build
```

This will:
1. Build the Docker images.
2. Start the AiTutor API, Ollama AI model service, and Redis.

### 3. Wait for the AI Model to Be Downloaded

The Ollama model (`mistral` in this case) may take some time to download, as it is a large file. During this process, the log will show "pulling manifest" and downloading information. Once the model is fully downloaded, AiTutor will be ready to use.

You should see messages like:
```
pulling manifest...
pulling ff82381e2bea... 100% ▕████████████████▏ 4.1 GB
verifying sha256 digest
writing manifest
success
```

### 4. Use the API

After the services are up and the AI model is downloaded, you can interact with the API. AiTutor exposes several endpoints for educational content generation.

For example, you can send a POST request to the following endpoint to get an explanation:

```Curl
curl -X 'POST' \
  'http://localhost:5000/api/Ai/explain' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '"Mathematics Golden Ratio"'
```

This will return a detailed explanation of the golden ratio.

Or just access 'http://localhost:5000/swagger'

## Technologies Used

- **Ollama AI**: Ollama provides an easy-to-use AI model serving API. We use Ollama for various AI tasks such as explanations and content generation.
- **Redis**: Redis is used for caching and managing sessions efficiently to ensure fast response times and scalability.

## Project Structure

The project is structured using **Clean Architecture**, ensuring that different layers (presentation, domain, and data) are well-separated. Key components of the project include:

- **API Layer**: Exposes HTTP endpoints to interact with the AiTutor system.
- **Application Layer**: Contains use cases and business logic for handling requests.
- **Domain Layer**: Represents core business logic and domain models.
- **Infrastructure Layer**: Handles the implementation of external services like Redis and Ollama API.

## Clean Architecture & DDD

The project follows **Clean Architecture** principles, ensuring that:
- The core business logic is independent of frameworks, databases, and external services.
- Dependencies flow inwards, keeping the core domain unaffected by external changes.

**Domain-Driven Design (DDD)** is used to focus on the core business needs, defining clear boundaries and responsibilities for the different parts of the system.

## SOLID Principles

The codebase adheres to the **SOLID** principles to ensure that it is:
- **S**ingle Responsibility: Each class has one reason to change.
- **O**pen/Closed: Classes are open for extension but closed for modification.
- **L**iskov Substitution: Derived classes can be substituted for their base class.
- **I**nterface Segregation: Interfaces are client-specific and not general-purpose.
- **D**ependency Inversion: High-level modules should not depend on low-level modules, but both should depend on abstractions.

## Troubleshooting

### Ollama Service Not Responding

In case the Ollama service doesn't respond immediately, give it some time for the AI model to be fully downloaded and initialized. Once it's ready, AiTutor will be fully operational.

## Conclusion

AiTutor is an AI-powered tutoring system that provides detailed explanations on various topics. With **Docker Compose**, setting up and running the system is as simple as running `docker-compose up --build`. The system follows **Clean Architecture**, **DDD**, and **SOLID principles**, ensuring scalability and maintainability.

I hope you enjoy exploring AiTutor!
