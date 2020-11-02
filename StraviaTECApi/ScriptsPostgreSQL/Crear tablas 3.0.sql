/*
--------------------------------------------------------------------
© 2020 STRAVATEC
--------------------------------------------------------------------
Nombre   : StravaTEC
Version: 3.0
--------------------------------------------------------------------
*/

-- create tables
CREATE TABLE DEPORTISTA
(
	Usuario				VARCHAR(20)		NOT NULL	UNIQUE,
	ClaveAcceso			VARCHAR(20)		NOT NULL,
	FechaNacimiento 	DATE			NOT NULL,
	Nombre				VARCHAR(20)     NOT NULL,
	Apellido1			VARCHAR(20)     NOT NULL,
	Apellido2			VARCHAR(20)		NOT NULL,
	NombreCategoria		VARCHAR(20),
	Nacionalidad    	VARCHAR(25)     NOT NULL,
	Foto				BYTEA			NOT NULL,
	PRIMARY KEY  		(Usuario)
);

CREATE TABLE CARRERA
(
	Nombre				VARCHAR(30)		NOT NULL  UNIQUE,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	Fecha				DATE			NOT NULL,
	Recorrido			BYTEA			NOT NULL,
	Costo				INT				NOT NULL,
	TipoActividad		VARCHAR(30)     NOT NULL,
	Privacidad          BOOL			NOT NULL,
	PRIMARY KEY			(Nombre, AdminDeportista) 
);

CREATE TABLE RETO
(
	Nombre					VARCHAR(20)		NOT NULL    UNIQUE,
	AdminDeportista			VARCHAR(20)		NOT NULL,
	FondoAltitud       		VARCHAR(7)		NOT NULL,
	TipoActividad			VARCHAR(20)     NOT NULL,
	PeriodoDisponibilidad	DATE			NOT NULL,
	Privacidad				BOOL			NOT NULL,
	KmAcumulados 			FLOAT8,
	KmTotales				FLOAT8			NOT NULL,
	Descripcion				VARCHAR(150),
	PRIMARY KEY 			(Nombre, AdminDeportista)
);

CREATE TABLE PATROCINADOR
(
	NombreComercial			VARCHAR(30)		NOT NULL,
	Logo					BYTEA,
	NombreRepresentante		VARCHAR(100)    NOT NULL,
	NumeroTelRepresentante	VARCHAR(15)		NOT NULL,
	PRIMARY KEY 			(NombreComercial)
);

CREATE TABLE INSCRIPCION
(
	Id					SERIAL				NOT NULL	UNIQUE,
	UsuarioDeportista	VARCHAR(20)			NOT NULL,
	Estado				VARCHAR(10)			NOT NULL,
	ReciboPago			BYTEA				NOT NULL,
	PRIMARY KEY 		(Id, UsuarioDeportista)
);

CREATE TABLE CATEGORIA
(
	Nombre 				VARCHAR(20)			NOT NULL,
	Descripcion			VARCHAR(150),
	PRIMARY KEY 		(Nombre)
);


CREATE TABLE ACTIVIDAD
(
	UsuarioDeportista	VARCHAR(20)			NOT NULL		UNIQUE,
	FechaHora			TIMESTAMP			NOT NULL,
	Duracion			VARCHAR(10)			NOT NULL,
	Kilometraje			FLOAT8				NOT NULL,
	TipoActividad		VARCHAR(20)		  	NOT NULL,
	RecorridoGPX		BYTEA,				
	PRIMARY KEY			(UsuarioDeportista, FechaHora)
);


CREATE TABLE GRUPO
(
	Nombre				VARCHAR(30)		NOT NULL   UNIQUE,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY         (Nombre, AdminDeportista)
);

CREATE TABLE AMIGO_DEPORTISTA
(
	UsuarioDeportista   VARCHAR(20)  	NOT NULL,
	AmigoID				VARCHAR(20)     NOT NULL,
	PRIMARY KEY 	(UsuarioDeportista, AmigoID)
);


CREATE TABLE GRUPO_DEPORTISTA
(
	UsuarioDeportista   VARCHAR(20)  	NOT NULL,	
	NombreGrupo			VARCHAR(30)		NOT NULL	UNIQUE,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY			(UsuarioDeportista, NombreGrupo, AdminDeportista)
);

CREATE TABLE GRUPO_CARRERA
(
	NombreCarrera   	VARCHAR(30)  	NOT NULL,	
	NombreGrupo			VARCHAR(30)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY			(NombreCarrera, NombreGrupo, AdminDeportista)
);

CREATE TABLE GRUPO_RETO
(
	NombreReto  		VARCHAR(20)  	NOT NULL,	
	NombreGrupo			VARCHAR(30)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY		(NombreReto, NombreGrupo, AdminDeportista)
);


CREATE TABLE INSCRIPCION_CARRERA
(
	IdInscripcion  			INT  			NOT NULL,
	DeportistaInscripcion	VARCHAR(20)		NOT NULL,
	NombreCarrera			VARCHAR(30)		NOT NULL,
	PRIMARY KEY		(IdInscripcion, NombreCarrera, DeportistaInscripcion)
);


CREATE TABLE CARRERA_PATROCINADOR
(
	NombrePatrocinador	VARCHAR(30)		NOT NULL,	
	NombreCarrera		VARCHAR(30)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY		(NombrePatrocinador, NombreCarrera, AdminDeportista)
);

