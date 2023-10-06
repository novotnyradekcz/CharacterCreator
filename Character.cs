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
        public static List<string> _classes = new List<string>();
        private List<int> _diceRolls = new List<int>();

        public Character(string name, string race, string characterClass)
        {
            this._name = name;
            this._race = race;
            this._class = characterClass;
            this._strength = 0;
            this._dexterity = 0;
            this._constitution = 0;
            this._intelligence = 0;
            this._wisdom = 0;
            this._charisma = 0;
        }

        public void SayHello()
        {
            Console.WriteLine("Ahoj, jmenuji se {0}, jsem {1} {2}.", this._name, this._race, this._class);
        }

        public void RollAbilityScores()
        {
            Random dice = new Random();
            int[] rolls = new int[6];
            for (int i = 0; i < 6; i++)
            {
                var diceRolls = new List<int>() {dice.Next(1, 7), dice.Next(1, 7), dice.Next(1, 7), dice.Next(1, 7)};
                diceRolls.Remove(diceRolls.Min());
                rolls[i] = diceRolls.Sum();
            }
            foreach (int roll in rolls)
            {
                this._diceRolls.Add(roll);
            }
            Console.WriteLine("Na kostkach ti padlo: {0}, {1}, {2}, {3}, {4}, {5}.", rolls[0], rolls[1], rolls[2], rolls[3], rolls[4], rolls[5]);
        }

        public void AssignScoreToAbilities()
        {
            while (this._strength == 0)
            {
                Console.WriteLine("Prirad hodnotu hodu k sile:");
                Console.WriteLine("Jeste ti zbyva priradit: {0}, {1}, {2}, {3}, {4}, {5}.", this._diceRolls[0], this._diceRolls[1], this._diceRolls[2], this._diceRolls[3], this._diceRolls[4], this._diceRolls[5]);
                string strength = Console.ReadLine();
                try
                {
                    int result = Int32.Parse(strength);
                    if (!this._diceRolls.Contains(result))
                    {
                        throw new FormatException();
                    }
                    this._strength = result;
                    this._diceRolls.Remove(result);
                    Console.WriteLine("Hodnota {0} prirazena k sile.", result);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"'{strength}' neni platna hodnota jednoho z hodu.");
                }
            }
            
            Console.WriteLine("Prirad hodnotu hodu k obratnosti:");
            Console.WriteLine("Jeste ti zbyva priradit: {0}, {1}, {2}, {3}, {4}.", this._diceRolls[0], this._diceRolls[1], this._diceRolls[2], this._diceRolls[3], this._diceRolls[4]);
            string dexterity = Console.ReadLine();
            try
            {
                int result = Int32.Parse(dexterity);
                this._dexterity = result;
                this._diceRolls.Remove(result);
                Console.WriteLine("Hodnota {0} prirazena k obratnosti.", result);
            }
            catch (FormatException)
            {
                Console.WriteLine($"'{dexterity}' neni platna hodnota jednoho z hodu.");
            }
            Console.WriteLine("Prirad hodnotu hodu k odolnosti:");
            Console.WriteLine("Jeste ti zbyva priradit: {0}, {1}, {2}, {3}.", this._diceRolls[0], this._diceRolls[1], this._diceRolls[2], this._diceRolls[3]);
            string constitution = Console.ReadLine();
            try
            {
                int result = Int32.Parse(constitution);
                this._constitution = result;
                this._diceRolls.Remove(result);
                Console.WriteLine("Hodnota {0} prirazena k odolnosti.", result);
            }
            catch (FormatException)
            {
                Console.WriteLine($"'{constitution}' neni platna hodnota jednoho z hodu.");
            }
            Console.WriteLine("Prirad hodnotu hodu k inteligenci:");
            Console.WriteLine("Jeste ti zbyva priradit: {0}, {1}, {2}.", this._diceRolls[0], this._diceRolls[1], this._diceRolls[2]);
            string intelligence = Console.ReadLine();
            try
            {
                int result = Int32.Parse(intelligence);
                this._intelligence = result;
                this._diceRolls.Remove(result);
                Console.WriteLine("Hodnota {0} prirazena k inteligenci.", result);
            }
            catch (FormatException)
            {
                Console.WriteLine($"'{intelligence}' neni platna hodnota jednoho z hodu.");
            }
            Console.WriteLine("Prirad hodnotu hodu k moudrosti:");
            Console.WriteLine("Jeste ti zbyva priradit: {0}, {1}.", this._diceRolls[0], this._diceRolls[1]);
            string wisdom = Console.ReadLine();
            try
            {
                int result = Int32.Parse(wisdom);
                this._wisdom = result;
                this._diceRolls.Remove(result);
                Console.WriteLine("Hodnota {0} prirazena k moudrosti.", result);
            }
            catch (FormatException)
            {
                Console.WriteLine($"'{wisdom}' neni platna hodnota jednoho z hodu.");
            }
            Console.WriteLine("Jeste zbyva priradit: {0}.", this._diceRolls[0]);
            int charisma = this._diceRolls[0];
            this._charisma = charisma;
            this._diceRolls.Remove(charisma);
            Console.WriteLine("Hodnota {0} prirazena k charismatu.", charisma);
        }

        public void ModifyAbilityScores()
        {
            switch(this._race)
            {
                case "clovek":
                    break;
                case "elf":
                    break;
                case "trpaslik":
                    break;
                case "pulcik":
                    break;
                case "pulork":
                    break;
                case "gnom":
                    break;
                case "drakorozeny":
                    break;
                case "tiefling":
                    break;
            }
            switch(this._class)
            {
                case "alchymista":
                    break;
                case "barbar":
                    break;
                case "bard":
                    break;
                case "bojovnik":
                    break;
                case "carodej":
                    break;
                case "cernokneznik":
                    break;
                case "druid":
                    break;
                case "klerik":
                    break;
                case "kouzelnik":
                    break;
                case "lovec netvoru":
                    break;
                case "tulak":
                    break;
            }
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