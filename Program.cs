namespace CharacterCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Vitej v tvorbe postavy!");

            Console.WriteLine("Zadej jmeno postavy:");
            string name = Console.ReadLine();
            Console.WriteLine("Zadej rasu postavy:");
            string race = Console.ReadLine();
            Console.WriteLine("Zadej povolani postavy:");
            string charClass = Console.ReadLine();

            Character character = new Character(name, race, charClass);

            character.SayHello();
        }
    }
}
