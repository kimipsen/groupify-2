# Copilot Instructions

This file provides custom instructions for GitHub Copilot to follow when generating code suggestions in this repository.

## General Guidelines
- Follow the project's existing code style and conventions.
- Prefer clear, maintainable, and well-documented code.
- Use dependency injection and SOLID principles where applicable.
- For C#/.NET code, use async/await for asynchronous operations.
- For Angular/TypeScript code, use modern Angular best practices.
- Use vertical slice architecture for backend development (see `docs/architecture/vertical-slice.md`).
- The backend must support switching between SQLite and PostgreSQL databases using the appsettings file and EFCore. Ensure new features and migrations are compatible with both.
- Reference `.copilot/copilot.json` for ignore patterns and additional configuration.

## Domain-Specific Instructions
- Use the existing domain models in `src/backend/app/Domain/` for backend features.
- For new API endpoints, follow the structure in `src/backend/app/Features/`.
- For frontend features, use the structure in `src/frontend/app/src/app/`.

## Testing
- Add or update tests when introducing new features or fixing bugs.
- Ensure tests cover both SQLite and PostgreSQL scenarios where relevant.

## Release
- The frontend, when built, is copied to the backend project, to be included in the release.
- The backend is built as a single, self-contained deployment to allow anyone to run it, without any pre-requisites.

---
Edit this file to add more specific Copilot instructions for your project.
