CREATE DATABASE dbPetShop;
GO
USE dbPetShop;
GO

-- Tabela Cliente
CREATE TABLE Cliente (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cpf VARCHAR(14) NOT NULL UNIQUE,
    Telefone VARCHAR(20) NOT NULL,
    DataNascimento DATE NOT NULL
);
GO

-- Tabela Veterinario
CREATE TABLE Veterinario (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Crmv VARCHAR(20) NOT NULL UNIQUE,
    Especialidade VARCHAR(50) NOT NULL
);
GO

-- Tabela ReservaServico
CREATE TABLE ReservaServico (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    DataHora DATETIME NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    StatusServico VARCHAR(30) NOT NULL,
    ClienteId INT NOT NULL,
    VeterinarioId INT NOT NULL,
    CONSTRAINT FK_ReservaServico_Cliente FOREIGN KEY (ClienteId) 
        REFERENCES Cliente(Codigo),
    CONSTRAINT FK_ReservaServico_Veterinario FOREIGN KEY (VeterinarioId) 
        REFERENCES Veterinario(Codigo)
);
GO

-- Inserindo Dados Iniciais para Teste
INSERT INTO Cliente (Nome, Cpf, Telefone, DataNascimento) VALUES 
('Maria Oliveira', '111.222.333-44', '(11) 98888-7777', '1990-05-15'),
('João Souza', '555.666.777-88', '(11) 97777-6666', '1985-10-20');

INSERT INTO Veterinario (Nome, Crmv, Especialidade) VALUES 
('Dra. Helena Rios', 'CRMV/SP 12345', 'Dermatologia Veterinaria'),
('Dr. Roberto Alves', 'CRMV/SP 65432', 'Ortopedia Veterinaria');

INSERT INTO ReservaServico (DataHora, Valor, StatusServico, ClienteId, VeterinarioId) VALUES 
('2026-10-15 14:00:00', 180.00, 'Agendada', 1, 1),
('2026-10-16 09:30:00', 250.00, 'Agendada', 2, 2);
GO

Select*from Veterinario