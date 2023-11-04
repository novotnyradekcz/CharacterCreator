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