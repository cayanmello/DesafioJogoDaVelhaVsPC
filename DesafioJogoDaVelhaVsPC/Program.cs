using System;

class DesafioJogoDaVelhaVsPC
{
    static char[] tabuleiro = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static int jogadorAtual = 1;
    static int escolhaDoUsuario;
    static int verificarJogo;
    static Random random = new Random();

    private static void Main(string[] args)
    {
        bool jogarNovamente = true;

        while (jogarNovamente)
        {
            jogadorAtual = 1;
            tabuleiro = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            if (jogadorAtual % 2 == 0)
            {
                EscolherMovimentoComputador();
                jogadorAtual++;
            }

            do
            {
                Console.Clear();
                Console.WriteLine("\r\n_________ _______  _______  _______    ______   _______             _______  _                 _______ \r\n\\__    _/(  ___  )(  ____ \\(  ___  )  (  __  \\ (  ___  )  |\\     /|(  ____ \\( \\      |\\     /|(  ___  )\r\n   )  (  | (   ) || (    \\/| (   ) |  | (  \\  )| (   ) |  | )   ( || (    \\/| (      | )   ( || (   ) |\r\n   |  |  | |   | || |      | |   | |  | |   ) || (___) |  | |   | || (__    | |      | (___) || (___) |\r\n   |  |  | |   | || | ____ | |   | |  | |   | ||  ___  |  ( (   ) )|  __)   | |      |  ___  ||  ___  |\r\n   |  |  | |   | || | \\_  )| |   | |  | |   ) || (   ) |   \\ \\_/ / | (      | |      | (   ) || (   ) |\r\n|\\_)  )  | (___) || (___) || (___) |  | (__/  )| )   ( |    \\   /  | (____/\\| (____/\\| )   ( || )   ( |\r\n(____/   (_______)(_______)(_______)  (______/ |/     \\|     \\_/   (_______/(_______/|/     \\||/     \\|\r\n                                                                                                       \r\n");
                Console.WriteLine("Player: X e Computador: O");
                Console.WriteLine("\n");

                if (jogadorAtual % 2 == 0)
                {
                    EscolherMovimentoComputador();
                }
                else
                {
                    Tabuleiro();

                    bool escolhaValida = false;
                    while (!escolhaValida)
                    {
                        bool entradaValida = int.TryParse(Console.ReadLine(), out escolhaDoUsuario);
                        if (entradaValida && escolhaDoUsuario >= 1 && escolhaDoUsuario <= 9 && tabuleiro[escolhaDoUsuario - 1] != 'X' && tabuleiro[escolhaDoUsuario - 1] != 'O')
                        {
                            escolhaValida = true;
                        }
                        else
                        {
                            Console.WriteLine("Jogada inválida. Por favor, escolha uma posição válida.");
                        }
                    }

                    tabuleiro[escolhaDoUsuario - 1] = 'X';
                }

                verificarJogo = VerificarVencedor();
                jogadorAtual++;
            }

            while (verificarJogo != 1 && verificarJogo != -1);

            Console.Clear();
            Tabuleiro();

            if (verificarJogo == 1)
            {
                Console.WriteLine(jogadorAtual % 2 == 0 ? "Você venceu!" : "Computador venceu!");
            }
            else
            {
                Console.WriteLine("Empate!");
            }

            Console.WriteLine("Deseja jogar novamente? (s/n)");
            char resposta = Console.ReadKey().KeyChar;
            if (resposta != 's' && resposta != 'S')
            {
                jogarNovamente = false;
            }
        }

        Console.WriteLine("Obrigado por jogar!");
    }

    private static void Tabuleiro()
    {
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", tabuleiro[0], tabuleiro[1], tabuleiro[2]);
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", tabuleiro[3], tabuleiro[4], tabuleiro[5]);
        Console.WriteLine("_____|_____|_____ ");
        Console.WriteLine("     |     |      ");
        Console.WriteLine("  {0}  |  {1}  |  {2}", tabuleiro[6], tabuleiro[7], tabuleiro[8]);
        Console.WriteLine("     |     |      ");
    }

    private static void EscolherMovimentoComputador()
    {
        int melhorMovimento = -1;
        int melhorPontuacao = -2;

        for (int i = 0; i < 9; i++)
        {
            if (tabuleiro[i] != 'X' && tabuleiro[i] != 'O')
            {
                tabuleiro[i] = 'O';
                int pontuacao = -Minimax(tabuleiro, -1);
                tabuleiro[i] = (char)(i + '1');

                if (pontuacao > melhorPontuacao)
                {
                    melhorPontuacao = pontuacao;
                    melhorMovimento = i;
                }
            }
        }

        tabuleiro[melhorMovimento] = 'O';
    }

    private static int Minimax(char[] novoTabuleiro, int jogador)
    {
        int pontuacao = VerificarVencedor();
        if (pontuacao != 0)
        {
            return pontuacao * jogador;
        }

        int melhorPontuacao = jogador == 1 ? -1 : 1;

        for (int i = 0; i < 9; i++)
        {
            if (novoTabuleiro[i] != 'X' && novoTabuleiro[i] != 'O')
            {
                novoTabuleiro[i] = jogador == 1 ? 'O' : 'X';
                int pontuacaoAtual = -Minimax(novoTabuleiro, -jogador);
                novoTabuleiro[i] = (char)(i + '1');

                if ((jogador == 1 && pontuacaoAtual > melhorPontuacao) || (jogador == -1 && pontuacaoAtual < melhorPontuacao))
                {
                    melhorPontuacao = pontuacaoAtual;
                }
            }
        }

        return melhorPontuacao;
    }

    private static int VerificarVencedor()
    {
        if (tabuleiro[0] == tabuleiro[1] && tabuleiro[1] == tabuleiro[2])
        {
            return 1;
        }
        else if (tabuleiro[3] == tabuleiro[4] && tabuleiro[4] == tabuleiro[5])
        {
            return 1;
        }
        else if (tabuleiro[6] == tabuleiro[7] && tabuleiro[7] == tabuleiro[8])
        {
            return 1;
        }
        else if (tabuleiro[0] == tabuleiro[3] && tabuleiro[3] == tabuleiro[6])
        {
            return 1;
        }
        else if (tabuleiro[1] == tabuleiro[4] && tabuleiro[4] == tabuleiro[7])
        {
            return 1;
        }
        else if (tabuleiro[2] == tabuleiro[5] && tabuleiro[5] == tabuleiro[8])
        {
            return 1;
        }
        else if (tabuleiro[0] == tabuleiro[4] && tabuleiro[4] == tabuleiro[8])
        {
            return 1;
        }
        else if (tabuleiro[2] == tabuleiro[4] && tabuleiro[4] == tabuleiro[6])
        {
            return 1;
        }
        else if (tabuleiro[0] != '1' && tabuleiro[1] != '2' && tabuleiro[2] != '3' && tabuleiro[3] != '4' &&
            tabuleiro[4] != '5' && tabuleiro[5] != '6' && tabuleiro[6] != '7' && tabuleiro[7] != '8' &&
            tabuleiro[8] != '9')
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}