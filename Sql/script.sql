-- Creación de base de datos y esquema para Gestor de Tareas
IF DB_ID('RetoTareasDB') IS NULL
BEGIN
    CREATE DATABASE [RetoTareasDB];
END
GO

USE [RetoTareasDB];
GO

-- Limpieza previa (para iteraciones durante desarrollo)
IF OBJECT_ID('Tareas', 'U') IS NOT NULL DROP TABLE Tareas;

GO

CREATE TABLE Tareas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(100) NOT NULL,
    Completada BIT NOT NULL
);

-- Relaciones
-- (sin relaciones explícitas)

-- Datos de ejemplo
INSERT INTO Tareas (Titulo, Completada) VALUES (N'Aprender LINQ', 0);
INSERT INTO Tareas (Titulo, Completada) VALUES (N'Practicar Regex', 1);
