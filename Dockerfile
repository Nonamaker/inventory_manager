# syntax=docker/dockerfile:1

FROM postgres

COPY ./scripts/ /docker-entrypoint-initdb.d/
