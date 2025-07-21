
# 📚 BookManagementAPI

A .NET 8 Web API for managing book records with CRUD operations, JWT authentication, logging, validation, and automated CI/CD via GitHub Actions.

---

## 🚀 Features

- 📖 CRUD operations for books
- 🔐 JWT-based authentication
- ✅ DTO validation using Data Annotations and FluentValidation
- 🧪 Unit and integration tests with xUnit
- 🧹 Code style enforcement using StyleCop.Analyzers
- 📈 Middleware-based request logging
- ⚙️ CI/CD with GitHub Actions

---

## 🏁 Getting Started

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 or later

### 💻 Run the Project

```bash
dotnet restore
dotnet build
dotnet run

## 🚀 CI/CD Pipeline

This project uses **GitHub Actions** to automate the Continuous Integration (CI) and Continuous Deployment (CD) processes for the BookManagementAPI.

---

### ✅ Objectives

- Automatically build the solution on each push or pull request.
- Run unit tests to verify correctness.
- Enforce code quality rules using **StyleCop**.
- Log output and performance metrics (optional: via Application Insights or custom middleware).
- Prevent merging broken or untested code into the main branch.

---

### 🔧 Pipeline Breakdown

The CI/CD pipeline is defined in `.github/workflows/dotnet.yml`. Here's what it does:

1. **Trigger**
   - Runs on `push` and `pull_request` to `main` or `feature/*` branches.

2. **Setup Environment**
   - Uses `.NET 8` setup on `ubuntu-latest`.

3. **Restore & Build**
   - Restores NuGet dependencies.
   - Builds the project and all dependencies.

4. **Test**
   - Runs all unit tests using `dotnet test`.

5. **Code Analysis**
   - Runs static code analysis using `StyleCop.Analyzers`.
   - Enforces code style and generates warnings if violations occur.

6. **Logs**
   - Logs build and test output for debugging.
   - Middleware in the app logs request/response times.

---

### ⚙️ Tools Used

- ✅ GitHub Actions — workflow orchestration
- ✅ .NET CLI — build, restore, test
- ✅ StyleCop.Analyzers — static code style enforcement
- ✅ xUnit — test framework
- ✅ Logging Middleware — performance monitoring

---

### 📦 Artifacts

This pipeline **does not generate deployment artifacts** at this stage. You can extend it to:
- Publish to Azure App Service
- Push Docker images to a container registry
- Notify teams via Slack or email

---

### 🧪 How to Run Locally

To run the steps locally:

```bash
dotnet restore
dotnet build
dotnet test
