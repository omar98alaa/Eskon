# Eskon - Airbnb Clone Web API

## Overview

Eskon is a web API for an Airbnb clone project, allowing users to book places to stay. The project implements a Clean Architecture, leveraging modern design patterns and libraries such as CQRS, MediatR, SignalR for real-time chat and notifications, and various integrations for payment and email services.

Key features of the project:

* **Role-based Management**: Admin, Owner, and Customer roles.
* **Authentication**: JWT Authentication with Role-based Authorization.
* **Payment Integration**: Stripe for payments and refunds using the Destination Charges model.
* **Email Service**: Mailgun integration for sending emails (e.g., booking confirmation, user notifications).
* **Real-time Notifications and Chat**: SignalR for chat functionality and real-time updates.

## Project Structure

* **API**: The main Web API layer where all HTTP requests and responses are managed.
* **Application**: Contains the business logic and MediatR commands/queries for CQRS.
* **Domain**: The core entities, value objects, and interfaces for the system.
* **Infrastructure**: Contains implementations for the external services, such as Stripe, Mailgun, SignalR, and data access.
* **Common**: Shared utilities, constants, and helper methods.
  
## Prerequisites

Before running the project, make sure you have the following installed:

1. **Visual Studio 2022** (VS2022) - Required for running and developing the project.
2. **.NET 9 SDK** - The application is built on .NET 9.0.
3. **Stripe Account** - For handling payments and refunds (you will need your Stripe keys for configuration).
4. **Mailgun Account** - For sending emails (you will need Mailgun API credentials for configuration).
5. **SQL Server** - For database storage.

## How to Run the Project

### 1. Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/omar98alaa/Eskon.git
```

### 2. Open the Project in Visual Studio 2022

1. Open **Visual Studio 2022**.
2. Select **Open a Project or Solution**.
3. Browse to the folder where you cloned the repository and select the **Eskon.sln** solution file.

### 3. Configure AppSettings

In the `appsettings.json` file, you will need to provide configuration values for services like Stripe, Mailgun, and any other environment-specific settings. The placeholders are already present, but you will need to fill in the appropriate values:

#### Example Configuration:

```json
{
  "AppSettings": {
    "JwtSecret": "your-jwt-secret-key",
    "JwtIssuer": "your-issuer",
    "JwtAudience": "your-audience",
    "JwtExpirationInMinutes": 120
  },
  "StripeSettings": {
    "ApiKey": "your-stripe-api-key",
    "WebhookSecret": "your-stripe-webhook-secret"
  },
  "MailgunSettings": {
    "ApiKey": "your-mailgun-api-key",
    "Domain": "your-mailgun-domain",
    "FromEmail": "your-email@example.com"
  },
  "ConnectionStrings": {
    "DefaultConnection": "your-sql-connection-string"
  }
}
```

Ensure the following configurations are set:

* **JwtSecret**: This is the secret key used for signing JWT tokens.
* **StripeSettings**: Include your Stripe API key and Webhook secret.
* **MailgunSettings**: Include your Mailgun API key, domain, and from email for sending email notifications.
* **ConnectionStrings**: Provide the SQL Server connection string to your database.

### 4. Database Migration

The project uses **Entity Framework Core** for data access. To apply the database migrations, open **Package Manager Console** in Visual Studio and run the following commands:

```bash
Update-Database
```

This will apply the migrations and set up the necessary tables in your database.

### 5. Run the Project

After completing the configuration and database setup, you can run the project:

1. In **Visual Studio 2022**, select **IIS Express** (or your preferred target) and click **Run**.
2. The API should now be running locally, and you can interact with it through Postman or any other HTTP client.
