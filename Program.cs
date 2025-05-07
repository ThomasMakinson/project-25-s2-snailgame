using System.Security.AccessControl;

namespace SnailMate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string direction;
            Console.WriteLine("Hello, You are in a room, a snail wants to kill you, good luck :3");
            Console.Write("which direction do you want to go?\nLeft, Right, Up, or Down: ");
            direction = Console.ReadLine().ToLower().Trim();
            switch (direction)
            {
                case "right":
                    Console.WriteLine("Wooo you turned right and fell over");
                    break;
                case "left":
                    Console.WriteLine("Awesome you found the exit!");
                    break;
                case "up":
                    Console.WriteLine("You can't fly you twit");
                    break;
                case "down":
                    Console.WriteLine("You sit on the floor and meditate....the snail catches and kills you");
                    break;
                default:
                    Console.WriteLine("You thought you were smart huh? What other direction did you think you could go?");
                    break;
            }
            Console.ReadLine();
        }


    }
}
