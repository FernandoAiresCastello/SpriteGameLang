using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteGameLang
{
    static class EntryPoint
    {
        [STAThread]
        static void Main(string[] args)
        {
            Log("*** Sprite Game Compiler ***");

            if (args.Length != 2)
            {
                Log("Missing arguments");
                return;
            }

            string srcFile = args[0];

            if (!File.Exists(srcFile))
            {
                Log("Source file \"" + srcFile + "\" not found");
                return;
            }

            try
            {
                string exeFile = args[1];
                string cppFile = "__generated__.cpp";

                File.Delete(exeFile);
                File.Delete(cppFile);

                string[] srcLines = File.ReadAllLines(srcFile);
                Compiler compiler = new Compiler();

                bool ok = compiler.CompileSglToCpp(srcLines, cppFile);
                if (ok)
                {
                    ok = compiler.CompileCppToExe(cppFile, exeFile);
                    if (ok)
                    {
                        //File.Delete(cppFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private static void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
