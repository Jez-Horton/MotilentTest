# EntrolyticsNotifier

EntrolyticsNotifier is a Command Line Application developed for Motilent to send webhook notifications upon the publishing of reports within the Entrolytics imaging platform. The application accepts a JSON file as input, which includes details on where to send the HTTP POST notification, and sends a notification containing specified content. This README provides instructions on installation, usage, testing, and project structure.

## Features

- Reads a JSON file containing `notificationUrl` and `notificationContent`.
- Sends an HTTP POST request to the specified `notificationUrl`.
- Prints the response, including HTTP status code, content sent and received, and the response time.
- Includes modular components, error handling, and is testable with defined criteria.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Testing](#testing)
- [Project Structure](#project-structure)
- [Requirements](#requirements)
- [Assessment Criteria](#assessment-criteria)

## Installation

### 1. Extract the contents

cd {path}/EntrolyticsNotifier
dotnet build

### 2. Configure the JSON file
Create a JSON file with the following structure:

json
Copy code
{
  "notificationUrl": "https://your-webhook-url",
  "notificationContent": {
    "reportUID": "20fb8e02-9c55-410a-93a9-489c6f1d7598",
    "studyInstanceUID": "9998e02-9c55-410a-93a9-489c6f789798"
  }
}

Or make use of the two pre-created inside of the MotilentTest\EntrolyticsNotifier\bin\Debug\net8.0\

valid-request-local.json
valid-request-webhooksite.json

## Testing
### Running the API for direct response testing
Included in this project is a skeleton API to return the notification data to test against in console opposed to mocks. 

To run this, the sln should be setup with Multiple Startup Projects but if your local Visual Studio resets this then add the setting inside of right click SLN -> Properties -> Startup Projects and make sure both projects are selected otherwise your json will have to validate against a different live api

### Running Unit Tests
To execute unit tests, use the following command:

bash
dotnet test

## Test Coverage
The project includes test coverage for the following scenarios:

Valid JSON File: Ensures successful loading and sending of notifications.
Invalid JSON File: Verifies error handling for cases like missing fields, malformed JSON, or incorrect URLs.
HTTP Response Handling: Tests for both success (status code 200) and failure scenarios (e.g., 400 Bad Request).

## Project Structure

EntrolyticsNotifier/
├── Models/
│   ├── Notification.cs               # Defines Notification and NotificationContent classes
├── Services/
│   ├── NotificationLoader.cs         # Loads and parses JSON files
│   ├── NotificationSender.cs         # Sends HTTP POST requests
├── Program.cs                        # Main program with console interface
├── README.md                         # Project documentation
└── Tests/
    ├── NotificationLoaderTests.cs    # Unit tests for NotificationLoader
    ├── NotificationSenderTests.cs    # Unit tests for NotificationSender

### Requirements
.NET 6 SDK or later.
External Dependencies:
Newtonsoft.Json: for JSON parsing.
System.Diagnostics: for measuring response times.

## Assessment Criteria
The application was developed with attention to the following requirements:

Modularity: Separated responsibilities into NotificationLoader, NotificationSender, and the main console interface for clean code organization.
Error Handling: Catches errors like file not found, JSON parsing errors, and HTTP request failures with meaningful messages.
Naming Conventions: Follows C# naming conventions and uses PascalCase for public members and camelCase for private fields.
Test Coverage: Unit tests cover core functionalities, such as JSON loading, HTTP response handling, and error cases.
Idiomatic C# Usage: Utilizes async/await for asynchronous operations, dependency injection principles, and Stopwatch for response time measurement.
Clarity of Documentation: This README provides clear instructions on how to install, run, and test the application.