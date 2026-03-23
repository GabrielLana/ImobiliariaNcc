CREATE DATABASE imobiliaria_ncc;
GO

USE imobiliaria_ncc;
GO

CREATE TABLE vendedores (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    data_criacao DATETIME2 NOT NULL DEFAULT GETDATE(),
    data_alteracao DATETIME2 NULL,
    ativo BIT NOT NULL DEFAULT 1,
    nome VARCHAR(200) NOT NULL,
    cpf VARCHAR(11) NOT NULL UNIQUE,
    senha VARCHAR(250) NOT NULL,
    data_nascimento DATETIME NOT NULL,
    email VARCHAR(200) NOT NULL,
    celular VARCHAR(11) NOT NULL,
    cep VARCHAR(8) NOT NULL,
    logradouro VARCHAR(150) NOT NULL,
    bairro VARCHAR(100) NOT NULL,
    numero VARCHAR(5) NOT NULL,
    complemento VARCHAR(100) NULL,
    setor VARCHAR(50) NOT NULL,
    numero_registro INT NOT NULL
);

CREATE TABLE clientes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    data_criacao DATETIME2 NOT NULL DEFAULT GETDATE(),
    data_alteracao DATETIME2 NULL,
    ativo BIT NOT NULL DEFAULT 1,
    nome VARCHAR(200) NOT NULL,
    cpf VARCHAR(11) NOT NULL UNIQUE,
    data_nascimento DATETIME NOT NULL,
    email VARCHAR(200) NOT NULL,
    celular VARCHAR(11) NOT NULL,
    estado_civil VARCHAR(20) NOT NULL
);

CREATE TABLE apartamentos (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    data_criacao DATETIME2 NOT NULL DEFAULT GETDATE(),
    data_alteracao DATETIME2 NULL,
    ocupado BIT NOT NULL DEFAULT 0,
    metragem INT NOT NULL,
    quartos INT NOT NULL,
    banheiros INT NOT NULL,
    vagas INT NOT NULL,
    detalhes_apartamento VARCHAR(250) NOT NULL,
    detalhes_condominio VARCHAR(250) NOT NULL,
    andar INT NOT NULL,
    bloco INT NOT NULL,
    valor_venda DECIMAL(15,2) NOT NULL,
    valor_condominio DECIMAL(10,2) NOT NULL,
    valor_iptu DECIMAL(10,2) NOT NULL,
    cep VARCHAR(8) NOT NULL,
    logradouro VARCHAR(150) NOT NULL,
    bairro VARCHAR(100) NOT NULL,
    numero VARCHAR(5) NOT NULL,
    complemento VARCHAR(100) NULL,
    estado VARCHAR(50) NOT NULL,
    cidade VARCHAR(200) NOT NULL
);

CREATE TABLE reservas (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    data_criacao DATETIME2 NOT NULL DEFAULT GETDATE(),
    data_alteracao DATETIME2 NULL,
    ativo BIT NOT NULL DEFAULT 1,
    ID_CLIENTE INT NOT NULL,
    ID_APARTAMENTO INT NOT NULL,
    FOREIGN KEY (ID_CLIENTE) REFERENCES clientes (ID),
    FOREIGN KEY (ID_APARTAMENTO) REFERENCES apartamentos (ID)
);

CREATE TABLE vendas (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    data_criacao DATETIME2 NOT NULL DEFAULT GETDATE(),
    data_alteracao DATETIME2 NULL,
    ID_CLIENTE INT NOT NULL,
    ID_APARTAMENTO INT NOT NULL,
    ID_VENDEDOR INT NOT NULL,
    FOREIGN KEY (ID_CLIENTE) REFERENCES clientes (ID),
    FOREIGN KEY (ID_APARTAMENTO) REFERENCES apartamentos (ID),
    FOREIGN KEY (ID_VENDEDOR) REFERENCES vendedores (ID)
);

INSERT INTO vendedores
(nome, cpf, senha, data_nascimento, email, celular, cep, logradouro, bairro, numero, complemento, setor, numero_registro)
VALUES
('Carlos Silva','11111111111','AQAAAAIAAYagAAAAEEwMIdGhEf2rkjgDAxy4gfmD02/0su9s2iTnCTHqt5dstqeLYy3Ozjh3BfLftDEKDw==','1985-02-10','carlos@construtora.com','31999990001','30110000','Rua A','Centro','100',NULL,'Vendas',1001),
('Mariana Souza','22222222222','AQAAAAIAAYagAAAAEFjwkP1G8lr7yfH1dGTjnDLkivxosKArZDlsVXQ56I+V9GTHKzLUwUSJzUBvejQBzQ==','1990-07-21','mariana@construtora.com','31999990002','30110000','Rua B','Funcionarios','200','Apto 101','Vendas',1002),
('Pedro Almeida','33333333333','AQAAAAIAAYagAAAAEP4rEekIYWstOo1sC9N0PETjHVPdSu8rg45MeXvocNU1/PX1t8BFORxVRi3D7iPdTw==','1988-11-05','pedro@construtora.com','31999990003','30110000','Rua C','Savassi','300',NULL,'Vendas',1003);

INSERT INTO clientes
(nome, cpf, data_nascimento, email, celular, estado_civil)
VALUES
('João Pereira','44444444444','1980-03-15','joao@email.com','31988880001','Casado'),
('Ana Costa','55555555555','1992-08-22','ana@email.com','31988880002','Solteira'),
('Lucas Martins','66666666666','1987-01-30','lucas@email.com','31988880003','Casado');

