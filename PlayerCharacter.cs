using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterCreator
{
    public class PlayerCharacter : Character
    {
        public PlayerCharacter(string name, string race, string characterClass) : base(name, race, characterClass)
        {
        }
    }
}