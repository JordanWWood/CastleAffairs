using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.XPath;
using DSharpPlus;
using Newtonsoft.Json;

namespace CastleAffairsBot {
    class Program {
        private static CommandManager CommandManager = new CommandManager();

        static void Main(string[] args) {
            CommandManager.RegisterCommand(new Finance());
            CommandManager.RegisterCommand(new Total());
            CommandManager.RegisterCommand(new Experience());
            
            CommandManager.HandleCommand(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}