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
                        TelaTabuleiro.imprimirPartida(partida); // cria partida

                        Console.Write("\n Origem: ");
                        Posicao origem = TelaTabuleiro.LerPosicaoXadres().ConvertePosicao(); // pega posicao de partida da peca

                        partida.ValidarPosicaoDeOrigem(origem); 

                        bool[,] posicoesPossiveis = partida.Tabuleiro.RetornaPeca(origem).MovimentosPosiveis(); // matriz que vai mostrar as possicoes possives por peca e jogada

                        Console.Clear();

                        TelaTabuleiro.imprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                        Console.Write("\n Destino: ");
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
