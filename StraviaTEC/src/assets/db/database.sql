CREATE TABLE IF NOT EXISTS DEPORTISTA_CARRERA(
    Usuario TEXT,
    AdminDeportista TEXT,
    NombreActividad TEXT, 
    TipoActividad TEXT, 
    NombreCarrera TEXT, 
    Duracion TEXT, 
    RecorridoGPX TEXT,
    FechaHora TEXT, 
    PRIMARY KEY(Usuario, NombreCarrera, FechaHora));

CREATE TABLE IF NOT EXISTS DEPORTISTA_RETO(
    Usuario TEXT, 
    AdminDeportista, 
    NombreActividad TEXT, 
    TipoActividad TEXT, 
    NombreReto TEXT, 
    Distancia FLOAT, 
    Duracion TEXT, 
    RecorridoGPX TEXT, 
    FechaHora TEXT
    PRIMARY KEY(Usuario, NombreActividad, NombreReto, Distancia, Duracion, RecorridoGPX));
