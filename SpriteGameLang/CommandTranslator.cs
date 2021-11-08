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
                cpp.Append("_system->ShowMsgBox(");
                cpp.Append(args[0]);
                cpp.Append(");");
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_system->Exit();");
            }
            else
            {
                throw new CompileError("Invalid command: " + srcLine);
            }

            return cpp.ToString();
        }
    }
}
