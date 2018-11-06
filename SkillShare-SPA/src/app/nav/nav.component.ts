import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService:AuthService) { }

  ngOnInit() {
  }
  login(){
   this.authService.login(this.model).subscribe(response=>{
     console.log("login successfully")
   },error=>{
     console.log("Failed to login")
   })
   
  }

  loggedIn(){
    const token=localStorage.getItem('token')
    return !!token  // if something inside token return true otherwise false
  }
  logout(){
    localStorage.removeItem('token')
    console.log("logout successfully")
  }

}
