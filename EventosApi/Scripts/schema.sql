CREATE TABLE dbo.Eventos (
    IdEvento INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Fecha DATE NOT NULL,
    Lugar NVARCHAR(100) NOT NULL,
    CONSTRAINT CK_Eventos_Nombre CHECK (LEN(Nombre) BETWEEN 5 AND 100),
    CONSTRAINT CK_Eventos_Lugar CHECK (LEN(Lugar) BETWEEN 5 AND 100)
);
GO

CREATE TABLE dbo.Organizadores (
    IdOrganizador INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Cargo NVARCHAR(50) NOT NULL,
    IdEvento INT NOT NULL,
    CONSTRAINT CK_Organizadores_Nombre CHECK (LEN(Nombre) BETWEEN 3 AND 50),
    CONSTRAINT CK_Organizadores_Cargo CHECK (LEN(Cargo) BETWEEN 3 AND 50),
    CONSTRAINT FK_Organizadores_Eventos FOREIGN KEY (IdEvento) REFERENCES dbo.Eventos(IdEvento) ON DELETE CASCADE
);
GO

CREATE TABLE dbo.Participantes (
    IdParticipante INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    IdEvento INT NOT NULL,
    CONSTRAINT CK_Participantes_Nombre CHECK (LEN(Nombre) BETWEEN 3 AND 50),
    CONSTRAINT CK_Participantes_Email CHECK (Email LIKE '_%@_%._%'),
    CONSTRAINT FK_Participantes_Eventos FOREIGN KEY (IdEvento) REFERENCES dbo.Eventos(IdEvento) ON DELETE CASCADE
);
GO

INSERT INTO dbo.Eventos (Nombre, Fecha, Lugar) VALUES
('Conferencia de Tecnologia 2026', '2026-10-15', 'Centro Comercial Las Palmas piso 3'),
('Taller de Desarrollo Web', '2026-11-05', 'Hotel Crown'),
('Evento Navideno', '2026-12-20', 'Cifco San Salvador');
GO

INSERT INTO dbo.Organizadores (Nombre, Cargo, IdEvento) VALUES
('Maria Garcia', 'Coordinadora General', 1),
('Carlos Rodriguez', 'Director Tecnico', 1),
('Laura Martinez', 'Responsable de Logistica', 2);
GO

INSERT INTO dbo.Participantes (Nombre, Email, IdEvento) VALUES
('Juan Perez', 'juan.perez@empresa.com', 1),
('Marta Ruiz', 'marta.ruiz@empresa.es', 1),
('Alberto Diaz', 'alberto.diaz@taller.com', 2);
GO
