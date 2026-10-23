# E-commerce sample

A single-service shop built with [Kaleido](https://github.com/no1ross/Kaleido): a product catalog
published as queries (sources and views), and a cart and checkout run as a process (steps). An
Angular UI consumes both over Kaleido's HTTP endpoints.

| Folder | Holds |
|---|---|
| `src/` | `NetCoreApp` (the ASP.NET Core host), `Queryable` (catalog, customers, orders, carts as query sources and views), `Process` (the cart and checkout steps), `Data` (EF Core over SQLite, and the seed data), `Shared` |
| `ui/` | the Angular app |
| `docs/` | design notes, when there are any |
| `tests/` | tests, when there are any |

## What it shows

- **Queries:** `ProductCatalogContextSource` with views per category and list (`ProductByCategoryQueryViewSource`, `ProductListQueryViewSource`, `CategoryListQueryViewSource`), plus customers, orders and carts. Search, filter, sort and paging come from the query contexts' declarations.
- **A process:** the cart (`AddItemToCartStep`, `UpdateCartItemStep`, `RemoveCartItemStep`, `ReconcileCartStep`) through checkout (`ProcessCartStep`, `StartOrderStep`, `SubmitBillingStep`, `AcceptTermsAndConditionsStep`, `SubmitOrderStep`), and `CancelOrderStep` / `ChangePaymentInfoStep` afterwards.
- **Persistence:** process state, the event outbox and idempotent results in Kaleido's SQLite reference store.

The service runs with `AuthorizationMode.None`: local evaluation only, not a deployment example.

## Run it

1. **Get the Kaleido packages.** The sample uses Kaleido's preview packages from its GitHub Packages
   feed (`nuget.config`). The feed needs a GitHub **classic** personal access token with
   `read:packages`, even though the packages are public (GitHub Settings > Developer settings >
   Personal access tokens > Tokens (classic)). Add it to your user-level NuGet configuration, never
   to a file in this repository; `Read-Host` keeps it off the command line:

   ```powershell
   $t = Read-Host 'token' -MaskInput
   dotnet nuget add source https://nuget.pkg.github.com/no1ross/index.json --name kaleido --username <your GitHub user> --password $t
   Remove-Variable t
   ```

   Building needs the .NET 9 SDK (`global.json`).

2. **Start the service:** `dotnet run --project src/NetCoreApp --launch-profile https` (the UI calls
   `https://localhost:7251`; `dotnet dev-certs https --trust` once if the development certificate
   is not trusted).
   - **The databases are generated, never committed.** On first start the service creates
     `kaleido-sample-ecomerce.sqlite` (the catalog) in `src/NetCoreApp` and seeds it from
     `src/Data/Assets/*.json` (taxonomy, suppliers, customers) plus generated products and inventory:
     about 8,700 products, the same on every machine (a fixed random seed). It takes about 15 seconds;
     later starts keep the catalog.
   - Start over with `dotnet run --project src/NetCoreApp --launch-profile https -- --reseed`.
   - Kaleido's own store, `kaleido-sample-process.sqlite`, is created at startup too.

3. **Start the UI:** `npm ci` then `npm start` in `ui/`, and open `http://localhost:4200`.

## More

- [Kaleido](https://github.com/no1ross/Kaleido) and its [consumer guide](https://github.com/no1ross/Kaleido/blob/master/docs/guide/README.md)
- [`AGENTS.md`](AGENTS.md): conventions for working on this repository