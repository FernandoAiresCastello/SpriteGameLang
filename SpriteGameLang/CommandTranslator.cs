using System;
using System.Collections.Generic;
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

        private string ArgString()
        {
            if (IxArg >= Args.Length)
                throw new CompileError("Not enough arguments in: " + SrcCode);

            string arg = Args[IxArg++];
            if (arg.StartsWith("$"))
                arg = string.Format("_api->GetVariable(\"{0}\")->StringValue", arg.Substring(1));

            return arg;
        }

        private string ArgNumber()
        {
            if (IxArg >= Args.Length)
                throw new CompileError("Not enough arguments in: " + SrcCode);

            string arg = Args[IxArg++];
            if (arg.StartsWith("$"))
                arg = string.Format("_api->GetVariable(\"{0}\")->NumberValue", arg.Substring(1));

            return arg;
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
            else if (cmd == "SET")
            {
                bool invalid = false;
                if (!args[0].StartsWith("$"))
                    invalid = true;
                args[0] = args[0].Substring(1);
                if (args[0] == string.Empty)
                    invalid = true;

                if (args[1].StartsWith("$"))
                {
                    args[1] = args[1].Substring(1);
                    if (args[1] == string.Empty)
                        throw new CompileError("Invalid variable identifier in: " + SrcCode);
                    else
                        cpp.Append(string.Format("_api->SetVariablesEqual(\"{0}\", \"{1}\");", args[0], args[1]));
                }
                else
                {
                    if (invalid)
                        throw new CompileError("Invalid variable identifier in: " + SrcCode);

                    cpp.Append(string.Format(
                        "_api->SetVariable(\"{0}\", {1});",
                        args[0], args[1]));
                }
            }
            else if (cmd == "MSGBOX")
            {
                cpp.Append(string.Format(
                    "_api->ShowMsgBox({0});",
                    ArgString()));
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_api->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append(string.Format(
                    "_api->SetWindowTitle({0});",
                    ArgString()));
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append(string.Format(
                    "_api->OpenWindow({0}, {1}, {2}, {3});",
                    ArgNumber(), ArgNumber(), ArgNumber(), ArgNumber()));
            }
            else if (cmd == "HALT")
            {
                cpp.Append("_api->Halt();");
            }
            else if (cmd == "LOOP")
            {
                cpp.AppendLine("while (true) {");
                cpp.AppendLine("_api->ProcessGlobalEvents();");
            }
            else if (cmd == "END")
            {
                cpp.Append("}");
            }
            else if (cmd == "TRANSP")
            {
                cpp.Append(string.Format(
                    "_api->SetTransparencyKey({0});",
                    ArgNumber()));
            }
            else if (cmd == "ROOT")
            {
                cpp.Append(string.Format(
                    "_api->SetFileRoot({0});",
                    ArgString()));
            }
            else if (cmd == "LDIMG")
            {
                cpp.Append(string.Format(
                    "_api->LoadImageFile({0}, {1});",
                   ArgString(), ArgString()));
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
                cpp.Append(string.Format(
                    "_api->SetWindowBackColor({0});",
                    ArgNumber()));
            }
            else if (cmd == "DWIMG")
            {
                cpp.Append(string.Format(
                    "_api->DrawImage({0}, {1}, {2});",
                   ArgString(), ArgNumber(), ArgNumber()));
            }
            else if (cmd == "TILES")
            {
                cpp.Append(string.Format(
                    "_api->MakeTileset({0}, {1}, {2}, {3});",
                    ArgString(), ArgString(), ArgNumber(), ArgNumber()));
            }
            else if (cmd == "DWTILE")
            {
                cpp.Append(string.Format(
                    "_api->DrawTile({0}, {1}, {2}, {3});",
                    ArgString(), ArgNumber(), ArgNumber(), ArgNumber()));
            }
            else if (cmd == "DWSTR")
            {
                cpp.Append(string.Format(
                    "_api->DrawString({0}, {1}, {2}, {3});",
                    ArgString(), ArgString(), ArgNumber(), ArgNumber()));
            }
            else
            {
                throw new CompileError("Invalid command in: " + SrcCode);
            }

            return cpp.ToString();
        }
    }
}
