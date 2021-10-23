using tabuleiro;

namespace PecasXadres
{
    class Dama : Peca
    {

        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        public override string ToString()
        {
            return "D";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.RetornaPeca(pos);
            return p == null || p.Cor != this.Cor;
        }

        public override bool[,] MovimentosPosiveis()
        {
            bool[,] mat = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            // esquerda
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha, pos.Coluna - 1);
            }

            // direita
            pos.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha, pos.Coluna + 1);
            }

            // acima
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna);
            }

            // abaixo
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna);
            }

            // NO
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            // NE
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna + 1);
            }

            // SE
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }

            // SO
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            while (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (tabuleiro.RetornaPeca(pos) != null && tabuleiro.RetornaPeca(pos).Cor != this.Cor)
                {
                    break;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }

            return mat;
        }
    }
}
