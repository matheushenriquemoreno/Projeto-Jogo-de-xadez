using tabuleiro;

namespace PecasXadres
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tabuleiro.RetornaPeca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPosiveis()
        {
            bool[,] mat = new bool[tabuleiro.Linhas, tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 2);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            pos.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 2);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 2);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna + 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna - 1);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            pos.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 2);
            if (tabuleiro.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;




        }
    }
}
