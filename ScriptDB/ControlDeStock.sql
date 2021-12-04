
CREATE TABLE Deposito(
    DepositoID     varchar(4)      NOT NULL,
    Descripcion    varchar(150)    NULL,
    CONSTRAINT PK1 PRIMARY KEY NONCLUSTERED (DepositoID)
)
go


CREATE TABLE Ubicacion(
    DepositoID      varchar(4)     NOT NULL,
    CodUbicacion    varchar(11)    NOT NULL,
    Area            varchar(20)     NOT NULL,
    Pasillo         varchar(20)     NOT NULL,
    Fila            varchar(20)     NOT NULL,
    Cara            varchar(20)     NOT NULL,
    CONSTRAINT PK2 PRIMARY KEY NONCLUSTERED (DepositoID, CodUbicacion)
)
go


CREATE TABLE UbicacionProducto(
    DepositoID      varchar(4)     NOT NULL,
    CodUbicacion    varchar(11)    NOT NULL,
    ProductoID      varchar(11)    NOT NULL,
    Cantidad        int            NOT NULL,
    CONSTRAINT PK3 PRIMARY KEY NONCLUSTERED (DepositoID, CodUbicacion, ProductoID)
)
go


ALTER TABLE Ubicacion ADD CONSTRAINT RefDeposito1 
    FOREIGN KEY (DepositoID)
    REFERENCES Deposito(DepositoID)
go


ALTER TABLE UbicacionProducto ADD CONSTRAINT RefUbicacion2 
    FOREIGN KEY (DepositoID, CodUbicacion)
    REFERENCES Ubicacion(DepositoID, CodUbicacion)
go


