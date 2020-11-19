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
		   ('malumababy', 'password', '2006-04-16', 'Maluma', 'Londoño', 'Arias', 'Junior', 'Colombia', NULL),
		   ('jbalvin', 'password', '2007-11-25', 'José', 'Osorio', 'Balvín', 'Junior', 'Colombia', NULL),
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
	id, nombre, admindeportista)
	VALUES (1, 'Las Estrellas', 'sam.astua'),
		   (2, 'Los Toros', 'kevintrox'),
		   (3, 'Los Bichos', 'cr7'),
		   (4, 'Los Físicos', 'crespo');

-- SE AGREGAN DEPORTISTAS A LOS GRUPOS	   
INSERT INTO public.grupo_deportista(
	usuariodeportista, idgrupo, admindeportista)
	VALUES ('etesech', 1, 'sam.astua'),
		   ('elpepe', 1, 'sam.astua'),
		   ('ozuna', 1, 'sam.astua'),
		   ('sam.astua', 2, 'kevintrox'),
		   ('auronplay', 2, 'kevintrox'),
		   ('cj', 3, 'cr7'),
		   ('auronplay', 3, 'cr7'),
		   ('ironman', 4, 'crespo'),
		   ('cr7', 4, 'crespo');
		   
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
	nombrecarrera, admincarrera, admingrupo, idgrupo)
	VALUES ('Endurance 2020', 'sam.astua', 'sam.astua', 1),
		   ('Endurance 2020', 'sam.astua', 'kevintrox', 2);
		   
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
	usuariodeportista, estado, recibopago, nombrecarrera, admincarrera)
	VALUES ('auronplay', 'Aceptado', NULL, 'Endurance 2020','sam.astua'), --Endurance 2020 
		   ('sam.astua', 'Aceptado', NULL, 'Endurance 2020','sam.astua'),
		   ('kevintrox', 'Aceptado', NULL, 'Endurance 2020','sam.astua'),
		   ('malumababy', 'Aceptado', NULL, 'Endurance 2020','sam.astua'),
		   ('jbalvin', 'Aceptado', NULL, 'Endurance 2020','sam.astua'),
		   ('cr7', 'Aceptado', NULL, 'Endurance 2020','sam.astua'),
		   ('elpepe', 'En espera', NULL, 'Endurance 2020', 'sam.astua'),
		   ('crespo', 'En espera', NULL,'The Best', 'cr7'), -- The Best
		   ('cj', 'En espera', NULL, 'The Best', 'cr7'),
		   ('etesech', 'Aceptado', NULL, 'The Best', 'cr7'),
		   ('ironman', 'Aceptado', NULL, 'The Best', 'cr7'),
		   ('sam.astua', 'Aceptado', NULL, 'The Best', 'cr7'),
		   ('kevintrox', 'Aceptado', NULL, 'The Best', 'cr7'),
		   ('ozuna', 'Aceptado', NULL, 'The Best', 'cr7'),
		   ('elpepe', 'Aceptado', NULL, 'The Best', 'cr7');


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
	nombrereto, adminreto, admingrupo, idgrupo)
	VALUES ('Reto 2', 'sam.astua', 'cr7', 3),
		   ('Reto 2', 'sam.astua', 'kevintrox', 2);


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


-- SE AGREGAN DEPORTISTAS A LAS CARRERAS (completan las carreras)
INSERT INTO public.deportista_carrera(
	usuariodeportista, nombrecarrera, admindeportista, completada)
	VALUES ('sam.astua', 'Endurance 2020', 'sam.astua', true),
		   ('kevintrox', 'Endurance 2020', 'sam.astua', true),
		   ('malumababy', 'Endurance 2020', 'sam.astua', true),
		   ('jbalvin', 'Endurance 2020', 'sam.astua', true),
		   ('auronplay', 'Endurance 2020', 'sam.astua', true),
		   ('cr7', 'Endurance 2020', 'sam.astua', true),
		   ('etesech', 'The Best', 'cr7', true),
		   ('ironman', 'The Best', 'cr7', true),
		   ('sam.astua', 'The Best', 'cr7', true),
		   ('kevintrox', 'The Best', 'cr7', true),
		   ('ozuna', 'The Best', 'cr7', true),
		   ('elpepe', 'The Best', 'cr7', true);

-- COMPLETACIÓN DE CARRERAS
INSERT INTO public.actividad(
	usuariodeportista, fechahora, nombre, duracion, kilometraje, tipoactividad, recorridogpx, nombreretocarrera, adminretocarrera, banderilla)
	VALUES ('sam.astua', '2020/11/11 15:30', 'Carrera 1', '2:30:55', 40, 'Ciclismo', NULL, 'Endurance 2020', 'sam.astua', 0), --Endurance 2020
		   ('kevintrox', '2020/11/11 15:30', 'Carrera 1', '2:37:21', 40, 'Ciclismo', NULL, 'Endurance 2020', 'sam.astua', 0),
		   ('malumababy', '2020/11/11 15:30', 'Carrera 1', '2:50:35', 40, 'Ciclismo', NULL, 'Endurance 2020', 'sam.astua', 0),
		   ('jbalvin', '2020/11/11 15:30', 'Carrera 1', '2:55:30', 40, 'Ciclismo', NULL, 'Endurance 2020', 'sam.astua', 0),
		   ('auronplay', '2020/11/11 15:30', 'Carrera 1', '2:10:14', 40, 'Ciclismo', NULL, 'Endurance 2020', 'sam.astua', 0),
		   ('cr7', '2020/11/11 15:30', 'Carrera 1', '1:50:34', 40, 'Ciclismo', NULL, 'Endurance 2020', 'sam.astua', 0),
		   ('etesech', '2020/12/11 10:10', 'Carrera 2', '2:50:14', 20, 'Correr', NULL, 'The Best', 'cr7', 0), -- The Best
		   ('ironman', '2020/12/11 10:10', 'Carrera 2', '2:10:44', 20, 'Correr', NULL, 'The Best', 'cr7', 0),
		   ('sam.astua', '2020/12/11 10:10', 'Carrera 2', '2:20:05', 20, 'Correr', NULL, 'The Best', 'cr7', 0),
		   ('kevintrox', '2020/12/11 10:10', 'Carrera 2', '2:19:30', 20, 'Correr', NULL, 'The Best', 'cr7', 0),
		   ('ozuna', '2020/12/11 10:10', 'Carrera 2', '2:12:15', 20, 'Correr', NULL, 'The Best', 'cr7', 0),
		   ('elpepe', '2020/12/11 10:10', 'Carrera 2', '2:30:09', 20, 'Correr', NULL, 'The Best', 'cr7', 0);
	
