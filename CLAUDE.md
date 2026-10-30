# Newsletter - instructions for AI agents

## Architecture

- Layers: Domain <- Application <- Infrastructure, Api. Dependencies point inward.
- Endpoints call an Application handler. Never inject a repository into an endpoint.
- Use cases live in Application/<Feature>/<UseCase>/ as <UseCase>Command|Query + Handler (sealed).
- Never return Domain entities from the API - map to a <Name>Response record.
- Repository implementations in Infrastructure stay internal sealed.

## Cleanup rules

- `dotnet build` must pass with zero warnings. Warnings are errors in this repo.
- Never silence a diagnostic: no `!` (null-forgiving), no `#pragma warning disable`,
  no `[SuppressMessage]`, no `<NoWarn>`. If a fix needs one, stop and ask.
- Fix one rule ID at a time, then rebuild.
- Namespace changes (IDE0130): use an IDE refactoring, not `dotnet format`.

## Definition of done

- `dotnet build` passes with zero warnings.
- `dotnet test` passes, including Newsletter.ArchitectureTests.
- `dotnet format --verify-no-changes` passes.
- Never edit, skip or delete an architecture test to make it pass. If a rule seems wrong, stop and ask.
