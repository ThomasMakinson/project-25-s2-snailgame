using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailMate
{
    internal class items
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public string Condition { get; set; }
        public int RoomID { get; set; }
        

        public void Inspect()
        {
            Console.Clear();
            Program.Typewriter($"Hmm let's take a look at this {Name}.\n", 30);
            Program.Typewriter($"It appears to be a {Type} of some sort.\n", 30);
            Program.Typewriter($"It seems to be made of {Material}.\n", 30);
            Program.Typewriter($"It looks to be in {Condition} condition.\n",30);
            Program.Typewriter($"{Description}\n", 30); 
            Console.ReadKey(true);
            Console.Clear();
        }

        public static void DisplayInventory(items[] inventory)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("========= INVENTORY =========");
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] != null)
                {
                    Console.WriteLine($"[{i + 1}] {inventory[i].Name} - {inventory[i].Type} - {inventory[i].Condition}");
                }

                else
                {
                    Console.WriteLine($"[{i + 1}] (Empty Slot)");
                }
            }
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to game...");
            Console.ReadKey(true);
            Console.Clear();

        }

        public void Use()
        {
            switch (Name.ToLower())
            {
                case "crumpled note":
                    Console.WriteLine("You delicately unfold the crumpled note and read it: ");
                    Console.WriteLine(Description);
                    break;
                case "harmonica":
                    Console.WriteLine("You raise the harmonica to your lips, ready to bust out a soulful tune...");
                    Thread.Sleep(1000);
                    Console.WriteLine("But then you remember — YOU'RE BEING CHASED BY A KILLER SNAIL!");
                    Thread.Sleep(1000);
                    Console.WriteLine("Maybe now isn't the best time for jazz.");
                    break;
                case "fidget spinner":
                    Console.WriteLine("You whip out your trusty fidget spinner and give it a heroic flick...");
                    Thread.Sleep(800);
                    Console.WriteLine("It spins with the fury of a thousand wasted afternoons.");
                    Thread.Sleep(1000);
                    Console.WriteLine("You feel... absolutely no safer.");
                    Thread.Sleep(1000);
                    Console.WriteLine("The snail is still coming.");
                    Thread.Sleep(800);
                    Console.WriteLine("It does not care for your toys.");
                    break;
                case "vaughns gin":
                case "gin":
                    Console.WriteLine("You sip Vaughn’s Gin. Suddenly, you understand every lecture ever given.");
                    Thread.Sleep(1000);
                    Console.WriteLine("You *are* the rubric. You *are* the feedback.");
                    Thread.Sleep(800);
                    Console.WriteLine("But also... you’re drunk. And the snail is still coming.");
                    break;
                case "unknown pills":
                case "pills":
                    Console.WriteLine("You take the Unknown Pills, expecting a strength boost or maybe night vision...");
                    Thread.Sleep(1000);
                    Console.WriteLine("Instead, your stomach gurgles like an ancient curse has been awakened.");
                    Thread.Sleep(1000);
                    Console.WriteLine("You're no longer the only one leaving a trail.");
                    Thread.Sleep(800);
                    Console.WriteLine("The snail senses weakness.");
                    Thread.Sleep(600);
                    Console.WriteLine("Stealth: broken. Dignity: missing. Urgency: extreme.");
                    break;
                case "rusty key":
                    //Console.WriteLine("Need to figure out what we doing with keys before i add use");
                    break;
                case "slimey key":
                case "slimy key":
                    //Console.WriteLine("Need to figure out what we doing with keys before i add use");
                    break;
                default:
                    Console.WriteLine("You aren't able to do that....");
                    break;
            }
        }


    }
}
