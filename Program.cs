using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace SnailMate
{
    internal class Program
    {
        public static int snailDistance;
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
            // Inserted game instruction menu basic version -Rhys 12/05/25 3:06pm
            Console.Clear();
            Console.WriteLine(@"
Welcome to Snailmate, adventurer!
You will be thrust into a strange and unknown place with threats around any corner, so be canny, and be wise.
If you're capable of that.

In order to interact with the world, describe what you want to do in simple terms,
such as:
'go left' 
'look at door'
'grab key'.

If a command is not accepted you may have to try other ways of describing your action");
            Console.ReadLine();


        }
        public static void Snailcheck()
        {
            if( snailDistance >10)
            {
                Console.WriteLine("The threat is distant");
            }
            else if (snailDistance >= 5 && snailDistance < 10)
            {
                Console.WriteLine("The threat draws nearer");
            }
            else if(snailDistance <5)
            {
                Console.WriteLine("Breathe softly, it's very close now");
            }
            else if(snailDistance <=1)
            {
                Console.WriteLine("It's right behind you");
            }
        }

        public static void NewGame()// Game code
        {
            
            int roomID = 1, runGame = 1, door2lock = 1;
            string direction;
            Console.Clear();
            while (runGame == 1)// while game is running will loop through whatever room is selected
            {
                switch (roomID) 
                {
                    case 1:
                        //room1


                        Console.WriteLine(@"Hello, You are in a room, a snail wants to kill you, good luck :3
there is a door on the far side of the room and a set of stairs to the right");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "right":
                                Console.WriteLine("you climb the stairs on the right of the room to the door");
                                if (door2lock == 0)
                                {
                                    Console.WriteLine("the door is unlocked");
                                    roomID = 2;//changes room to room 2 and starts it
                                }
                                else
                                {
                                    Console.WriteLine("the door is locked");
                                }
                                
                                break;
                            case "forward":
                                Console.WriteLine("the door ahead of you opens");
                                Console.WriteLine("going to room3");
                                roomID = 3;
                                break;
                            case "left":
                                Console.WriteLine("that is a wall");
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

                        //setting up rooms and the correct relations between them for movement - rhys 13/05/23 12:09am
                    case 2:                      
                        //room2
                        Console.WriteLine("room 2 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "left":
                                Console.WriteLine("going to room1");
                                roomID = 1; //goes back to room 1;
                                break;
                            case "back":
                                Console.WriteLine("going to room 3");
                                roomID = 3; //teleport to room 3 as per map
                                break;
                        }
                        break;
                    case 3:
                        //room 3
                        Console.WriteLine("room 3 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                Console.WriteLine("going to room1");
                                roomID = 1; //goes back to room 1;
                                break;
                            case "forward":
                                Console.WriteLine("going to room2");
                                roomID = 2; //teleport to room 2 as per map
                                break;
                            case "left":
                                Console.WriteLine("going to room4");
                                roomID = 4; //goes to room 4
                                break;
                        }
                        break;
                    case 4:
                        //room4
                        Console.WriteLine("room 4 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "right":
                                Console.WriteLine("going to room3");
                                roomID = 3; //goes back to room 3;
                                break;
                            case "down":
                                Console.WriteLine("going to room5");
                                roomID = 5; //goes to room 5
                                break;
                        }
                        break;
                    case 5:
                        //room5
                        Console.WriteLine("room 5 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "up":
                                Console.WriteLine("going to room4");
                                roomID = 4; //goes back to room 4;
                                break;
                            case "left": //doing opposite to map because of the way a player would be facing after having gone this way, we should make this clearer -Rhys
                                Console.WriteLine("going to room6");
                                roomID = 6; //goes to room 6
                                break;
                            case "right":
                                Console.WriteLine("going to room7");
                                roomID = 7; //goes to room 7
                                break;
                        }
                        break;
                    case 6:
                        //room6
                        Console.WriteLine("room 6 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                Console.WriteLine("going to room5");
                                roomID = 5; //goes back to room 5;
                                break;
                            case "forward":
                                
                                Console.WriteLine("dead end");
                                
                                break;
                        }
                        break;
                    case 7:
                        //room7
                        Console.WriteLine("room 7 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                Console.WriteLine("going to room5");
                                roomID = 5; //goes back to room 5;
                                break;
                            case "forward":
                                Console.WriteLine("going to room8");
                                roomID = 8; //goes to room 8
                                break;
                        }
                        break;
                    case 8:
                        //room8
                        Console.WriteLine("room 8 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                Console.WriteLine("going to room7");
                                roomID = 7; //goes back to room 7;
                                break;
                            case "up":
                                Console.WriteLine("climbing ladder to room9");
                                roomID = 9; //goes to room 9
                                break;
                        }
                        break;
                    case 9:
                        //room 9
                        Console.WriteLine("room 9 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "down":
                                Console.WriteLine("going to room9");
                                roomID = 8; //goes back to room 9;
                                break;
                            case "forward":
                                
                                Console.WriteLine("you win!");
                                runGame = 0; //return to menu
                                break;
                            case "right":
                                Console.WriteLine("going to room10");
                                roomID = 10; //goes to room 10
                                break;
                        }
                        break;
                    case 10:
                        //room10
                        Console.WriteLine("room 10 descript");
                        Console.Write("What would you like to do?: ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                Console.WriteLine("going to room9");
                                roomID = 9; //goes back to room 9;
                                break;
                            case "forward":
                                
                                Console.WriteLine("you fall off the ledge");
                                runGame = 0; //return to menu
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
