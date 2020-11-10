/*
--------------------------------------------------------------------
© 2020 STRAVIATEC
--------------------------------------------------------------------
Nombre   : StraviaTEC
Version: 2.0
--------------------------------------------------------------------
*/

--insertar data

-- SE AGREGAN LAS CATEGORIAS
INSERT INTO public.categoria(
	nombre, descripcion)
	VALUES ('Junior', 'menos de 15 años'),
	       ('Sub-23', 'de 15 a 23 años'),
		   ('Open', 'de 24 a 30 años'),
		   ('Elite', 'cualquiera que quiera inscribirse'),
		   ('Master A', 'de 30 a 40 años'),
		   ('Master B', 'de 41 a 50 años'),
		   ('Master C', 'más de 51 años');

-- SE AGREGAN LOS DEPORTISTAS
INSERT INTO public.deportista(
	usuario, claveacceso, fechanacimiento, nombre, apellido1, apellido2, nombrecategoria, nacionalidad, foto)
	VALUES ('sam.astua', 'password', '2000-09-15', 'Saymon', 'Astúa', 'Madrigal', 'Sub-23', 'Costa Rica', NULL),
		   ('kevintrox', 'password', '2000-12-21', 'Kevin', 'Acevedo', 'Rodríguez', 'Sub-23', 'Costa Rica', NULL),
		   ('etesech', 'password', '1993-12-03', 'Sech', 'Morales', 'Williams', 'Open', 'Panamá', NULL),
		   ('elpepe', 'password', '1964-01-24', 'Pepe', 'Ramírez', 'González', 'Master C', 'México', NULL),
		   ('crespo', 'password', '1994-05-19', 'José', 'Crespo', 'Cepeda', 'Open', 'España', NULL),
		   ('ironman', 'password', '1965-04-04', 'Robert', 'Downey', 'Jr', 'Master C', 'Estados Unidos', NULL),
		   ('ozuna', 'password', '1974-06-10', 'Juan', 'Ozuna', 'Rosado', 'Master B', 'Puerto Rico', NULL),
		   ('cj', 'password', '1978-07-11', 'Carl', 'Johnson', 'Rodríguez', 'Master B', 'Canadá', NULL),
		   ('auronplay', 'password', '1988-11-05', 'Raúl', 'Álvarez', 'Genes', 'Master A', 'España', NULL),
		   ('cr7', 'password', '1985-02-05', 'Cristiano', 'Dos Santos', 'Aveiro', 'Master A', 'Portugal', NULL);
		   

-- SE AGREGAN LOS PATROCINADORES DE LA CARRERA		   
INSERT INTO public.patrocinador(
	nombrecomercial, logo, nombrerepresentante, numerotelrepresentante)
	VALUES ('Grupo INS', 'https://acortar.link/gGMhs', 'Róger Guillermo Arias Agüero','(+506)2287-6000'),
		   ('CoopeTarrazú', 'https://acortar.link/jtm5s', 'Yendry Leiva','(+506)2546-8615'),
		   ('KOLBI', 'https://acortar.link/rI9vm', 'Marjorie González Cascante','(+506)2255-1155'),
		   ('TDMAS', 'https://acortar.link/L2LyQ', 'Andres Nicolas','(+506)2232-2222');
	
-- SE CREAN LOS GRUPOS	
INSERT INTO public.grupo(
	nombre, admindeportista)
	VALUES ('Las Estrellas', 'sam.astua'),
		   ('Los Toros', 'kevintrox'),
		   ('Los Bichos', 'cr7'),
		   ('Los Físicos', 'crespo');

-- SE AGREGAN DEPORTISTAS A LOS GRUPOS	   
INSERT INTO public.grupo_deportista(
	usuariodeportista, nombregrupo, admindeportista)
	VALUES ('etesech', 'Las Estrellas', 'sam.astua'),
		   ('elpepe', 'Las Estrellas', 'sam.astua'),
		   ('ozuna', 'Las Estrellas', 'sam.astua'),
		   ('sam.astua', 'Los Toros', 'kevintrox'),
		   ('auronplay', 'Los Toros', 'kevintrox'),
		   ('cj', 'Los Bichos', 'cr7'),
		   ('auronplay', 'Los Bichos', 'cr7'),
		   ('ironman', 'Los Físicos', 'crespo'),
		   ('cr7', 'Los Físicos', 'crespo');
		   
-- SE CREAN CARRERAS	
INSERT INTO public.carrera(
	nombre, admindeportista, fecha, recorrido, costo, tipoactividad, privacidad)
	VALUES ('Endurance 2020', 'sam.astua', '2020-12-10', NULL, 15000, 'Ciclismo', true),
		   ('The Best', 'cr7', '2020-12-20', NULL, 40000, 'Correr', false);
	
-- SE AGREGAN LAS CUENTAS BANCARIAS DE LAS CARRERAS
INSERT INTO public.carrera_cuentabancaria(
	nombrecarrera, admindeportista, cuentabancaria)
	VALUES ('Endurance 2020', 'sam.astua', 'CR05010056898889927823'),
		   ('Endurance 2020', 'sam.astua', 'CR05010053798887649823'),
		   ('The Best', 'cr7', 'CR05010053797417649855'),
		   ('The Best', 'cr7', 'CR05010053791947649822');

