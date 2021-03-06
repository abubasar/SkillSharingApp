import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private alertify:AlertifyService,
    public authService:AuthService,
    private router:Router ) { }

  ngOnInit() {
  }
  login(){
   this.authService.login(this.model).subscribe(response=>{
     this.alertify.success ("Logged in successfully")
   },error=>{
     this.alertify.error(error)
   },()=>{
     this.router.navigate(['/members'])
   })
   
  }

  loggedIn(){
    //const token=localStorage.getItem('token')
    //return !!token 
   return this.authService.loggedIn(); 
  }
  logout(){
    localStorage.removeItem('token')
    this.alertify.message("Logged out successfully")
     this.router.navigate(['/home'])
  }

}
