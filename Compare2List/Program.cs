using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compare2List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> firstList = new List<Item>()
            {
                new Item(){Id = 0,Name = "One", Order = 0},
                new Item(){Id = 1, Name = "Two", Order = 1},
                new Item(){Id = 2, Name = "Three", Order = 2}
            };

            List<Item> secondList = new List<Item>()
            {
                new Item(){Id = 0,Name = "One", Order = 0},
                new Item(){Id = 2, Name = "Three", Order = 2},
                new Item(){Id = 3, Name = "Four", Order = 2},
                new Item(){Id = 4, Name = "Four", Order = 2},
            };

            var newList = firstList.CompareTo(secondList, t => t.Id, t => new { t.Name, t.Order });
        }
    }
}
