import { Component, OnInit } from '@angular/core';
import { Produto as ResponseProdutos} from './produto.model';
import { ProdutoService } from './produto.service'; 

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.css']
})
export class ProdutoComponent implements OnInit {

  responseProdutos : ResponseProdutos;
  constructor(private produtoService : ProdutoService) { }

  ngOnInit() {
    this.produtoService.getProdutos().subscribe(res => this.responseProdutos = res);
  }

}
