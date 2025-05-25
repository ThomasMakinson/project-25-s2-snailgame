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
using static System.Net.Mime.MediaTypeNames;

namespace SnailMate
{
    internal class Program
    {
        
        public static int snailDistance = 15, blood = 5, inventoryCount = 0, soundID = 0, death = 0, ded = 0, delay = 37, roomID = 0;
        public static string text = "\0";
        public static items[] inventory = new items[10];
        public static bool exitGame = false;
        public static StreamReader sr = new StreamReader($@"Room-by-Room\1-2\frame (1).txt");
        public static items rustyKey = new items { Name = "Rusty Key", Type = "Key", Description = "Feel free to add description, otherwise i can -KF", Material = "Metal", Condition = "Weathered", RoomID = 3 };
        public static items crumpledNote = new items { Name = "Crumpled Note", Type = "Note", Description = "Feel free to add description, otherwise i can -KF", Material = "Paper", Condition = "Fragile", RoomID = 3 };
        public static items harmonica = new items { Name = "Harmonica", Type = "Instrument", Description = "Feel free to add description, otherwise i can -KF", Material = "Brass", Condition = "Wet", RoomID = 3 };
        public static items slimeyKey = new items { Name = "Slimey Key", Type = "Key", Description = "Feel free to add description, otherwise i can -KF", Material = "Metal", Condition = "Slimey", RoomID = 3 };
        public static items fidgetSpinner = new items { Name = "Fidget Spinner", Type = "Toy", Description = "Feel free to add description, otherwise i can -KF", Material = "Plastic & Stainless Steel", Condition = "Scratched", RoomID = 3 };
        public static items vaughnsGin = new items { Name = "Bottle of Gin", Type = "Alcohol", Description = "Feel free to add description, otherwise i can -KF", Material = "Glass", Condition = "Pristine", RoomID = 3 };
        public static items unknownPills = new items { Name = "Container of Pills", Type = "Medicine?", Description = "Feel free to add description, otherwise i can -KF", Material = "Plastic & Unknown Substances", Condition = "Old", RoomID = 3 };
        public static items[] itemsList = new items[7]; itemsList[0] = rustyKey; itemsList[1] = crumpledNote; itemsList[2] = harmonica; itemsList[3] = slimeyKey; itemsList[4] = fidgetSpinner; itemsList[5] = vaughnsGin; itemsList[6] = unknownPills;
       


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
                "4. Exit \"\uE11D\""
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
            Typewriter(text, delay);
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
                soundID = 50;
                text = "The threat is distant.";
            }
            else if (snailDistance >= 5 && snailDistance < 10)
            {
                soundID = 51;
                text = "The threat draws nearer.";
            }
            else if (snailDistance < 5 && snailDistance >1)
            {
                soundID = 52;
                text = "Breathe softly, it's very close now.";
            }
            else if (snailDistance == 1)
            {
                soundID = 53;
                text = "It's right behind you.";
            }
            else
            {
                soundID = 54;
                text = "Oh no.";
                Thread.Sleep(500);
                int animationID = 1;
                Animations(ref animationID);
                ded = 1;
                
            }
            SoundPlayer(soundID);
            Typewriter(text, delay);
        }

        public static void SnailCheckStealth() //Rhys 22/05/2025
        {
            if(snailDistance <=0)
            {
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
                text = "The sacrifice is accepted.";
            }
            else if (blood == 2)
            {
                text = "Are you sure? This will be your last: y/n";
                Typewriter(text, delay);
                temp = Console.ReadLine();
                temp = temp.ToLower().Trim();
                if (temp == "y" || temp == "yes")
                {
                    snailDistance += 5;
                    blood -= 1;
                    text = "The sacrifice is accepted.";
                }
                else
                {
                    text = "Very well.";
                }
            }
            else if (blood == 1)
            {
                text = "That would kill you. No.";
            }
            Typewriter(text, delay);
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

        public static void SaveGame()//Rhys saves the game
        {
            //this should write to a savefile
            StreamWriter sw = new StreamWriter(@"save.txt");
            sw.WriteLine(roomID);
            sw.WriteLine(blood);
            sw.WriteLine(snailDistance);
            sw.Close();
        }

        public static void NewGame()// Game code
        {
            Random runJump = new Random(10);
            Random jump = new Random(10);
            int runGame = 1, animationID = 0, door2lock = 1, jumpSuccess = 0;
            int[] first = new int [10];
            string direction;
            char skip;
            bool sound = true; 
            Console.Clear();
            while (runGame == 1)// while game is running will loop through whatever room is selected
            {
                SnailCheck();
                DeathCheck(out runGame);
                switch (roomID)
                {
                    case 0: //Just changing this text to roomID 0 so it won't appear if they re-enter room 1 through-out the game. - Cat
                        soundID = 0;
                        text = "Please full screen the console for the room animations.\nDo you wish to skip the typing animation? Y/N: "; //Asking if user wants to skip text animation, if so, it skips soundplayer too. - cat
                        Typewriter(text, delay);
                        skip = Convert.ToChar(Console.ReadLine().ToLower());
                        if (skip == 'y')
                        {
                            delay = 0;
                            sound = false;
                        }
                        if (sound == true)
                        {
                            SoundPlayer(soundID);
                        }
                        text = "Hello, you are in a room, a snail wants to kill you, good luck :3";
                        Typewriter(text, delay);
                        roomID = 1;
                        break;

                    case 1:
                        //room1
                        soundID = 1;
                        if (sound == true)
                        {
                            SoundPlayer(soundID);
                        }
                        if (first[0] == 0) //Makes it so a different dialogue shows if they pick an option and didn't work so they restart the room. - Cat
                        {
                            text = "\nThere is a door on the far side of the room and a set of stairs to the right.\nWhat would you like to do? "; //Working on getting sound and text to sync up - Cat
                            first[0] = 1;
                        }
                        else
                        {
                            text = "Turning around you see the stairs to your right again and the door you just came from in front of you.\nWhat would you like to do? ";
                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "right":
                                text = "You climb the stairs on the right of the room to the door. ";
                                Typewriter(text, delay);
                                if (door2lock == 1)
                                {
                                    bool hasKey = inventory.Contains(rustyKey);

                                    if (hasKey)
                                    {
                                        text = "You use the Rusty Key to unlock the door.";
                                        door2lock = 0; // unlocks door
                                        DropFromInventory(rustyKey); //remove key after use
                                    }
                                    else
                                    {
                                        text = "The door is locked. You need a key.";
                                        break;
                                    }
                                    Typewriter(text, delay);
                                    text = "The door is unlocked.";
                                    Typewriter(text, delay);
                                    animationID = 12;
                                    Animations(ref animationID);
                                    roomID = 2;//changes room to room 2 and starts it
                                }
                                else
                                {
                                    text = "The door is locked so you move back to where you started.";
                                }
                                break;
                            case "forward":
                                text = "The door ahead of you opens.\nGoing to Room 3.";
                                Typewriter(text, delay);
                                animationID = 13;
                                Animations(ref animationID);
                                roomID = 3;
                                break;
                            case "left":
                                text = "That is a wall.";
                                break;
                            case "up":
                                text = "You can't fly, you twit.";
                                break;
                            case "down":
                                text = "You sit on the floor and meditate... the snail catches and kills you";
                                break;
                            default:
                                text = "You thought you were smart, huh? What other direction did you think you could go in?";
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        Typewriter(text, delay);
                        break;

                    //setting up rooms and the correct relations between them for movement - rhys 13/05/23 12:09am
                    case 2:
                        //room2
                        soundID = 2;
                        if (sound == true)
                        {
                            SoundPlayer(soundID);
                        }
                        if (first[1] == 0)
                        {
                            text = @"You're suddenly in a another room. There's a corner in front of you to the left. 
You can't see what's beyond it. It could be interesting if you were feeling courageous. 
But we all that know that that's a stretch.
What would you like to do? ";
                            first[1] = 1;
                        }
                        else // Second description - Cat
                        {

                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "left":
                                text = "going to room1"; //need to add the branch for getting the key, unlocking the door and going to room 1 -Thomas
                                Typewriter(text, delay);
                                animationID = 21;
                                Animations(ref animationID);
                                roomID = 1; //goes back to room 1;
                                break;
                            case "back":
                                text = "Back the way you came? Alright then, have it your way.";
                                Typewriter(text, delay);
                                animationID = 23;
                                Animations(ref animationID);
                                roomID = 3; //teleport to room 3 as per map
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 3:
                        //room 3
                        if (first[2] == 0)
                        {
                            text = @"You're in what appears to be a new room. There is a door at the other end, and a corner on the left, halfway between you and door.
What would you like to do? ";
                        }
                        else // Second Description - Cat
                        {

                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                text = "Back from whence you came? You do realise the goal is to win the game, right?";
                                Typewriter(text, delay);
                                animationID = 31;
                                Animations(ref animationID);
                                roomID = 1; //goes back to room 1;
                                break;
                            case "forward":
                                text = "Could be a useful choice, or maybe not. Are you clever enough to figure out which?";
                                Typewriter(text, delay);
                                animationID = 32;
                                Animations(ref animationID);
                                roomID = 2; //teleport to room 2 as per map
                                break;
                            case "left":
                                text = "Further into the maze, eh? Ain't no snail gonna catch you, clearly.";
                                Typewriter(text, delay);
                                animationID = 34;
                                Animations(ref animationID);
                                roomID = 4; //goes to room 4
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 4:
                        //room4
                        if (first[3] == 0)
                        {
                            text = @"It is a square (ish), completely blank room. There is rising fog ahead, or is it smoke? There are stairs going down to your left through a person-sized hole in the wall.
What would you like to do?: ";
                            first[3] = 1;
                        }
                        else // Second Description - Cat
                        {

                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "right":
                                text = "going to room 3";
                                Typewriter(text, delay);
                                animationID = 43;
                                Animations(ref animationID);
                                roomID = 3; //goes back to room 3;
                                break;
                            case "forward":
                            case "fog":
                                text = @"As you go towards the fog, you feel slight breeze brush against your face. 
Is this it, have you found where you can escape? Perhaps, but you can't see through the fog. 
You reach the edge of the room, there is a ledge.
What would you like to do?";
                                Typewriter(text, delay);
                                switch (direction)
                                {
                                    case "jump":
                                        text = "You jump into the fog from where you are. Hope you know the laws physics reaaally well..";
                                        Typewriter(text, delay);
                                        jumpSuccess = jump.Next(10); //randomly decides if the jump is successful or not
                                        if (jumpSuccess == 0-2)
                                        {
                                            text = "Apparently a standing jump was enough!.";
                                            Typewriter(text, delay);
                                            //animationID = 44;
                                            //Animations(ref animationID);
                                            roomID = 10; //goes to room 10 in reverse, need to add the reverse part
                                        }
                                        else
                                        {
                                            text = @"You try to get across from a standing jump without knowing where you're going. 
Bad life choice? Yes. You don't jump anywhere near far enough. If there was anything there, you haven't reached it. You scream as you fall.
As you fall, an even larger snail eats you.";
                                            Typewriter(text, delay);
                                            animationID = 1; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                        }
                                        break;
                                    case "running jump":
                                        text = "Looking behind you, you could back to the hallways before this room, and do a running jump. The laws of physics would certainly be more in your favour...";
                                        Typewriter(text, delay);
                                        Thread.Sleep(1000);
                                        text = "You walk back into the hallway. You are the furthest you can from the fog, it's now or never. You start running.";
                                        Typewriter(text, delay);
                                        jumpSuccess = runJump.Next(10); //randomly decides if the jump is successful or not
                                        Thread.Sleep(1000);
                                        if (jumpSuccess == 0 - 4)
                                        {
                                            text = "The run up was a success!";
                                            Typewriter(text, delay);
                                            //animationID = 44;
                                            //Animations(ref animationID);
                                            roomID = 10; //goes to room 10 in reverse, need to add the reverse part
                                        }
                                        else if (jumpSuccess == 5 - 7)
                                        {
                                            text = @"Oof. The run up still wasn't enough. You don't jump anywhere near far enough. If there was anything there, you haven't reached it. You scream as you fall. An even larger snail eats you.";
                                            Typewriter(text, delay);
                                            animationID = 1; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                        }
                                        else if (jumpSuccess == 8 - 9)
                                        {
                                            text = @"There was snail goop on the ground that you didn't notice before. You slip on it as you run, and die. The snail eats your corpse..";
                                            Typewriter(text, delay);
                                            animationID = 1; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                        }
                                        break;
                                    case "back":
                                        text = "going to room 3";
                                        Typewriter(text, delay);
                                        animationID = 43;
                                        Animations(ref animationID);
                                        roomID = 3; //goes back to room 3;
                                        break;
                                    case "climb":
                                    case "down":
                                        text = "As you climb down, an even larger snail is there, and eats you.";
                                        Typewriter(text, delay);
                                        //animationID = 45;
                                        //Animations(ref animationID);
                                        break;
                                    default:
                                        text = "You stand there, contemplating your life choices. The snail finds you and eats you.";
                                        Typewriter(text, delay);
                                        break;
                                }
                                break;
                            case "left":
                            case "down":
                                text = "going to room 5";
                                Typewriter(text, delay);
                                animationID = 45;
                                Animations(ref animationID);
                                roomID = 5; //goes to room 5
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 5:
                        //room5
                        if (first[4] == 0)
                        {
                            text = @"You are at a crossroads. (I mean, it's actually a T-Junction, but crossroads sounds cooler, y'know?).
You can see a dark room with no door to your left, and a well-lit one to your right. One could lead to your salvation, the other could lead to your doom, or both, or neither.
I trust you know which is which.
What would you like to do?: ";
                            first[4] = 1;
                        }
                        else // Second description - Cat
                        {

                        }

                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "up":
                            case "back":
                                text = "Back from whence you came? You do realise the goal is to win the game, right?";
                                Typewriter(text, delay);
                                animationID = 54;
                                Animations(ref animationID);
                                roomID = 4; //goes back to room 4;
                                break;
                            case "left": //doing opposite to map because of the way a player would be facing after having gone this way, we should make this clearer -Rhys
                                text = "The dark room. It matches your soul.";
                                Typewriter(text, delay);
                                animationID = 56;
                                Animations(ref animationID);
                                roomID = 6; //goes to room 6
                                break;
                            case "right":
                                text = "The bright room. Feeling happy are you? We'll fix that.";
                                Typewriter(text, delay);
                                animationID = 57;
                                Animations(ref animationID);
                                roomID = 7; //goes to room 7
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 6:
                        //room6
                        text = @"There is nothing. There is only pitch black. 
You have entered a room that is so blank, it appears to be a black void. 
The void seems to draw you in, it calls to you, it makes you feel welcome, you feel like nothing could stop you -- just kidding! 
You got lost in a trance. The snail finds you and eats you.";
                        Typewriter(text, delay);
                        animationID = 1; //death animation
                        Animations(ref animationID);
                        ded = 1; //makes you die
                        break;
                    case 7:
                        //room7
                        if (first[6] == 0)
                        {
                            text = @"This is a very large room. It is well lit. It feels almost like you've finally escaped, like you've reached the end, and yet, you haven't. There is only an opening to your right.
What would you like to do?: ";
                            first[6] = 1;
                        }
                        else // Second Description - Cat
                        {

                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                text = "Backwards it is then.";
                                Typewriter(text, delay);
                                animationID = 75;
                                Animations(ref animationID);
                                roomID = 5; //goes back to room 5, in reverse
                                break;
                            case "forward":
                                text = "A whole new wooorld!";
                                Typewriter(text, delay);
                                Thread.Sleep(1000);
                                text = "Oh wait, that's copyright. Forget that!";
                                Typewriter(text, delay);
                                animationID = 78;
                                Animations(ref animationID);
                                roomID = 8; //goes to room 8
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 8:
                        //room8
                        if (first[7] == 0)
                        {
                            text = @"You are in a hallway. There is a ladder ahead of you. 
More darkness creeps down over the ladder, preventing you from seeing where it goes. 
Could the snail be at the top waiting for you? There's only one way to find out. 
What would you like to do? ";
                            first[7] = 1;
                        }
                        else // Second Description - Cat
                        { 

                        }

                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                text = "Backwards it is then.";
                                Typewriter(text, delay);
                                animationID = 87;
                                Animations(ref animationID);
                                roomID = 7; //goes back to room 7 in reverse
                                break;
                            case "up":
                            case "climb":
                                text = "Insert climbing animation here.";
                                Typewriter(text, delay);
                                animationID = 89;
                                Animations(ref animationID);
                                roomID = 9; //goes to room 9
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 9:
                        //room 9
                        if (first[8] == 0)
                        {
                            text = @"Another hallway. Smaller though, than the one at the bottom of the ladder. 
To your left, a door, no different than any other that you've encountered. 
To your right, an opening, leading to a large room. Both could be inviting. 
What would you like to do? ";
                            first[8] = 1;
                        }
                        else // Second Decription - Cat
                        {

                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "down":
                                text = "Backwards it is, then.";
                                Typewriter(text, delay);
                                animationID = 98;
                                Animations(ref animationID);
                                roomID = 8; //goes back to room 8 in reverse
                                break;
                            case "forward":

                                text = "you win!";
                                Typewriter(text, delay);
                                animationID = 0; //need to change this to the win "room"
                                Animations(ref animationID);
                                runGame = 0; //return to menu
                                break;
                            case "right":
                                text = "Let's see what's this way.";
                                Typewriter(text, delay);
                                animationID = 910;
                                Animations(ref animationID);
                                roomID = 10; //goes to room 10
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                    case 10:
                        //room10
                        if (first[9] == 0)
                        {
                            text = @"At least you haven't been eaten, yet. 
You feel a slight breeze caress your legs, and there's fog ahead. 
Are you near, or are you even further away? 
The room has an interesting shape, there are angles leading back to the opening you just came from, but there are no other doors. 
What would you like to do? ";
                        }
                        else // Second Description - Cat
                        {

                        }
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                text = "Backwards it is, then.";
                                Typewriter(text, delay);
                                animationID = 109;
                                Animations(ref animationID);
                                roomID = 9; //goes back to room 9;
                                break;
                            case "forward":
                            case "fog":
                                text = @"The fog... is fog. It's very... foggy? If there is anything there, you can't see it. 
What would you like to do?";
                                Typewriter(text, delay);
                                animationID = 2; //added death
                                Animations(ref animationID);
                                runGame = 0; //return to menu
                                break;
                            case "blood":
                            case "give blood":
                            case "leave blood":
                            case "bleed":
                            case "appease":
                                Appease();
                                break;
                            case "check danger":
                            case "danger":
                            case "snail":
                            case "snailcheck":
                            case "snail check":
                            case "how far?":
                            case "am I going to die?":
                                SnailCheck();
                                break;
                            case "save":
                                SaveGame();
                                break;
                        }
                        break;
                }
            }
        }

        public static void LoadGame()
        {
            //string aline;
            StreamReader sr = new StreamReader(@"save.txt");
            roomID = Convert.ToInt32(sr.ReadLine());
            blood = Convert.ToInt32(sr.ReadLine());
            snailDistance = Convert.ToInt32(sr.ReadLine());
            NewGame();
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

        public static void AddToInventory(items item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    text = $"You added {item} from your inventory.";
                    Typewriter(text, delay);
                    inventoryCount++;
                    return;
                }
            }
        }

        public static void DropFromInventory(items item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] != null && inventory[i] == item)
                {
                    text = $"You dropped {item} from your inventory.";
                    Typewriter(text, delay);
                    inventory[i] = null;
                    inventoryCount--;
                    return;
                }
            }
        }
        
        static void Main(string[] args)
        {
            
            int userMenuSelection;

            do
            {
                // Displays title screen method then asks for a menu option
                DisplayTitleScreen();
                Console.WriteLine("Select Option (Enter Number): ");
                userMenuSelection = Convert.ToInt32(Console.ReadLine());

                switch (userMenuSelection)
                {
                    case 1:
                        roomID = 0;
                        blood = 5;
                        snailDistance = 15;
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
            switch (soundID)                                                        // Adding seperate files for each piece of dialogue - Cat
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

        public static void Typewriter(string text , int delay) //Setting up typewriter and delay based on if they skip dialogue. - Cat
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        public static void checkRoomItems(int roomID)
        {
            if (rustyKey.RoomID == roomID)
            {
                Console.WriteLine("A rusty key lies on the ground.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(rustyKey);
                    rustyKey.RoomID = -1;
                    Console.WriteLine("You picked up the Rusty Key.");
                }
                else
                {
                    Console.WriteLine("You leave the Rusty Key where it is.");
                }
            }

            if (slimeyKey.RoomID == roomID)
            {
                Console.WriteLine("A slime-drenched key rests on the floor. You really hope it didn’t come from the snail.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(slimeyKey);
                    slimeyKey.RoomID = -1;
                    Console.WriteLine("You picked up the Slimey Key.");
                }
                else
                {
                    Console.WriteLine("You leave the Slimey Key where it is.");
                }
            }

            if (harmonica.RoomID == roomID)
            {
                Console.WriteLine("A slightly dented harmonica lies nearby. It looks like it’s seen things. Emotional things.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(harmonica);
                    harmonica.RoomID = -1;
                    Console.WriteLine("You picked up the Harmonica.");
                }
                else
                {
                    Console.WriteLine("You leave the Harmonica where it is.");
                }
            }

            if (vaughnsGin.RoomID == roomID)
            {
                Console.WriteLine("A full bottle of expensive-looking gin rests on a dusty shelf. It’s the only thing in the room without dust.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(vaughnsGin);
                    vaughnsGin.RoomID = -1;
                    Console.WriteLine("You picked up the Bottle of Gin.");
                }
                else
                {
                    Console.WriteLine("You leave the Bottle of Gin where it is.");
                }
            }

            if (crumpledNote.RoomID == roomID)
            {
                Console.WriteLine("A crumbled piece of paper sticks out from under a cracked tile.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(crumpledNote);
                    crumpledNote.RoomID = -1;
                    Console.WriteLine("You picked up the Crumpled Note.");
                }
                else
                {
                    Console.WriteLine("You leave the Crumpled Note where it is.");
                }
            }

            if (fidgetSpinner.RoomID == roomID)
            {
                Console.WriteLine("A brightly colored fidget spinner gleams unnaturally in the corner.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(fidgetSpinner);
                    fidgetSpinner.RoomID = -1;
                    Console.WriteLine("You picked up the Fidget Spinner.");
                }
                else
                {
                    Console.WriteLine("You leave the Fidget Spinner where it is.");
                }
            }

            if (unknownPills.RoomID == roomID)
            {
                Console.WriteLine("A small bottle of unlabelled pills sits ominously on a desk.");
                Console.Write("Would you like to pick it up? (yes/no): ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "yes")
                {
                    AddToInventory(unknownPills);
                    unknownPills.RoomID = -1;
                    Console.WriteLine("You picked up the Unknown Pills.");
                }
                else
                {
                    Console.WriteLine("You leave the Unknown Pills where they are.");
                }
            }
        }
    }
}