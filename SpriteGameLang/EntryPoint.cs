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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CompilerWindow wnd = new CompilerWindow();
            Application.Run(wnd);
        }

        private static void Compile(string[] args)
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
                string generatedExeFile = args[1];
                string generatedCppFile = "__generated__.cpp";

                File.Delete(generatedExeFile);
                File.Delete(generatedCppFile);

                string[] srcLines = File.ReadAllLines(srcFile);
                Compiler compiler = new Compiler();

                bool ok = compiler.CompileSglToCpp(srcLines, generatedCppFile);
                if (ok)
                {
                    ok = compiler.CompileCppToExe(generatedCppFile, generatedExeFile, null);
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
