import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Player } from "../models/player";
import { CreatePlayerRequest } from "../models/create-player-request";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  private readonly http = inject(HttpClient);

  private readonly endpoint =
    'https://localhost:7110/api/players';

  private readonly createFromDtoEndpoint =
    `${this.endpoint}/create-from-dto`;

  public getPlayers(): Observable<Player[]> {

    return this.http.get<Player[]>(this.endpoint);

  }
  
  public getPlayer(id: string): Observable<Player> {

    return this.http.get<Player>(
      `${this.endpoint}/${id}`
    );
  }

  public addPlayer( player: CreatePlayerRequest): Observable<Player> {
    return this.http.post<Player>(
        this.createFromDtoEndpoint,
        player);
      }
}