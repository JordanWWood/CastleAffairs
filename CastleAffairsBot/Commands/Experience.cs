using System;
using DSharpPlus.Entities;

namespace CastleAffairsBot {
    public class Experience : CommandBase {
        public Experience() : base("experience", new [] {"xp"}) { }
        public override bool execute(string[] args, Items party, DiscordUser sender, DiscordMessage message) {
            bool isParty = false;

            switch (args[0]) {
                case "party": isParty = true; break;
                default: isParty = false; break;
            }
            
            Items items = party;
            if (!isParty) CommandManager.players.TryGetValue(sender.Id, out items);
            
            String s = message.Content.ToLower().Replace("+xp: ", "");
            Int64 value = Int64.Parse(s);
            items.xp += value;

            string direction = (value < 0) ? "removed" : "added";
            message.RespondAsync($"{s}xp {direction} successfully for a total of " + items.xp).Wait();

            return true;
        }
    }
}