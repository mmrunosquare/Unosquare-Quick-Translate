CREATE DATABASE QuickTranslate;
GO
USE QuickTranslate;

CREATE TABLE Translation (
    id INT IDENTITY(1,1) PRIMARY KEY,
    fecha_traduccion DATE,
    idioma_origen VARCHAR(30),
    idioma_destino VARCHAR(30),
    texto_origen VARCHAR(500),
    texto_destino VARCHAR(500)
);
