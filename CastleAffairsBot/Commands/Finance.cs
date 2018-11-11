using System;
using DSharpPlus.Entities;

namespace CastleAffairsBot {
    public class Finance : CommandBase {
        public Finance() : base("finance", new[] {"f", "money"}) { }

        public override bool execute(string[] args, Items party, DiscordUser sender, DiscordMessage message) {
            bool isParty;
            
            switch (args[0]) {
                case "party":
                    isParty = true; break;
                case "self":
                    isParty = false; break;
                default:
                    message.RespondAsync("Incorrect usage! Please specify whether or not this command targets the party or yourself!");
                    break;
            }
            
            Items items = party;
            if (!isParty) CommandManager.players.TryGetValue(sender.Id, out items);
            string funds = (isParty) ? "(party)" : "(personal)";
            
            string finalMessage = "Messages: \n";

            int argValue = 0;
            foreach (var t in args) {
                try {
                    if (t.EndsWith("pp")) {
                        int value = Int32.Parse(t.Replace("pp", ""));
                        items.pp += value;

                        string direction = (value < 0) ? "removed" : "added";
                        finalMessage += $"{t} has been {direction} successfully by {sender.Username}. {funds} \n";
                    }
                    else if (t.EndsWith("gp")) {
                        int value = Int32.Parse(t.Replace("gp", ""));
                        items.gp += value;

                        string direction = (value < 0) ? "removed" : "added";
                        finalMessage += $"{t} has been {direction} successfully by {sender.Username}. {funds} \n";
                    }
                    else if (t.EndsWith("sp")) {
                        int value = Int32.Parse(t.Replace("sp", ""));
                        items.sp += value;

                        string direction = (value < 0) ? "removed" : "added";
                        finalMessage += $"{t} has been {direction} successfully by {sender.Username}. {funds} \n";
                    }
                    else if (t.EndsWith("ep")) {
                        int value = Int32.Parse(t.Replace("ep", ""));
                        items.ep += value;

                        string direction = (value < 0) ? "removed" : "added";
                        finalMessage += $"{t} has been {direction} successfully by {sender.Username}. {funds} \n";
                    }
                    else if (t.EndsWith("cp")) {
                        int value = Int32.Parse(t.Replace("cp", ""));
                        items.cp += value;

                        string direction = (value < 0) ? "removed" : "added";
                        finalMessage += $"{t} has been {direction} successfully by {sender.Username}. {funds} \n";
                    }
                }
                catch (Exception ex) {
                    finalMessage += $"arg {argValue} could not be added. Try again or complain at Jordan \n";

                    Console.WriteLine(ex.Message);
                }

                argValue++;
            }

            message.RespondAsync(finalMessage).Wait();

            return true;
        }
    }
}