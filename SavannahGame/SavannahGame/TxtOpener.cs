using System;

namespace SavannahGame
{
    public class TxtOpener
    {

        public static void OpenTxtFile(string txt)
        {
            System.Diagnostics.Process.Start(txt);
        }

        public static void supportTxt()
        {

            string p = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                               "/ResultsFromSavannaGame.txt";

            OpenTxtFile(p);
        }
    }
}
