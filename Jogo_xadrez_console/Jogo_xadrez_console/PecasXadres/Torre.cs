using tabuleiro;

namespace PecasXadres
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { } // construtor
        public override string ToString()
        {
            return "T";
        }
        private bool podeMover(Posicao pos)
        {
            Peca torre = tabuleiro.RetornaPeca(pos);
            return torre == null || torre.Cor != Cor;
        }

        public override bool[,] MovimentosPosiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            while(tabuleiro.PosicaoValida(pos) && podeMover(pos))  // verifica as casas que pode mover.
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if(tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor) // verifica se existe peca e se essa peca tem a cor adversaria
                {
                    break;
                }

                pos.Linha = pos.Linha - 1;
               
            }
            // abaixo
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))  
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }

                pos.Linha = pos.Linha + 1;
            }

            // direita
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))  
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor) 
                {
                    break;
                }

                pos.Coluna = pos.Coluna + 1;

            }
            // esquerda
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))  
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor) 
                {
                    break;
                }

                pos.Coluna = pos.Coluna - 1;
            }
            return matriz;
        }


    }
}