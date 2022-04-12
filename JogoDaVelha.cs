using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SuperJogoDaVelha
{
    [Browsable(true)]
    [DisplayName("Jogo da Velha")]
    public partial class JogoDaVelha : UserControl
    {
        // Propriedades
        private int[,] tabuleiro = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        private PictureBox[,] tabuleiroBoxes =
        {
            { new PictureBox(), new PictureBox(), new PictureBox() },
            { new PictureBox(), new PictureBox(), new PictureBox() },
            { new PictureBox(), new PictureBox(), new PictureBox() }
        };


        // GET & SET
        public int Dimension
        {
            get => this.Width;
            set
            {
                if (value < 10)
                    throw new Exception("Jogo da Velha deve ter dimensão maior que 3 pixels.");
                this.Width = value;
                this.Height = value;
            }
        }

        public int[,] getTabuleiro()
        {
            int[,] newTabuleiro = new int[3,3];
            tabuleiro.CopyTo(newTabuleiro, 0);
            return newTabuleiro;
        }
        public void setTabuleiro(int jogada, int player)
        {
            if (jogada < 0 || jogada > 9)
                throw new Exception("Tentativa de jogáda inválida");
            tabuleiro[(int)Math.Floor((double)jogada / 3), jogada % 3] = player;
        }



        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // JogoDaVelha
            // 
            this.BackColor = Color.Magenta;
            this.Name = "JogoDaVelha";
            this.Size = new System.Drawing.Size(578, 525);
            this.Resize += new System.EventHandler(this.JogoDaVelha_Resize);
            this.ResumeLayout(false);
        }



        private void JogoDaVelha_Resize(object sender, EventArgs e)
        {
            /*
            this.Height = Math.Min(this.Height, this.Width);
            this.Width = Math.Min(this.Height, this.Width);
            */

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tabuleiroBoxes[i, j].BackColor = Color.FromArgb(255, 255, 255);

                    // Alinhamento Vertical
                    tabuleiroBoxes[0, j].Top = this.Top;

                    tabuleiroBoxes[1, j].Top = this.Top + this.Height / 2 - tabuleiroBoxes[i, j].Height / 2;

                    tabuleiroBoxes[2, j].Top = this.Bottom - tabuleiroBoxes[i, j].Height;

                    // Alinhamento Horizontal
                    tabuleiroBoxes[i, 0].Left = this.Left;

                    tabuleiroBoxes[i, 1].Left = this.Left + this.Width / 2 - tabuleiroBoxes[i, j].Width / 2;

                    tabuleiroBoxes[i, 2].Left = this.Right - tabuleiroBoxes[i, j].Width;
                }
            }
        }
    }
}
