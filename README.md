# DataExchange

## Project Overview



**DataExchange** is a robust data exchange platform designed to facilitate reliable and scalable data transfer between microservices and external systems. The project leverages **Kafka** as a distributed message queue, **Debezium** for change data capture (CDC), and implements the **Transactional Outbox** pattern to ensure data consistency and integrity.

---

## Architecture

1. **Transactional Outbox Pattern**  
   Each service writes events to an "outbox" table within the same database transaction as its business data. This ensures that either both the business data and the event are committed, or neither is, preventing data loss or duplication.

2. **Debezium**  
   Debezium monitors the outbox table for new events using CDC. When a new event is detected, Debezium streams the change to Kafka, ensuring that all changes are captured and published in real-time.

3. **Kafka**  
   Kafka acts as the central message broker, receiving events from Debezium and distributing them to interested consumers. This decouples producers and consumers, allowing for scalable and fault-tolerant data exchange.

---

## Data Flow

1. **Service writes to DB and Outbox:**  
   - A microservice processes a business transaction and writes both the business data and an event record to the outbox table in a single atomic transaction.

2. **Debezium captures the event:**  
   - Debezium detects the new outbox entry and publishes it to a Kafka topic.

3. **Kafka distributes the event:**  
   - Downstream services consume the event from Kafka and process it as needed.

---

## Benefits

- **Atomicity & Consistency:**  
  The transactional outbox pattern ensures that business data and events are always in sync.

- **Scalability:**  
  Kafka enables high-throughput, distributed event streaming.

- **Loose Coupling:**  
  Producers and consumers are decoupled, allowing independent scaling and evolution.

- **Reliability:**  
  Debezium ensures no events are missed, and Kafka provides durable message storage.

---

## Example Use Case

1. **Order Service** writes a new order and an "OrderCreated" event to its database and outbox table.
2. **Debezium** detects the new "OrderCreated" event and streams it to the `order-events` Kafka topic.
3. **Inventory Service** consumes the event from Kafka and updates stock levels accordingly.

---

## Technologies Used

- **Apache Kafka**: Distributed streaming platform for event delivery.
- **Debezium**: Change data capture tool for streaming database changes.
- **Transactional Outbox**: Pattern for reliable event publishing.

---

## References

- [Debezium Documentation](https://debezium.io/documentation/)
- [Kafka Documentation](https://kafka.apache.org/documentation/)
- [Transactional Outbox Pattern](https://microservices.io/patterns/data/transactional-outbox.html)

