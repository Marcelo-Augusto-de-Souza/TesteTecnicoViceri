CREATE DATABASE ViceriTesteTecnico;
GO

USE ViceriTesteTecnico;
GO

CREATE TABLE Herois (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Nome NVARCHAR(120) NOT NULL,
	NomeHeroi NVARCHAR(120) NOT NULL,
	DataNascimento DATETIME2 NULL,
	Altura FLOAT(24) NOT NULL,
	Peso FLOAT(24) NOT NULL
);

CREATE TABLE Superpoderes (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Superpoder NVARCHAR(50) NOT NULL,
	Descricao NVARCHAR(250) NULL
);
GO

CREATE TABLE HeroisSuperpoderes (
	HeroiId INT,
	SuperpoderId INT,
	CONSTRAINT FK_HeroiId FOREIGN KEY (HeroiId) REFERENCES Herois(Id),
	CONSTRAINT FK_SuperpoderId FOREIGN KEY (SuperpoderID) REFERENCES Superpoderes(Id)
);

INSERT INTO Superpoderes (Superpoder, Descricao)
VALUES
('Telecinese','Mover objetos com a mente'),
('Super Forca','Forca sobre humana'),
('Teletransporte','Se desloca instantaneamente para qualquer lugar');
GO 

SELECT * FROM Superpoderes

SELECT * FROM Herois

SELECT * FROM HeroisSuperpoderes