/*
--------------------------------------------------------------------
© 2020 STRAVATEC
--------------------------------------------------------------------
Nombre   : StravaTEC
Version: 4.0
--------------------------------------------------------------------
*/

-- create tables
CREATE TABLE DEPORTISTA
(
	Usuario				VARCHAR(20)		NOT NULL	UNIQUE,
	ClaveAcceso			VARCHAR(20)		NOT NULL,
	FechaNacimiento 	DATE,
	Nombre				VARCHAR(20),
	Apellido1			VARCHAR(20),
	Apellido2			VARCHAR(20),
	NombreCategoria		VARCHAR(20),
	Nacionalidad    	VARCHAR(25),
	Foto				BYTEA,
	PRIMARY KEY  		(Usuario)
);

CREATE TABLE CARRERA
(
	Nombre				VARCHAR(100)	NOT NULL  UNIQUE,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	Fecha				DATE 			NOT NULL,
	Recorrido			XML,
	Costo				INT				NOT NULL,
	TipoActividad		VARCHAR(30),
	Privacidad          BOOL,
	PRIMARY KEY			(Nombre, AdminDeportista) 
);

CREATE TABLE RETO
(
	Nombre					VARCHAR(100)	NOT NULL    UNIQUE,
	AdminDeportista			VARCHAR(20)		NOT NULL,
	FondoAltitud       		VARCHAR(7),
	TipoActividad			VARCHAR(20),
	PeriodoDisponibilidad	DATE		    NOT NULL,
	Privacidad				BOOL,
	KmTotales				FLOAT8			NOT NULL,
	Descripcion				VARCHAR(150),
	PRIMARY KEY 			(Nombre, AdminDeportista)
);

CREATE TABLE PATROCINADOR
(
	NombreComercial			VARCHAR(50)		NOT NULL,
	Logo					VARCHAR(200),
	NombreRepresentante		VARCHAR(100),
	NumeroTelRepresentante	VARCHAR(15),
	PRIMARY KEY 			(NombreComercial)
);

CREATE TABLE INSCRIPCION
(
	UsuarioDeportista	VARCHAR(20),
	Estado				VARCHAR(10),
	ReciboPago			BYTEA,
	NombreCarrera		VARCHAR(100),
	AdminCarrera 		VARCHAR(20),
	PRIMARY KEY 		(UsuarioDeportista, NombreCarrera, AdminCarrera)
);

CREATE TABLE CATEGORIA
(
	Nombre 				VARCHAR(20)			NOT NULL,
	Descripcion			VARCHAR(150),
	PRIMARY KEY 		(Nombre)
);


CREATE TABLE ACTIVIDAD
(
	UsuarioDeportista	VARCHAR(20)			NOT NULL,
	FechaHora			TIMESTAMP,
	Nombre				VARCHAR(100),
	Duracion			TIME WITHOUT TIME ZONE	NOT NULL,
	Kilometraje			FLOAT8				NOT NULL,
	TipoActividad		VARCHAR(20),
	RecorridoGPX		XML,
	NombreRetoCarrera	VARCHAR(100),
	AdminRetoCarrera	VARCHAR(30),
	Banderilla			INT,
	PRIMARY KEY			(UsuarioDeportista, FechaHora)
);


CREATE TABLE GRUPO
(
	Id 					SERIAL,
	Nombre				VARCHAR(30)		NOT NULL,
	AdminDeportista		VARCHAR(20),
	PRIMARY KEY         (Id, AdminDeportista)
);

CREATE TABLE AMIGO_DEPORTISTA
(
	UsuarioDeportista   VARCHAR(20)  	NOT NULL,
	AmigoID				VARCHAR(20)     NOT NULL,
	PRIMARY KEY 	(UsuarioDeportista, AmigoID)
);


CREATE TABLE GRUPO_DEPORTISTA
(
	UsuarioDeportista   VARCHAR(20),
	IdGrupo				INT,
	AdminDeportista		VARCHAR(20),
	PRIMARY KEY			(UsuarioDeportista, IdGrupo, AdminDeportista)
);

CREATE TABLE GRUPO_CARRERA
(
	NombreCarrera   	VARCHAR(100),
	AdminCarrera		VARCHAR(20),
	AdminGrupo			VARCHAR(20),
	IdGrupo				INT,
	PRIMARY KEY			(NombreCarrera, AdminCarrera, AdminGrupo, IdGrupo)
);

CREATE TABLE GRUPO_RETO
(
	NombreReto  		VARCHAR(100),
	AdminReto			VARCHAR(20),
	AdminGrupo			VARCHAR(20),
	IdGrupo				INT,
	PRIMARY KEY		(NombreReto, AdminReto, AdminGrupo, IdGrupo)
);


CREATE TABLE CARRERA_PATROCINADOR
(
	NombrePatrocinador	VARCHAR(50),	
	NombreCarrera		VARCHAR(100),
	AdminDeportista		VARCHAR(20),
	PRIMARY KEY		(NombrePatrocinador, NombreCarrera, AdminDeportista)
);

CREATE TABLE CARRERA_CATEGORIA
(
	NombreCategoria		VARCHAR(20),	
	NombreCarrera		VARCHAR(100),
	AdminDeportista		VARCHAR(20),
	PRIMARY KEY		(NombreCategoria, NombreCarrera, AdminDeportista)
);

