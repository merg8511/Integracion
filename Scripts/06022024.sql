--Tabla de Categoria
CREATE TABLE Categoria(
	CategoriaId INT NOT NULL IDENTITY,
	Nombre NVARCHAR(120) NOT NULL
);

ALTER TABLE Categoria
	ADD CONSTRAINT PK_Categoria PRIMARY KEY(CategoriaId)

-- Tabla de Marca
CREATE TABLE Marca(
	MarcaId INT NOT NULL IDENTITY,
	Nombre NVARCHAR(120) NOT NULL
);

ALTER TABLE Marca
	ADD CONSTRAINT PK_Marca PRIMARY KEY(MarcaId)

-- Tabla de Producto
CREATE TABLE Producto(
	ProductoId INT NOT NULL IDENTITY,
	NombreProducto NVARCHAR(120) NOT NULL,
	Precio NUMERIC(7,2) NOT NULL,
	Costo NUMERIC(7,2) NOT NULL,
	CategoriaId INT NOT NULL,
	MarcaId INT NOT NULL
);

ALTER TABLE Producto
	ADD CONSTRAINT PK_Producto PRIMARY KEY(ProductoId);

ALTER TABLE Producto
	ADD CONSTRAINT FK_Producto_Categoria_CategoriaId FOREIGN KEY(CategoriaId)
	REFERENCES Categoria(CategoriaId) ON DELETE CASCADE;

ALTER TABLE Producto
	ADD CONSTRAINT FK_Producto_Marca_MarcaId FOREIGN KEY(MarcaId)
	REFERENCES Marca(MarcaId) ON DELETE CASCADE;

INSERT INTO Categoria(Nombre)
	VALUES('Computadoras'), ('Impresoras');

INSERT INTO Marca(Nombre)
	VALUES('HP'),('Apple');
