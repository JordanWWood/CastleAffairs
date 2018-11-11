using System.Threading;
using DSharpPlus.Entities;

namespace CastleAffairsBot {
    public class Total : CommandBase {
        public Total() : base("total", new[] {"t"}) { }

        public override bool execute(string[] args, Items party, DiscordUser sender, DiscordMessage message) {
            bool isParty;

            switch (args[0]) {
                case "party": isParty = true; break;
                default: isParty = false; break;
            }

            Items items = party;
            if (!isParty) CommandManager.players.TryGetValue(sender.Id, out items);

            string partyString = (isParty) ? " party" : "";
            message.RespondAsync($"Total{partyString} finances: \n"
                                 + items.pp + "pp = " + (items.pp * 10) + "gp \n"
                                 + items.gp + "gp = " + (items.gp / 10) + "pp \n"
                                 + items.ep + "ep = " + (items.ep / 2) + "gp \n"
                                 + items.sp + "sp = " + (items.sp / 10) + "gp \n"
                                 + items.cp + "cp = " + (items.cp / 100) + "gp \n " 
                                 + $"\nTotal in GP: {((items.pp * 10) + (items.gp) + (items.sp / 10) + (items.cp / 100) + (items.ep / 2))} \n"
                                 + $"Total XP: {items.xp}").Wait();

            return true;
        }
    }
}