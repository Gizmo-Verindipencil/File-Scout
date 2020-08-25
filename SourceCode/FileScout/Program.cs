using FileScout.UI;
using System;
using System.Windows.Forms;

namespace FileScout
{
    class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChoosingScoutForm());
        }
    }
}
