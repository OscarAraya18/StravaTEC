/*
--------------------------------------------------------------------
© 2020 STRAVATEC
--------------------------------------------------------------------
Nombre   : StravaTEC
Version: 1.0
--------------------------------------------------------------------
*/

--insertar data

INSERT INTO public.deportista(
	usuario, claveacceso, fechanacimiento, nombre, apellido1, apellido2, nombrecategoria, nacionalidad, foto)
	VALUES ('sam.astua', 'password', '2000-09-15', 'Saymon', 'Astúa', 'Madrigal', NULL, 'Costarricense', NULL),
		   ('kevintrox', 'password', '2000-12-15', 'Kevin', 'Acevedo', 'Rodríguez', NULL, 'Costarricense', NULL);
		   
INSERT INTO public.categoria(
	nombre, descripcion)
	VALUES ('Junior', 'menos de 15 años'),
	       ('Sub-23', 'de 15 a 23 años'),
		   ('Open', 'de 24 a 30 años'),
		   ('Elite', 'cualquiera que quiera inscribirse'),
		   ('Master A', 'de 30 a 40 años'),
		   ('Master B', 'de 41 a 50 años'),
		   ('Master C', 'más de 51 años');
		   
INSERT INTO public.patrocinador(
	nombrecomercial, logo, nombrerepresentante, numerotelrepresentante)
	VALUES ('Astua Café', NULL, 'Sergio Astúa Quesada','(+506) 85681546');
	
INSERT INTO public.carrera(
	nombre, admindeportista, fecha, recorrido, costo, tipoactividad, privacidad)
	VALUES ('Endurance 2020', 'sam.astua', '2020-12-10', NULL, 15000, 'Ciclismo', true);
	
	


	
