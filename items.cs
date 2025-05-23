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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }


    }
}
