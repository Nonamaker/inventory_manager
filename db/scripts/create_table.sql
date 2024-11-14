-- This file is only run on initial startup of the container.
CREATE DATABASE dockerx;
\connect dockerx;

CREATE TABLE IF NOT EXISTS hello (
	i integer
);

INSERT INTO hello (i) VALUES (42);
