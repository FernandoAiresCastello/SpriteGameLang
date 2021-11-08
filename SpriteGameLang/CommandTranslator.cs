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

            if (cmd == "MSGBOX")
            {
                cpp.Append(string.Format("_system->ShowMsgBox({0});", args[0]));
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_system->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append(string.Format("_window->SetTitle({0});", args[0]));
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append(string.Format("_window->Open({0}, {1}, {2});", 
                    args[0], args[1], args[2]));
            }
            else if (cmd == "HALT")
            {
                cpp.Append("_system->Halt();");
            }
            else if (cmd == "LOOP")
            {
                cpp.AppendLine("while (true) {");
                cpp.AppendLine("_system->ProcessGlobalEvents();");
            }
            else if (cmd == "END")
            {
                cpp.Append("}");
            }
            else
            {
                throw new CompileError("Invalid command: " + srcLine);
            }

            return cpp.ToString();
        }
    }
}
