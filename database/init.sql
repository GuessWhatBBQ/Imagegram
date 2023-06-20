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

INSERT INTO users (username, fullname, password) VALUES ('tleneve0', 'Tiebout Leneve', 'bM6"e/gCo\');
INSERT INTO users (username, fullname, password) VALUES ('ydurrad1', 'Yolande Durrad', 'jG8)0<tuK3W@rdX$');
INSERT INTO users (username, fullname, password) VALUES ('smccome2', 'Shawnee McCome', 'dP6"IBt=ap');
INSERT INTO users (username, fullname, password) VALUES ('drubinowitch3', 'Daffie Rubinowitch', 'iY3,kX>+jdJ(p1vh');
INSERT INTO users (username, fullname, password) VALUES ('gkinny4', 'Gweneth Kinny', 'eF8%hkBi>tM@(ke');
INSERT INTO users (username, fullname, password) VALUES ('llukehurst5', 'Loreen Lukehurst', 'yK5%!5~t');
INSERT INTO users (username, fullname, password) VALUES ('hhalse6', 'Hildegarde Halse', 'rN6'',#+M');
INSERT INTO users (username, fullname, password) VALUES ('ktether7', 'Kleon Tether', 'tA9,$NRt42=p7s1');
INSERT INTO users (username, fullname, password) VALUES ('ahrachovec8', 'Alick Hrachovec', 'wO5%7AhR)E#`3');
INSERT INTO users (username, fullname, password) VALUES ('dmourant9', 'Del Mourant', 'mJ2$p?lt|f`j..');
INSERT INTO users (username, fullname, password) VALUES ('samin', 'Samin Islam', '1234');

INSERT INTO sessions (userid, sessiontoken) VALUES (1, 'aa5877db-772d-4540-a57c-cacc6da007b6');
INSERT INTO sessions (userid, sessiontoken) VALUES (7, '3a7670c2-7a71-42a4-8c80-5b063c9e0722');
INSERT INTO sessions (userid, sessiontoken) VALUES (3, 'a495b09f-d299-4580-a93a-d5f4a9ee58ff');
INSERT INTO sessions (userid, sessiontoken) VALUES (6, 'f4d9e8ae-739c-4fd3-949c-17de5dcf93fc');
INSERT INTO sessions (userid, sessiontoken) VALUES (5, '77dcbd4e-093b-4400-bc36-a7350323fd8a');
INSERT INTO sessions (userid, sessiontoken) VALUES (1, '1053d1eb-f6db-4fa2-bf64-0973e7420da3');
INSERT INTO sessions (userid, sessiontoken) VALUES (2, 'cdbf7202-9637-47ad-8055-751da1688ce3');
INSERT INTO sessions (userid, sessiontoken) VALUES (7, 'fa7608c3-bfa7-4f53-aded-ede5e82ab133');
