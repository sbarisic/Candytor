using CandytorAPI;

namespace Candytor
{
    internal static class Program
    {
        public static CandyAPI API;

        [STAThread]
        static void Main()
        {
            API = new CandyAPI();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}