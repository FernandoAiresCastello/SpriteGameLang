using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteGameLang
{
    public partial class CompilerWindow : Form
    {
        private readonly string TempFolder;
        private readonly string TempExeFile;
        private readonly string TempCppFile;
        private readonly string RecentFileList = "recent.ini";

        public CompilerWindow()
        {
            InitializeComponent();
            TempFolder = Path.Combine(Application.StartupPath, "temp");
            TempExeFile = Path.Combine(TempFolder, "__generated__.exe");
            TempCppFile = Path.Combine(TempFolder, "__generated__.cpp");

            if (File.Exists(RecentFileList))
                TxtFile.Text = File.ReadAllText(RecentFileList).Trim();

            TxtFile.Select(0, 0);
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
                TxtFile.Text = dialog.FileName;
        }

        private void BtnCompile_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void Compile()
        {
            TxtLog.Clear();
            TxtLog.Refresh();
            Log("*** Sprite Game Compiler ***");

            try
            {
                File.WriteAllText(RecentFileList, TxtFile.Text);

                string srcFile = TxtFile.Text.Trim();

                Directory.CreateDirectory(TempFolder);
                if (File.Exists(TempExeFile))
                    File.Delete(TempExeFile);
                if (File.Exists(TempCppFile))
                    File.Delete(TempCppFile);

                string[] srcLines = File.ReadAllLines(srcFile);
                Compiler compiler = new Compiler();

                Log("Compiling SGL to C++...");
                bool ok = compiler.CompileSglToCpp(srcLines, TempCppFile);

                if (ok)
                {
                    Log("Compiled OK!");
                    Log("Compiling C++ to EXE...");
                    List<string> output = new List<string>();
                    ok = compiler.CompileCppToExe(TempCppFile, TempExeFile, output);

                    if (ok)
                    {
                        Log("Compiled OK!");

                        string outFile = Path.ChangeExtension(srcFile, "exe");
                        if (File.Exists(outFile))
                            File.Delete(outFile);

                        File.Copy(TempExeFile, outFile);
                        Log("Generated EXE file in " + outFile);
                    }
                    else
                    {
                        Log("Compilation error:");
                        Log(string.Join(Environment.NewLine, output.ToArray()));
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        private void Log(string text)
        {
            TxtLog.AppendText(text + Environment.NewLine);
        }
    }
}
