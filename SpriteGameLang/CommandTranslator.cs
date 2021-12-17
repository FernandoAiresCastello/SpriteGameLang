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

        public string Translate(string srcCode, string cmd, string[] args)
        {
            SrcCode = srcCode;

            StringBuilder cpp = new StringBuilder();
            if (cmd != "VAR" && cmd != "CALL" && cmd != "RET" && cmd != "WHILE" && cmd != "FOR" && cmd != "LOOP" && 
                cmd != "INC" && cmd != "DEC" && cmd != "HKEY" && cmd != "IFKEY" && cmd != "IFKMOD")
                AddArgs(cpp, args);

            if (cmd == "TEST")
            {
                cpp.Append("_api->Test();");
            }
            else if (cmd == "CALL")
            {
                cpp.Append(string.Format("{0}();", args[0]));
            }
            else if (cmd == "RET")
            {
                cpp.Append("return;");
            }
            else if (cmd == "VAR")
            {
                if (char.IsLetter(args[1][0]))
                    cpp.Append(string.Format("_api->SetVariablesEqual(\"{0}\", \"{1}\");", args[0], args[1]));
                else
                    cpp.Append(string.Format("_api->SetVariable(\"{0}\", {1});", args[0], args[1]));
            }
            else if (cmd == "INC")
            {
                cpp.AppendLine("_api->Args.clear();");
                cpp.AppendLine(string.Format("_api->AddFunctionCallArgument(\"{0}\");", args[0]));
                cpp.Append("_api->IncrementVariable();");
            }
            else if (cmd == "DEC")
            {
                cpp.Append("_api->DecrementVariable();");
            }
            else if (cmd == "MSGBOX")
            {
                cpp.Append("_api->ShowMsgBox();");
            }
            else if (cmd == "EXIT")
            {
                cpp.Append("_api->Exit();");
            }
            else if (cmd == "TITLE")
            {
                cpp.Append("_api->SetWindowTitle();");
            }
            else if (cmd == "WINDOW")
            {
                cpp.Append("_api->OpenWindow();");
            }
            else if (cmd == "FULLSCR")
            {
                cpp.Append("_api->SetFullscreen();");
            }
            else if (cmd == "HALT")
            {
                cpp.Append("_api->Halt();");
            }
            else if (cmd == "WHILE")
            {
                cpp.AppendLine(string.Format("while ({0}) {{", args[0]));
                cpp.Append("_api->ProcessGlobalEvents();");
            }
            else if (cmd == "LOOP")
            {
                cpp.AppendLine("while (true) {");
                cpp.Append("_api->ProcessGlobalEvents();");
            }
            else if (cmd == "FOR")
            {
                cpp.AppendLine(string.Format("for (int {0} = {1}; {2}; {0} += {3}) {{", args[0], args[1], args[2], args[3]));
                cpp.AppendLine("_api->ProcessGlobalEvents();");
                cpp.Append(string.Format("_api->SetVariable(\"{0}\", {0});", args[0]));
            }
            else if (cmd == "END")
            {
                cpp.Append("}");
            }
            else if (cmd == "TRANSP")
            {
                cpp.Append("_api->SetTransparencyKey();");
            }
            else if (cmd == "ROOT")
            {
                cpp.Append("_api->SetFileRoot();");
            }
            else if (cmd == "LDIMG")
            {
                cpp.Append("_api->LoadImageFile();");
            }
            else if (cmd == "CLS")
            {
                cpp.Append("_api->DeleteAllSprites();");
            }
            else if (cmd == "CLL")
            {
                cpp.Append("_api->DeleteSprites();");
            }
            else if (cmd == "REFS")
            {
                cpp.Append("_api->UpdateWindow();");
            }
            else if (cmd == "BGCOLOR")
            {
                cpp.Append("_api->SetWindowBackColor();");
            }
            else if (cmd == "DWIMG")
            {
                cpp.Append("_api->DrawImage();");
            }
            else if (cmd == "TILES")
            {
                cpp.Append("_api->MakeTileset();");
            }
            else if (cmd == "DWTILE")
            {
                cpp.Append("_api->DrawTile();");
            }
            else if (cmd == "DWSTR")
            {
                cpp.Append("_api->DrawString();");
            }
            else if (cmd == "LAYER")
            {
                cpp.Append("_api->SelectLayer();");
            }
            else if (cmd == "DISL")
            {
                cpp.Append("_api->EnableLayer();");
            }
            else if (cmd == "ENAL")
            {
                cpp.Append("_api->EnableLayer();");
            }
            else if (cmd == "SCRL")
            {
                cpp.Append("_api->ScrollLayerToPoint();");
            }
            else if (cmd == "DWRECT")
            {
                cpp.Append("_api->DrawRectangle();");
            }
            else if (cmd == "PAUSE")
            {
                cpp.Append("_api->Pause();");
            }
            else if (cmd == "WKEY")
            {
                cpp.Append("_api->WaitKeyPress();");
            }
            else if (cmd == "RDKEY")
            {
                cpp.Append("_api->ReadKeyboardState();");
            }
            else if (cmd == "IFKEY")
            {
                cpp.Append(string.Format("if (_api->KeyboardState[SDL_SCANCODE_{0}]) {{", args[0].ToUpper()));
            }
            else if (cmd == "IFKMOD")
            {
                cpp.Append(string.Format("if (_api->GetKeyModifiers() & {0}) {{", args[0].ToUpper()));
            }
            else
            {
                throw new CompileError("Invalid command in: " + SrcCode);
            }

            return cpp.ToString();
        }

        private void AddArgs(StringBuilder cpp, string[] args)
        {
            if (args != null && args.Length != 0)
            {
                cpp.AppendLine("_api->Args.clear();");
                foreach (string arg in args)
                {
                    string quotedArg = arg;
                    if (!arg.StartsWith("\"") && !arg.EndsWith("\""))
                        quotedArg = string.Format("\"{0}\"", arg);

                    if (char.IsLetter(arg[0]))
                        cpp.AppendLine(string.Format("_api->AddFunctionCallArgument(_api->GetVariable({0})->StringValue);", quotedArg));
                    else
                        cpp.AppendLine(string.Format("_api->AddFunctionCallArgument({0});", quotedArg));
                }
            }

        }
    }
}
