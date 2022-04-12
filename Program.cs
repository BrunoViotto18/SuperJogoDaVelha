using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SuperJogoDaVelha
{
    internal static class Program
    {
        static Color[] colors = {
            Color.FromArgb(23, 143, 49), Color.FromArgb(58, 219, 94, 86), Color.FromArgb(94, 255, 129, 100), Color.FromArgb(202, 202, 202, 80), Color.FromArgb(255, 255, 255, 100)
        };

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new JogoDaVelhaForm());
        }
    }
}
