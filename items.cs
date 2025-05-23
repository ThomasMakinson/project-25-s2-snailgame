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


    }
}
