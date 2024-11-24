DROP TABLE IF EXISTS AllowedUsers, AllowedChannels;

CREATE TABLE AllowedUsers(
    id SERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL UNIQUE
);

CREATE TABLE AllowedChannels(
    id SERIAL PRIMARY KEY,
    channel_id BIGINT NOT NULL UNIQUE
);

COMMIT;