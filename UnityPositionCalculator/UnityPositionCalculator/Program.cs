using System;
using System.Windows.Forms;

namespace UnityPositionCalculator
{
    static class Program
    {
        public const String AppName = "Unity Position Caluclator";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
