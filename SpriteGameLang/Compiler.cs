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

        public Compiler()
        {
            TemplateCpp = File.ReadAllText("SpriteGameApi.cpp");
        }

        public bool CompileSglToCpp(string[] srcLines, string outputFileName)
        {
            Log("Compiling SGL to C++...");

            srcLines = SplitLines(srcLines);
            StringBuilder main = new StringBuilder();
            foreach (string src in srcLines)
                main.AppendLine(CompileLine(src));

            string output = TemplateCpp;
            output = output.Replace(BeginMain, main.ToString());
            output = output.Trim() + Environment.NewLine;
            File.WriteAllText(outputFileName, output);
            Log("Compiled OK!");

            return true;
        }

        public bool CompileCppToExe(string cppFile, string exeFile)
        {
            Log("Compiling C++ to EXE...");

            ProcessStartInfo psi = new ProcessStartInfo("mingw/bin/g++.exe");
            psi.Arguments = string.Format(
                "-o {0} {1} -mwindows -lmingw32 -lSDL2main -lSDL2", exeFile, cppFile);

            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;

            Process proc = Process.Start(psi);
            proc.WaitForExit();

            string stdout = proc.StandardOutput.ReadToEnd().Replace("\n", Environment.NewLine);
            string stderr = proc.StandardError.ReadToEnd().Replace("\n", Environment.NewLine);

            if (!string.IsNullOrWhiteSpace(stdout))
                Log("g++: " + stdout);
            if (!string.IsNullOrWhiteSpace(stderr))
                Log("g++: " + stderr);

            if (proc.ExitCode == 0)
                Log("Compiled OK!");

            return proc.ExitCode == 0;
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
                return "//" + srcLine.Substring(1);

            string cmd = null;
            string[] args = null;

            int ixFirstSpace = srcLine.IndexOf(' ');
            if (ixFirstSpace > 0)
            {
                cmd = srcLine.Substring(0, ixFirstSpace).Trim();
                args = ParseArgs(srcLine.Substring(ixFirstSpace).Trim());
            }
            else
            {
                cmd = srcLine;
            }

            return CmdTranslator.Translate(srcLine, cmd, args);
        }

        public string JoinArgs(string[] args, int initialIndex = 0)
        {
            if (initialIndex > 0)
                return string.Join(" ", args, initialIndex, args.Length - 1).Trim();
            else
                return string.Join(" ", args).Trim();
        }

        public string[] ParseArgs(string src)
        {
            List<string> args = new List<string>();
            bool quote = false;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < src.Length; i++)
            {
                char ch = src[i];

                if (ch == '"')
                {
                    quote = !quote;
                }

                if ((ch != ',' && ch != ';') || quote)
                {
                    sb.Append(ch);
                }

                if (ch == ';' && !quote)
                {
                    args.Add(sb.ToString().Trim());
                    break;
                }
                else if ((ch == ',' && !quote) || i == src.Length - 1)
                {
                    args.Add(sb.ToString().Trim());
                    sb.Clear();
                }
            }

            return args.ToArray();
        }

        private string[] SplitLines(string[] srcLines)
        {
            List<string> lines = new List<string>();

            foreach (string srcLine in srcLines)
                lines.AddRange(QuotedSplit(srcLine, ":"));

            return lines.ToArray();
        }

        private IEnumerable<char> ReadNext(string str, int currentPosition, int count)
        {
            for (var i = 0; i < count; i++)
            {
                if (currentPosition + i >= str.Length)
                {
                    yield break;
                }
                else
                {
                    yield return str[currentPosition + i];
                }
            }
        }

        public IEnumerable<string> QuotedSplit(string s, string delim)
        {
            const char quote = '\"';

            var sb = new StringBuilder(s.Length);
            var counter = 0;
            while (counter < s.Length)
            {
                // if starts with delmiter if so read ahead to see if matches
                if (delim[0] == s[counter] &&
                    delim.SequenceEqual(ReadNext(s, counter, delim.Length)))
                {
                    yield return sb.ToString();
                    sb.Clear();
                    counter = counter + delim.Length; // Move the counter past the delimiter 
                }
                // if we hit a quote read until we hit another quote or end of string
                else if (s[counter] == quote)
                {
                    sb.Append(s[counter++]);
                    while (counter < s.Length && s[counter] != quote)
                    {
                        sb.Append(s[counter++]);
                    }
                    // if not end of string then we hit a quote add the quote
                    if (counter < s.Length)
                    {
                        sb.Append(s[counter++]);
                    }
                }
                else
                {
                    sb.Append(s[counter++]);
                }
            }

            if (sb.Length > 0)
            {
                yield return sb.ToString();
            }
        }

        private void Log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
