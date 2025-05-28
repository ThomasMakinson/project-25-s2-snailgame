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
using System.Threading;
using NAudio.CoreAudioApi;

namespace SnailMate
{
    internal class Program
    {
        public static int snailDistance = 15, blood = 5, inventoryCount = 0, soundID = 0, death = 0, ded = 0, delay = 37, roomID = 0, count = 0;
        public static string text = "\0";
        public static items[] inventory = new items[10];
        public static bool exitGame = false, sound = true;
        public static StreamReader sr = new StreamReader($@"Room-by-Room\1-2\frame (1).txt");
        public static items rustyKey = new items { Name = "Rusty Key", Type = "Key", Description = "It definitely opens something. Probably. Maybe.", Material = "Metal", Condition = "Weathered", RoomID = 2 };
        public static items crumpledNote = new items { Name = "Crumpled Note", Type = "Note", Description = "- Day 12. The walls are closing in. I've named the snail Dale. I don't think he likes it.", Material = "Paper", Condition = "Fragile", RoomID = 3 };
        public static items harmonica = new items { Name = "Harmonica", Type = "Instrument", Description = "It's damp. It drips. It smells faintly of jazz and failure.", Material = "Brass", Condition = "Wet", RoomID = 7 };
        public static items slimeyKey = new items { Name = "Slimey Key", Type = "Key", Description = "It's dripping. You're 80% sure the snail did this. You're 100% not okay with it.", Material = "Metal", Condition = "Slimey", RoomID = 10 };
        public static items fidgetSpinner = new items { Name = "Fidget Spinner", Type = "Toy", Description = "It's warm. It vibrates slightly. You probably shouldn't touch it. You're going to touch it.", Material = "Plastic & Stainless Steel", Condition = "Scratched", RoomID = 1 };
        public static items vaughnsGin = new items { Name = "Bottle of Gin", Type = "Alcohol", Description = "The label reads: 'Vaughn's Gin' You freeze. That name... why does it feel familiar? ", Material = "Glass", Condition = "Pristine", RoomID = 8 };
        public static items unknownPills = new items { Name = "Unknown Pills", Type = "Medicine?", Description = "The label is scratched off. They look like painkillers, but they feel like a dare.", Material = "Plastic & Unknown Substances", Condition = "Old", RoomID = 4 };       


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
            soundID = 2;
            delay = 48;
            text = @"Welcome to SnailMate, adventurer!
You will be thrust into a strange and unknown place with threats around any corner, so be canny, and be wise.
If you're capable of that.

In order to interact with the world, describe what you want to do in simple terms,
such as:
'left, right, forward or back' 
'look at door'
'grab/pick up x'
'check inventory/inventory'
'inspect x'
'use x'

If a command is not accepted, you may have to try other ways of describing your action.";
            SoundPlayer(soundID);
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
                case 410: //4 to 10
                    for (int i = 1; i <= 24; i++)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\4-10\frame ({i}).txt");
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
                case 104:
                    for (int i = 24; i >= 1; i--)
                    {
                        Console.Clear();
                        sr = new StreamReader($@"Room-by-Room\4-10\frame ({i}).txt");
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
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    switch (deathSelect)
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
                    Console.ResetColor();
                    break;
                 //eldritch snail monster death
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
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
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    Console.Clear();
                    //runGame = 0;
                    ded = 1;//makes you die
                    break;
                case 3://win animation, currently looping 4 times
                    for (int j = 1; j <= 4; j++)
                    {
                        for (int i = 1; i <= 21; i++)
                        {
                            Console.Clear();
                            sr = new StreamReader($@"youWin\frame ({i}).txt");
                            while (!sr.EndOfStream)
                            {
                                aline = sr.ReadLine();
                                Console.WriteLine(aline);
                            }
                            sr.Close();
                            Thread.Sleep(70);
                            Console.Clear();
                        }
                    }
                    
                    break;


                default:
                    Console.WriteLine("Animation not found.");
                    break;
                    

            }
        }



        public static void SnailCheck()// call this to check distance of snail - Rhys
        {
            //for now will count down each time is called
            snailDistance -= 1;
            if (snailDistance >= 10)
            {
                soundID = 13;
                text = "\nThe threat is distant.";
            }
            else if (snailDistance >= 5 && snailDistance < 10)
            {
                soundID = 14;
                text = "\nThe threat draws nearer.";
            }
            else if (snailDistance < 5 && snailDistance >1)
            {
                soundID = 15;
                text = "\nBreathe softly, it's very close now.";
            }
            else if (snailDistance == 1)
            {
                soundID = 16;
                text = "\nIt's right behind you.";
            }
            else
            {
                soundID = 17;
                text = "\nOh no.";
                Thread.Sleep(500);
                int animationID = 1;
                Animations(ref animationID);
                ded = 1;
                
            }
            SoundPlayer(soundID);
            Typewriter(text, delay);
            Thread.Sleep(500);
        }

