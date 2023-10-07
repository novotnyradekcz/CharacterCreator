using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class PlayerCharacter : Character
    {
        public static List<string> _races = new List<string>()
        {
            "clovek",
            "elf",
            "trpaslik",
            "pulcik",
            "pulork",
            "gnom",
            "drakorozeny",
            "tiefling"
        };

        public static List<string> _classes = new List<string>()
        {
            "alchymista",
            "barbar",
            "bard",
            "bojovnik",
            "carodej",
            "cernokneznik",
            "druid",
            "klerik",
            "kouzelnik",
            "lovec netvoru",
            "tulak"
        };

        public PlayerCharacter(string name, string race, string characterClass) : base(name, race, characterClass)
        {
        }
    }
}