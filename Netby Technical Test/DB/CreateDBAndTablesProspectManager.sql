CREATE DATABASE ProspectManagerDb
GO

USE ProspectManager
GO

CREATE TABLE Prospects (
	Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	[Name] NVARCHAR(100) NOT NULL,
    CellPhoneNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
	CreationDate DATETIME DEFAULT GETDATE()
)


CREATE TABLE Activities(
	Id INT IDENTITY(1,1) PRIMARY KEY, 
    ProspectId UNIQUEIDENTIFIER NOT NULL,
    [Date] DATETIME NOT NULL,
    [Description] NVARCHAR(255),
    [Type] NVARCHAR(20) CHECK ([Type] IN ('llamada', 'mensaje', 'correo')),
    Rating TINYINT CHECK (Rating BETWEEN 1 AND 5),
	CONSTRAINT FK_Activities_Prospects FOREIGN KEY (ProspectId) REFERENCES Prospects(Id)
)


--Datos para insertar directamente

-- Insertar un prospecto
INSERT INTO Prospects ([Name], CellPhoneNumber, Email) 
VALUES ('Juan Pérez', '0987654321', 'juan.perez@example.com');

-- Obtener el ID del prospecto recién creado
DECLARE @ProspectoId UNIQUEIDENTIFIER;
SET @ProspectoId = (SELECT TOP 1 Id FROM Prospects WHERE [Name] = 'Juan Pérez');

-- Insertar actividades para el prospecto
INSERT INTO Activities(ProspectId, [Date], [Description], [Type], Rating)
VALUES 
    (@ProspectoId, '2024-11-19', 'Llamada inicial para ofrecer el servicio', 'llamada', 4),
    (@ProspectoId, '2024-11-20', 'Envío de información por correo', 'correo', 5),
    (@ProspectoId, '2024-11-21', 'Seguimiento por mensaje de texto', 'mensaje', 3);

