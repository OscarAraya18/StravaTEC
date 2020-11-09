import { Injectable } from '@angular/core';
import { SQLitePorter } from '@ionic-native/sqlite-porter/ngx';
import { HttpClient } from '@angular/common/http';
import { SQLite, SQLiteObject } from '@ionic-native/sqlite/ngx';
import { BehaviorSubject, Observable } from 'rxjs';
import { Platform } from '@ionic/angular';

export interface DeportistaCarrera{
  Usuario: string,
  NombreActividad: string,
  NombreCarrera: string,
  Duracion: string
}

export interface DeportistaReto{
  Usuario: string,
  NombreActividad: string,
  NombreReto: string,
  Distancia: number,
  Duracion: string,
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

   constructor(private plt: Platform, private sqlitePorter: SQLitePorter, private sqlite: SQLite, private http: HttpClient) {
    this.plt.ready().then(() => {
      this.sqlite.create({
        name: 'straviaMobileFinal.db',
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

  loadDeportistasCarrera(Usuario: string) {
    return this.database.executeSql('SELECT * FROM DEPORTISTA_CARRERA WHERE Usuario = ?', [Usuario]).then(data => {
      let dc: DeportistaCarrera[] = [];
 
      if (data.rows.length > 0) {
        for (var i = 0; i < data.rows.length; i++) {
 
          dc.push({ 
            Usuario: data.rows.item(i).Usuario,
            NombreActividad: data.rows.item(i).NombreActividad,
            NombreCarrera: data.rows.item(i).NombreCarrera, 
            Duracion: data.rows.item(i).Duracion
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
            NombreActividad: data.rows.item(i).NombreActividad,
            NombreReto: data.rows.item(i).NombreReto, 
            Distancia: data.rows.item(i).Distancia, 
            Duracion: data.rows.item(i).Duracion,
            RecorridoGPX: data.rows.item(i).RecorridoGPX
           });
        }
      }
      this.deportistasReto.next(dr);
    });
  }

  addDeportistaCarrera(Usuario, NombreActividad, NombreCarrera, Duracion) {
    let data = [Usuario, NombreActividad, NombreCarrera, Duracion];
    return this.database.executeSql('INSERT INTO DEPORTISTA_CARRERA (Usuario, NombreActividad, NombreCarrera, Duracion) VALUES (?, ?, ?, ?)', data).then(data => {
      this.loadDeportistasCarrera(Usuario);
    });
  }

  addDeportistaReto(Usuario, NombreActividad, NombreReto, Distancia, Duracion, RecorridoGPX) {
    let data = [Usuario, NombreActividad, NombreReto, Distancia, Duracion, RecorridoGPX];
    return this.database.executeSql('INSERT INTO DEPORTISTA_RETO (Usuario, NombreActividad, NombreReto, Distancia, Duracion, RecorridoGPX) VALUES (?, ?, ?, ?, ?, ?)', data).then(data => {
      this.loadDeportistasReto(Usuario);
    });
  }

  deleteDeportistaCarrera(Usuario, NombreCarrera) {
    return this.database.executeSql('DELETE FROM DEPORTISTA_CARRERA WHERE Usuario = ? AND NombreCarrera = ?', [Usuario, NombreCarrera]).then(_ => {
      this.loadDeportistasCarrera(Usuario);
    });
  }

  deleteDeportistaReto(Usuario, NombreReto) {
    return this.database.executeSql('DELETE FROM DEPORTISTA_RETO WHERE Usuario = ? AND NombreReto = ?', [Usuario, NombreReto]).then(_ => {
      this.loadDeportistasReto(Usuario);
    });
  }

  
}
