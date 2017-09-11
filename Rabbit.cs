using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    class Rabbit : Animal
    {
        public Rabbit(AnimalType T)
            : base(T)
        {
        }

        override public int[] Move(Island tempIsland, int[] coords, int size)
        {
            coords = base.Move(tempIsland, coords, size);
            return coords;
        }

        override public List<Animal> Action(List<Animal> templist, int row, int column, int place, List<String> events)
        {
            templist = base.Action(templist, row, column, place, events);
            if (!templist[place].Is_alive)
            {
                templist.Remove(templist[place]);
                return templist;
            }
            if (templist[place].Has_action)
            {
                if (templist[place].Hitpoints >= 8.0)
                {
                    var buf = new byte[1];
                    var rand = new System.Security.Cryptography.RNGCryptoServiceProvider();
                    rand.GetBytes(buf);
                    int new_rabbit_chance = (buf[0] % 10) + 1;
                    if (new_rabbit_chance == 5)
                    {
                        templist.Add(new Rabbit(AnimalType.Rabbit));
                        templist[templist.Count - 1].Hitpoints = templist[place].Hitpoints;
                        events.Add("New rabbit has appeared in row " + (row + 1) + ", column " + (column + 1));
                        templist[place].Has_action = false;
                    }
                }
            }
            return templist;
        }
    }
}
