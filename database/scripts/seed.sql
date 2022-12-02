INSERT INTO [dbo].[Movie] ([Name], [ProductionYear], [BoxOffice])
VALUES
    ('Inception', 2010, 825532764),
    ('The Prestige', 2006, 55364480),
    ('The Departed', 2006, 285716295),
    ('The Lion King', 1994, 968303039),
    ('Goodfellas', 1990, 460935665),
    ('The Matrix Reloaded', 2003, 725230925),
    ('The Great Dictator', 1940, 32000000),
    ('The Matrix Revolutions', 2003, 427200000),
    ('Gladiator', 2000, 457844033),
    ('Gone with the Wind', 1939, 390450000),
    ('The Shawshank Redemption', 1994, 28341469),
    ('The Godfather', 1972, 134821952),
    ('The Godfather: Part II', 1974, 55364480),
    ('The Dark Knight', 2008, 1004558444),
    ('12 Angry Men', 1957, 3600000),
    ('Pulp Fiction', 1994, 107930000),
    ('The Good, the Bad and the Ugly', 1966, 25000000),
    ('The Silence of the Lambs', 1991, 27212421),
    ('Fight Club', 1999, 100455844),
    ('The Matrix', 1999, 463717083);

INSERT INTO [dbo].[LoggedUser] ([Username], [UserKey], [MoviesWatched])
VALUES
    ('user1', '12345678-1234-1234-1234-123456789012', 5),
    ('user2', '23456789-1234-1234-1234-123456789012', 3),
    ('user3', '34567890-1234-1234-1234-123456789012', 8),
    ('user4', '45678901-1234-1234-1234-123456789012', 2),
    ('user5', '56789012-1234-1234-1234-123456789012', 7),
    ('user6', '67890123-1234-1234-1234-123456789012', 10),
    ('user7', '78901234-1234-1234-1234-123456789012', 9),
    ('user8', '89012345-1234-1234-1234-123456789012', 1),
    ('user9', '90123456-1234-1234-1234-123456789012', 6),
    ('user10', '01234567-1234-1234-1234-123456789012', 4);

INSERT INTO [dbo].[LoggedUserMovie] ([UserId], [MovieId])
VALUES
    (1, 1),
    (2, 2),
    (3, 3),
    (4, 4),
    (5, 5),
    (6, 6),
    (7, 7),
    (8, 8),
    (9, 9),
    (10, 10),
    (2, 1),
    (2, 3),
    (2, 4),
    (3, 2),
    (3, 1),
    (4, 2),
    (5, 1);
