namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }

        private Peca[,] pecas;

        public Tabuleiro (int linha, int colunas)
        {
            Linhas = linha;
            Colunas = colunas;
            pecas = new Peca[Linhas, Colunas];
        }

        public Peca RetornaPeca(int linha, int coluna) 
        {
            return pecas[linha, coluna];
        }

        public Peca RetornaPeca(Posicao pos)
        {
            return pecas[pos.Linha, pos.Coluna];
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return RetornaPeca(pos) != null;
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }

            pecas[pos.Linha, pos.Coluna] = p;
            p.PosicaoPeca = pos;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if(RetornaPeca(pos) == null)
            {
                return null;
            }
            Peca auxiliar = RetornaPeca(pos);
            auxiliar.PosicaoPeca = null;
            pecas[pos.Linha, pos.Coluna] = null; // marcando a posicao da peca no tabuleiro como null
            return auxiliar;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if(pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

    }
}
