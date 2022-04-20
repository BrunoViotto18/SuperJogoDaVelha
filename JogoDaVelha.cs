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
        // Atributos
        private PictureBox[,] pictureBoxes;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;

        private PictureBox[] lines;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        private PictureBox pictureBox12;
        private PictureBox pictureBox13;

        // Modelo lógico do tabuleiro
        private int[,] boxes;

        private int currentUser;
        private int winner;

        public int Winner
        {
            get => winner;
            set
            {
                MessageBox.Show($"Vencedor: {value}");
                winner = value;
            }
        }

        // Construtor
        public JogoDaVelha()
        {
            InitializeComponent();
        }


        // LOAD
        private void JogoDaVelha_Load(object sender, EventArgs e)
        {
            // Inicialização básica de variáveis
            pictureBoxes = new PictureBox[3, 3];
            pictureBoxes[0, 0] = this.pictureBox1;
            pictureBoxes[0, 1] = this.pictureBox2;
            pictureBoxes[0, 2] = this.pictureBox3;
            pictureBoxes[1, 0] = this.pictureBox4;
            pictureBoxes[1, 1] = this.pictureBox5;
            pictureBoxes[1, 2] = this.pictureBox6;
            pictureBoxes[2, 0] = this.pictureBox7;
            pictureBoxes[2, 1] = this.pictureBox8;
            pictureBoxes[2, 2] = this.pictureBox9;

            lines = new PictureBox[4];
            lines[0] = pictureBox10;
            lines[1] = pictureBox11;
            lines[2] = pictureBox12;
            lines[3] = pictureBox13;


            // Inicialização

            // Torna o UserControl um quadrado
            this.Height = Math.Min(this.Height, this.Width);
            this.Width = Math.Min(this.Height, this.Width);


            //
            // Setando os quadrados
            //

            // Seta a cor e tamanho dos quadrados
            for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    pictureBoxes[i, j].BackColor = Color.FromArgb(0, 0, 0, 0);
                    pictureBoxes[i, j].Height = this.Height * 3 / 10;
                    pictureBoxes[i, j].Width = this.Width * 3 / 10;
                    pictureBoxes[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                }
            }

            // Seta o posicionamento dos quadrados
            for (int i = 0; i < 3; i++)
            {
                // Alinhamento Vertical
                pictureBoxes[0, i].Top = 0;
                pictureBoxes[1, i].Top = this.Height * 7 / 20;
                pictureBoxes[2, i].Top = this.Height * 7 / 10;

                // Alinhamento Horizontal
                pictureBoxes[i, 0].Left = 0;
                pictureBoxes[i, 1].Left = this.Width * 7 / 20;
                pictureBoxes[i, 2].Left = this.Width * 7 / 10;
            }


            //
            // Setando as linhas
            //

            // Seta a cor, altura e posicionamento das linhas
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i].BackColor = Color.FromArgb(0, 0, 0);

                if (i < 2)
                {
                    lines[i].Height = this.Height;
                    lines[i].Width = this.Width / 20;
                    lines[i].Top = 0;
                    continue;
                }

                lines[i].Height = this.Height / 20;
                lines[i].Width = this.Width;
                lines[i].Left = 0;
            }

            // Finaliza o posicionamento das linhas
            lines[0].Left = this.Width * 3 / 10;
            lines[1].Left = this.Width * 13 / 20;
            lines[2].Top = this.Height * 3 / 10;
            lines[3].Top = this.Height * 13 / 20;


            //
            // Setando a lógica dos quadrados
            //

            boxes = new int[3,3];
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                for (int j = 0; j < boxes.GetLength(1); j++)
                {
                    boxes[i, j] = (int)box.Void;
                }
            }

            currentUser = (int)box.X;
            winner = (int)box.Void;
        }


        // Evento de click/jogada
        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (winner != 0)
                return;

            int[] pbXY = getPictureBoxIndex(sender);
            if (boxes[pbXY[0], pbXY[1]] == (int)box.X || boxes[pbXY[0], pbXY[1]] == (int)box.O)
                return;

            // Atualiza o modelo lógico
            boxes[pbXY[0], pbXY[1]] = currentUser;

            // Obtém o filename da imagem desejada
            string filename = $".\\X.png";
            if (currentUser == (int)box.O)
                filename = $"C:\\Users\\Aluno\\Desktop\\SuperJogoDaVelha\\O.png";

            pictureBoxes[pbXY[0], pbXY[1]].Image = new Bitmap(filename);

            checkEndGame();

            updateCurrentUser();
        }


        // Retorna o indíce de uma PictureBox na matriz
        private int[] getPictureBoxIndex(object pb)
        {
            for (int i = 0; i < pictureBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < pictureBoxes.GetLength(1); j++)
                {
                    if (pb == pictureBoxes[i, j])
                        return new int[2] { i, j };
                }
            }
            return new int[2] { -1, -1 };
        }


        // Atualiza a vez de qual usuário
        private void updateCurrentUser()
        {
            if (currentUser == (int)box.O)
            {
                currentUser = (int)box.X;
                return;
            }
            currentUser = (int)box.O;
        }


        // Checa se o jogo acabou
        private void checkEndGame()
        {
            // Checa linhas
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                if (boxes[i, 0] == boxes[i, 1] && boxes[i, 1] == boxes[i, 2] && boxes[i, 0] != (int)box.Void)
                {
                    Winner = boxes[i, 0];
                    return;
                }
            }

            // Checa colunas
            for (int i = 0; i < boxes.GetLength(0); i++)
            {
                if (boxes[0, i] == boxes[1, i] && boxes[1, i] == boxes[2, i] && boxes[0, i] != (int)box.Void)
                {
                    Winner = boxes[0, i];
                    return;
                }
            }

            // Checa diagonais
            if (boxes[0, 0] == boxes[1, 1] && boxes[1, 1] == boxes[2, 2] || boxes[2, 0] == boxes[1, 1] && boxes[1, 1] == boxes[0, 2])
            {
                if (boxes[1, 1] != (int)box.Void)
                    Winner = boxes[1, 1];
            }
        }



        // INITIALIZE
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(141, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 100);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(283, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 100);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.Location = new System.Drawing.Point(6, 142);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(100, 100);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox5.Location = new System.Drawing.Point(141, 142);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(100, 100);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox6.Location = new System.Drawing.Point(283, 142);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(100, 100);
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox7.Location = new System.Drawing.Point(6, 273);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(100, 100);
            this.pictureBox7.TabIndex = 6;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox8.Location = new System.Drawing.Point(141, 273);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(100, 100);
            this.pictureBox8.TabIndex = 7;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox9.Location = new System.Drawing.Point(283, 273);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(100, 100);
            this.pictureBox9.TabIndex = 8;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // pictureBox10
            // 
            this.pictureBox10.Location = new System.Drawing.Point(116, 3);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(19, 386);
            this.pictureBox10.TabIndex = 9;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Location = new System.Drawing.Point(247, -2);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(19, 386);
            this.pictureBox11.TabIndex = 10;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Location = new System.Drawing.Point(-24, 112);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(418, 24);
            this.pictureBox12.TabIndex = 11;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox13
            // 
            this.pictureBox13.Location = new System.Drawing.Point(-13, 243);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(418, 24);
            this.pictureBox13.TabIndex = 12;
            this.pictureBox13.TabStop = false;
            // 
            // JogoDaVelha
            // 
            this.Controls.Add(this.pictureBox13);
            this.Controls.Add(this.pictureBox12);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "JogoDaVelha";
            this.Size = new System.Drawing.Size(396, 386);
            this.Load += new System.EventHandler(this.JogoDaVelha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.ResumeLayout(false);

        }
    }

    enum box
    {
        Void,
        X,
        O
    }
}
