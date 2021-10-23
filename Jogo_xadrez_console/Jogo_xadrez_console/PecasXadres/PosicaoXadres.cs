using System;
using tabuleiro;
namespace PecasXadres
{
    class PosicaoXadres
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadres(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ConvertePosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a'); 
        }


        public override string ToString()
        {
            return "" + Coluna + Linha;
        }



    }
}
