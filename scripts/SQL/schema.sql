DROP TABLE IF EXISTS AllowedUsers, AllowedChannels;

CREATE TABLE AllowedUsers(
    id SERIAL PRIMARY KEY,
    user_id VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE AllowedChannels(
    id SERIAL PRIMARY KEY,
    channel_id VARCHAR(100) NOT NULL UNIQUE
);

COMMIT;