# Trade Flow

## **1. Overview**  
This system implements an **event-driven architecture** using **.NET 8, RabbitMQ, PostgreSQL (Marten and EF Core), MassTransit, and CQRS**. The goal is to **process financial data in real-time** across two microservices, each demonstrating a different architectural approach.

### **Rates Service (Vertical Slice Architecture + CQRS)**
- Fetches cryptocurrency exchange rates from the CoinMarketCap API.  
- Uses Marten (Event Store + PostgreSQL) for data persistence.  
- Implements an event-driven architecture with RabbitMQ + MassTransit.  
- Uses FluentValidation for input validation.  
- Implements CQRS using MediatR.  
- Uses Carter for lightweight API definition.  

### **Positions Service (Clean Architecture + DDD + CQRS)**
- Listens for `RateChangedEvent` and updates profit/loss calculations.  
- Uses a relational database (SQL Server / PostgreSQL) with EF Core.  
- Implements Clean Architecture and Domain-Driven Design (DDD).  
- Implements CQRS using MediatR.  
- Uses Mapster for efficient DTO mapping.  
- Implements event-driven communication with RabbitMQ + MassTransit.  

### **Communication**
- RabbitMQ + MassTransit for event-driven messaging.  
- Docker for deployment.

---

## **2. Technologies and Tools**
| Technology        | Description |
|--------------------|-------------------------------|
| .NET 8         | Primary framework for microservices. |
| MassTransit + RabbitMQ | Event-driven communication between services. |
| PostgreSQL + Marten (Rates Service) | NoSQL-like event store for data persistence. |
| SQL Server / PostgreSQL + EF Core (Positions Service) | Traditional relational database with ORM. |
| CQRS + MediatR | Separates commands and queries for scalability. |
| FluentValidation | Ensures API request validation. |
| Mapster | Efficient DTO conversion and mapping. |
| Carter | Minimal API framework for clean API definition. |
| Docker + Docker Compose | Containerized deployment. |

---

## **3. Architecture and Workflow**  

### **Rates Service (Vertical Slice Architecture + CQRS)**
**Workflow:**
1. Triggered by a scheduler (Hangfire/Background Service) or RabbitMQ event.  
2. Fetches cryptocurrency rates from CoinMarketCap API.  
3. Checks if the change is greater than 5%.  
4. Uses Marten Event Store to persist rate history.  
5. If the change is greater than 5%, publishes `RateChangedEvent` to RabbitMQ.  

**Technology stack:**
- Vertical Slice Architecture – Each feature is isolated, avoiding traditional service layers.  
- MediatR + CQRS – Commands (`FetchRatesCommand`) and queries (`GetRatesQuery`) are separated.  
- Marten – Event Store for efficient historical data tracking.  
- FluentValidation – Input validation for API requests.  
- Carter – Clean and minimal API definition.  

---

### **Positions Service (Clean Architecture + DDD + CQRS)**
**Workflow:**
1. Listens for `RateChangedEvent` from RabbitMQ.  
2. Queries the database for positions linked to that currency pair.  
3. Recalculates profit/loss based on the new exchange rate.  
4. Persists the updated values in SQL database (EF Core).  

**Technology stack:**
- Clean Architecture – Separation of `Application`, `Domain`, and `Infrastructure` layers.  
- Domain-Driven Design (DDD) – Entities (`Position`), aggregates, and repository pattern.  
- CQRS + MediatR – Separate handlers for `RecalculateProfitLossCommand` and `GetPositionsQuery`.  
- EF Core – ORM for relational data management.  
- Mapster – High-performance object mapping.  

---

## **4. Testing Strategy**
- Check RabbitMQ queues to verify event flow.  
- Review logs in Rates and Positions services (`dotnet run`).  
- Call `GET /positions` API to inspect changes in the database.  
- Use Postman to simulate `FetchRatesCommand` and `RateChangedEvent`.  

---

## **5. Deployment and Environment**
**Docker setup (`docker-compose.yml`)**  

---

## **6. Possible Enhancements (If Time Allows)**
- Implement a gRPC API for more efficient service-to-service communication.  
- Add Redis cache to reduce database queries.  
- Replace RabbitMQ with Kafka for higher scalability.  
- Introduce event sourcing for the Positions Service to track historical financial data.  

---

## **7. Conclusion**
- The system demonstrates two different architectural approaches: Vertical Slice + CQRS + Marten vs Clean Architecture + DDD + CQRS.  
- RabbitMQ + MassTransit enables event-driven messaging between services.  
- PostgreSQL is used differently in each service (Marten for NoSQL-like event storage vs EF Core for relational data).  
- Carter and FluentValidation provide lightweight and clean API handling.  
- Dockerized deployment ensures easy testing and scalability.  
