# BFR

BFR is an PoC or the Start of the Fan-Made Brave Frontier Re:Coded Server-API

## Prioritization

To use the API effectively, we need to prioritize the following:

1. Authentication
2. An efficient method to retrieve assets that balances bundling assets with the base game and downloading each file individually
3. APIs for game data such as available units and dungeon contents
4. Different aspects of player data such as unit storage, item storage, and story progress.

## TechStack

To achieve optimal performance, we plan to use:

1. Entity Framework Core for data access, ensuring the correct configuration and separate connections for read and write operations.
2. Redis Caching to reduce database calls and improve API response times, with an appropriate invalidation strategy.
3. PostgreSQL Database for its performance and scalability.
4. Mapster for its ease of use and performance.
5. Logstash for logging.
6. Swagger(-UI) for testing and documentation using OpenAI standards.
7. xUnit (and Moq) as the Testing Framework most widely used paired with Dependency Injection

## Considerations

We also need to consider:

1. Data separation/CQRS to maximize performance by separating read and write operations with separate database connections.
2. SOLID principles for maintainable and extensible code, including Single Responsibility, Open-Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion.
3. Performance optimization, including reducing database calls and ensuring scalability.
4. Efficient algorithms for data manipulation and processing, such as binary search, sorting, and hashing where applicable.
5. Security measures such as authentication, SQL injection prevention, cross-site scripting (XSS) prevention, cross-site request forgery (CSRF) prevention, input validation, parameterized queries, and HTTPS.
6. Testing with unit tests, etc.
7. Automated deployment.

## Solution Structure

The proposed folder structure for the solution is as follows:

- BFR.API
  - Controllers/
  - Intents/ (Model for Request)
  - Filters/
  
- BFR.Core
  - Services/
  - Enums/
  - Interfaces/
  - Entities/
  - DTO/
  - Queries/ (EF-Core compiled queries)
  
- BFR.Infrastructure
  - Caching/
  - Logging/
  - Database/
    - Migrations/
    - Repositories/
  - Mapping/
  
- BFR.Tests
  - ...

