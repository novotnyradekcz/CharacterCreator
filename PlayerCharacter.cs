using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class PlayerCharacter : Character
    {
        public PlayerCharacter(string name) : base(name)
        {
            this._races.Add("clovek");
            this._races.Add("elf");
            this._races.Add("trpaslik");
            this._races.Add("pulcik");
            this._races.Add("pulork");
            this._races.Add("gnom");
            this._races.Add("drakorozeny");
            this._races.Add("tiefling");
            this._classes.Add("alchymista");
            this._classes.Add("barbar");
            this._classes.Add("bard");
            this._classes.Add("bojovnik");
            this._classes.Add("carodej");
            this._classes.Add("cernokneznik");
            this._classes.Add("druid");
            this._classes.Add("klerik");
            this._classes.Add("kouzelnik");
            this._classes.Add("lovec netvoru");
            this._classes.Add("tulak");
        }

        override public void AssignRace()
        {
            while (this._race == "")
            {
                Console.WriteLine($"Vyber si rasu postavy: {string.Join(", ", this._races)}.");
                string race = Console.ReadLine();
                if (this._races.Contains(race))
                {
                    this._race = race;
                }
                else
                {
                    Console.WriteLine($"'{race}' neni jedna z hratelnych ras.");
                }
            }
        }

        override public void AssignClass()
        {
            while (this._class == "")
            {
                Console.WriteLine($"Vyber si povolani postavy: {string.Join(", ", this._classes)}.");
                string characterClass = Console.ReadLine();
                if (this._classes.Contains(characterClass))
                {
                    this._class = characterClass;
                }
                else
                {
                    Console.WriteLine($"'{characterClass}' neni jedno z hratelnych povolani.");
                }
            }
        }
    }
}