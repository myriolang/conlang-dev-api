<div align="center">
<br><div><img height="118" src="https://raw.githubusercontent.com/myriolang/conlang-dev/main/public/conlang-dev-logo.svg" alt="conlang.dev"/></div>

[![Build status][shields-ci]][github-ci]

**conlang.dev** is a tool for conlangers and linguists to document their languages.<br>
This is the repository for the API back-end.
</div>

## 🔥 Development Information
### Overview
This app uses
* .NET 5.0 Web API
* MongoDB as a NoSQL database
* MediatR for interfacing mutations/queries

### Project Structure
* `Services/` contains interfaces for services, which are in place to mediate database access. Controllers/handlers should not use the database directly, and should instead go via service actions.
  * `Default/` is where implementations for the services are stored. These contain the main logic of the services used in this project.
  * `Setup/` contains services to be run on startup such as certain database setup jobs.
* `Models/` contains object representations of documents as stored in the database.
  * `Responses/` contains object representations that do not exist in the database and are merely containers of information to be returned in the API
* `Commands/` contains mediator data relating to various tasks that mutate or query the API's state. These are divided into folders for different areas of the API. Queries return data, whilst Mutations mutate data. Handlers contain the logic that returns the result for queries and mutations.
* `Controllers/` contains the controllers, which bind API endpoints to mediator commands and format the results if need be.

[shields-ci]: https://img.shields.io/github/workflow/status/myriolang/conlang-dev-api/deploy
[github-ci]: https://github.com/myriolang/conlang-dev-api/actions/workflows/deploy.yml
