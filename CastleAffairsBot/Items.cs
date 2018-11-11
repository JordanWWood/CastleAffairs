using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CastleAffairsBot {
    public class Items {
        public int pp { get; set; }
        public int gp { get; set; }
        public int ep { get; set; }
        public int sp { get; set; }
        public int cp { get; set; }

        public Int64 xp { get; set; }

        public List<string> items { get; set; }

        [JsonIgnore]
        private readonly string _path;

        public void save() {
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(_path, json);
        }
        
        public void save(string loc) {
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(loc, json);
        }

        public Items(ulong playerId) {
            _path = $"{Environment.GetEnvironmentVariable("FILE_LOCATION")}/{playerId}.json";
            if (!File.Exists(_path)) {
                this.pp = 0;
                this.gp = 0;
                this.ep = 0;
                this.sp = 0;
                this.cp = 0;

                this.xp = 0;
                this.items = new List<string>();
            } else {
                Items item = JsonConvert.DeserializeObject<Items>(System.IO.File.ReadAllText(_path));
                this.pp = item.pp;
                this.gp = item.gp;
                this.ep = item.ep;
                this.sp = item.sp;
                this.cp = item.cp;
                
                this.xp = item.xp;
                this.items = item.items;
            }
        }
        
        public Items(int pp, int gp, int ep, int sp, int cp, int xp, List<string> items, string path) {
            this.pp = pp;
            this.gp = gp;
            this.ep = ep;
            this.sp = sp;
            this.cp = cp;

            this.xp = xp;
            this.items = items;
        }
    }
}