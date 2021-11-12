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
        private string SrcFolder;
        private string SrcFile;
        private string ExeFile;
        private string FileEditorPath;

        public CompilerWindow()
        {
            InitializeComponent();
            TempFolder = Path.Combine(Application.StartupPath, "temp");
            TempExeFile = Path.Combine(TempFolder, "__generated__.exe");
            TempCppFile = Path.Combine(TempFolder, "__generated__.cpp");
            FileEditorPath = "C:\\Program Files\\Notepad++\\notepad++.exe";

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
            if (!string.IsNullOrEmpty(TxtFile.Text.Trim()))
                Compile();
        }

        private void BtnOpenInExplorer_Click(object sender, EventArgs e)
        {
            if (SrcFolder != null)
                Process.Start(SrcFolder);
        }

        private void BtnEditProgram_Click(object sender, EventArgs e)
        {
            if (File.Exists(SrcFile))
                OpenInEditor(SrcFile);
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (ExeFile != null)
                RunCompiledExe(false);
        }

        private void BtnViewGenerated_Click(object sender, EventArgs e)
        {
            if (File.Exists(TempCppFile))
                OpenInEditor(TempCppFile);
        }

        private void OpenInEditor(string file)
        {
            Process.Start(FileEditorPath, file);
        }

        private void RunCompiledExe(bool waitForExit)
        {
            ProcessStartInfo psi = new ProcessStartInfo(ExeFile);
            SrcFolder = new FileInfo(ExeFile).DirectoryName;
            psi.WorkingDirectory = SrcFolder;
            Process proc = new Process();
            proc.StartInfo = psi;
            proc.Start();
            if (waitForExit)
                proc.WaitForExit();
        }

        private void Compile()
        {
            TxtLog.Clear();
            TxtLog.Refresh();
            Log("*** Sprite Game Compiler ***");

            try
            {
                File.WriteAllText(RecentFileList, TxtFile.Text);

                SrcFile = TxtFile.Text.Trim();

                Directory.CreateDirectory(TempFolder);
                if (File.Exists(TempExeFile))
                    File.Delete(TempExeFile);
                if (File.Exists(TempCppFile))
                    File.Delete(TempCppFile);

                string[] srcLines = File.ReadAllLines(SrcFile);
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

                        ExeFile = Path.ChangeExtension(SrcFile, "exe");
                        if (File.Exists(ExeFile))
                            File.Delete(ExeFile);

                        File.Copy(TempExeFile, ExeFile);
                        Log("Generated EXE file in " + ExeFile);

                        Log("Running...");
                        RunCompiledExe(true);
                        Log("Execution finished!");
                    }
                    else
                    {
                        Log("Compilation error:");
                        Log(string.Join(Environment.NewLine, output.ToArray()).Trim());
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
