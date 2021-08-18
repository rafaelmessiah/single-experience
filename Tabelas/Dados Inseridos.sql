USE [rafael.messias];

INSERT INTO Produto (CategoriaId, Nome, Preco, Detalhe, QtdeEmEstoque, Ranking, Disponivel )
VALUES(1,'PC GAMER T-GAMER KINGSTON FURY', 7500.00, 'Intel i5 11600K / NVIDIA GeForce GTX 1660 Super / 16GB (2x8GB) DDR4 / SSD M.2 NVMe 500GB', 45, 5, 1),
(1,'PC GAMER T-POWER WARLORD', 8300.00 , 'AMD Ryzen 7 / Nvidia GeForce / DDR4 8GB / HD 1TB', 3, 5, 1),
(1,'Computador F�cil Intel Pentium Dual Core 4GB DDR3 HD 500GB' , 1029.00, 'Processador Intel Core i5 3.1GHz / 4 de mem�ria RAM / HD 500GB / Sistema Operacional Linux / Compre direto com o fabricante. Certifica��o ISO9001', 20, 3, 0),
(2,'Computador Intel Core i5 6GB HD 500GB EasyPC Go', 1469.00, 'Intel Pentium Dual Core / 4GB de RAM / HD 500GB', 46, 2, 1),
(2,'Notebook Lenovo IdeaPad 3i', 3000.00, 'S�rie Ideapad 3 Marca Lenovo Usos espec�ficos do produto Personal Tamanho do visor 15.6 polegadas LCD Sistema operacional Windows 10', 23, 5, 1),
(2,'Notebook IdeaPad Gaming 3i', 5200.00, 'S�rie: Gaming 3i Marca: Lenovo Usos espec�ficos do produto: Personal Tamanho do visor 15.6 polegadas LCD Sistema operacional Windows 10', 10, 4, 1),
(2,'Notebook Positivo Motion Q464C', 1500.00, 'Microsoft 365 Incluso Tecnologia Teclado UP mais conforto na digita��o Webcam HD e Microfone Digital Bateria de alta capacidade com autonomia de at� 7 horas Produto port�til pesando apenas 1.3kg Tamanho da tela: 14.0 Item Bivolt', 5, 1, 0),
(3,'Mouse Gamer Logitech G203',119.00, 'Marca: Logitech G Cor:Preto Tecnologia de conex�o Sem fio S�rie: G203 LIGHTSYNC Tipo de conector: USB tipo A', 30, 4, 1),
(3, 'Teclado Multilaser Slim - TC213', 29.00, 'Cor: Preto Descri��o do teclado Membrana tecnologia de conex�o USB Caracter�sticas especiais Compat�vel com Mac Dispositivos compat�veis Windows ou Mac OS Marca Multilaser', 23, 2, 0),
(3, 'Fones de Ouvido Philips com Microfone � Preto', 32.00, 'Marca PHILIPS Cor Preto Tecnologia de conectividade Com fio Nome do modelo TAUE101BK/00 Fator de forma dos fones de ouvido', 27, 3, 1),
(4, 'Celular Xiaomi Redmi 9 64GB/4GB Dual Chip', 1019.00, 'Operadora de celulares e tecnologia sem fio Desbloqueado Marca Xiaomi Formato Smartphone Capacidade de armazenamento da mem�ria 64 GB Sistema operacional Android 10.0', 11, 4, 1),
(4, 'Smartphone Positivo Q20', 799.00, 'Nome do modelo Q20 Operadora de celulares e tecnologia sem fio Todas as Operadoras Marca Positivo Fator de forma Smartphone Capacidade de armazenamento da mem�ria 4 GB', 2, 2, 0),
(4,'Celular Multilaser F 32Gb 3G 1Gb P9130 Preto', 449.00, 'Nome do modelo CELULAR F 32GB 3G 1GB P9130 PRETO Operadora de celulares e tecnologia sem fio 3 Marca Multilaser Formato Smartphone Capacidade de armazenamento da mem�ria 1 GB', 9, 2, 1),
(5, 'Pad Pro 2020 11'' 2 Geracao 256gb Mxdc2ll/a Wifi' ,7500.00, 'Marca Apple Tamanho do visor 11 polegadas Capacidade de armazenamento da mem�ria 256 GB Dimens�es do produto 24.74 x 17.83 x 0.58 cm', 9, 7, 1),
(5, 'Tablet Multilaser M7', 339.00, 'Nome do modelo NB304 Marca Multilaser Tamanho do visor 7 polegadas TN Sistema operacional Android 8.1 Capacidade de armazenamento da mem�ria', 1, 2, 0),
(5, 'Kindle 10a.  � Cor Preta', 349.00, 'A luz embutida ajust�vel permite que voc� leia confortavelmente por horas � em lugares abertos ou fechados  durante o dia ou a noite. A tela de 167 ppi proporciona uma leitura sem reflexo  mesmo sob a luz do sol. � como se voc� estivesse lendo em papel. Leia sem distra��es  Voc� pode marcar trechos melhorar seu vocabul�rio com o dicion�rio  traduzir palavras e ajustar o tamanho da fonte sem precisar sair da p�gina.', 199, 5, 1);

INSERT INTO Cliente (Cpf, Nome, Email, Senha, DataNascimento,Telefone)
VALUES ('11111111111', 'Rafael', 'rafael@email.com', 'tech123', '19970816', '41987654321'),
('22222222222','Antonio','antonio@email.com','tech321','19721206','12312312312'),
('33333333333','Alda','alda@email.com','tech123', '19791016','4654654654'),
('44444444444','Peter Parker','miranha@email.com','miranha123','19701112' ,'4984984984984');

INSERT INTO Endereco (ClienteId,Rua,Numero,Complemento,Cep)
VALUES(1,'Av. Republica Argentina', '1228','Sala 1004','81200000'),
(1,'Mario','200','bl 2 ap 2','1111111'),
(1,'Francisco Camargo','1000', 'Casa','asdasd');

INSERT INTO CartaoCredito (ClienteId,Numero,Bandeira,CodigoSeguranca,DataVencimento)
VALUES(1,'1234567812341234','MasterCard','111', '20271231'),
(1,'1234567812341234','Visa','222', '20230831');

SELECT * FROM CartaoCredito;

SELECT * FROM Cliente

