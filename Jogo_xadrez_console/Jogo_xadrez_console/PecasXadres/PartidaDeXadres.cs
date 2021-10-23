using System;
using System.Collections.Generic;
using tabuleiro;

namespace PecasXadres
{
    class PartidaDeXadres
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; set; }
        public Cor JogadorAtual { get; set; }
        public bool Terminada { get; set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; set; }
        public Peca vulneravelEnPassant { get;  private set;}

        
        public PartidaDeXadres()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            Xeque = false;
            colocarPecas();
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> auxiliar = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.Cor == cor)
                {
                    auxiliar.Add(x);
                }
            }
            return auxiliar;
        }

        public HashSet<Peca> PecaEmJogo(Cor cor)
        {
            HashSet<Peca> auxiliar = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    auxiliar.Add(x);
                }
            }
            auxiliar.ExceptWith(PecasCapturadas(cor)); 
            return auxiliar;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in PecaEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        public bool EstaEmXeque(Cor cor)
        {

            Peca R = rei(cor);

            if(R == null)
            {
                throw new TabuleiroException("não tem rei da cor " + cor + " no Tabuleiro!");
            }

            foreach(Peca x in PecaEmJogo(Adversaria(cor)))
            {
                bool[,] matriz = x.MovimentosPosiveis();
                if (matriz[R.PosicaoPeca.Linha, R.PosicaoPeca.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca x in PecaEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPosiveis();

                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for(int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(x.PosicaoPeca, new Posicao(i, j));
                            bool testeXeque = EstaEmXeque(cor);
                            desfazMovimento(x.PosicaoPeca, destino, pecaCapturada);
                            
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQtdMovimento();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // jogada especial roque pequeno

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tabuleiro.RetirarPeca(origemTorre);
                T.IncrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, destinoTorre);
            }

            // jogada especial roque grande

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tabuleiro.RetirarPeca(origemTorre);
                T.IncrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, destinoTorre);
            }

            // jogada especia en passant

            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao pegaPeao;
                    if (p.Cor == Cor.Branca)
                    {
                        pegaPeao = new Posicao(destino.Linha + 1, destino.Coluna);

                    }
                    else
                    {
                        pegaPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    pecaCapturada = Tabuleiro.RetirarPeca(pegaPeao);
                    capturadas.Add(pecaCapturada);
                }
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (capturada != null)
            {
                Tabuleiro.ColocarPeca(capturada, destino);
                capturadas.Remove(capturada);
            }
            Tabuleiro.ColocarPeca(p, origem);

            // desfazendo jogada especial roque pequeno

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca T = Tabuleiro.RetirarPeca(destinoTorre);
                T.DecrementarQtdMovimentos();
                Tabuleiro.ColocarPeca(T, origemTorre);
            }

            // desfazendo jogada especial roque grande

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca T = Tabuleiro.RetirarPeca(destinoTorre);
                T.IncrementarQtdMovimento();
                Tabuleiro.ColocarPeca(T, origemTorre);
            }

            // desfazendo jogada el passant

            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && capturada == vulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posicaopeao;
                    if(p.Cor == Cor.Branca)
                    {
                        posicaopeao = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posicaopeao = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.ColocarPeca(peao, posicaopeao);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {

           Peca pecacapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                desfazMovimento(origem, destino, pecacapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Tabuleiro.RetornaPeca(destino);

            // jogada especial promocao
            if(p is Peao)
            {
                if((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tabuleiro.RetirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(Tabuleiro, p.Cor);
                    Tabuleiro.ColocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // jogada especial En Passant

            if(p is Peao && (destino.Linha == origem.Linha -2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }

        }

        public void ValidarPosicaoDeOrigem(Posicao origem)
        {
            if(Tabuleiro.RetornaPeca(origem) == null)
            {
                throw new TabuleiroException("não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tabuleiro.RetornaPeca(origem).Cor)
            {
                throw new TabuleiroException("a peça de origm escolhida não e sua!"); 
            }
            if (!Tabuleiro.RetornaPeca(origem).existeMovimentosPossiveis()) 
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidadarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.RetornaPeca(origem).MovimentoPossivel(destino)) 
            {
                throw new TabuleiroException("Posição de destino inválida!!");
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadres(coluna, linha).ConvertePosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }

    }
}