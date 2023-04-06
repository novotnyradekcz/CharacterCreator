using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class Character
    {
        private string _name;
        private string _race;
        private string _class;

        public Character(string name, string race, string characterClass)
        {
            this._name = name;
            this._race = race;
            this._class = characterClass;
        }

        public void SayHello()
        {
            Console.WriteLine("Ahoj, jmenuji se {0}, jsem {1} {2}.", this._name, this._race, this._class);
        }
    }
}