        public static void SnailCheckStealth() //Rhys 22/05/2025
        {
            snailDistance -= 5;
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
            Random jump = new Random(10);
            int[] first = new int [10];
            int runGame = 1, animationID = 0, door2lock = 1, jumpCount = 0;
            string direction;
            Console.Clear();
            while (runGame == 1)// while game is running will loop through whatever room is selected
            {
                SnailCheckStealth(); //has to be the stealth version to unobtrusively count down -Rhys
                DeathCheck(out runGame);
                switch (roomID)
                {
                    case 0: //Just changing this text to roomID 0 so it won't appear if they re-enter room 1 through-out the game. - Cat
                        soundID = 0;
                        SoundPlayer(soundID);
                        text = "Please full screen the console for the room animations.\nTo speed up the typing and narration, press the spacebar."; //Need to rerecord - cat
                        Typewriter(text, delay);
                        Thread.Sleep(1500);
                        delay = 48;
                        soundID = 1;
                        SoundPlayer(soundID);
                        Console.Clear();
                        text = "Hello, you are in a room, a snail wants to kill you, good luck! :3";
                        Typewriter(text, delay);
                        Thread.Sleep(1000);
                        roomID = 1;
                        break;

                    case 1:
                        //room1
                        bool door1lock = true;
                        Console.Clear();
                        if (first[0] == 0) //Makes it so a different dialogue shows if they pick an option and didn't work so they restart the room. - Cat
                        {
                            soundID = 11;
                            SoundPlayer(soundID);
                            delay = 40;
                            text = "There is a door on the far side of the room and a set of stairs to the right."; //Working on getting sound and text to sync up - Cat
                            first[0] = 1;
                        }
                        else
                        {
                            soundID = 12;
                            SoundPlayer(soundID);
                            delay = 32;
                            text = "Oh look you're back where you started. Turning around you see the stairs to your right again and a door in front of you.";
                        }
                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "use rusty key":
                            case "unlock door":
                            case "use key":
                            case "forward":
                                if (door1lock == true)
                                {
                                    bool hasKey = inventory.Contains(rustyKey);
                                    if (hasKey)
                                    {
                                        text = "You use the Rusty Key to unlock the door.";
                                        Thread.Sleep(1000);
                                        door1lock = false; // unlocks door
                                        DropFromInventory(rustyKey); //remove key after use
                                        Typewriter(text, delay);
                                        animationID = 13;
                                        Animations(ref animationID);
                                        roomID = 3;//changes room to room 3 and starts it
                                    }
                                    else
                                    {
                                        text = "The door is locked. You need a key.";
                                        Console.WriteLine(text);
                                        Thread.Sleep(1500);
                                    }
                                                                 
                                }
                                break;
                            case "pick up fidget spinner":
                            case "grab fidget spinner":
                                AddToInventory(fidgetSpinner);
                                Console.WriteLine($"You added {fidgetSpinner.Name} to your Inventory.");
                                Thread.Sleep(1500);
                                fidgetSpinner.RoomID = -1;
                                break;
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                        break;
                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                    { item.Inspect(); break; }
                                break;

                            case "right":
                                text = "You climb the stairs on the right of the room and head through the door.\n";
                                Typewriter(text, delay);
                                animationID = 12;
                                Animations(ref animationID);
                                roomID = 2;//changes room to room 2 and starts it
                                break;    
                            //case "forward":
                                //text = "This door is locked, it looks like you're gonna need a key";
                                //Typewriter(text, delay);
                                //break;
                            case "left":
                                text = "That is a wall.";
                                Typewriter(text, delay);
                                break;
                            case "up":
                                text = "You can't fly, you twit.";
                                Typewriter(text, delay);
                                break;
                            case "down":
                                text = "You sit on the floor and meditate... the snail catches and kills you";
                                Typewriter(text, delay);
                                animationID = 1;
                                Animations(ref animationID);
                                break;
                            
                            case "save":
                                SaveGame();
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
                            default:
                                text = "You thought you were smart, huh? What other direction did you think you could go in?";
                                Typewriter(text, delay);
                                break;
                        }
                        break;

                    //setting up rooms and the correct relations between them for movement - rhys 13/05/23 12:09am
                    case 2:
                        //room2
                        Console.Clear();
                        if (first[1] == 0)
                        {
                            soundID = 21;
                            delay = 32;
                            SoundPlayer(soundID);
                            text = @"You're suddenly in a another room. There's a corner in front of you to the left. 
You can't see what's beyond it. It could be interesting if you were feeling courageous. 
But we all know that that's a stretch.";
                            first[1] = 1;
                        }
                        else // Second description - Cat
                        {
                            soundID = 22;
                            //delay = ?
                            SoundPlayer(soundID);
                            text = "You've been here before. Silly billy, are you going around in circles?";
                        }
                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "pick up rusty key":
                            case "pick up key":
                            case "grab key":
                            case "grab rusty key":
                                AddToInventory(rustyKey);
                                Console.WriteLine($"You added {rustyKey.Name} to your Inventory.");
                                Thread.Sleep(1500);
                                rustyKey.RoomID = -1;
                                break;
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                break;
                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                    { item.Inspect(); break; }
                                break;
                            case "left":
                                animationID = 21;
                                Animations(ref animationID);
                                roomID = 1; //goes back to room 1;
                                break;
                            case "back":
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
                            default:
                                Console.WriteLine("what?");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case 3:
                        //room 3
                        Console.Clear();
                        if (first[2] == 0)
                        {
                            soundID = 31;
                            delay = 43;// fix delay
                            SoundPlayer(soundID);
                            text = "You're in what appears to be a new room. There is a door at the other end, and a corner to the left, halfway between you and the door.";
                        }
                        else // Second Description - Cat
                        {
                            soundID = 32;
                            //delay = ?;
                            SoundPlayer(soundID);
                            text = "You've been here before. Silly billy, are you going around in circles?";
                        }
                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "pick up crumpled note":
                            case "pick up note":
                            case "grab crumpled note":
                            case "grab note":
                                AddToInventory(crumpledNote);
                                Console.WriteLine($"You added {crumpledNote.Name} to your Inventory.");
                                Thread.Sleep(1500);
                                crumpledNote.RoomID = -1;
                                break;
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                break;

                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                    { item.Inspect(); break; }
                                break;
                            case "back":
                                animationID = 31;
                                Animations(ref animationID);
                                roomID = 1; //goes back to room 1;
                                break;
                            case "forward":
                                animationID = 32;
                                Animations(ref animationID);
                                roomID = 2; //teleport to room 2 as per map
                                break;
                            case "left":
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
                            default:
                                Console.WriteLine("what?");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case 4:
                        //room4
                        Console.Clear();
                        if (first[3] == 0)
                        { 
                            sound = true; //testing - cat
                            soundID = 41;
                            delay = 43;// fix delay
                            SoundPlayer(soundID);
                            text = @"It is a square (ish), completely blank room. There is rising fog ahead, or is it smoke? 
There are stairs going down to your left through a person-sized hole in the wall.";
                            Typewriter(text, delay);
                            checkRoomItems(roomID);
                            delay = 37;
                            SoundPlayer(soundID);
                            text = "\nWhat would you like to do? ";
                            Typewriter(text, delay);
                            direction = Console.ReadLine().ToLower().Trim();
                            switch (direction)
                            {
                                case "pick up unknown pills":
                                case "pick up pills":
                                case "grab unknown pills":
                                case "grab pills":
                                    AddToInventory(unknownPills);
                                    Console.WriteLine($"You added {unknownPills.Name} to your Inventory.");
                                    Thread.Sleep(1500);
                                    unknownPills.RoomID = -1;
                                    break;
                                case "inventory":
                                case "check inventory":
                                    items.DisplayInventory(inventory);
                                    break;

                                case var command when command.StartsWith("use "):
                                    foreach (items item in inventory)
                                        if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                        { item.Use(); break; }
                                    break;

                                case var command2 when command2.StartsWith("inspect "):
                                    foreach (items item in inventory)
                                        if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                        { item.Inspect(); break; }
                                    break;

                                case "right":
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
                                    direction = Console.ReadLine().ToLower().Trim();
                                    switch (direction)
                                    {
                                        case "jump":
                                            text = "You jump into the fog from where you are. Hope you know the laws physics reaaally well...\n";
                                            Typewriter(text, delay);
                                            if (jump.Next(10) <= 2)
                                            {
                                                text = "Apparently a standing jump was enough!.";
                                                Typewriter(text, delay);
                                                animationID = 104;
                                                Animations(ref animationID);
                                                roomID = 10; //goes to room 10
                                            }
                                            else
                                            {
                                                text = @"You try to get across from a standing jump without knowing where you're going. 
Bad life choice? Yes. You don't jump anywhere near far enough. If there was anything there, you haven't reached it. You scream as you fall.
As you fall, an even larger snail eats you.";
                                                Typewriter(text, delay);
                                                animationID = 1; //death animation
                                                Animations(ref animationID);
                                                //ded = 1; //makes you die
                                                
                                            }
                                            break;
                                        case "running jump":
                                            text = @"Looking behind you, you could back to the hallways before this room, and do a running jump. The laws of physics would certainly be more in your favour...
You walk back into the hallway. You are the furthest you can from the fog, it's now or never. You start running.";
                                            Typewriter(text, delay);
                                            Thread.Sleep(1000);
                                            if (jump.Next(10) <= 4)
                                            {
                                                text = "The run up was a success!";
                                                Typewriter(text, delay);
                                                jumpCount = 1;
                                                animationID = 104;
                                                Animations(ref animationID);
                                                roomID = 10; //goes to room 10
                                            }
                                            else if (jump.Next(10) == 5 - 7)
                                            {
                                                text = @"Oof. The run up still wasn't enough. You don't jump anywhere near far enough. If there was anything there, you haven't reached it. 
You scream as you fall. An even larger snail eats you.";
                                                Typewriter(text, delay);
                                                animationID = 1; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                            }
                                            else if (jump.Next(10) == 8 - 9)
                                            {
                                                text = @"There was snail goop on the ground that you didn't notice before. You slip on it as you run, and die. 
The snail eats your corpse..";
                                                Typewriter(text, delay);
                                                animationID = 1; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                            }
                                            break;
                                        case "back":
                                            animationID = 43;
                                            Animations(ref animationID);
                                            roomID = 3; //goes back to room 3;
                                            break;
                                        case "climb":
                                        case "down":
                                            text = "As you climb down, an even larger snail is there, and eats you.";
                                            Typewriter(text, delay);
                                            animationID = 1; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                            break;
                                        default:
                                            text = "You stand there, contemplating your life choices. The snail finds you and eats you.";
                                            break;
                                    }
                                    break;
                                case "left":
                                case "down":
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
                                default :
                                    Console.WriteLine("what?");
                                    Thread.Sleep(1000);
                                    break;
                            }
                            first[3] = 1;
                        }
                        else // Second Description - Cat
                        {
                            text = "It is a square (ish), completely blank room. There is rising fog ahead, or is it smoke? There are stairs going down to your left through a person-sized hole in the wall.";
                            Typewriter(text, delay);
                            checkRoomItems(roomID);
                            delay = 37;
                            SoundPlayer(soundID);
                            text = "\nWhat would you like to do? ";
                            Typewriter(text, delay);
                            // added reverse room 4 description here↑
                            checkRoomItems(roomID);
                            direction = Console.ReadLine().ToLower().Trim();
                            switch (direction)
                            {
                                case "right":
                                    animationID = 43;
                                    Animations(ref animationID);
                                    roomID = 3; //goes back to room 3;
                                    break;
                                case "forward":
                                case "fog":
                                    text = @"Jump back across, you know how far it is now. Have fun?
What would you like to do?";
                                    Typewriter(text, delay);
                                    direction = Console.ReadLine().ToLower().Trim();
                                    switch (direction)
                                    {
                                        case "jump":
                                            text = "You jump into the fog from where you are. Hope you know the laws physics reaaally well..";
                                            Typewriter(text, delay);
                                            Thread.Sleep(1000);
                                            text = @"You try to get across from a standing jump without knowing where you're going. 
Bad life choice? Yes. You don't jump anywhere near far enough. You scream as you fall.
As you fall, an even larger snail eats you.";
                                            Typewriter(text, delay);
                                            animationID = 2; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                            break;
                                        case "running jump":
                                            text = "You walk back into the hallway. You are the furthest you can be from the fog, it's now or never. You start running.";
                                            Typewriter(text, delay);
                                            Thread.Sleep(1000);
                                            if (jump.Next(10) < 5) //randomly decides which outcome the player gets
                                            {
                                                text = @"There was snail goop on the ground that you didn't notice before. You slip on it as you run, and die. The snail eats your corpse...";
                                                Typewriter(text, delay);
                                                animationID = 1; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                            }
                                            else if (jump.Next(10) > 5)
                                            {
                                                text = "A bigger snail reaches up through the fog and eats you. That'll teach you.";
                                                Typewriter(text, delay);
                                                animationID = 2; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                            }
                                            break;
                                        case "back":
                                            animationID = 43;
                                            Animations(ref animationID);
                                            roomID = 3; //goes back to room 3;
                                            break;
                                        case "climb":
                                        case "down":
                                            text = "As you climb down, an even larger snail is there, and eats you.";
                                            Typewriter(text, delay);
                                            animationID = 2; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                            break;
                                        default:
                                            text = "You stand there, contemplating your life choices. The snail finds you and eats you.";
                                            break;
                                    }
                                    break;
                                case "left":
                                case "down":
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
                                default:
                                    Console.WriteLine("what?");
                                    Thread.Sleep(1000);
                                    break;
                            }
                        }

                        break;
                    case 5:
                        //room5
                        Console.Clear();
                        if (first[4] == 0)
                        {
                            soundID = 51;
                            delay = 48;
                            SoundPlayer(soundID);
                            text = @"You are at a crossroads. (I mean, it's actually a T-Junction, but crossroads sounds cooler, y'know?).
You can see a dark room with no door to your left, and a well-lit one to your right. One could lead to your salvation, the other could lead to your doom, or both, or neither.
I trust you know which is which.";
                            first[4] = 1;
                        }
                        else // Second description - Cat
                        {
                            soundID = 52;
                            //delay = ?;
                            SoundPlayer(soundID);
                            text = "You've been here before. Silly billy, are you going around in circles?";

                        }
                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                break;
                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                    { item.Inspect(); break; }
                                break;
                            case "up":
                            case "back":
                                animationID = 54;
                                Animations(ref animationID);
                                roomID = 4; //goes back to room 4;
                                break;
                            case "left": //doing opposite to map because of the way a player would be facing after having gone this way, we should make this clearer -Rhys
                                animationID = 56;
                                Animations(ref animationID);
                                roomID = 6; //goes to room 6
                                break;
                            case "right":
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
                            default:
                                Console.WriteLine("what?");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case 6:
                        //room6
                        Console.Clear();
                        text = @"There is nothing. There is only pitch black. 
You have entered a room that is so blank, it appears to be a black void. 
The void seems to draw you in, it calls to you, it makes you feel welcome, you feel like nothing could stop you -- just kidding! 
You got lost in a trance. The snail finds you and eats you.";
                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        animationID = 1; //death animation
                        Animations(ref animationID);
                        ded = 1; //makes you die
                        break;
                    case 7:
                        //room7
                        Console.Clear();
                        if (first[6] == 0)
                        {
                            soundID = 71;
                            delay = 48;
                            SoundPlayer(soundID);
                            text = @"This is a very large room. It is well lit. It feels almost like you've finally escaped, like you've reached the end, and yet, you haven't. 
There is only an opening to your right.";
                            first[6] = 1;
                        }
                        else // Second Description - Cat
                        {
                            soundID = 72;
                            //delay = ?;
                            SoundPlayer(soundID);
                            text = "You've been here before. Silly billy, are you going around in circles?";

                        }
                        Typewriter(text, delay);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "pick up harmonica":
                            case "grab harmonica":
                                AddToInventory(harmonica);
                                Console.WriteLine($"You added {harmonica.Name} to your Inventory.");
                                Thread.Sleep(1500);
                                harmonica.RoomID = -1;
                                break;
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                break;
                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                    { item.Inspect(); break; }
                                break;
                            case "back":
                                animationID = 75;
                                Animations(ref animationID);
                                roomID = 5; //goes back to room 5
                                break;
                            case "forward":
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
                            default:
                                Console.WriteLine("what?");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case 8:
                        //room8
                        Console.Clear();
                        if (first[7] == 0)
                        {
                            soundID = 81;
                            delay = 48;
                            SoundPlayer(soundID);
                            text = @"You are in a hallway. There is a ladder ahead of you. 
More darkness creeps down over the ladder, preventing you from seeing where it goes. 
Could the snail be at the top waiting for you? There's only one way to find out.";
                            first[7] = 1;
                        }
                        else // Second Description - Cat
                        {
                            soundID = 82;
                            //delay = ?;
                            SoundPlayer(soundID);
                            text = "You've been here before. Silly billy, are you going around in circles?";
                        }

                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "pick up bottle of gin":
                            case "pick up gin":
                            case "grab bottle of gin":
                            case "grab gin":
                                AddToInventory(vaughnsGin);
                                Console.WriteLine($"You added {vaughnsGin.Name} to your Inventory.");
                                Thread.Sleep(1500);
                                vaughnsGin.RoomID = -1;
                                break;
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                break;
                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(8).Trim())
                                    { item.Inspect(); break; }
                                break;
                            case "back":
                                animationID = 87;
                                Animations(ref animationID);
                                roomID = 7; //goes back to room 7
                                break;
                            case "up":
                            case "climb":
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
                            default:
                                Console.WriteLine("what?");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case 9:
                        //room 9
                        Console.Clear();
                        if (first[8] == 0)
                        {
                            soundID = 91;
                            delay = 48;
                            SoundPlayer(soundID);
                            text = @"Another hallway. Smaller though, than the one at the bottom of the ladder. 
To your left, a door, no different than any other that you've encountered. 
To your right, an opening, leading to a large room. Both could be inviting.";
                            first[8] = 1;
                        }
                        else // Second Decription - Cat
                        {
                            soundID = 92;
                            //delay = ?;
                            SoundPlayer(soundID);
                            text = "You've been here before. Silly billy, are you going around in circles?";
                        }
                        Typewriter(text, delay);
                        checkRoomItems(roomID);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        bool door9lock = true;
                        switch (direction)
                        {
                            case "use slimey key":
                            case "unlock door":
                            case "use key":
                                if (door9lock == true)
                                {
                                    bool hasKey = inventory.Contains(slimeyKey);
                                    if (hasKey)
                                    {
                                        text = "You use the Slimey Key to unlock the door.";
                                        door9lock = false; // unlocks door
                                        DropFromInventory(slimeyKey); //remove key after use
                                        roomID = 11;
                                    }
                                    else
                                    {
                                        text = "The door is locked. You need a key.";
                                    }
                                    Typewriter(text, delay);
                                    roomID = 11;//win room

                                }
                                break;
                            case "inventory":
                            case "check inventory":
                                items.DisplayInventory(inventory);
                                break;

                            case var command when command.StartsWith("use "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                    { item.Use(); break; }
                                break;
                            case var command2 when command2.StartsWith("inspect "):
                                foreach (items item in inventory)
                                    if (item != null && item.Name.ToLower() == command2.Substring(4).Trim())
                                    { item.Inspect(); break; }
                                break;
                            case "down":
                                animationID = 98;
                                Animations(ref animationID);
                                roomID = 8; //goes back to room 8
                                break;
                            case "forward":
                                text = "You try the door... This one is gonna need a key as well. ";
                                Typewriter(text, delay);
                                break;
                            case "right":
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
                            default:
                                Console.WriteLine("What?");
                                Thread.Sleep(1000);
                                break;
                        }
                        break;
                    case 10:
                        //room10
                        Console.Clear();
                        if (first[9] == 0)
                        {
                            text = @"At least you haven't been eaten, yet. 
You feel a slight breeze caress your legs, and there's fog ahead. 
Are you near, or are you even further away? 
The room has an interesting shape, there are angles leading back to the opening you just came from, but there are no other doors. 
What would you like to do? ";
                            Typewriter(text, delay);
                            checkRoomItems(roomID);
                            direction = Console.ReadLine().ToLower().Trim();
                            switch (direction)
                            {
                                case "pick up slimey key":
                                case "pick up key":
                                case "grab slimey key":
                                case "grab key":
                                    AddToInventory(slimeyKey);
                                    Console.WriteLine($"You added {slimeyKey.Name} to your Inventory.");
                                    Thread.Sleep(1500);
                                    slimeyKey.RoomID = -1;
                                    break;
                                case "inventory":
                                case "check inventory":
                                    items.DisplayInventory(inventory);
                                    break;

                                case var command when command.StartsWith("use "):
                                    foreach (items item in inventory)
                                        if (item != null && item.Name.ToLower() == command.Substring(4).Trim())
                                        { item.Use(); break; }
                                    break;
                                case var command2 when command2.StartsWith("inspect "):
                                    foreach (items item in inventory)
                                        if (item != null && item.Name.ToLower() == command2.Substring(4).Trim())
                                        { item.Inspect(); break; }
                                    break;
                                case "back":
                                    animationID = 109;
                                    Animations(ref animationID);
                                    roomID = 9; //goes back to room 9;
                                    break;
                                case "forward":
                                case "fog":
                                    text = @"The fog... is fog. It's very... foggy? If there is anything there, you can't see it. 
What would you like to do?";
                                    direction = Console.ReadLine().ToLower().Trim();
                                    switch (direction)
                                    {
                                        case "jump":
                                            text = "You jump into the fog from where you are. Hope you know the laws physics reaaally well...";
                                            Typewriter(text, delay);
                                            if (jump.Next(10) <= 2)
                                            {
                                                text = "Apparently a standing jump was enough!.";
                                                Typewriter(text, delay);
                                                jumpCount = 1;
                                                //animationID = 44;
                                                //Animations(ref animationID);
                                                roomID = 4; //goes to room 4
                                            }
                                            else if (jump.Next(10) > 2)
                                            {
                                                text = @"You try to get across from a standing jump without knowing where you're going.
Bad life choice? Yes. You don't jump anywhere near far enough. 
If there was anything there, you haven't reached it. You scream as you fall and the snail eats you.";
                                                Typewriter(text, delay);
                                                animationID = 2; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                                
                                            }
                                            break;
                                        case "running jump":
                                            text = @"Looking behind you, you could back to the hallways before this room, and do a running jump. The laws of physics would certainly be more in your favour...
You walk back into the hallway. You are the furthest you can from the fog, it's now or never. You start running.";
                                            Typewriter(text, delay);
                                            Thread.Sleep(1000);
                                            if (jump.Next(10) <= 4)
                                            {
                                                text = "The run up was a success!";
                                                Typewriter(text, delay);
                                                jumpCount = 1;
                                                //animationID = 44;
                                                //Animations(ref animationID);
                                                roomID = 4; //goes to room 4
                                            }
                                            else if (jump.Next(10) == 5 - 7)
                                            {
                                                text = @"Oof. The run up still wasn't enough. You don't jump anywhere near far enough.";
                                                Typewriter(text, delay);
                                                animationID = 2; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                                
                                            }
                                            if (jump.Next(10) == 8 - 9)
                                            {
                                                text = @"There was snail goop on the ground that you didn't notice before. You slip on it as you run, and die. The snail eats your corpse..";
                                                Typewriter(text, delay);
                                                animationID = 2; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                                
                                            }
                                            break;
                                        case "back":
                                            animationID = 109; //goes back to room 9;
                                            break;
                                        default:
                                            text = "You stand there, contemplating your life choices. The snail finds you and eats you.";
                                            Typewriter(text, delay);
                                            animationID = 2; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                            break;
                                    }
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
                                default:
                                    Console.WriteLine("What?");
                                    Thread.Sleep(1000);
                                    break;
                            }
                        }
                        else // Second Description - Cat
                        {
                            text = "You've been here before. Silly billy, are you going around in circles?";
                            checkRoomItems(roomID);
                            direction = Console.ReadLine().ToLower().Trim();
                            switch (direction)
                            {
                                case "back":
                                    animationID = 109;
                                    Animations(ref animationID);
                                    roomID = 9; //goes back to room 9;
                                    break;
                                case "forward":
                                case "fog":
                                    text = @"Jump back across, you know how far it is now. Have fun? 
What would you like to do?";
                                    direction = Console.ReadLine().ToLower().Trim();
                                    switch (direction)
                                    {
                                        case "jump":
                                            text = "You jump into the fog from where you are. Hope you know the laws physics reaaally well...";
                                            Typewriter(text, delay);
                                            Thread.Sleep(1000);
                                            text = "A bigger snail reaches up through the fog and eats you. That'll teach you.";
                                            Typewriter(text, delay);
                                            animationID = 2; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                            break;
                                        case "running jump":
                                            if (jump.Next(10) < 5) //randomly decides which outcome the player gets
                                            {
                                                text = @"There was snail goop on the ground that you didn't notice before. You slip on it as you run, and die. The snail eats your corpse...";
                                                Typewriter(text, delay);
                                                animationID = 1; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                            }
                                            else if (jump.Next(10) > 5)
                                            {
                                                text = "A bigger snail reaches up through the fog and eats you. That'll teach you.";
                                                Typewriter(text, delay);
                                                animationID = 2; //death animation
                                                Animations(ref animationID);
                                                ded = 1; //makes you die
                                            }
                                            break;
                                        case "back":
                                            animationID = 109; //goes back to room 9;
                                            break;
                                        default:
                                            text = "You stand there, contemplating your life choices. The snail finds you and eats you.";
                                            Typewriter(text, delay);
                                            animationID = 1; //death animation
                                            Animations(ref animationID);
                                            ded = 1; //makes you die
                                            break;
                                    }
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
                                default:
                                    Console.WriteLine("What?");
                                    Thread.Sleep(1000);
                                    break;
                            }
                        }
                        break;
                    case 11: //"win room"
                        Console.Clear();
                        soundID = 111;
                        delay = 40;
                        SoundPlayer(soundID);
                        text = @"There is a super bright light.
It appears that the map hasn't loaded yet. You can't see anything.";
                        Typewriter(text, delay);
                        delay = 37;
                        SoundPlayer(soundID);
                        text = "\nWhat would you like to do? ";
                        Typewriter(text, delay);
                        direction = Console.ReadLine().ToLower().Trim();
                        switch (direction)
                        {
                            case "back":
                                //animationID = 110;
                                //Animations(ref animationID);
                                roomID = 9; //goes back to room 9;
                                break;
                            default:
                                text = @"The light is blinding.
In your confusion, you fall on the ledge you're on.
The snail finds you, sucks your blood, and eats your corpse.";
                                Typewriter(text, delay);
                                animationID = 3; //win animation
                                Animations(ref animationID);
                                Thread.Sleep(5000);
                                runGame = 0; //return to menu
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
                Console.Write("Select Option (Enter Number): ");
                userMenuSelection = Convert.ToInt32(Console.ReadLine());

                switch (userMenuSelection)
                {
                    case 1:
                        //resets global vars to default on selecting new game
                        roomID = 0;
                        blood = 5;
                        snailDistance = 15;
                        ded = 0;
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
            Volume(0.7f);
            SoundPlayer player = new SoundPlayer();
            switch (soundID)                                                        // Adding seperate files for each piece of dialogue - Cat
            {
                case 0:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\UserPref.wav";
                    break;
                case 1:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Intro.wav";
                    break;
                case 2:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\HowToPlay.wav";
                    break;
                case 3:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\WWYLTD.wav";
                    break;
                case 13:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\ThreatDistant.wav";
                    break;
                case 14:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\ThreatNearer.wav";
                    break;
                case 15:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\ThreatDistant.wav";
                    break;
                case 16:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\ThreatDistant.wav";
                    break;
                case 11:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room1.1.wav";
                    soundID = 3;
                    break;
                case 12:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room1.2.wav";
                    soundID = 3;
                    break;
                case 21:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";
                    soundID = 3;
                    break;
                case 22:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 31:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room3.1.wav";
                    soundID = 3;
                    break;
                case 32:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 41:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room4.1.wav";
                    soundID = 3;
                    break;
                case 42:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 51:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 52:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 71:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 72:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 81:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 82:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 91:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 92:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
                case 111:
                    player.SoundLocation = Environment.CurrentDirectory + @"\TTS\Room2.1.wav";//
                    soundID = 3;
                    break;
            }
            player.Play();
        }

        public static void Volume(float volume) // Hopefully fixing volume issue - Cat
        {
            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
        }

        public static void Typewriter(string text , int delay) //Setting up typewriter and delay based on if they skip dialogue or not. - Cat
        {
            foreach (char c in text)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Spacebar)
                    {
                        delay = 0;
                        Volume(0);
                    }
                }
                else
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
            }
            delay = 25;
        }

        public static void checkRoomItems(int roomID)
        {
            string roomText = ""; // local string to avoid touching global text

            if (rustyKey.RoomID == roomID)
                roomText += "\nA rusty key lies on the ground.\n";

            if (slimeyKey.RoomID == roomID)
                roomText += "\nA slimey key rests on the floor. You really hope it didn’t come from the snail.\n";

            if (harmonica.RoomID == roomID)
                roomText += "\nA slightly dented harmonica lies nearby. It looks like it’s seen things. Emotional things.\n";

            if (vaughnsGin.RoomID == roomID)
                roomText += "\nA full bottle of expensive-looking gin rests on a dusty shelf. It’s the only thing in the room without dust.\n";

            if (crumpledNote.RoomID == roomID)
                roomText += "\nA crumpled note sticks out from under a cracked tile.\n";

            if (fidgetSpinner.RoomID == roomID)
                roomText += "\nA brightly colored fidget spinner gleams unnaturally in the corner.\n";

            if (unknownPills.RoomID == roomID)
                roomText += "\nA small bottle of unknown pills sits ominously on a desk.\n";

            if (!string.IsNullOrWhiteSpace(roomText))
                Typewriter(roomText, delay); // only prints once
        }


    }
}