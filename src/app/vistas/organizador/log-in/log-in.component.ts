import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute} from '@angular/router';
import { LogIn } from 'src/app/modelos/log-in';
import { LogInService} from 'src/app/services/log-in.service';


@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {
invalid = false;
constructor( private router: Router,private _logInService: LogInService) { }

  ngOnInit(): void {
  }

ingresar(usuario, contrasena): void{
this.invalid = false;
this.router.navigate(['/organizador']);
this._logInService.setUsuario(usuario);


  }

}
