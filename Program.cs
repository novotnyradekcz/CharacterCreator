﻿namespace CharacterCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Vitej v tvorbe postavy!");

            Console.WriteLine("Zadej jmeno postavy:");
            string name = Console.ReadLine();

            PlayerCharacter character = new PlayerCharacter(name);

            character.AssignRace();
            character.AssignClass();
            character.SayHello();
            character.RollAbilityScores();
            character.AssignScoreToAbilities();
            character.ShowAbilityScores();
        }
    }
}
