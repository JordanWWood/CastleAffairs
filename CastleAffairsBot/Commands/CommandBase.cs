using DSharpPlus.Entities;

namespace CastleAffairsBot {
    public abstract class CommandBase {
        public string name { get; private set; }
        public string[] alias { get; private set; }
        
        protected CommandBase(string name, string[] alias) {
            this.name = name;
            this.alias = alias;
        }
        
        public abstract bool execute(string[] args, Items party, DiscordUser sender, DiscordMessage message);
    }
}