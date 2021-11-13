using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteGameLang
{
    public class Compiler
    {
        private readonly string TemplateCpp;
        private readonly string BeginMain = "// _BEGIN_MAIN_";
        private readonly CommandTranslator CmdTranslator = new CommandTranslator();
        private List<string> Output;
        
        public Compiler()
        {
            TemplateCpp = File.ReadAllText("SpriteGameApi.cpp");
        }

        public bool CompileSglToCpp(string[] srcLines, string outputFileName)
        {
            StringBuilder main = new StringBuilder();

            foreach (string src in srcLines)
            {
                string cpp = CompileLine(src.Trim()).Trim();
                if (cpp != string.Empty)
                    main.AppendLine(cpp);
            }

            string output = TemplateCpp;
            output = output.Replace(BeginMain, main.ToString().Trim()).Trim();
            File.WriteAllText(outputFileName, output);

            return true;
        }

        public bool CompileCppToExe(string cppFile, string exeFile, List<string> output)
        {
            Output = output;

            ProcessStartInfo psi = new ProcessStartInfo("mingw/bin/g++.exe");
            psi.Arguments = string.Format(
                "-o {0} {1} -mwindows -lmingw32 -lSDL2main -lSDL2", exeFile, cppFile);

            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;

            Process proc = Process.Start(psi);
            proc.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            proc.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();

            return proc.ExitCode == 0;
        }

        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (Output != null)
                Output.Add("\t" + outLine.Data);
        }

        private string CompileLine(string srcLine)
        {
            int ixLastQuote = srcLine.LastIndexOf('"');
            int ixLastColon = srcLine.LastIndexOf(';');
            if (ixLastQuote < ixLastColon)
                srcLine = srcLine.Substring(0, ixLastColon).Trim();

            if (string.IsNullOrEmpty(srcLine))
                return srcLine;
            if (srcLine.StartsWith(";"))
                return string.Empty;

            string cmd = null;
            string[] args = null;

            int ixFirstSpace = srcLine.IndexOf(' ');
            if (ixFirstSpace > 0)
            {
                cmd = srcLine.Substring(0, ixFirstSpace).Trim();
                args = ParseArgs(srcLine, srcLine.Substring(ixFirstSpace).Trim());
            }
            else
            {
                cmd = srcLine;
            }

            cmd = cmd.Trim();
            return CmdTranslator.Translate(srcLine, cmd, args);
        }

        public string[] ParseArgs(string srcLine, string srcArgs)
        {
            List<string> args = new List<string>();
            StringBuilder arg = new StringBuilder();
            bool insideStringLiteral = false;

            for (int i = 0; i < srcArgs.Length; i++)
            {
                char ch = srcArgs[i];

                if (ch == '"')
                {
                    insideStringLiteral = !insideStringLiteral;
                    arg.Append(ch);
                    if (i == srcArgs.Length - 1)
                        args.Add(arg.ToString().Trim());
                }
                else if (ch == ',' && !insideStringLiteral)
                {
                    string completeArg = arg.ToString().Trim();
                    if (completeArg == string.Empty)
                        throw new CompileError("Argument parse error in: " + srcLine);

                    args.Add(completeArg);
                    arg.Clear();
                }
                else
                {
                    arg.Append(ch);
                    if (i == srcArgs.Length - 1)
                        args.Add(arg.ToString().Trim());
                }
            }

            return args.ToArray();
        }
    }
}
