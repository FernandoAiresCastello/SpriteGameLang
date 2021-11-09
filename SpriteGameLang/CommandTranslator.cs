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
                cpp.Append(string.Format("_api->ShowMsgBox({0});", args[0]));
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_api->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append(string.Format("_api->SetWindowTitle({0});", args[0]));
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append(string.Format("_api->OpenWindow({0}, {1}, {2});", 
                    args[0], args[1], args[2]));
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
            else if (cmd == "ROOT")
            {
                cpp.Append(string.Format("_api->SetFileRoot({0});", args[0]));
            }
            else if (cmd == "LDIMG")
            {
                cpp.Append(string.Format("_api->LoadImageFile({0}, {1});", 
                    args[0], args[1]));
            }
            else if (cmd == "DWIMG")
            {
                cpp.Append(string.Format("_api->DrawImage({0}, {1}, {2});",
                    args[0], args[1], args[2]));
            }
            else if (cmd == "CLS")
            {
                cpp.Append(string.Format("_api->ClearWindow({0});", args[0]));
            }
            else if (cmd == "REFS")
            {
                cpp.Append("_api->UpdateWindow();");
            }
            else
            {
                throw new CompileError("Invalid command: " + srcLine);
            }

            return cpp.ToString();
        }
    }
}
