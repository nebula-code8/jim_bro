INSERT INTO users (name, surname, gender, date_of_birth, phone_number, email, password, role) VALUES     
('Marko', 'Marković', 0, '2000-05-15', '+381-60-1234567', 'marko@example.com', 'marko123', 0),
('Ana', 'Anić', 1, '1998-08-22', '+381-60-1234568', 'ana@example.com', 'ana123', 1),
('Petar', 'Petrović', 0, '2001-03-10', '+381-60-1234569', 'petar@example.com', 'petar123', 0),
('Jovana', 'Jovanović', 1, '1999-11-30', '+381-60-1234570', 'jovana@example.com', 'jovana123', 1),
('Stefan', 'Stefanović', 0, '2002-01-25', '+381-60-1234571', 'stefan@example.com', 'stefan123', 0),
('Milica', 'Milić', 1, '2000-07-08', '+381-60-1234572', 'milica@example.com', 'milica123', 0),
('Nikola', 'Nikolić', 0, '1997-12-18', '+381-60-    1234573', 'nikola@example.com', 'nikola123', 1),
('Ivana', 'Ivanović', 1, '2001-09-05', '+381-60-1234574', 'ivana@example.com', 'ivana123', 2);

INSERT INTO client (id, height, weight, goal, health_problems) VALUES
(1, 180, 80, 'to get stronger', NULL),
(3, 175, 90, 'run 5k',NULL),
(5, 193, 84, 'to get faster',NULL),
(6, 182, 80, 'to get stronger',NULL);

INSERT INTO trainer (id, specialization, biography, license) VALUES
(2, 'Yoga', '...', 'Certifed Personal Trainer'),
(4, 'Cardio', '...', 'Certifed Personal Trainer'),
(7, 'Bodybuilding', '...', 'Certifed Personal Trainer');

INSERT INTO equipments (id, name, description) VALUES
(1, 'Teg', 'Teg od 5kg'),
(2, 'Teg', 'Teg od 10kg'),
(3, 'Teg', 'Teg od 20kg');

INSERT INTO machines (id, name, description) VALUES
(1, 'Mašina za potisak za grudi', 'Mašina za potisak za grudi'),
(2, 'Smit mašina', 'Smit mašina'),
(3, 'Traka za trčanje', 'Traka za trčanje');