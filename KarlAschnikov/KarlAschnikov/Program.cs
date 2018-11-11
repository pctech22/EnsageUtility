using System;
using Ensage;
using Ensage.Common;

namespace KarlAschnikov
{
    internal class Program
    {
        private static Entity _me;

        private static void Main(string[] args)
        {
            Events.OnLoad += Events_OnLoad;
            Events.OnClose += Events_OnClose;
        }

        // just memeing ^_^ 
        public static string[] Disrespect =
        {
            "What's the difference between {0} and eggs? Eggs get laid and {0} doesn't.",
            "{0}'s birth certificate is an apology letter from the condom factory.",

        };

        private static void Events_OnClose(object sender, EventArgs e)
        {
            Game.OnFireEvent -= Game_OnFireEvent;
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            _me = ObjectManager.LocalPlayer;

            Game.OnFireEvent += Game_OnFireEvent;
        }

        private static void Game_OnFireEvent(FireEventEventArgs args)
        {
            if (args.GameEvent.Name == "dota_player_kill")
            {
                var killer = (uint) args.GameEvent.GetInt("killer1_userid");
                var rekted = (uint) args.GameEvent.GetInt("victim_userid");

                if (ObjectManager.GetPlayerById(killer).Name == _me.Name)
                {
                    var disrespect = string.Format(Disrespect[new Random().Next(0, Disrespect.Length)],
                        ObjectManager.GetPlayerById(rekted).Name);

                    Game.ExecuteCommand($"say {disrespect}");
                }
            }
        }
    }
} 
