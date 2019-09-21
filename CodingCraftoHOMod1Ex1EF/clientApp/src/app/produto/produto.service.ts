import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Produto as ResponseProdutos} from './produto.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  private url = "http://localhost:59314/";
  constructor(private http: HttpClient) { }

  getProdutos() : Observable<ResponseProdutos>{
      return this.http.get<ResponseProdutos>(this.url);
        
  }
}
