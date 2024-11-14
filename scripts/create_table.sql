CREATE DATABASE docker;
\connect docker;

CREATE TABLE IF NOT EXISTS hello (
	i integer
);

INSERT INTO hello (i) VALUES (42);
