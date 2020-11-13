import { Injectable } from '@angular/core';
import { SQLitePorter } from '@ionic-native/sqlite-porter/ngx';
import { HttpClient } from '@angular/common/http';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { BehaviorSubject, Observable } from 'rxjs';
import { Platform } from '@ionic/angular';

export interface DeportistaCarrera{
  Usuario: string,
  AdminDeportista: string,
  NombreActividad: string,
  TipoActividad: string,
  NombreCarrera: string,
  Duracion: string,
  RecorridoGPX: string,
  FechaHora: string
}

export interface DeportistaReto{
  Usuario: string,
  AdminDeportista: string,
  NombreActividad: string,
  TipoActividad: string,
  NombreReto: string,
  Distancia: number,
  Duracion: string,
  RecorridoGPX: string,
  FechaHora: string
}

export interface DeportistaActividadLibre{
  Usuario: string,
  NombreActividad: string,
  TipoActividad: string,
  FechaHora: string,
  Duracion: string,
  Distancia: number,
  RecorridoGPX: string 
}

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {
  private database: SQLiteObject;
  private dbReady: BehaviorSubject<boolean> = new BehaviorSubject(false);
 
  deportistasCarrera = new BehaviorSubject([]);
  deportistasReto = new BehaviorSubject([]);
  deportistasActividadLibre = new BehaviorSubject([]);

   constructor(private plt: Platform, private sqlitePorter: SQLitePorter, private sqlite: SQLite, private http: HttpClient) {
    this.plt.ready().then(() => {
      this.sqlite.create({
        name: 'straviaTECDB.db',
        location: 'default'
      })
      .then((db: SQLiteObject) => {
          this.database = db;
          this.seedDatabase();
      });
    });
  }

  seedDatabase() {
    this.http.get('assets/db/database.sql', { responseType: 'text'})
    .subscribe(sql => {
      this.sqlitePorter.importSqlToDb(this.database, sql)
        .then(_ => {
          this.dbReady.next(true);
        })
        .catch(e => console.error(e));
    });
  }

  getDatabaseState() {
    return this.dbReady.asObservable();
  }
 
  getDeportistasCarrera(): Observable<DeportistaCarrera[]> {
    return this.deportistasCarrera.asObservable();
  }
 
  getDeportistasReto(): Observable<DeportistaReto[]> {
    return this.deportistasReto.asObservable();
  }

  getDeportistasActividadLibre(): Observable<DeportistaActividadLibre[]> {
    return this.deportistasActividadLibre.asObservable();
  }


  loadDeportistasCarrera(Usuario: string) {
    return this.database.executeSql('SELECT * FROM DEPORTISTA_CARRERA WHERE Usuario = ?', [Usuario]).then(data => {
      let dc: DeportistaCarrera[] = [];
 
      if (data.rows.length > 0) {
        for (var i = 0; i < data.rows.length; i++) {
 
          dc.push({ 
            Usuario: data.rows.item(i).Usuario,
            AdminDeportista: data.rows.item(i).AdminDeportista,
            NombreActividad: data.rows.item(i).NombreActividad,
            TipoActividad: data.rows.item(i).TipoActividad,
            NombreCarrera: data.rows.item(i).NombreCarrera, 
            Duracion: data.rows.item(i).Duracion,
            RecorridoGPX: data.rows.item(i).RecorridoGPX,
            FechaHora: data.rows.item(i).FechaHora
           });
        }
      }
      this.deportistasCarrera.next(dc);
    });
  }

  loadDeportistasReto(Usuario: string) {
    return this.database.executeSql('SELECT * FROM DEPORTISTA_RETO WHERE Usuario = ?', [Usuario]).then(data => {
      let dr: DeportistaReto[] = [];
 
      if (data.rows.length > 0) {
        for (var i = 0; i < data.rows.length; i++) {
 
          dr.push({ 
            Usuario: data.rows.item(i).Usuario,
            AdminDeportista: data.rows.item(i).AdminDeportista,
            NombreActividad: data.rows.item(i).NombreActividad,
            TipoActividad: data.rows.item(i).TipoActividad,
            NombreReto: data.rows.item(i).NombreReto, 
            Distancia: data.rows.item(i).Distancia, 
            Duracion: data.rows.item(i).Duracion,
            RecorridoGPX: data.rows.item(i).RecorridoGPX,
            FechaHora: data.rows.item(i).FechaHora
           });
        }
      }
      this.deportistasReto.next(dr);
    });
  }

  loadDeportistasActividadLibre(Usuario: string) {
    return this.database.executeSql('SELECT * FROM DEPORTISTA_ACTIVIDAD_LIBRE WHERE Usuario = ?', [Usuario]).then(data => {
      let dr: DeportistaActividadLibre[] = [];
 
      if (data.rows.length > 0) {
        for (var i = 0; i < data.rows.length; i++) {
 
          dr.push({ 
            Usuario: data.rows.item(i).Usuario,
            NombreActividad: data.rows.item(i).NombreActividad,
            TipoActividad: data.rows.item(i).TipoActividad,
            FechaHora: data.rows.item(i).FechaHora,
            Duracion: data.rows.item(i).Duracion,
            Distancia: data.rows.item(i).Distancia,
            RecorridoGPX: data.rows.item(i).RecorridoGPX,
           
           });
        }
      }
      this.deportistasActividadLibre.next(dr);
    });
  }

  addDeportistaCarrera(Usuario, AdminDeportista, NombreActividad, TipoActividad, NombreCarrera, Duracion, RecorridoGPX, FechaHora) {
    let data = [Usuario, AdminDeportista, NombreActividad, TipoActividad, NombreCarrera, Duracion, RecorridoGPX, FechaHora];
    return this.database.executeSql('INSERT INTO DEPORTISTA_CARRERA (Usuario, AdminDeportista, NombreActividad, TipoActividad, NombreCarrera, Duracion, RecorridoGPX, FechaHora) VALUES (?, ?, ?, ?, ?, ?, ?, ?)', data).then(data => {
      this.loadDeportistasCarrera(Usuario);
    });
  }

  addDeportistaReto(Usuario, AdminDeportista, NombreActividad, TipoActividad,  NombreReto, Distancia, Duracion, RecorridoGPX, FechaHora) {
    let data = [Usuario, AdminDeportista, NombreActividad, TipoActividad, NombreReto, Distancia, Duracion, RecorridoGPX, FechaHora];
    return this.database.executeSql('INSERT INTO DEPORTISTA_RETO (Usuario, AdminDeportista, NombreActividad, TipoActividad, NombreReto, Distancia, Duracion, RecorridoGPX, FechaHora) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)', data).then(data => {
      this.loadDeportistasReto(Usuario);
    });
  }

  addDeportistaActividadLibre(Usuario, NombreActividad, TipoActividad,FechaHora, Duracion, Distancia, RecorridoGPX) {
    let data = [Usuario, NombreActividad, TipoActividad,FechaHora, Duracion, Distancia, RecorridoGPX];
    return this.database.executeSql('INSERT INTO DEPORTISTA_ACTIVIDAD_LIBRE (Usuario, NombreActividad, TipoActividad, FechaHora, Duracion, Distancia, RecorridoGPX) VALUES (?, ?, ?, ?, ?, ?, ?)', data).then(data => {
      this.loadDeportistasActividadLibre(Usuario);
    });
  }

  deleteDeportistaCarrera(Usuario, NombreCarrera) {
    return this.database.executeSql('DELETE FROM DEPORTISTA_CARRERA WHERE Usuario = ? AND NombreCarrera = ?', [Usuario, NombreCarrera]).then(_ => {
      this.loadDeportistasCarrera(Usuario);
    });
  }

  deleteDeportistaReto(Usuario, NombreReto, NombreActividad) {
    return this.database.executeSql('DELETE FROM DEPORTISTA_RETO WHERE Usuario = ? AND NombreReto = ? AND NombreActividad = ?', [Usuario, NombreReto, NombreActividad]).then(_ => {
      this.loadDeportistasReto(Usuario);
    });
  }

  deleteDeportistaActividadLibre(Usuario, NombreActividad, FechaHora) {
    return this.database.executeSql('DELETE FROM DEPORTISTA_ACTIVIDAD_LIBRE WHERE Usuario = ? AND NombreActividad = ? AND FechaHora = ?', [Usuario, NombreActividad, FechaHora]).then(_ => {
      this.loadDeportistasActividadLibre(Usuario);
    });
  }

  
}