CREATE TABLE CARRERA_CATEGORIA
(
	NombreCategoria		VARCHAR(20)		NOT NULL,	
	NombreCarrera		VARCHAR(30)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY		(NombreCategoria, NombreCarrera, AdminDeportista)
);

CREATE TABLE RETO_PATROCINADOR
(
	NombrePatrocinador	VARCHAR(30)		NOT NULL,	
	NombreReto			VARCHAR(20)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	PRIMARY KEY		(NombrePatrocinador, NombreReto, AdminDeportista)
);

CREATE TABLE CARRERA_CUENTABANCARIA
(
	NombreCarrera	VARCHAR(30)		NOT NULL,
	AdminDeportista	VARCHAR(20)		NOT NULL,
	CuentaBancaria	VARCHAR(50)		NOT NULL,
	PRIMARY KEY		(NombreCarrera, AdminDeportista)
);

CREATE TABLE DEPORTISTA_CARRERA
(
	UsuarioDeportista	VARCHAR(20)		NOT NULL,
	NombreCarrera		VARCHAR(30)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	Completada			BOOL 			NOT NULL,
	PRIMARY KEY 		(UsuarioDeportista, NombreCarrera, AdminDeportista)
);

CREATE TABLE DEPORTISTA_RETO
(
	UsuarioDeportista	VARCHAR(20)		NOT NULL,
	NombreReto			VARCHAR(20)		NOT NULL,
	AdminDeportista		VARCHAR(20)		NOT NULL,
	Completado			BOOL 			NOT NULL,
	PRIMARY KEY 		(UsuarioDeportista, NombreReto, AdminDeportista)
);

-- se crean las llaves foraneas

-- RETO
ALTER TABLE RETO
ADD FOREIGN KEY (AdminDeportista) REFERENCES DEPORTISTA(Usuario);

-- INSCRIPCION
ALTER TABLE INSCRIPCION
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario);

-- ACTIVIDAD
ALTER TABLE ACTIVIDAD
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario);

-- GRUPO
ALTER TABLE GRUPO
ADD FOREIGN KEY (AdminDeportista) REFERENCES DEPORTISTA(Usuario);

-- DEPORTISTA
ALTER TABLE DEPORTISTA
ADD FOREIGN KEY (NombreCategoria) REFERENCES CATEGORIA(Nombre);

-- CARRERA
ALTER TABLE CARRERA
ADD FOREIGN KEY (AdminDeportista) REFERENCES DEPORTISTA(Usuario);

-- CARRERA CUENTA BANCARIA
ALTER TABLE CARRERA_CUENTABANCARIA
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista);

-- GRUPO DEPORTISTA
ALTER TABLE GRUPO_DEPORTISTA
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario),
ADD FOREIGN KEY (NombreGrupo, AdminDeportista) REFERENCES GRUPO(Nombre, AdminDeportista);

-- GRUPO CARRERA
ALTER TABLE GRUPO_CARRERA
ADD FOREIGN KEY (NombreGrupo) REFERENCES GRUPO(Nombre),
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista);

-- GRUPO RETO
ALTER TABLE GRUPO_RETO
ADD FOREIGN KEY (NombreGrupo) REFERENCES GRUPO(Nombre),
ADD FOREIGN KEY (NombreReto, AdminDeportista) REFERENCES RETO(Nombre, AdminDeportista);

-- CARRERA CATEGORIA
ALTER TABLE CARRERA_CATEGORIA
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista),
ADD FOREIGN KEY (NombreCategoria) REFERENCES CATEGORIA(Nombre);

-- CARREREA PATROCINADOR
ALTER TABLE CARRERA_PATROCINADOR
ADD FOREIGN KEY (NombrePatrocinador) REFERENCES PATROCINADOR(NombreComercial),
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista);

-- RETO PATROCINADOR
ALTER TABLE RETO_PATROCINADOR
ADD FOREIGN KEY (NombrePatrocinador) REFERENCES PATROCINADOR(NombreComercial),
ADD FOREIGN KEY (NombreReto, AdminDeportista) REFERENCES RETO(Nombre, AdminDeportista);

-- INSCRIPCION CARRERA
ALTER TABLE INSCRIPCION_CARRERA
ADD FOREIGN KEY (IdInscripcion, DeportistaInscripcion) REFERENCES INSCRIPCION(Id, UsuarioDeportista),
ADD FOREIGN KEY (NombreCarrera) REFERENCES CARRERA(Nombre);

-- AMIGO DEPORTISTA
ALTER TABLE AMIGO_DEPORTISTA
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario),
ADD FOREIGN KEY (AmigoID) REFERENCES DEPORTISTA(Usuario);

-- DEPORTISTA RETO
ALTER TABLE DEPORTISTA_RETO
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario),
ADD FOREIGN KEY (NombreReto, AdminDeportista) REFERENCES RETO(Nombre, AdminDeportista);

-- DEPORTISTA CARRERA
ALTER TABLE DEPORTISTA_CARRERA
ADD FOREIGN KEY (UsuarioDeportista) REFERENCES DEPORTISTA(Usuario),
ADD FOREIGN KEY (NombreCarrera, AdminDeportista) REFERENCES CARRERA(Nombre, AdminDeportista);