-- SE AGREGAN LOS PATROCINADORES A LAS CARRERAS		   
INSERT INTO public.carrera_patrocinador(
	nombrepatrocinador, nombrecarrera, admindeportista)
	VALUES ('KOLBI', 'Endurance 2020', 'sam.astua'),
		   ('CoopeTarrazú', 'Endurance 2020', 'sam.astua'),
		   ('TDMAS', 'The Best', 'cr7'),
		   ('Grupo INS', 'The Best', 'cr7');

-- SE AGREGA LOS GRUPOS A MOSTRAR PARA UNA CARRERA PRIVADA	
INSERT INTO public.grupo_carrera(
	nombrecarrera, admincarrera, admingrupo, nombregrupo)
	VALUES ('Endurance 2020', 'sam.astua', 'sam.astua', 'Las Estrellas'),
		   ('Endurance 2020', 'sam.astua', 'kevintrox', 'Los Toros');
		   
-- SE AGREGAN LAS CATEGORIAS DISPONIBLES A LAS CARRERAS
INSERT INTO public.carrera_categoria(
	nombrecategoria, nombrecarrera, admindeportista)
	VALUES ('Junior', 'Endurance 2020', 'sam.astua'),
	       ('Sub-23', 'Endurance 2020', 'sam.astua'),
		   ('Master A', 'Endurance 2020', 'sam.astua'),
		   ('Master B', 'Endurance 2020', 'sam.astua'),
		   ('Master C', 'Endurance 2020', 'sam.astua'),
		   ('Open', 'The Best', 'cr7'),
		   ('Sub-23', 'The Best', 'cr7'),
		   ('Master B', 'The Best', 'cr7'),
		   ('Master C', 'The Best', 'cr7');
		   
-- SE AGREGAN INSCRIPCIONES A LAS CARRERAS
INSERT INTO public.inscripcion(
	usuariodeportista, estado, recibopago)
	VALUES ('auronplay', 'En espera', NULL),
		   ('elpepe', 'En espera', NULL),
		   ('crespo', 'En espera', NULL),
		   ('cj', 'En espera', NULL);

-- SE ASOCIAN LAS INSCRIPCIONES A UNA CARRERA
INSERT INTO public.inscripcion_carrera(
	EstadoInscripcion, deportistainscripcion, nombrecarrera, admincarrera)
	VALUES ('En espera', 'auronplay', 'Endurance 2020','sam.astua'),
		   ('En espera', 'elpepe', 'Endurance 2020', 'sam.astua'),
		   ('En espera', 'crespo', 'The Best', 'cr7'),
		   ('En espera', 'cj', 'The Best', 'cr7');


-- SE CREAN LOS RETOS
INSERT INTO public.reto(
	nombre, admindeportista, fondoaltitud, tipoactividad, periododisponibilidad, privacidad, kmtotales, descripcion)
	VALUES ('Reto 1', 'kevintrox', 'fondo', 'Correr', '2020-12-10', false, 25, 'Deberá completar 25km corriendo'),
		   ('Reto 2', 'sam.astua', 'altitud', 'Ciclismo', '2020-11-29', true, 2, 'Deberá completar un total de 2km ascendidos en bicicleta');

-- SE AGREGAN LOS APTROCINADORES A LOS RETOS
INSERT INTO public.reto_patrocinador(
	nombrepatrocinador, nombrereto, admindeportista)
	VALUES ('KOLBI', 'Reto 1', 'kevintrox'),
		   ('TDMAS', 'Reto 1', 'kevintrox'),
		   ('CoopeTarrazú', 'Reto 2', 'sam.astua'),
		   ('Grupo INS', 'Reto 2', 'sam.astua');
	
-- SE AGREGAN LOS GRUPOS VISIBLES PARA UNO DE LOS RETOS
INSERT INTO public.grupo_reto(
	nombrereto, adminreto, admingrupo, nombregrupo)
	VALUES ('Reto 2', 'sam.astua', 'cr7', 'Los Bichos'),
		   ('Reto 2', 'sam.astua', 'kevintrox', 'Los Toros');


-- SE INSCRIBEN DEPORTISTAS A LOS RETOS
INSERT INTO public.deportista_reto(
	usuariodeportista, nombrereto, admindeportista, completado, kmacumulados)
	VALUES ('etesech', 'Reto 1', 'kevintrox', false, 0),
		   ('cj', 'Reto 1', 'kevintrox', false, 0),
		   ('auronplay', 'Reto 2', 'sam.astua', false, 0),
		   ('ozuna', 'Reto 2', 'sam.astua', false, 0);
	
-- SEGUIMIENTO DE DEPORTISTAS
INSERT INTO public.amigo_deportista(
	usuariodeportista, amigoid)
	VALUES ('sam.astua', 'kevintrox'),
		   ('sam.astua', 'auronplay'),
		   ('sam.astua', 'etesech'),
		   ('kevintrox', 'ozuna'),
		   ('kevintrox', 'cr7'),
		   ('kevintrox', 'ironman'),
		   ('crespo', 'cr7'),
		   ('crespo', 'cj');
	
	
	
