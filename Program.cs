using System;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Media;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.ComponentModel;
using System.Numerics;
using System.IO;

namespace SnailMate
{
    internal class Program
    {
        
        public static int snailDistance = 15, blood = 5, inventoryCount = 0, soundID = 0, count = 0, death = 0, ded = 0;
        public static string text = "\0";
        public static string[] inventory = new string[10];
        int userMenuSelection;
        public static bool exitGame = false;
        public static StreamReader sr = new StreamReader($@"Room-by-Room\1-2\frame (1).txt");
       


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
            soundID = 11;
            text = @"
Welcome to Snailmate, adventurer!
You will be thrust into a strange and unknown place with threats around any corner, so be canny, and be wise.
If you're capable of that.

In order to interact with the world describe what you want to do in simple terms,
such as:
'go left' 
'look at door'
'grab key'.

If a command is not accepted you may have to try other ways of describing your action";
            Typewriter(text);
            Console.ReadLine();


        }
        public static void Animations(ref int animationID)// room transition animations - Rhys
        {
            //the case numbers are direction, so 12 is room1  room2, and 21 is room2 to room1
            string aline;
            switch (animationID)
            {
                case 12://1 to 2
                    for (int i = 1; i <= 24; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\1-2\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 13://1 to 3
                    for (int i = 1; i <= 24; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\1-3\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                            
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 32://3 to 2
                    for (int i = 1; i <= 23; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\3-2\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 34://3 to 4
                    for (int i = 1; i <= 26; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\3-4\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 45://4 to 5
                    for (int i = 1; i <= 26; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\4-5\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 56://5 to 6
                    for (int i = 1; i <= 26; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\5-6\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 57://5 to 7
                    for (int i = 1; i <= 26; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\5-7\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 78://7 to 8
                    for (int i = 1; i <= 26; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\7-8\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 89://8 to 9
                    for (int i = 1; i <= 26; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\8-9\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 910://9 to 10
                    for (int i = 1; i <= 27; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\9-10\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                    // reverse animations start here
                case 21://2 to 1
                    for (int i = 24; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\1-2\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 31://3 to 1
                    for (int i = 24; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\1-3\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 23://2 to 3
                    for (int i = 23; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\2-3\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 43://4 to 3
                    for (int i = 26; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\3-4\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 54://5 to 4
                    for (int i = 26; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\4-5\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 65://6 to 5
                    for (int i = 26; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\5-6\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 75://7 to 5
                    for (int i = 26; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\5-7\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 87://8 to 7
                    for (int i = 26; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\7-8\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 98://9 to 8
                    for (int i = 26; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\8-9\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                case 109://10 to 9
                    for (int i = 27; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\9-10\frame ({i}).txt");
                        while (!sr.EndOfStream)
                        {
                            aline = sr.ReadLine();
                            Console.WriteLine(aline);
                        }
                        sr.Close();
                        Thread.Sleep(83);
                        Console.Clear();
                    }
                    break;
                //need win and death animations
                //death normal
                case 1:
                    Random random = new Random();
                    int deathSelect;
                    deathSelect = random.Next(61);
                    switch(deathSelect)
                    {
                        case <=55:
                            sr = new StreamReader($@"death-screen-stuff\normal\youDied.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;
                        case 56:
                            sr = new StreamReader($@"death-screen-stuff\normal\bloodless.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;
                        case 57:
                            sr = new StreamReader($@"death-screen-stuff\normal\missionCritical.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;
                        case 58:
                            sr = new StreamReader($@"death-screen-stuff\normal\motherF.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;
                        case 59:
                            sr = new StreamReader($@"death-screen-stuff\normal\slimey.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;
                        case 60:
                            sr = new StreamReader($@"death-screen-stuff\normal\snailTime.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;
                        default:
                            sr = new StreamReader($@"death-screen-stuff\normal\youDied.txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(2500);
                            Console.Clear();
                            //runGame = 0;
                            ded = 1;//makes you die
                            break;

                    }
                    break;
                 //eldritch snail monster death
                case 2:
                    sr = new StreamReader($@"death-screen-stuff\monster\pt1.txt");
                    while (!sr.EndOfStream)
                    {
                        aline = sr.ReadLine();
                        Console.WriteLine(aline);
                    }
                    sr.Close();
                    Thread.Sleep(2000);
                    Console.Clear();

                    sr = new StreamReader($@"death-screen-stuff\monster\pt2.txt");
                    while (!sr.EndOfStream)
                    {
                        aline = sr.ReadLine();
                        Console.WriteLine(aline);
                    }
                    sr.Close();
                    Thread.Sleep(2000);
                    Console.Clear();

                    sr = new StreamReader($@"death-screen-stuff\monster\pt3.txt");
                    while (!sr.EndOfStream)
                    {
                        aline = sr.ReadLine();
                        Console.WriteLine(aline);
                    }
                    sr.Close();
                    Thread.Sleep(2000);
                    Console.Clear();
                    //runGame = 0;
                    ded = 1;//makes you die
                    break;
                    ded = 1;
                default:
                    Console.WriteLine("animation not found");
                    break;
                    

            }
        }



        public static void SnailCheck()// call this to check distance of snail - Rhys
        {
            //for now will count down each time is called
            snailDistance -= 1;
            if (snailDistance >= 10)
            {
                Console.WriteLine("The threat is distant.");
            }
            else if (snailDistance >= 5 && snailDistance < 10)
            {
                Console.WriteLine("The threat draws nearer.");
            }
            else if (snailDistance < 5 && snailDistance >1)
            {
                Console.WriteLine("Breathe softly, it's very close now.");
            }
            else if (snailDistance == 1)
            {
                Console.WriteLine("It's right behind you.");
            }
            else
            {
                Console.WriteLine("oh no");
                Thread.Sleep(500);
                int animationID = 1;
                Animations(ref animationID);
                ded = 1;
                
            }
        }
        
        public static void Appease()// makes the snail go further away - rhys
        {
            string temp;
            if (blood > 2)
            {
                snailDistance += 5;
                blood -= 1;
                Console.WriteLine("The sacrifice is accepted.");
            }
            else if (blood == 2)
            {
                Console.WriteLine(@"Are you sure? This will be your last: y/n");
                temp = Console.ReadLine();
                temp = temp.ToLower().Trim();

                if (temp == "y" || temp == "yes")
                {
                    snailDistance += 5;
                    blood -= 1;
                    Console.WriteLine("The sacrifice is accepted.");
                }
                else
                {
                    Console.WriteLine("Very well.");
                }
            }
            else if (blood == 1)
            {
                Console.WriteLine("That would kill you. No.");
            }

        }

        public static void DeathCheck(out int runGame)// rhys method to check if ded
        {
            if (ded != 0)
            {
                runGame = 0;
            }
            else
            {
                runGame = 1;
            }
        }

        public static void NewGame()// Game code
        {
            int runGame = 1;
            int animationID = 0;
            int roomID = 0, door2lock = 1;
            string direction;
            Keys doorTwoKey = new Keys();
            doorTwoKey.Type = "Big Rusty Key"; doorTwoKey.Description = ""; ; doorTwoKey.DoorUnlock = 2;
            Console.Clear();
            while (runGame == 1)// while game is running will loop through whatever room is selected
            {
                SnailCheck();
                DeathCheck(out runGame);
                switch (roomID)
                {
                    case 0: //Just changing this text to roomID 0 so it won't appear if they re-enter room 1 through-out the game. - Cat
                        soundID = 0;
                        SoundPlayer(soundID);
                        text = "Hello, you are in a room, a snail wants to kill you, good luck :3";
                        Typewriter(text);
                        roomID = 1;
                        break;

                    case 1:
                        //room1
                        soundID = 1;
                        SoundPlayer(soundID);
                        if (count == 0) //Makes it so a different dialogue shows if they pick an option and didn't work so they restart the room. - Cat
                        {
                            text = "\nThere is a door on the far side of the room and a set of stairs to the right.\nWhat would you like to do? "; //Working on getting sound and text to sync up - Cat
                        }
                        else
                        {
                            text = "gdrg"; //Thomas need type
                        }
                        soundID = 11;
                        Typewriter(text);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "right":
                                text = "You climb the stairs on the right of the room to the door. ";
                                AddToInventory("Rusty Key");
                                Typewriter(text);
                                if (door2lock == 1)
                                {
                                    {
                                        bool hasKey = inventory.Contains("Rusty Key");

                                        if (hasKey)
                                        {
                                            Console.WriteLine("You use the Rusty Key to unlock the door.");
                                            door2lock = 0; // unlocks door
                                            DropFromInventory("Rusty Key"); //remove key after use
                                        }
                                        else
                                        {
                                            Console.WriteLine("The door is locked. You need a key.");
                                            break;
                                        }
                                    }
                                    Console.WriteLine("The door is unlocked.");
                                    animationID = 12;
                                    Animations(ref animationID);
                                    roomID = 2;//changes room to room 2 and starts it
                                }
                                else
                                {
                                    text = "The door is locked so you move back to where you started.";
                                    Typewriter(text);
                                }

                                break;
                            case "forward":
                                Console.WriteLine("The door ahead of you opens.");
                                Console.WriteLine("Going to Room 3.");
                                animationID = 13;
                                Animations(ref animationID);
                                roomID = 3;
                                break;
                            case "left":
                                Console.WriteLine("That is a wall.");
                                break;
                            case "up":
                                Console.WriteLine("You can't fly, you twit.");
                                break;
                            case "down":
                                Console.WriteLine("You sit on the floor and meditate... the snail catches and kills you");
                                break;
                            default:
                                Console.WriteLine("You thought you were smart, huh? What other direction did you think you could go in?");
                                break;
                        }
                        break;

                    //setting up rooms and the correct relations between them for movement - rhys 13/05/23 12:09am
                    case 2:
                        //room2
                        soundID = 2;
                        SoundPlayer(soundID);
                        text = @"You're suddenly in a another room. There's a corner in front of you to the left. 
You can't see what's beyond it. It could be interesting if you were feeling courageous. 
But we all that know that that's a stretch.";
                        Typewriter(text);
                        Console.Write("What would you like to do? ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "left":
                                Console.WriteLine("going to room1");
                                animationID = 21;
                                Animations(ref animationID);
                                roomID = 1; //goes back to room 1;
                                break;
                            case "back":
                                Console.WriteLine("Back the way you came? Alright then, have it your way.");
                                animationID = 23;
                                Animations(ref animationID);
                                roomID = 3; //teleport to room 3 as per map
                                break;
                        }
                        break;
                    case 3:
                        //room 3
                        Console.WriteLine("You're in what appears to be a new room. There is a door at the other end, and a corner on the left, halfway between you and door.");
                        Console.Write("What would you like to do? ");
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                Console.WriteLine("Back to where you just came from? You do realise the goal is to win the game, right?");
                                animationID = 31;
                                Animations(ref animationID);
                                roomID = 1; //goes back to room 1;
                                break;
                            case "forward":
                                Console.WriteLine("Could be a useful choice, or maybe not. Are you clever enough to figure out which?");
                                animationID = 32;
                                Animations(ref animationID);
                                roomID = 2; //teleport to room 2 as per map
                                break;
                            case "left":
                                Console.WriteLine("Further into the maze, eh? Ain't no snail gonna catch you, clearly.");
                                animationID = 34;
                                Animations(ref animationID);
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
                                animationID = 43;
                                Animations(ref animationID);
                                roomID = 3; //goes back to room 3;
                                break;
                            case "down":
                                Console.WriteLine("going to room5");
                                animationID = 45;
                                Animations(ref animationID);
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
                                animationID = 54;
                                Animations(ref animationID);
                                roomID = 4; //goes back to room 4;
                                break;
                            case "left": //doing opposite to map because of the way a player would be facing after having gone this way, we should make this clearer -Rhys
                                Console.WriteLine("going to room6");
                                animationID = 56;
                                Animations(ref animationID);
                                roomID = 6; //goes to room 6
                                break;
                            case "right":
                                Console.WriteLine("going to room7");
                                animationID = 57;
                                Animations(ref animationID);
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
                                animationID = 65;
                                Animations(ref animationID);
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
                                animationID = 75;
                                Animations(ref animationID);
                                roomID = 5; //goes back to room 5;
                                break;
                            case "forward":
                                Console.WriteLine("going to room8");
                                animationID = 78;
                                Animations(ref animationID);
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
                                animationID = 87;
                                Animations(ref animationID);
                                roomID = 7; //goes back to room 7;
                                break;
                            case "up":
                                Console.WriteLine("climbing ladder to room9");
                                animationID = 89;
                                Animations(ref animationID);
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
                                animationID = 98;
                                Animations(ref animationID);
                                roomID = 8; //goes back to room 9;
                                break;
                            case "forward":

                                Console.WriteLine("you win!");
                                animationID = 0; //need to add a win animation
                                Animations(ref animationID);
                                runGame = 0; //return to menu
                                break;
                            case "right":
                                Console.WriteLine("going to room10");
                                animationID = 910;
                                Animations(ref animationID);
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
                                animationID = 109;
                                Animations(ref animationID);
                                roomID = 9; //goes back to room 9;
                                break;
                            case "forward":

                                Console.WriteLine("you fall off the ledge");
                                animationID = 2; //added death
                                Animations(ref animationID);
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
            string exitText = "EXITING GAME IN: ";
            int x = (Console.WindowWidth - exitText.Length) / 2;
            int y = Console.WindowHeight / 2;
            Console.Clear();
            for (int i = 5; i > 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(x, y);
                Console.CursorVisible = false;
                Console.Write(exitText);
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Clear();
            }
            exitGame = true;

        }

        public static void AddToInventory(string item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    Console.WriteLine($"You added {item} from your inventory.");
                    inventory[i] = item;
                    inventoryCount++;
                    return;
                }
            }
        }

        public static void DropFromInventory(string item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] != null && inventory[i] == item)
                {
                    Console.WriteLine($"You dropped {item} from your inventory.");
                    inventory[i] = null;
                    inventoryCount--;
                    return;
                }
            }
        }

        public static void InspectItem(string item)
        {
         // Need to add inspect feature once we implement items
        }
        
        static void Main(string[] args)
        {
            
            int userMenuSelection;

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


        static void SoundPlayer(int SoundID) //Cat - Adding soundplayer, doesn't error now.
        {
            SoundPlayer player = new SoundPlayer();
            switch (soundID)
            {
                case 0:
                    player.SoundLocation = Environment.CurrentDirectory + @"\Intro.wav";
                    break;
                case 1:
                    player.SoundLocation = Environment.CurrentDirectory + @"\Room1.wav";
                    break;
                case 11:
                    player.SoundLocation = Environment.CurrentDirectory + @"\HowToPlay.wav";
                    break;
                case 12: //Play what would you like to do
                    player.SoundLocation = Environment.CurrentDirectory + @"\HowToPlay.wav";
                    break;
            }
            player.Play();
        }

        public static void Typewriter(string text)
        {
            int delay = 12;//reduced delay from 37 to 12 so it types faster
            foreach (char c in text )
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }
        
    }
}

        