INSERT INTO apartamentos
(metragem, quartos, banheiros, vagas, detalhes_apartamento, detalhes_condominio,
andar, bloco, valor_venda, valor_condominio, valor_iptu,
cep, logradouro, bairro, numero, complemento, estado, cidade)
VALUES
(70,2,2,1,'Varanda;Armarios Planejados;Ar Condicionado','Piscina;Academia;Portaria 24h',1,1,350000,450,120,'30110000','Rua das Flores','Centro','100',NULL, 'Minas Gerais', 'Belo Horizonte'),
(75,2,2,1,'Varanda Gourmet;Vista Cidade;Piso Porcelanato','Piscina;Academia;Portaria 24h',2,1,360000,450,120,'30110000','Rua das Flores','Centro','101',NULL, 'Minas Gerais', 'Belo Horizonte'),
(80,3,2,2,'Varanda;Suite Master;Armarios Planejados','Piscina;Sauna;Academia',3,1,420000,500,150,'30110000','Rua das Flores','Centro','102',NULL, 'Minas Gerais', 'Belo Horizonte'),
(60,2,1,1,'Varanda;Piso Laminado;Cozinha Americana','Area Gourmet;Playground',1,2,280000,350,90,'30110000','Rua Savassi','Savassi','200',NULL, 'Minas Gerais', 'Belo Horizonte'),
(65,2,2,1,'Varanda Gourmet;Suite;Armarios','Area Gourmet;Playground;Salao de Festas',2,2,300000,350,90,'30110000','Rua Savassi','Savassi','201',"Próximo ao restaurante Barolho", 'Minas Gerais', 'Belo Horizonte'),
(90,3,2,2,'Vista Panoramica;Varanda;Suite','Piscina;Academia;Quadra',5,1,520000,600,200,'30110000','Rua Bahia','Funcionarios','300',NULL, 'Minas Gerais', 'Belo Horizonte'),
(85,3,2,2,'Varanda Gourmet;Armarios;Ar Condicionado','Piscina;Academia;Quadra',4,1,500000,600,200,'30110000','Rua Bahia','Funcionarios','301',NULL, 'Minas Gerais', 'Belo Horizonte'),
(72,2,2,1,'Varanda;Suite;Piso Porcelanato','Academia;Portaria 24h',2,3,340000,420,110,'30110000','Rua Timbiras','Centro','400',NULL, 'Minas Gerais', 'Belo Horizonte'),
(68,2,2,1,'Varanda;Cozinha Americana;Armarios','Academia;Portaria 24h',3,3,330000,420,110,'30110000','Rua Timbiras','Centro','401', NULL, 'Minas Gerais', 'Belo Horizonte'),
(78,3,2,2,'Varanda Gourmet;Suite;Vista Cidade','Academia;Salao de Festas',4,3,390000,420,110,'30110000','Rua Timbiras','Centro','402',NULL, 'Minas Gerais', 'Belo Horizonte'),
(95,3,3,2,'Cobertura;Jacuzzi;Vista Panoramica','Piscina;Sauna;Academia',10,1,800000,900,300,'30110000','Rua Paraiba','Savassi','500',NULL, 'Minas Gerais', 'Belo Horizonte'),
(100,3,3,2,'Cobertura Duplex;Jacuzzi;Area Gourmet','Piscina;Sauna;Academia',11,1,850000,900,300,'30110000','Rua Paraiba','Savassi','501',NULL, 'Minas Gerais', 'Belo Horizonte'),
(55,1,1,1,'Studio;Cozinha Americana;Armarios','Coworking;Academia',1,4,220000,250,70,'30110000','Rua da Tecnologia','Centro','600',NULL, 'Minas Gerais', 'Belo Horizonte'),
(58,1,1,1,'Studio;Varanda;Piso Porcelanato','Coworking;Academia',2,4,230000,250,70,'30110000','Rua da Tecnologia','Centro','601',NULL, 'Minas Gerais', 'Belo Horizonte'),
(62,2,1,1,'Varanda;Cozinha Americana;Piso Laminado','Playground;Salao de Festas',3,4,260000,300,80,'30110000','Rua das Crianças','Centro','700',NULL, 'Minas Gerais', 'Belo Horizonte'),
(66,2,1,1,'Varanda;Armarios;Piso Laminado','Playground;Salao de Festas',4,4,270000,300,80,'30110000','Rua das Crianças','Centro','701',NULL, 'Minas Gerais', 'Belo Horizonte'),
(74,2,2,1,'Varanda Gourmet;Suite;Vista Cidade','Piscina;Academia',5,2,340000,400,100,'30110000','Rua Azul','Savassi','800',NULL, 'Minas Gerais', 'Belo Horizonte'),
(82,3,2,2,'Varanda;Suite;Armarios','Piscina;Academia',6,2,410000,400,100,'30110000','Rua Azul','Savassi','801',NULL, 'Minas Gerais', 'Belo Horizonte'),
(88,3,2,2,'Varanda Gourmet;Suite;Ar Condicionado','Piscina;Academia',7,2,450000,400,100,'30110000','Rua Azul','Savassi','802',NULL, 'Minas Gerais', 'Belo Horizonte'),
(92,3,3,2,'Varanda Gourmet;Suite Master;Ar Condicionado','Piscina;Academia',8,2,480000,400,100,'30110000','Rua Azul','Savassi','803',NULL, 'Minas Gerais', 'Belo Horizonte');

INSERT INTO vendas (ID_CLIENTE, ID_APARTAMENTO, ID_VENDEDOR)
VALUES
(1,1,1),
(2,2,1);

UPDATE apartamentos
SET ocupado = 1
WHERE ID IN (1,2);

INSERT INTO reservas (ID_CLIENTE, ID_APARTAMENTO)
VALUES (3,3);