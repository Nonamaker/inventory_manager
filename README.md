After changing Dockerfile, run `build.sh`. If the container is running it will need to be removed. Run `docker compose down`, then `docker compose up`.

To run normally: `docker compose up -d`

To view unused volumes run `docker volume ls -f dangling=true`. To remove unused volumes run `docker volume prune`.