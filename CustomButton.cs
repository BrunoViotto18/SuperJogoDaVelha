using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperJogoDaVelha
{
    [Browsable(true)]
    [DisplayName("Custom Button")]
    public partial class CustomButton : UserControl
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        private void CustomButton_Load(object sender, EventArgs e)
        {
            label1.Top = this.Height / 2 - label1.Height / 2;
            label1.Left = this.Width / 2 - label1.Width / 2;

            label1.BackColor = Program.colors[4];
        }
    }
}
