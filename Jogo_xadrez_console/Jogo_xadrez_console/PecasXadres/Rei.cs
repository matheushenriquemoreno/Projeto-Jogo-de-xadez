using tabuleiro;

namespace PecasXadres
{
    class Rei : Peca
    {
        private PartidaDeXadres _partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadres partida) : base(tab, cor)
        {
            this._partida = partida;
        } // construtor

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca rei = tabuleiro.RetornaPeca(pos);
            return rei == null || rei.Cor != Cor;
        }
        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = tabuleiro.RetornaPeca(pos);
            return p != null && p is Torre && p.Cor == this.Cor && p.QuantidadeMovimentos == 0; // p is Torre = a peça 'p' e uma instacia/subclasse de Torre
        }


        public override bool[,] MovimentosPosiveis()
        {
            bool[,] matriz = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            /* verificação de posiveis jogadas do rei */

            // acima
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // nordeste
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // direita
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // sudeste
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // abaixo
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // sudoeste
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // Esquerda
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // Noroeste
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;
            }

            // Jogada especial Roque

            if (QuantidadeMovimentos == 0 && !_partida.Xeque)
            {
                // jogada espcial roque pequeno
                Posicao posicaoTorre = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 3);
                if (TesteTorreParaRoque(posicaoTorre))
                {
                    Posicao p1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
                    Posicao p2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 2);
                    if (tabuleiro.RetornaPeca(p1) == null && tabuleiro.RetornaPeca(p2) == null)
                    {
                        matriz[PosicaoPeca.Linha, PosicaoPeca.Coluna + 2] = true;
                    }
                }
            }

            // jogada especial roque grande

            Posicao posicaoTorre2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 4);
            if (TesteTorreParaRoque(posicaoTorre2))
            {
                Posicao p1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
                Posicao p2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 2);
                Posicao p3 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 3);

                if (tabuleiro.RetornaPeca(p1) == null && tabuleiro.RetornaPeca(p2) == null && tabuleiro.RetornaPeca(p3) == null)
                {
                    matriz[PosicaoPeca.Linha, PosicaoPeca.Coluna - 2] = true;
                }
            }


            return matriz;
        }
    }
}
