using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Collections;
using System.Data;
using System.IO;
using Google.Apis.Translate.v2;
using Google.Apis.Services;
using ManyConsole;

namespace GoogleTranslatorResource
{
    class Program
    {
        static int Main(string[] args)
        {
            var commands = GetCommands();

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
