using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CastleAffairsBot {
    class Items {
        public int pp { get; set; }
        public int gp { get; set; }
        public int ep { get; set; }
        public int sp { get; set; }
        public int cp { get; set; }

        public Int64 xp { get; set; }

        public List<String> items { get; set; }

        public void save() {
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(@"C:\Users\jorda_000\dndbot\data.json", json);
        }
    }
}