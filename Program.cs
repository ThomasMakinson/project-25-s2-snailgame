using System.Security.AccessControl;

namespace SnailMate
{
    internal class Program
    {
        public static void DisplayTitleScreen()
        { 
            string[] asciiArt = new string[]
            {
                "  ██████  ███▄    █  ▄▄▄       ██▓ ██▓     ███▄ ▄███▓ ▄▄▄     ▄▄▄█████▓▓█████ ",
                "▒██    ▒  ██ ▀█   █ ▒████▄    ▓██▒▓██▒    ▓██▒▀█▀ ██▒▒████▄   ▓  ██▒ ▓▒▓█   ▀ ",
                "░ ▓██▄   ▓██  ▀█ ██▒▒██  ▀█▄  ▒██▒▒██░    ▓██    ▓██░▒██  ▀█▄ ▒ ▓██░ ▒░▒███   ",
                "  ▒   ██▒▓██▒  ▐▌██▒░██▄▄▄▄██ ░██░▒██░    ▒██    ▒██ ░██▄▄▄▄██░ ▓██▓ ░ ▒▓█  ▄ ",
                "▒██████▒▒▒██░   ▓██░ ▓█   ▓██▒░██░░██████▒▒██▒   ░██▒ ▓█   ▓██▒ ▒██▒ ░ ░▒████▒",
                "▒ ▒▓▒ ▒ ░░ ▒░   ▒ ▒  ▒▒   ▓▒█░░▓  ░ ▒░▓  ░░ ▒░   ░  ░ ▒▒   ▓▒█░ ▒ ░░   ░░ ▒░ ░",
                "░ ░▒  ░ ░░ ░░   ░ ▒░  ▒   ▒▒ ░ ▒ ░░ ░ ▒  ░░  ░      ░  ▒   ▒▒ ░   ░     ░ ░  ░",
                "░  ░  ░     ░   ░ ░   ░   ▒    ▒ ░  ░ ░   ░      ░     ░   ▒    ░         ░   ",
                "      ░           ░       ░  ░ ░      ░  ░       ░         ░  ░           ░  ░"
            };
            int centrex = (Console.WindowWidth - asciiArt.Length / 2), centrey = (Console.WindowHeight / 2);

            Console.Clear();
            Console.SetCursorPosition(centrex, centrey);
            Console.WriteLine(asciiArt);
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Load Previous Game");
            Console.WriteLine("3. How to Play");
            Console.WriteLine("4. Start Game");
            Console.ReadLine();
        }







        static void Main(string[] args)
        {
            string direction;
            DisplayTitleScreen();
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
