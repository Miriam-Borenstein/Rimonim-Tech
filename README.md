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
  "readings":
  {
    "2026-03-18T10:15:00Z": 1234.56,
    "2026-03-18T10:00:00Z": 1234.51
  }
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


### Run Locally (Minikube)

Run the following command to deploy the entire system:

minikube start

chmod +x deploy.sh
./deploy.sh


kubectl port-forward svc/metersystem-api 5000:80
