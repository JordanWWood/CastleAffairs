using System;
using System.Threading.Tasks;
using System.Xml.XPath;
using DSharpPlus;
using Newtonsoft.Json;

namespace CastleAffairsBot {
    class Program {
        private static DiscordClient _discord;
        private static Items _items;

        static void Main(string[] args) {
            _items = JsonConvert.DeserializeObject<Items>(System.IO.File.ReadAllText(@"C:\Users\jorda\dndbot\data.json"));

            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args) {
            _discord = new DiscordClient(new DiscordConfiguration {
                Token = "NDE3NDQzMDAyNDMzMTQyNzk0.DXTM8Q.FMGlF1koiWN218GktSvqH0lAAa8",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            _discord.MessageCreated += async e => {
                if (e.Author.IsBot) return;
                if (e.Channel.IsPrivate) return;
                if (e.Message.Content.ToLower().StartsWith("+finance:")) {
                    String s = e.Message.Content.ToLower().Replace("+finance:", "");
                    String[] items = s.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    String finalMessage = "Messages: \n";
                    foreach (var t in items) {
                        if (t.EndsWith("pp")) {
                            try {
                                _items.pp += Int32.Parse(t.Replace("pp", ""));

                                finalMessage += t.Replace("pp", "") + "pp has been added successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("pp", "") + " could not be added. Try again or complain at Jordan" + "\n";
                                Console.WriteLine(ex.Message);
                            }
                        } else if(t.EndsWith("gp")) {
                            try {
                                _items.gp += Int32.Parse(t.Replace("gp", "")); 

                                finalMessage += t.Replace("gp", "") + "gp has been added successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("gp", "") + " could not be added. Try again or complain at Jordan" + "\n";
                                Console.WriteLine(ex.Message);
                            }
                        } else if (t.EndsWith("sp")) {
                            try {
                                _items.sp += Int32.Parse(t.Replace("sp", ""));

                                finalMessage += t.Replace("sp", "") + "sp has been added successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("sp", "") + " could not be added. Try again or complain at Jordan" + "\n";
                                Console.WriteLine(ex.Message);
                            }
                        } else if (t.EndsWith("ep")) {
                            try {
                                _items.ep += Int32.Parse(t.Replace("ep", "")); 

                                finalMessage += t.Replace("ep", "") + "ep has been added successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("ep", "") + " could not be added. Try again or complain at Jordan" + "\n";

                                Console.WriteLine(ex.Message);
                            }
                        } else if (t.EndsWith("cp")) {
                            try {
                                _items.cp += Int32.Parse(t.Replace("cp", "")); 

                                finalMessage +=  t.Replace("cp", "") + "cp has been added successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("cp", "") + " could not be added. Try again or complain at Jordan" + "\n";

                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    await e.Message.RespondAsync(finalMessage);
                    _items.save();
                }

                if (e.Message.Content.ToLower().StartsWith("-finance:")) {
                    String s = e.Message.Content.ToLower().Replace("-finance:", "");
                    String[] items = s.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    String finalMessage = "Messages: \n";
                    foreach (var t in items) {
                        if (t.EndsWith("pp")) {
                            try {
                                _items.pp -= Int32.Parse(t.Replace("pp", ""));

                                finalMessage += t.Replace("pp", "") + "pp has been removed successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("pp", "") + " could not be removed. Try again or complain at Jordan" + "\n";
                                Console.WriteLine(ex.Message);
                            }
                        } else if(t.EndsWith("gp")) {
                            try {
                                _items.gp -= Int32.Parse(t.Replace("gp", "")); 

                                finalMessage += t.Replace("gp", "") + "gp has been removed successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("gp", "") + " could not be removed. Try again or complain at Jordan" + "\n";
                                Console.WriteLine(ex.Message);
                            }
                        } else if (t.EndsWith("sp")) {
                            try {
                                _items.sp -= Int32.Parse(t.Replace("sp", ""));

                                finalMessage += t.Replace("sp", "") + "sp has been removed successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("sp", "") + " could not be removed. Try again or complain at Jordan" + "\n";
                                Console.WriteLine(ex.Message);
                            }
                        } else if (t.EndsWith("ep")) {
                            try {
                                _items.ep -= Int32.Parse(t.Replace("ep", "")); 

                                finalMessage += t.Replace("ep", "") + "ep has been removed successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("ep", "") + " could not be removed. Try again or complain at Jordan" + "\n";

                                Console.WriteLine(ex.Message);
                            }
                        } else if (t.EndsWith("cp")) {
                            try {
                                _items.cp -= Int32.Parse(t.Replace("cp", "")); 

                                finalMessage +=  t.Replace("cp", "") + "cp has been removed successfully by " + e.Author.Username + "\n";
                            } catch (Exception ex) {
                                finalMessage += t.Replace("cp", "") + " could not be removed. Try again or complain at Jordan" + "\n";

                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    await e.Message.RespondAsync(finalMessage);
                    _items.save();
                }

                if (e.Message.Content.ToLower().StartsWith("!total")) {
                    await e.Message.RespondAsync("Total finances: \n" 
                                                 + _items.pp + "pp = " + (_items.pp * 10) + "gp \n" 
                                                 + _items.gp + "gp = " + (_items.gp / 10) + "pp \n" 
                                                 + _items.ep + "ep = " + (_items.ep / 2) + "gp \n" 
                                                 + _items.sp + "sp = " + (_items.sp / 10) + "gp \n" 
                                                 + _items.cp + "cp = " + (_items.cp / 100) + "gp \n " +
                                                 "\nTotal in GP: " + ((_items.pp * 10) + (_items.gp) + (_items.sp / 10) + (_items.cp / 100) + (_items.ep / 2)));
                }

                if (e.Message.Content.ToLower().StartsWith("+xp:")) {
                    String s = e.Message.Content.ToLower().Replace("+xp: ", "");
                    _items.xp += Int64.Parse(s);

                    await e.Message.RespondAsync(s + "xp added successfully for a total of " + _items.xp);
                    _items.save();
                }

                if (e.Message.Content.ToLower().StartsWith("-xp:")) {
                    String s = e.Message.Content.ToLower().Replace("-xp: ", "");
                    _items.xp -= Int64.Parse(s);

                    await e.Message.RespondAsync(s + "xp removed successfully for a total of " + _items.xp);
                    _items.save();
                }

                if (e.Message.Content.ToLower().StartsWith("!xp")) {
                    await e.Message.RespondAsync(_items.xp + " total xp");
                }
            };

            await _discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}