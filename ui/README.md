# E-commerce UI

The Angular UI of the e-commerce sample: the product catalog, the cart and checkout, over the
service's Kaleido HTTP endpoints (see the [repository README](../README.md)).

- **Service first:** start `src/NetCoreApp` (`https://localhost:7251`, set in
  `src/configuration/urlConfig.ts`) before the UI.
- `npm ci`, then `npm start` (`ng serve`, `http://localhost:4200`).
- `npm test` runs the unit tests (Vitest); `npm run build` builds into `dist/`.