CREATE TABLE RETO_PATROCINADOR
(
	NombrePatrocinador	VARCHAR(50),	
	NombreReto			VARCHAR(100),
	AdminDeportista		VARCHAR(20),
	PRIMARY KEY		(NombrePatrocinador, NombreReto, AdminDeportista)
);

CREATE TABLE CARRERA_CUENTABANCARIA
(
	NombreCarrera	VARCHAR(100),
	AdminDeportista	VARCHAR(20),
	CuentaBancaria	VARCHAR(50),
	PRIMARY KEY		(NombreCarrera, AdminDeportista, CuentaBancaria)
);

CREATE TABLE DEPORTISTA_CARRERA
(
	UsuarioDeportista	VARCHAR(20),
	NombreCarrera		VARCHAR(100),
	AdminDeportista		VARCHAR(20),
	Completada			BOOL,
	PRIMARY KEY 		(UsuarioDeportista, NombreCarrera, AdminDeportista)
);

CREATE TABLE DEPORTISTA_RETO
(
	UsuarioDeportista	VARCHAR(20),
	NombreReto			VARCHAR(100),
	AdminDeportista		VARCHAR(20),
	Completado			BOOL,
	KmAcumulados 		FLOAT8,
	PRIMARY KEY 		(UsuarioDeportista, NombreReto, AdminDeportista)
);

-- se crean las llaves foraneas

-- RETO
ALTER TABLE RETO
ADD FOREIGN KEY (AdminDeportista) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE;

-- INSCRIPCION
ALTER TABLE INSCRIPCION
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE,
ADD FOREIGN KEY (NombreCarrera, AdminCarrera) REFERENCES CARRERA(Nombre, AdminDeportista) ON DELETE CASCADE;

-- ACTIVIDAD
ALTER TABLE ACTIVIDAD
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario)
ON DELETE CASCADE;

-- GRUPO
ALTER TABLE GRUPO
ADD FOREIGN KEY (AdminDeportista) REFERENCES DEPORTISTA(Usuario)
ON DELETE CASCADE;

-- DEPORTISTA
ALTER TABLE DEPORTISTA
ADD FOREIGN KEY (NombreCategoria) REFERENCES CATEGORIA(Nombre)
ON DELETE CASCADE;

-- CARRERA
ALTER TABLE CARRERA
ADD FOREIGN KEY (AdminDeportista) REFERENCES DEPORTISTA(Usuario)
ON DELETE CASCADE;

-- CARRERA CUENTA BANCARIA
ALTER TABLE CARRERA_CUENTABANCARIA
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista)
ON DELETE CASCADE;

-- GRUPO DEPORTISTA
ALTER TABLE GRUPO_DEPORTISTA
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE,
ADD FOREIGN KEY (IdGrupo, AdminDeportista) REFERENCES GRUPO(Id, AdminDeportista)
ON DELETE CASCADE ON UPDATE CASCADE;

-- GRUPO CARRERA
ALTER TABLE GRUPO_CARRERA
ADD FOREIGN KEY (IdGrupo, AdminGrupo) REFERENCES GRUPO(Id, AdminDeportista) ON DELETE CASCADE ON UPDATE CASCADE,
ADD FOREIGN KEY (NombreCarrera, AdminCarrera) REFERENCES CARRERA(Nombre, AdminDeportista) ON DELETE CASCADE;

-- GRUPO RETO
ALTER TABLE GRUPO_RETO
ADD FOREIGN KEY (IdGrupo, AdminGrupo) REFERENCES GRUPO(Id, AdminDeportista) ON DELETE CASCADE ON UPDATE CASCADE,
ADD FOREIGN KEY (NombreReto, AdminReto) REFERENCES RETO(Nombre, AdminDeportista) ON DELETE CASCADE;

-- CARRERA CATEGORIA
ALTER TABLE CARRERA_CATEGORIA
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista) ON DELETE CASCADE,
ADD FOREIGN KEY (NombreCategoria) REFERENCES CATEGORIA(Nombre) ON DELETE CASCADE;

-- CARREREA PATROCINADOR
ALTER TABLE CARRERA_PATROCINADOR
ADD FOREIGN KEY (NombrePatrocinador) REFERENCES PATROCINADOR(NombreComercial) ON DELETE CASCADE,
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista) ON DELETE CASCADE;

-- RETO PATROCINADOR
ALTER TABLE RETO_PATROCINADOR
ADD FOREIGN KEY (NombrePatrocinador) REFERENCES PATROCINADOR(NombreComercial) ON DELETE CASCADE,
ADD FOREIGN KEY (NombreReto, AdminDeportista) REFERENCES RETO(Nombre, AdminDeportista) ON DELETE CASCADE;

-- AMIGO DEPORTISTA
ALTER TABLE AMIGO_DEPORTISTA
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE,
ADD FOREIGN KEY (AmigoID) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE;

-- DEPORTISTA RETO
ALTER TABLE DEPORTISTA_RETO
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE,
ADD FOREIGN KEY (NombreReto, AdminDeportista) REFERENCES RETO(Nombre, AdminDeportista) ON DELETE CASCADE;

-- DEPORTISTA CARRERA
ALTER TABLE DEPORTISTA_CARRERA
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario) ON DELETE CASCADE,
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista) ON DELETE CASCADE;

