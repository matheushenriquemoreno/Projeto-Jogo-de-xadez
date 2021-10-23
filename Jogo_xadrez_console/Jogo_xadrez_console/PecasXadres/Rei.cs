using tabuleiro;

namespace PecasXadres
{
    class Rei : Peca 
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { } // construtor

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca rei = tabuleiro.RetornaPeca(pos);
            return rei == null || rei.Cor != Cor;
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

            return matriz;
        }
    }
}
