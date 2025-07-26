import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { UserService, SuperHeroi } from '../../services/user.service';

@Component({
  selector: 'app-tela-principal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './tela-principal.component.html',
  styleUrl: './tela-principal.component.css'
})
export class TelaPrincipalComponent implements OnInit {
  heroiBuscaId: number | null = null;
  heroiBuscado: any = null;
  mensagemBuscaHeroi: string = '';

  // ...
  heroiDeletar: any = { id: '' };
  mensagemDeletaHeroi: string = '';

  currentScreen: number = 1;
  heroes: SuperHeroi[] = [];
  novoHeroi: any = { nome: '', nomeHeroi: '', dataNascimento: '', altura: null, peso: null, poderes: [] };
  mensagemHeroi: string = '';

  heroiAtualizar: any = { id: '', nome: '', nomeHeroi: '', dataNascimento: '', altura: null, peso: null, poderes: [] };
  mensagemAtualizaHeroi: string = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadHeroes();
  }

  loadHeroes(): void {
    this.userService.getSuperHerois().subscribe({
      next: h => this.heroes = h,
      error: err => console.error(err)
    });
  }

  adicionarHeroi(): void {
    this.userService.createHeroi(this.novoHeroi).subscribe({
      next: res => {
        this.mensagemHeroi = 'Super-herói adicionado com sucesso!';
        this.novoHeroi = { nome: '', nomeHeroi: '', dataNascimento: '', altura: null, peso: null, poderes: [] };
        this.novoPoder = '';
        this.loadHeroes();
      },
      error: err => {
        if (err.status === 409) {
          this.mensagemHeroi = 'Herói já existe no banco de dados.';
        } else {
          this.mensagemHeroi = 'Erro ao adicionar herói.';
        }
      }
    });
  }

  atualizarHeroi(): void {
    if (!this.heroiAtualizar.id) {
      this.mensagemAtualizaHeroi = 'ID é obrigatório para atualizar.';
      return;
    }
    this.userService.updateHeroi(this.heroiAtualizar.id, this.heroiAtualizar).subscribe({
      next: res => {
        this.mensagemAtualizaHeroi = 'Super-herói atualizado com sucesso!';
        this.heroiAtualizar = { id: '', nome: '', nomeHeroi: '', dataNascimento: '', altura: null, peso: null, poderes: [] };
        this.atualizaPoder = '';
        this.loadHeroes();
      },
      error: err => {
        if (err.status === 409) {
          this.mensagemAtualizaHeroi = 'Herói não existe no banco de dados.';
        } else {
          this.mensagemAtualizaHeroi = 'Erro ao atualizar herói.';
        }
      }
    });
  }

  deletarHeroi(): void {
    if (!this.heroiDeletar.id) {
      this.mensagemDeletaHeroi = 'ID é obrigatório para deletar.';
      return;
    }
    this.userService.deleteHeroi(this.heroiDeletar.id).subscribe({
      next: () => {
        this.mensagemDeletaHeroi = 'Super-herói deletado com sucesso!';
        this.heroiDeletar = { id: '' };
        this.loadHeroes();
      },
      error: err => {
        if (err.status === 404) {
          this.mensagemDeletaHeroi = 'Herói não encontrado.';
        } else {
          this.mensagemDeletaHeroi = 'Erro ao deletar herói.';
        }
      }
    });
  }



  buscarHeroiPorId(): void {
    this.mensagemBuscaHeroi = '';
    this.heroiBuscado = null;
    if (!this.heroiBuscaId) {
      this.mensagemBuscaHeroi = 'Informe um ID válido.';
      return;
    }
    this.userService.getHeroiPorId(this.heroiBuscaId).subscribe({
      next: (heroi) => {
        this.heroiBuscado = heroi;
        this.mensagemBuscaHeroi = '';
      },
      error: (err) => {
        if (err.status === 404) {
          this.mensagemBuscaHeroi = 'Herói não encontrado.';
        } else {
          this.mensagemBuscaHeroi = 'Erro ao buscar herói.';
        }
      }
    });
  }

  novoPoder: string = '';
  addPoderNovo() {
    if (this.novoPoder && !this.novoHeroi.poderes.includes(this.novoPoder)) {
      this.novoHeroi.poderes.push(this.novoPoder);
      this.novoPoder = '';
    }
  }
  removePoderNovo(poder: string) {
    this.novoHeroi.poderes = this.novoHeroi.poderes.filter((p: string) => p !== poder);
  }

  atualizaPoder: string = '';
  addPoderAtualiza() {
    if (this.atualizaPoder && !this.heroiAtualizar.poderes.includes(this.atualizaPoder)) {
      this.heroiAtualizar.poderes.push(this.atualizaPoder);
      this.atualizaPoder = '';
    }
  }
  removePoderAtualiza(poder: string) {
    this.heroiAtualizar.poderes = this.heroiAtualizar.poderes.filter((p: string) => p !== poder);
  }

  showScreen(screenNumber: number) {
    this.currentScreen = screenNumber;
  }
}

