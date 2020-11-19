import { Component, OnInit } from '@angular/core';
import { LogInService} from 'src/app/services/log-in.service';
import { Grupo } from 'src/app/modelos/grupo';
import { GrupoService} from 'src/app/services/grupo.service';

@Component({
  selector: 'app-grupos',
  templateUrl: './grupos.component.html',
  styleUrls: ['./grupos.component.css']
})
export class GruposComponent implements OnInit {
grupos: Grupo[];
formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;
grupo = new Grupo();

  constructor(private _logInService: LogInService,
    private _GrupoService: GrupoService) { }

  ngOnInit(): void {
    this.formVisibility = false;
        this._GrupoService.getMisGrupos().subscribe(data => this.grupos = data );
  }


agregar(){
  this.formVisibility = true;
  this.grupo = new Grupo();

}

submit(nombre){
this.grupo = new Grupo();
this.grupo.nombre = nombre;
this.grupo.admindeportista = this._logInService.getUsuario();
console.log(this.grupo);
this.grupos.push(this.grupo);
 this.formVisibility = false;
 this._GrupoService.nuevoGrupo(this.grupo).subscribe(data => {});

  this._GrupoService.getMisGrupos().subscribe(data => this.grupos = data );
  for(let i = 0 ; i < this.grupos.length; i++) {
      if(this.grupos[i].nombre === nombre){
        this.grupo = this.grupos[i];
      }

}
}



 actualiza(grupo){
  this.grupo = grupo;
  this.form2Visibility = true;
}

modifica(nombre){
  console.log("Modifica");
this.grupo.nombre = nombre;
this.grupo.admindeportista = this._logInService.getUsuario();
console.log(this.grupo);
for(let i = 0 ; i < this.grupos.length; i++) {
      if(this.grupos[i].nombre === nombre){
        this.grupos[i] = this.grupo;
      }
 this.form2Visibility = false;
 this._GrupoService.actualizaGrupo(this.grupo).subscribe(data => {} );

}
}


eliminar(id){
    const confirmed = window.confirm('¿Seguro que desea eliminar este grupo?');
if (confirmed) {
this.elimina = false;

this._GrupoService.borraGrupo(id).subscribe(data => {} );
this.grupos = this.grupos.filter((i) => i !== id); // filtramos



}
}
}

