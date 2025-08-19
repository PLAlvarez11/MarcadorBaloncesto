export const environment = {
  apiUrl: 'http://localhost:5000/api' // Ajusta el puerto según tu backend
};

// services/partido.service.ts
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class PartidoService {
  constructor(private http: HttpClient) {}

  getPartidos() {
    return this.http.get<Partido[]>(`${environment.apiUrl}/partidos`);
  }
}