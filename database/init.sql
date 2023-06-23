CREATE TABLE users (
       id SERIAL PRIMARY KEY,
       username TEXT NOT NULL UNIQUE,
       password TEXT NOT NULL,
       fullname text NOT NULL
);

CREATE TABLE post (
       id SERIAL PRIMARY KEY,
       caption TEXT NOT NULL,
       creatorid INT REFERENCES users(id) NOT NULL,
       creationdate TIMESTAMP WITH time zone NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE sessions (
       id SERIAL PRIMARY KEY,
       userid INT REFERENCES users(id) NOT NULL,
       sessiontoken TEXT NOT NULL
);

CREATE TABLE image (
       id SERIAL PRIMARY KEY,
       imagepath text NOT NULL,
       postid INT REFERENCES post(id) NOT NULL
);

CREATE TABLE comment (
       id SERIAL PRIMARY KEY,
       postid INT REFERENCES post(id) NOT NULL,
       creatorid INT REFERENCES users(id) NOT NULL,
       content TEXT,
       creationdate TIMESTAMP WITH time zone NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO users (username, fullname, password) VALUES ('tstelle0', 'Tobi Stelle', '$2a$04$u5q4MV7rJKZjWjQoLDk8YuusN9WnzgnrzjEr.DJkKnQQAh0EVO8rK');
INSERT INTO users (username, fullname, password) VALUES ('cbabe1', 'Coriss Babe', '$2a$04$7Z6GflUoGbd54CAQX5yzmuoUQpcFUbWSAoKNVZ4QwjW44wfncFyUS');
INSERT INTO users (username, fullname, password) VALUES ('hnaseby2', 'Heidie Naseby', '$2a$04$IQqVs9w2tMSOV7tWicM3Y.efuCAiIRhMgw/ymVDk9Sv1rImYgfTH.');
INSERT INTO users (username, fullname, password) VALUES ('ecicetti3', 'Elia Cicetti', '$2a$04$pXINDufWH21pZ/x0ATp.x.YGl.6GVXVvyAuiTPUbKq95BS8eh.ALC');
INSERT INTO users (username, fullname, password) VALUES ('cmoine4', 'Ceciley Moine', '$2a$04$s1Wn6Vs42uWGTpp1u/JTvO.MbZ137P5fnIYXLXtX5TgTOnW9FYE.i');
INSERT INTO users (username, fullname, password) VALUES ('ctalboy5', 'Clayton Talboy', '$2a$04$LRr2C3854SQ5YTbfXWsuz.3sdk1XC65.dgDkwkhsHQrm/QQaqe2qi');
INSERT INTO users (username, fullname, password) VALUES ('nharnell6', 'Nichols Harnell', '$2a$04$GrHPrceJ4Impg7Gbs9uFseDtpNjNYRXEvktvb7kLICqPQiNU1GcHq');
INSERT INTO users (username, fullname, password) VALUES ('tboriston7', 'Tobe Boriston', '$2a$04$J/JVMxOpTqH2PCMsw6tvkOneUyPZULGJlmZVQl8jWCDdRL71WVzpu');
INSERT INTO users (username, fullname, password) VALUES ('ltattersfield8', 'Luis Tattersfield', '$2a$04$.uFS9b.dOmpbzSTKGEnZeu6rswPAb3c6cbYb5qkVKLxJN0zkFNyTi');
INSERT INTO users (username, fullname, password) VALUES ('dpillans9', 'Davis Pillans', '$2a$04$zqLdmgIjdwT.oV.DY.zIBeRTxCyDREeqopveBHr6y7lPIIt58AXwa');
INSERT INTO users (username, fullname, password) VALUES ('samin', 'Samin Islam', '$2a$07$oWsMrmF.hT7wJbg/hBYiqukGWVcwFETihVAB6tC1popyuFusthH.q');

INSERT INTO sessions (userid, sessiontoken) VALUES (11, 'e3c593be-343f-49a5-9d35-d602d5d57316');
INSERT INTO sessions (userid, sessiontoken) VALUES (4, 'd1c3bd04-88d1-443f-bf5f-e6ff04d1e8d1');
