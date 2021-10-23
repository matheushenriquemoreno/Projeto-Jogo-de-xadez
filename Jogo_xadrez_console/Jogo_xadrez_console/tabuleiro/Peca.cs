namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao PosicaoPeca { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro tabuleiro { get; protected set; }


        public Peca(Tabuleiro tab, Cor cor)
        {
            this.PosicaoPeca = null;
            this.Cor = cor;
            this.tabuleiro = tab;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarQtdMovimento()
        {
            QuantidadeMovimentos++;
        }

        public void DecrementarQtdMovimentos()
        {
            QuantidadeMovimentos--;
        }


        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPosiveis();
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao destino)
        {
            return MovimentosPosiveis()[destino.Linha, destino.Coluna];
        }
        

        public abstract bool[,] MovimentosPosiveis();
     
    }
}
