using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SuperJogoDaVelha
{
    public static class Program
    {
        public static Color[] colors = {
            Color.FromArgb(41, 255, 65), Color.FromArgb(90, 232, 107), Color.FromArgb(125, 255, 140), Color.FromArgb(159, 142, 147), Color.FromArgb(237, 230, 232)
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
