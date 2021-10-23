using System;
using System.Collections.Generic;
using tabuleiro;
using PecasXadres;

namespace Jogo_xadrez_console
{
    class TelaTabuleiro
    {
        public static void imprimirPartida(PartidaDeXadres partida)
        {
            imprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
            if (partida.Xeque)
            {
                Console.WriteLine("Xeque");
            }
           
        }

        public static void imprimirPecasCapturadas(PartidaDeXadres partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            imprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");

        }

        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            Console.WriteLine("        TABULEIRO    ");
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {

                    ImprimirPeca(tab.RetornaPeca(i, j));

                }

                Console.WriteLine();
            }
            Console.WriteLine("\n   A  B  C  D  E  F  G  H ");

        }
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] possisoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {

                    if (possisoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    ImprimirPeca(tab.RetornaPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }
            Console.WriteLine("\n   A  B  C  D  E  F  G  H ");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadres LerPosicaoXadres()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadres(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write(" - ");
            }
            else
            {

                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(" " + peca);
                }
                else
                { // colocando a cor DarkCyan nas pecas de um dos jogadores
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(" " + peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");

            }

        }
    }
}
