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
        private int _strength;
        private int _dexterity;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _charisma;

        public Character(string name, string race, string characterClass)
        {
            this._name = name;
            this._race = race;
            this._class = characterClass;
            this._strength = 10;
            this._dexterity = 10;
            this._constitution = 10;
            this._intelligence = 10;
            this._wisdom = 10;
            this._charisma = 10;
        }

        public void SayHello()
        {
            Console.WriteLine("Ahoj, jmenuji se {0}, jsem {1} {2}.", this._name, this._race, this._class);
        }

        public void RollAbilityScores()
        {
            Random dice = new Random();
            dice.Next(1, 7);
        }

        public void ShowAbilityScores()
        {
            Console.WriteLine("Sila: {0}", this._strength);
            Console.WriteLine("Obratnost: {0}", this._dexterity);
            Console.WriteLine("Odolnost: {0}", this._constitution);
            Console.WriteLine("Inteligence: {0}", this._intelligence);
            Console.WriteLine("Moudrost: {0}", this._wisdom);
            Console.WriteLine("Charisma: {0}", this._charisma);
        }
    }
}