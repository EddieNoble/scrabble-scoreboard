import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Player } from "../models/player";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  private readonly http = inject(HttpClient);

  private readonly endpoint =
    'https://localhost:7110/api/players';

  public getPlayers(): Observable<Player[]> {

    return this.http.get<Player[]>(this.endpoint);

  }
  
  public getPlayer(id: string): Observable<Player> {

    return this.http.get<Player>(
      `${this.endpoint}/${id}`
    );
  }
}