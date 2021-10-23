using System;
using tabuleiro;
using PecasXadres;

namespace Jogo_xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadres partida = new PartidaDeXadres();

                while (!partida.Terminada)
                {
                    try
                    {

                        Console.Clear();
                        TelaTabuleiro.imprimirPartida(partida);

                        Console.Write("\nOrigem: ");
                        Posicao origem = TelaTabuleiro.LerPosicaoXadres().ConvertePosicao();

                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tabuleiro.RetornaPeca(origem).MovimentosPosiveis();

                        Console.Clear();

                        TelaTabuleiro.imprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.Write("Destino: ");
                        Posicao destino = TelaTabuleiro.LerPosicaoXadres().ConvertePosicao();

                        partida.ValidadarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
