import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SuperHeroi {
  id: number;
  nome: string;
  nomeHeroi: string;
  dataNascimento: string;
  altura: number;
  peso: number;
  poderes: string[];
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiBase = 'http://localhost:5120';

  constructor(private http: HttpClient) {}

  getSuperHerois(): Observable<SuperHeroi[]> {
    return this.http.get<SuperHeroi[]>(`${this.apiBase}/lista_superherois`);
  }

  createHeroi(heroi: any): Observable<any> {
    return this.http.post(`${this.apiBase}/adiciona_superheroi`, heroi);
  }

  updateHeroi(id: number, heroi: any): Observable<any> {
    return this.http.put(`${this.apiBase}/atualiza_heroi/${id}`, heroi);
  }

  deleteHeroi(id: number): Observable<any> {
    return this.http.delete(`${this.apiBase}/deletar_heroi/${id}`);
  }

  getHeroiPorId(id: number): Observable<any> {
    return this.http.get(`${this.apiBase}/lista_heroi/${id}`);
  }
}