import { Component, OnInit } from '@angular/core';
import { CarreraService} from 'src/app/services/carrera.service';
import { Carrera } from 'src/app/modelos/carrera';
import { ReporteService} from 'src/app/services/reporte.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.component.html',
  styleUrls: ['./reportes.component.css']
})
export class ReportesComponent implements OnInit {

 carreras: Carrera[];

formVisibility: boolean;
form2Visibility: boolean;
elimina: boolean;
pdf: string;
pdf2: string;

  constructor(private _CarreraService: CarreraService,
    private downloadService: ReporteService) { }

  ngOnInit(): void {
    this.formVisibility = true;
  	 this._CarreraService.getCarreras().subscribe(data => {this.carreras = data; console.log(data);} );



}





 actualiza(nombre){
   this.formVisibility = false;
  this.form2Visibility = true;

     this.downloadService.getParticipantes(nombre).subscribe(data => {this.pdf = data; 

      const byteCharacters = atob(this.pdf);
      const byteNumbers = new Array(byteCharacters.length);
for (let i = 0; i < byteCharacters.length; i++) {
    byteNumbers[i] = byteCharacters.charCodeAt(i);
}
const byteArray = new Uint8Array(byteNumbers);
        let blob = new Blob([byteArray], { type: "application/pdf"});
    //change download.pdf to the name of whatever you want your file to be
    saveAs(blob, "Participantes"+nombre+".pdf");
this.pdf = 'data:application/pdf;base64,' + this.pdf;
        console.log(data);




      });

     

 


}





eliminar(nombre){
   this.formVisibility = false;
  this.elimina = true;
 
this.downloadService.getPosiciones(nombre).subscribe(data => {this.pdf2 = data; 
    const byteCharacters = atob(this.pdf2);
      const byteNumbers = new Array(byteCharacters.length);
for (let i = 0; i < byteCharacters.length; i++) {
    byteNumbers[i] = byteCharacters.charCodeAt(i);
}
const byteArray = new Uint8Array(byteNumbers);
        let blob = new Blob([byteArray], { type: "application/pdf"});
    //change download.pdf to the name of whatever you want your file to be
    saveAs(blob, "Posiciones"+nombre+".pdf");

this.pdf2 = 'data:application/pdf;base64,' + this.pdf2;
        console.log(data)});


}

  dostuff(b64Data){
    const byteCharacters = atob(b64Data);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    //const blob = new Blob([byteArray], {type: 'application/pdf'});
    //console.log(blob)
    return byteArray
  }
  Volver(){
    this.formVisibility = true;
    this.form2Visibility = false;
    this.elimina = false;
  }
}


