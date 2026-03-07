CREATE DATABASE DapperMTMDB
GO
USE DapperMTMDB

CREATE TABLE Genres(
[GenreId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Genre] NVARCHAR(100) NOT NULL UNIQUE,
CHECK(TRIM([Genre]) <>'')
)

GO
CREATE TABLE Books(
BookId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(200) NOT NULL UNIQUE,
CHECK(TRIM([Name]) <> ''),
[GenreId] INT FOREIGN KEY REFERENCES Genres(GenreId) NOT NULL
)


GO

CREATE TABLE Authors(
AuthorId INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[FullName] NVARCHAR(30) NOT NULL UNIQUE,
CHECK(TRIM([FullName]) <> ''),
Age INT NOT NULL,
CHECK([Age] > 0),
Experience INT NOT NULL,
CONSTRAINT CHK_Experience_Age CHECK([Experience] >= 0 AND [Experience] < ([Age] - 10))
)

GO

CREATE TABLE BookAuthors(
[BookId] INT FOREIGN KEY REFERENCES Books(BookId) NOT NULL,
[AuthorId] INT FOREIGN KEY REFERENCES Authors(AuthorId) NOT NULL,
CONSTRAINT UQ_Book_Author UNIQUE(BookId,AuthorId)
)

GO

INSERT INTO Genres([Genre])
VALUES('Science-Fiction'),
('Fiction'),
('Technical'),
('Political Science'),
('Economics'),
('Biography')

INSERT INTO Books([Name],GenreId)
VALUES ('Dune',1),
('1984',2),
('Design Patterns',3),
('Failed States',4),
('The Changing World Order',5),
('Manufacturing Consent',4)

GO

INSERT INTO Authors([FullName],[Age],[Experience])
VALUES('Frank Herbert', 65, 41),
('George Orwell', 46, 21),
('Erich Gamma', 65, 40),
('Richard Helma', 68, 42),
('Ralph Johnson', 71, 45),
('John Vlissides', 47, 25),
('Noam Chomsky', 97, 71),
('Ray Dalio', 76, 55),
('Edward S. Herman', 92, 60)


GO
INSERT INTO BookAuthors([BookId],[AuthorId])
VALUES(1,1),
(2,2),
(3,3),(3,4),(3,5),(3,6),
(4,7),
(5,8),
(6,7),(6,9)

SELECT B.[Name], G.Genre
FROM Books AS B
INNER JOIN Genres AS G
ON G.GenreId = B.GenreId

SELECT B.BookId, B.[Name], A.AuthorId, A.FullName, G.GenreId, G.Genre
FROM BOOKS AS B
INNER JOIN BookAuthors AS BA
ON B.BookId = BA.BookId
	INNER JOIN Authors AS A
	ON A.AuthorId = BA.AuthorId
INNER JOIN Genres AS G
ON G.GenreId =B.GenreId