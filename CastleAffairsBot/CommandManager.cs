using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DSharpPlus;
using Newtonsoft.Json;

namespace CastleAffairsBot {
    public class CommandManager {
        private static DiscordClient _discord;
        private static Items _party;

        public static Dictionary<ulong, Items> players = new Dictionary<ulong, Items>();

        private Dictionary<string, CommandBase> commandMap = new Dictionary<string, CommandBase>();

        public CommandManager() {
            string file = $"{Environment.GetEnvironmentVariable("FILE_LOCATION")}/party.json";
            if (File.Exists(file))
                _party = JsonConvert.DeserializeObject<Items>(System.IO.File.ReadAllText(file));
            else {
                new Items(0, 0, 0, 0, 0, 0, new List<string>(), file).save();
            }
        }

        public async Task HandleCommand(string[] args) {
            _discord = new DiscordClient(new DiscordConfiguration {
                Token = Environment.GetEnvironmentVariable("DISCORD_TOKEN"),
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            _discord.MessageCreated += async e => {
                if (e.Author.IsBot) return;
                if (e.Channel.IsPrivate) return;
                if (!players.ContainsKey(e.Author.Id)) players.Add(e.Author.Id, new Items(e.Author.Id));

                if (e.Message.Content.ToLower().StartsWith("!")) {
                    string commandString = Regex.Match(e.Message.Content, @"\!([^:]*)\:").Groups[1].Value;
                    if (commandMap.ContainsKey(commandString)) {
                        string s = e.Message.Content.ToLower();
                        s = s.Replace($"!{commandString.ToLower()}:", "");

                        CommandBase commandBase;
                        commandMap.TryGetValue(commandString, out commandBase);

                        commandBase.execute(s.Split(','), _party, e.Author, e.Message);
                    }
                    else {
                        return;
                    }
                }

                _party.save();

                foreach (var playersValue in players.Values)
                    playersValue.save();
            };

            await _discord.ConnectAsync();
            await Task.Delay(-1);
        }

        public void RegisterCommand(CommandBase commandBase) {
            commandMap.Add(commandBase.name, commandBase);
            foreach (string s in commandBase.alias)
                commandMap.Add(s, commandBase);
        }
    }
}