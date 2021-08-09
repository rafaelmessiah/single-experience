CREATE TABLE Categoria (
	CategoriaId INT NOT NULL PRIMARY KEY,
	Descricao varchar(50) NOT NULL,
);

INSERT INTO Categoria (CategoriaId, Descricao)
VALUES(1, 'Computador')

INSERT INTO Categoria (CategoriaId, Descricao)
VALUES(2, 'Notebook')

INSERT INTO Categoria (CategoriaId, Descricao)
VALUES(3, 'Acessorio')

INSERT INTO Categoria (CategoriaId, Descricao)
VALUES(4, 'Celular')

INSERT INTO Categoria (CategoriaId, Descricao)
VALUES(5, 'Tablet')

CREATE TABLE Produto(
ProdutoId INT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
CategoriaId INT NOT NULL,
Nome varchar(50) NOT NULL,
Detalhe varchar(max),
QtdeEmEstoque INT NOT NULL,
Ranking INT NOT NULL,
Disponivel BIT NOT NULL
);

ALTER TABLE Produto
ADD FOREIGN KEY (CategoriaId) REFERENCES Categoria(CategoriaId);

CREATE TABLE Cliente (
ClienteId INT IDENTITY(1,1) NOT NULL PRIMARY KEY ,
Cpf VARCHAR(11) NOT NULL,
Nome VARCHAR(100) NOT NULL,
Email VARCHAR(100) NOT NULL,
Senha VARCHAR(100) NOT NULL,
DataNascimento DATE NOT NULL,
Telefone VARCHAR(20) NOT NULL
);

CREATE TABLE StatusCarrinhoProduto(
StatusCarrinhoProdutoId INT PRIMARY KEY,
Descricao varchar(50)
);

INSERT INTO StatusCarrinhoProduto (StatusCarrinhoProdutoId, Descricao)
VALUES(1, 'Ativo')

INSERT INTO StatusCarrinhoProduto (StatusCarrinhoProdutoId, Descricao)
VALUES(2, 'Comprado')

INSERT INTO StatusCarrinhoProduto (StatusCarrinhoProdutoId, Descricao)
VALUES(3, 'Excluido')

CREATE TABLE Carrinho(
CarrinhoId INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
ProdutoId INT NOT NULL,
ClienteId INT NOT NULL, 
Qtde INT NOT NULL,
StatusCarrinhoProdutoId INT NOT NULL
);

ALTER TABLE Carrinho
ADD FOREIGN KEY (ProdutoId) REFERENCES Produto(ProdutoId);

ALTER TABLE Carrinho
ADD FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId);

ALTER TABLE Carrinho
ADD FOREIGN KEY (StatusCarrinhoProdutoId) REFERENCES StatusCarrinhoProduto(StatusCarrinhoProdutoId);

CREATE TABLE CartaoCredito (
CartaoCreditoId INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
ClienteId INT NOT NULL,
Numero VARCHAR(20) NOT NULL,
Bandeira VARCHAR(20) NOT NULL,
CodigoSeguranca VARCHAR(5) NOT NULL,
DataVencimento DATE NOT NULL
);

ALTER TABLE CartaoCredito
ADD FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)

CREATE TABLE Endereco(
EnderecoId INT  IDENTITY (1,1)NOT NULL PRIMARY KEY,
ClienteId INT NOT NULL,
Rua VARCHAR(100) NOT NULL,
Numero VARCHAR(10) NOT NULL,
Complemento VARCHAR(10) NOT NULL,
Cep VARCHAR(7) NOT NULL,
);

ALTER TABLE Endereco
ADD FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)

CREATE TABLE StatusCompra(
StatusCompraId INT NOT NULL PRIMARY KEY,
Descricao VARCHAR(50)
);

INSERT INTO StatusCompra(StatusCompraId, Descricao)
VALUES(1, 'Aberta')

INSERT INTO StatusCompra(StatusCompraId, Descricao)
VALUES(2, 'Finalizada')

INSERT INTO StatusCompra(StatusCompraId, Descricao)
VALUES(3, 'Cancelada')

CREATE TABLE FormaPagamento(
FormaPagamentoId INT NOT NULL PRIMARY KEY,
Descricao VARCHAR(50)
);

INSERT INTO FormaPagamento(FormaPagamentoId, Descricao)
VALUES (1, 'Boleto')

INSERT INTO FormaPagamento(FormaPagamentoId, Descricao)
VALUES (2, 'Pix')

INSERT INTO FormaPagamento(FormaPagamentoId, Descricao)
VALUES (3, 'Cartao')

CREATE TABLE Compra(
CompraId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
StatusCompraId INT NOT NULL,
FormaPagamentoId INT NOT NULL,
ClienteId INT NOT NULL,
EnderecoId INT NOT NULL,
StatusPagamento BIT NOT NULL,
DataCompra DATE NOT NULL,
DataPagamento DATE,
ValorFinal FLOAT,
);

ALTER TABLE Compra
ADD FOREIGN KEY (StatusCompraId) REFERENCES StatusCompra(StatusCompraId)

ALTER TABLE Compra
ADD FOREIGN KEY (FormaPagamentoId) REFERENCES FormaPagamento(FormaPagamentoId)

ALTER TABLE Compra
ADD FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)

ALTER TABLE Compra
ADD FOREIGN KEY (EnderecoId) REFERENCES Endereco(EnderecoId)

CREATE TABLE ListaProdutoCompra(
ListaProdutoCompraId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
CompraId INT NOT NULL, 
ProdutoId INT NOT NULL, 
Qtde INT NOT NULL
);

ALTER TABLE ListaProdutoCompra
ADD FOREIGN KEY (CompraId) REFERENCES Compra(CompraId)

ALTER TABLE ListaProdutoCompra
ADD FOREIGN KEY (ProdutoId) REFERENCES Produto(ProdutoId)