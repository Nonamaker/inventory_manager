After changing Dockerfile, run `build.sh`. If the container is running it will need to be removed. Run `docker compose down`, then `docker compose up`.

To run normally: `docker compose up -d`

To view unused volumes run `docker volume ls -f dangling=true`. To remove unused volumes run `docker volume prune`.

App schema changes: After making changes run `dotnet ef migrations add MigrationName`, then `./bundle_migrations.sh`, and then rebuild the image.


General Flow:
- The postgres container is started first.
- The dotnet container is started and the dfbundle script is run inside of it to apply database migrations.
- After the migrations are complete the app begins.