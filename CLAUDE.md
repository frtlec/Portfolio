# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

Backend + admin panel + public site for selinozoglu.com, a portfolio site. Originally built as ~7 microservices (Gateway, IdentityServer4, WorkItems, PhotoStock, MailSender, Setting) each with their own database; this was consolidated into a single monolith (`Portfolio.Api`) backed by one PostgreSQL instance. The route paths, auth token contract, and Angular API constants still carry naming from that history (e.g. `services/workitems/...` prefixes, `/connect/token`) — this is intentional backward compatibility with the existing Angular frontend, not leftover cruft to "fix".

## Commands

### Backend (.NET, net5.0 — EOL, but this repo's SDK still targets it)

```bash
# Build everything
dotnet build Portfolio.sln -c Release

# Run the API locally (needs local Postgres+RabbitMQ first, see below)
dotnet run --project Portfolio.Api
# listens on http://localhost:5000 / https://localhost:5001 (fixed in Properties/launchSettings.json)

# Local infra (Postgres on 5432, RabbitMQ on 5672/15672)
docker-compose -f docker-compose.local.yml up -d
```

There is no automated test suite in this repo.

EF Core migrations require a runtime roll-forward because the installed SDKs here are newer than net5.0's shared runtime:

```bash
DOTNET_ROLL_FORWARD=LatestMajor dotnet ef migrations add <Name> \
  --context <FullyQualifiedContextName> \
  --startup-project Portfolio.Api \
  --project <ProjectContainingTheDbContext> \
  -o Migrations[/<Subfolder>]
```

The four `DbContext`s and where their migrations live:
- `WorkItemsDbContext` — `Services/WorkItems/Portfolio.Services.WorkItems.Infrastructure` (schema `works`)
- `Portfolio.Api.Identity.IdentityDataContext` — `Portfolio.Api/Migrations/Identity` (schema `identity`)
- `Portfolio.Api.Mail.Infrastructure.MailDbContext` — `Portfolio.Api/Mail/Migrations` (schema `mail`)
- `Portfolio.Api.Settings.Infrastructure.SettingsDbContext` — `Portfolio.Api/Settings/Migrations` (schema `settings`)

All four point at the *same* Postgres database/connection string (`ConnectionStrings:PostgreSql`), separated only by schema — this is deliberate, see Architecture below.

### Frontend (Angular 13, `WebUI/SelinOzoglu/selin.ozoglu/`)

```bash
npm install
npm start        # ng serve, http://localhost:4200, points at local API via urlConstants.ts
npm run build     # ng build
npm test          # ng test (karma/jasmine)
```

## Architecture

### Portfolio.Api is one process, organized by module, not by layer

`Portfolio.Api/` hosts everything:
- `Controllers/` — every controller lives here flat (not nested per-module), matching standard ASP.NET Core convention. Route attributes still carry the module prefix, e.g. `[Route("services/workitems/[controller]")]`, `[Route("services/PhotoStock/[controller]")]`, `[Route("services/MailSender/[controller]")]`, `[Route("services/Settings/[controller]")]` — these paths mirror the old Ocelot gateway's upstream routes so the Angular app's URL construction didn't need to change during the migration.
- `Identity/` — the auth system: `ApplicationUser`, `IdentityDataContext` (ASP.NET Core Identity, schema `identity`), `TokenService` (issues/validates JWTs with a symmetric key from `Jwt:SigningKey`), `OAuthClients` (hardcoded client_id/secret pairs matching what the Angular app already sends — see `AuthController`).
- `Mail/`, `PhotoStock/`, `Settings/` — each module's services/dtos/models/infrastructure (own `DbContext` for Mail and Settings; PhotoStock has no DB, just reads/writes `wwwroot/photos` and `wwwroot/svg`).
- `Services/WorkItems/Portfolio.Services.WorkItems.{Domain,Domain.Core,Application,Infrastructure}` are separate projects referenced by `Portfolio.Api` — this Clean Architecture layering predates the microservices split and was kept as-is when everything else got flattened into `Portfolio.Api`. Only `Portfolio.Services.WorkItems.API` (the old standalone host) was removed; its controllers moved into `Portfolio.Api/Controllers`.

### Auth: hand-rolled JWT, not IdentityServer4 anymore

`AuthController.Token` (`POST /connect/token`) implements just enough of the OAuth token endpoint contract to satisfy the existing Angular `auth.service.ts`: `client_credentials` (public/read-only access, client `SelinOzogluUI`), `password` (admin panel login, client `SelinOzogluUIAdminPanel`), and `refresh_token`. Tokens carry a `scope` claim (`selin.ozoglu.com.work.read` / `.work.write`) that `[Authorize(Policy = "ReadAndWrite"/"WriteEditWork")]` checks against — same policy names/semantics as the old IdentityServer4 setup.

Gotcha worth knowing before touching `Startup.ConfigureServices`: `AddIdentity<>()` internally calls `AddAuthentication()` and registers its own cookie scheme as default. It must be registered *before* the `AddAuthentication().AddJwtBearer()` call (with `DefaultAuthenticateScheme`/`DefaultChallengeScheme`/`DefaultScheme` explicitly set to JWT), otherwise unauthenticated API requests get redirected to a nonexistent `/Account/Login` (404) instead of returning 401.

### Single Postgres, one schema per module

No more per-service databases. One `portfoliodb` Postgres instance, four schemas (`works`, `identity`, `mail`, `settings`), each owned by its respective `DbContext`. `Program.cs` runs `Database.Migrate()` for all four on startup and seeds two admin users (hardcoded in `Program.cs`, matching what used to be in the old IdentityServer's seed logic).

### Deployment

- `docker-compose.yml` (repo root) + `.github/workflows/deploy.yml`: on push to `master`, builds two images (`portfolio-api`, `portfolio-selinozoglu-webui`) and pushes to `ghcr.io/frtlec/`, then SSHes into the production VPS to `docker-compose pull && up -d --remove-orphans` in `/opt/Portfolio`. Requires `JWT_SIGNING_KEY` to be set in `/opt/Portfolio/.env` on the server (app fails fast on startup if missing — see `Portfolio.Api/Identity/TokenService.cs`).
- Host-level nginx on the VPS (outside this repo) reverse-proxies `www.`/`gateway.`/`identity.`/`photostock.selinozoglu.com` — all four now point at the same `portfolio.api` container (port 5100); only `www.` (the Angular webui, port 5110) is different. `converter.selinozoglu.com`/`convertapi.selinozoglu.com` in that same nginx config are unrelated to this repo.
- `docker-compose.local.yml`: Postgres + RabbitMQ only, for local dev (`Portfolio.Api` and the Angular app run outside Docker via `dotnet run` / `ng serve` for fast iteration).

### Frontend API URL configuration

`WebUI/SelinOzoglu/selin.ozoglu/src/shared/constants/urlConstants.ts` (local dev default, used by `ng serve`) vs. `urlConstants.prod.ts` (swapped in via `angular.json`'s `fileReplacements` for `ng build --configuration production`, which is what the Docker image build runs). Don't hand-edit which block is "active" by commenting/uncommenting — that pattern was replaced by the fileReplacements mechanism.
