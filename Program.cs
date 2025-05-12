using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace SnailMate
{
    internal class Program
    {
        public static void DisplayTitleScreen()
        {
            // SnailMate art I generated online and turned into a string array - Kavarn 12/05/25 11:18am
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

            // Menu Options in a string array so i can centre them easily with a loop - Kavarn 11:22am
            string[] menuOptions = new string[]
            {
                "1. Start Game",
                "2. Load Game",
                "3. How to Play",
                "4. Exit"
            };

            // Variables I am using to centre the text and menu options - Kavarn 11:32am
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int verticalStart = (screenHeight / 2) - (asciiArt.Length + menuOptions.Length) / 2;

            // Clearing console to start the method - Kavarn 11:37am
            Console.Clear();

            // Running through each lines of the ascii art array to centre them - Kavarn 11:40am
            for (int i = 0; i < asciiArt.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                int centredx = (screenWidth - asciiArt[i].Length) / 2;
                Console.SetCursorPosition(centredx, verticalStart + i);
                Console.WriteLine(asciiArt[i]);
            }

            Console.ResetColor();

            // Display and centre menu options - Kavarn 11:46am
            int menuStartVertical = verticalStart + asciiArt.Length + 2;
            for (int i = 0; i < menuOptions.Length; i++)
            {
                int centredx = (screenWidth - menuOptions[i].Length) / 2;
                Console.SetCursorPosition(centredx, menuStartVertical + i);
                Console.WriteLine(menuOptions[i]);
            }
        }

        public static void HowToPlay()
        {
            // Insert game instruction menu
            Console.WriteLine(@"
Welcome to Snailmate adventurer!
you will be thrust into a strange and unknown place with threats around any corner, so be canny, and be wise.

in order to interact with the world describe what you want to do in simple terms,
such as:
'go left' 
'look at door'
'grab key.
if a command is not accepted you may have to try other ways of describing your action");



        }

        public static void NewGame()// Game code
        {
            int roomID = 1, runGame = 1;
            string direction;
            DisplayTitleScreen();
            while (runGame == 1)// while game is running will loop through whatever room is selected
            {
                switch (roomID) 
                {
                    case 1:



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
                                roomID = 2;//changes room to room 2 and starts it
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
                        break;

                    case 2:
                        Console.WriteLine("you see a door with a big sign marked exit in front of you, behind you waits the door you came through");
                        Console.Write("which direction do you want to go?\nback or forward?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                roomID = 1; //goes back to room 1;
                                break;
                            case "forward":
                                Console.WriteLine("you win!");
                                runGame = 0; //return to menu
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                }
            }
        }

        public static void LoadGame()
        {
            // Load game menu
        }

        public static void ExitGame()
        {
            // exit game process
        }

        static void Main(string[] args)
        {
            int userMenuSelection;
            bool exitGame = false;

            do
            {
                // Displays title screen method then asks for a menu option
                DisplayTitleScreen();
                Console.Write("Select Option (Enter Number): ");
                userMenuSelection = Convert.ToInt32(Console.ReadLine());

                switch (userMenuSelection)
                {
                    case 1:
                        NewGame();
                        break;

                    case 2:
                        LoadGame();
                        break;

                    case 3:
                        HowToPlay();
                        break;

                    case 4:
                        ExitGame();
                        break;

                }
            } while (exitGame == false);


        }
    }
}
