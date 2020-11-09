CREATE TABLE IF NOT EXISTS DEPORTISTA_CARRERA(Usuario TEXT, NombreActividad TEXT, NombreCarrera TEXT, Duracion TEXT, PRIMARY KEY(Usuario, NombreCarrera));

CREATE TABLE IF NOT EXISTS DEPORTISTA_RETO(Usuario TEXT, NombreActividad TEXT, NombreReto TEXT, Distancia FLOAT, Duracion TEXT, RecorridoGPX TEXT, PRIMARY KEY(Usuario, NombreActividad, NombreReto, Distancia, Duracion, RecorridoGPX));
