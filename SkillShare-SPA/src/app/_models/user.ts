import { Photo } from "./photo";

export interface User {
    id:number;
    username:string
    yearsOfExperience:number
    skill:string
    created:Date
    lastActive:Date
    photoUrl:string
    city:string
    country:string
    specialization?:string
    introduction?:string
    photos?:Photo[]


}
