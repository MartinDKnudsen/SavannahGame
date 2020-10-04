using System;

namespace BusinessLogic
{
    public class TxtOpener
    {

        public static void OpenTxtFile(string txt)
        {
            System.Diagnostics.Process.Start(txt);
        }

        public static void SupportTxt()
        {

            string p = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                               "/ResultsFromSavannaGame.txt";

            OpenTxtFile(p);
        }
    }
}
