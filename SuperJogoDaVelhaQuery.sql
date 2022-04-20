/*
use master
go
drop database superjogodavelha
*/

CREATE DATABASE SuperJogoDaVelha;
GO

USE SuperJogoDaVelha;
GO

CREATE TABLE Usuario(
	id INT PRIMARY KEY IDENTITY,
	username VARCHAR(20) NOT NULL,
	senha VARCHAR(20) NOT NULL,
	ratingNormal INT NOT NULL DEFAULT(0) CHECK(ratingNormal >= 0),
	ratingSuper INT NOT NULL DEFAULT(0) CHECK(ratingSuper >= 0)
);
GO

CREATE TABLE Fila(
	id INT PRIMARY KEY IDENTITY,
	usuario_id INT FOREIGN KEY REFERENCES Usuario(id) NOT NULL,
	tipoJogo VARCHAR(10) NOT NULL CHECK(tipoJogo IN ('Normal', 'Super'))
);
GO

CREATE TABLE Jogo(
	id INT PRIMARY KEY IDENTITY,
	usuario1_id INT FOREIGN KEY REFERENCES Usuario(id) NOT NULL,
	usuario2_id INT FOREIGN KEY REFERENCES Usuario(id) NOT NULL,
	tipoJogo VARCHAR(10) NOT NULL CHECK(tipoJogo IN ('Normal', 'Super'))
);
GO

CREATE TABLE Jogada(
	id INT PRIMARY KEY IDENTITY,
	jogo_id INT FOREIGN KEY REFERENCES Jogo(id) NOT NULL,
	campo INT NOT NULL CHECK(campo >= 1 AND campo <= 9),
	subCampo INT CHECK(subCampo >= 1 AND subCampo <= 9 OR subCampo IS NULL) DEFAULT(NULL),
	jogada CHAR(1) NOT NULL CHECK(jogada IN ('X', 'O'))
);
GO

INSERT INTO Usuario(username, senha) VALUES
('Bruhhno', '12345');
GO
