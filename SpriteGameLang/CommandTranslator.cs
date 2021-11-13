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

        private string Arg()
        {
            if (IxArg >= Args.Length)
                throw new CompileError("Not enough arguments in: " + SrcCode);

            return Args[IxArg++];
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
                cpp.Append(string.Format(
                    "auto {0} = {1};",
                    Arg(), Arg()));
            }
            else if (cmd == "MSGBOX")
            {
                cpp.Append(string.Format(
                    "_api->ShowMsgBox({0});",
                    Arg()));
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_api->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append(string.Format(
                    "_api->SetWindowTitle({0});",
                    Arg()));
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append(string.Format(
                    "_api->OpenWindow({0}, {1}, {2}, {3});",
                    Arg(), Arg(), Arg(), Arg()));
            }
            else if (cmd == "FULLSCR")
            {
                cpp.Append(string.Format("_api->SetFullscreen({0});", 
                    Arg()));
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
                    Arg()));
            }
            else if (cmd == "ROOT")
            {
                cpp.Append(string.Format(
                    "_api->SetFileRoot({0});",
                    Arg()));
            }
            else if (cmd == "LDIMG")
            {
                cpp.Append(string.Format(
                    "_api->LoadImageFile({0}, {1});",
                   Arg(), Arg()));
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
                    Arg()));
            }
            else if (cmd == "DWIMG")
            {
                cpp.Append(string.Format(
                    "_api->DrawImage({0}, {1}, {2});",
                   Arg(), Arg(), Arg()));
            }
            else if (cmd == "TILES")
            {
                cpp.Append(string.Format(
                    "_api->MakeTileset({0}, {1}, {2}, {3});",
                    Arg(), Arg(), Arg(), Arg()));
            }
            else if (cmd == "DWTILE")
            {
                cpp.Append(string.Format(
                    "_api->DrawTile({0}, {1}, {2}, {3});",
                    Arg(), Arg(), Arg(), Arg()));
            }
            else if (cmd == "DWSTR")
            {
                cpp.Append(string.Format(
                    "_api->DrawString({0}, {1}, {2}, {3});",
                    Arg(), Arg(), Arg(), Arg()));
            }
            else if (cmd == "LAYER")
            {
                cpp.Append(string.Format(
                    "_api->SelectLayer({0});",
                    Arg()));
            }
            else if (cmd == "DISL")
            {
                cpp.Append(string.Format(
                    "_api->EnableLayer({0}, false);",
                    Arg()));
            }
            else if (cmd == "ENAL")
            {
                cpp.Append(string.Format(
                    "_api->EnableLayer({0}, true);",
                    Arg()));
            }
            else if (cmd == "SCRL")
            {
                cpp.Append(string.Format(
                    "_api->ScrollLayerToPoint({0}, {1}, {2});",
                    Arg(), Arg(), Arg()));
            }
            else
            {
                throw new CompileError("Invalid command in: " + SrcCode);
            }

            return cpp.ToString();
        }
    }
}
