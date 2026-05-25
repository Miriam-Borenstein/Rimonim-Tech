## Meter System

A distributed meter readings processing system built with ASP.NET Core, RabbitMQ, PostgreSQL, and Kubernetes.

The system accepts batches of meter readings through an HTTP API, processes them asynchronously using RabbitMQ, and stores them in PostgreSQL while ensuring idempotent inserts.

## Features
Accept batch readings per meter
Asynchronous processing using RabbitMQ
Background worker for queue consumption
PostgreSQL persistence
Idempotent inserts using unique database constraints
Kubernetes deployment using Minikube
Separation between API, Worker, Messaging, and Shared contracts

## Processing Flow
1. Client sends readings to the API
2. API validates the request
3. Readings are published to RabbitMQ
4. Worker consumes messages from the queue
5. Worker creates the meter if it does not exist
6. Readings are inserted into PostgreSQL
7. Duplicate readings are ignored using database constraints

## API Endpoint
#### Submit Meter Readings
POST /readings

Request Example: 
{
  "meter_number": 12345,
  "readings": {
    "2026-03-18T10:15:00Z": 1234.56,
    "2026-03-18T10:00:00Z": 1234.51}
}

## Design Highlights

Event-driven architecture for scalability
Separation of API and processing logic
Asynchronous processing via message queue
Idempotent data handling using database constraints

## Run Locally (Minikube)
#### Prerequisites

Docker Desktop
Minikube
kubectl
.NET 8 SDK


#### Steps

minikube start

kubectl apply -f queue/deploy.yaml

kubectl apply -f database/deploy.yaml

kubectl exec -i postgres-65c498f85d-m7p4x -- psql -U postgres -d meters < database/schema.sql

docker build -t metersystem-api -f src/MeterSystem.Api/Dockerfile .

minikube image load metersystem-api:latest

kubectl apply -f src/MeterSystem.Api/deploy.yaml

docker build -t metersystem-worker -f src/MeterSystem.Worker/Dockerfile .

minikube image load metersystem-worker:latest 

kubectl apply -f src/MeterSystem.Worker/deploy.yaml

