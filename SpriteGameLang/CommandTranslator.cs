using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpriteGameLang
{
    public class CommandTranslator
    {
        public string Translate(string srcLine, string cmd, string[] args)
        {
            StringBuilder cpp = new StringBuilder();

            if (cmd == "TEST")
            {
                cpp.Append("_api->Test();");
            }
            else if (cmd == "MSGBOX")
            {
                cpp.Append(string.Format(
                    "_api->ShowMsgBox({0});", 
                    args[0]));
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_api->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append(string.Format(
                    "_api->SetWindowTitle({0});", 
                    args[0]));
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append(string.Format(
                    "_api->OpenWindow({0}, {1}, {2}, {3});", 
                    args[0], args[1], args[2], args[3]));
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
                    args[0]));
            }
            else if (cmd == "ROOT")
            {
                cpp.Append(string.Format(
                    "_api->SetFileRoot({0});", 
                    args[0]));
            }
            else if (cmd == "LDIMG")
            {
                cpp.Append(string.Format(
                    "_api->LoadImageFile({0}, {1});", 
                    args[0], args[1]));
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
                    args[0]));
            }
            else if (cmd == "DWIMG")
            {
                cpp.Append(string.Format(
                    "_api->DrawImage({0}, {1}, {2});",
                    args[0], args[1], args[2]));
            }
            else if (cmd == "TILES")
            {
                cpp.Append(string.Format(
                    "_api->MakeTileset({0}, {1}, {2}, {3});",
                    args[0], args[1], args[2], args[3]));
            }
            else if (cmd == "DWTILE")
            {
                cpp.Append(string.Format(
                    "_api->DrawTile({0}, {1}, {2}, {3});",
                    args[0], args[1], args[2], args[3]));
            }
            else if (cmd == "DWSTR")
            {
                cpp.Append(string.Format(
                    "_api->DrawString({0}, {1}, {2}, {3});",
                    args[0], args[1], args[2], args[3]));
            }
            else
            {
                throw new CompileError("Invalid command: " + srcLine.Trim());
            }

            return cpp.ToString();
        }
    }
}
