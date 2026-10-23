# Working on the e-commerce sample

- **Layout:** `src/` (the .NET projects: `NetCoreApp` host, `Queryable`, `Process`, `Data`, `Shared`), `ui/` (Angular), `docs/`, `tests/`. See the [README](README.md).
- **Kaleido is a package** (`Directory.Packages.props`, one `KaleidoVersion` for every Kaleido package) from its GitHub Packages feed (`nuget.config`). Raise `KaleidoVersion` by hand: Dependabot cannot read the feed. The token lives in your user-level NuGet configuration, never in this repository.
- **Databases are generated, never committed** (`*.sqlite` is ignored). `ECommerceDbInitializer` creates and seeds the catalog when it does not exist; `--reseed` rebuilds it. Keep the seed deterministic: the seeders draw from `new Random(SeedSettings.RandomSeed)`, never `Random.Shared`, and generated keys that must be unique (SKUs) are unique by construction.
- **Verify:**
  - backend: `dotnet build Kaleido.Samples.ECommerce.slnx` (0 warnings: warnings are errors), and start the service once after a data or seed change;
  - UI: `npm run build` and `npm test` in `ui/`; `npm audit` stays at 0, upgrading only to versions published at least 7 days earlier, never `npm audit fix --force`.
- `ui/package.json` `overrides` pin `vite` 7.3.7 and `@modelcontextprotocol/sdk` 1.32.1, the fixed versions that `@angular/build`/`@angular/cli` 21.2.28 depend on. **Remove both overrides when Angular is updated to 21.2.28 or later**: they would otherwise hold back the versions Angular asks for.
- The SDK is .NET 9 for now (`global.json`), as Kaleido's ([no1ross/Kaleido#424](https://github.com/no1ross/Kaleido/issues/424)).