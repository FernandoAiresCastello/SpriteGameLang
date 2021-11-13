using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteGameLang
{
    public class CommandTranslator
    {
        private string SrcCode;
        private string Cmd;
        private string[] Args;
        private int IxArg;
        private readonly HashSet<string> DeclaredVars = new HashSet<string>();

        private string[] Arg(int count)
        {
            List<string> args = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (IxArg >= Args.Length)
                    throw new CompileError("Not enough arguments in: " + SrcCode);

                args.Add(Args[IxArg++]);
            }

            return args.ToArray();
        }

        public string Translate(string srcCode, string cmd, string[] args)
        {
            SrcCode = srcCode;
            Cmd = cmd;
            Args = args;
            IxArg = 0;

            StringBuilder cpp = new StringBuilder();

            if (cmd == "TEST")
            {
                cpp.Append("_api->Test();");
            }
            else if (cmd == "VAR")
            {
                string var = args[0];

                if (DeclaredVars.Contains(var))
                {
                    cpp.Append(string.Format("{0} = {1};", Arg(2)));
                }
                else
                {
                    DeclaredVars.Add(var);

                    string type;
                    string value = args[1];
                    if (value.StartsWith("\"") && value.EndsWith("\""))
                        type = "std::string";
                    else if (char.IsDigit(value[0]))
                        type = "int";
                    else
                        type = "auto";

                    cpp.Append(type + string.Format(" {0} = {1};", Arg(2)));
                }
            }
            else if (cmd == "INC")
            {
                cpp.Append(string.Format("{0}++;", Arg(1)));
            }
            else if (cmd == "DEC")
            {
                cpp.Append(string.Format("{0}--;", Arg(1)));
            }
            else if (cmd == "MSGBOX")
            {
                cpp.Append(string.Format("_api->ShowMsgBox({0});", Arg(1)));
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_api->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append(string.Format("_api->SetWindowTitle({0});", Arg(1)));
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append(string.Format("_api->OpenWindow({0}, {1}, {2}, {3});", Arg(4)));
            }
            else if (cmd == "FULLSCR")
            {
                cpp.Append(string.Format("_api->SetFullscreen({0});", Arg(1)));
            }
            else if (cmd == "HALT")
            {
                cpp.Append("_api->Halt();");
            }
            else if (cmd == "WHILE")
            {
                cpp.AppendLine(string.Format("while({0}) {{", Arg(1)));
                cpp.AppendLine("_api->ProcessGlobalEvents();");
            }
            else if (cmd == "LOOP")
            {
                cpp.AppendLine("while (true) {");
                cpp.AppendLine("_api->ProcessGlobalEvents();");
            }
            else if (cmd == "FOR")
            {
                cpp.AppendLine(string.Format("for (int {0} = {1}; {2}; {0} += {3}) {{", Arg(4)));
                cpp.AppendLine("_api->ProcessGlobalEvents();");
            }
            else if (cmd == "END")
            {
                cpp.Append("}");
            }
            else if (cmd == "TRANSP")
            {
                cpp.Append(string.Format("_api->SetTransparencyKey({0});", Arg(1)));
            }
            else if (cmd == "ROOT")
            {
                cpp.Append(string.Format("_api->SetFileRoot({0});", Arg(1)));
            }
            else if (cmd == "LDIMG")
            {
                cpp.Append(string.Format("_api->LoadImageFile({0}, {1});", Arg(2)));
            }
            else if (cmd == "CLS")
            {
                cpp.Append("_api->ClearWindow();");
            }
            else if (cmd == "REFS")
            {
                cpp.Append("_api->UpdateWindow();");
            }
            else if (cmd == "BGCOLOR")
            {
                cpp.Append(string.Format("_api->SetWindowBackColor({0});", Arg(1)));
            }
            else if (cmd == "DWIMG")
            {
                cpp.Append(string.Format("_api->DrawImage({0}, {1}, {2});", Arg(3)));
            }
            else if (cmd == "TILES")
            {
                cpp.Append(string.Format("_api->MakeTileset({0}, {1}, {2}, {3});", Arg(4)));
            }
            else if (cmd == "DWTILE")
            {
                cpp.Append(string.Format("_api->DrawTile({0}, {1}, {2}, {3});", Arg(4)));
            }
            else if (cmd == "DWSTR")
            {
                cpp.Append(string.Format("_api->DrawString({0}, {1}, {2}, {3});", Arg(4)));
            }
            else if (cmd == "LAYER")
            {
                cpp.Append(string.Format("_api->SelectLayer({0});", Arg(1)));
            }
            else if (cmd == "DISL")
            {
                cpp.Append(string.Format("_api->EnableLayer({0}, false);", Arg(1)));
            }
            else if (cmd == "ENAL")
            {
                cpp.Append(string.Format("_api->EnableLayer({0}, true);", Arg(1)));
            }
            else if (cmd == "SCRL")
            {
                cpp.Append(string.Format("_api->ScrollLayerToPoint({0}, {1}, {2});", Arg(3)));
            }
            else
            {
                throw new CompileError("Invalid command in: " + SrcCode);
            }

            return cpp.ToString();
        }
    }
}
