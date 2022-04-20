using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperJogoDaVelha
{
    public class JogoDaVelhaLogic
    {
        // 0 = Void
        // 1 = X
        // 2 = O
        int[,] tabuleiro;

        // 0 = Jogo em andamento
        // 1 = X venceu
        // 2 = O venceu
        // 3 = Velha
        int winner;


        public int this[int i, int j]
        {
            get => tabuleiro[i, j];
            set
            {
                // Apenas permite X ou O
                if (value < 1 || value > 2)
                    return;

                // Se casa não estiver vazia, impedir alterações
                if (tabuleiro[i, j] != 0)
                    return;

                tabuleiro[i, j] = value;
                checkVelha();
            }
        }

        // GET & SET
        public int Winner
        {
            get => winner;
        }


        // Construtor
        public JogoDaVelhaLogic()
        {
            tabuleiro = new int[3,3];
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    tabuleiro[i, j] = 0;
                }
            }

            winner = 0;
        }


        // Retorna:
        // 0 = Jogo em andamento
        // 1 = X venceu
        // 2 = O venceu
        // 3 = Velha
        public int checkVelha()
        {
            // Checa as Linhas
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == tabuleiro[i, 1] && tabuleiro[i, 1] == tabuleiro[i, 2])
                    return tabuleiro[i, 0];
            }

            // Checa as Colunas
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[0, i] == tabuleiro[1, i] && tabuleiro[1, i] == tabuleiro[2, i])
                    return tabuleiro[0, i];
            }

            // Checa as Diagonais
            if (tabuleiro[0, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[2, 2] || tabuleiro[2, 0] == tabuleiro[1, 1] && tabuleiro[1, 1] == tabuleiro[0, 2])
                return tabuleiro[1, 1];

            // Checa se jogo está em andamento
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == 0)
                        return 0;
                }
            }

            // Deu velha
            return 3;
        }
    }
}
