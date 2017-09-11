using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    class She_Wolf : Animal
    {
        public She_Wolf(AnimalType T)
            : base(T)
        {
        }

        override public int[] Move(Island tempIsland, int[] coords, int size)
        {
            bool find_victim = false;
            int[] copy = new int[2];
            int[] seq = GetSequence();
            for (int i = 0; i < 8; i++)
            {
                copy[0] = coords[0];
                copy[1] = coords[1];
                switch (seq[i])
                {
                    case 1:
                        copy[0]--;
                        copy[1]--;
                        break;
                    case 2:
                        copy[0]--;
                        break;
                    case 3:
                        copy[0]--;
                        copy[1]++;
                        break;
                    case 4:
                        copy[1]--;
                        break;
                    case 5:
                        copy[1]++;
                        break;
                    case 6:
                        copy[0]++;
                        copy[1]--;
                        break;
                    case 7:
                        copy[0]++;
                        break;
                    case 8:
                        copy[0]++;
                        copy[1]++;
                        break;
                }
                if ((copy[0] >= 0) && (copy[0] <= (size - 1)) && (copy[1] >= 0) && (copy[1] <= (size - 1)))
                {
                    foreach (Animal anim in tempIsland.Fields[copy[0], copy[1]])
                    {
                        if (anim.Type == AnimalType.Rabbit)
                        {
                            find_victim = true;
                            if (!((copy[0] >= tempIsland.Safe_zone.first_row) && (copy[0] <= tempIsland.Safe_zone.last_row) && (copy[1] >= tempIsland.Safe_zone.first_column) && (copy[1] <= tempIsland.Safe_zone.last_column)))
                            {
                                coords[0] = copy[0];
                                coords[1] = copy[1];
                            }
                            break;
                        }
                    }
                }
                if (find_victim)
                {
                    break;
                }
            }
            if (!find_victim)
            {
                copy[0] = coords[0];
                copy[1] = coords[1];
                copy = base.Move(tempIsland, copy, size);
                if (!((copy[0] >= tempIsland.Safe_zone.first_row) && (copy[0] <= tempIsland.Safe_zone.last_row) && (copy[1] >= tempIsland.Safe_zone.first_column) && (copy[1] <= tempIsland.Safe_zone.last_column)))
                {
                    coords[0] = copy[0];
                    coords[1] = copy[1];
                }
            }
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
                for (int iter = 0; iter < templist.Count; iter++)
                {
                    if (templist[iter].Type == AnimalType.Rabbit)
                    {
                        templist.Remove(templist[iter]);
                        if (iter < place)
                        {
                            place--;
                        }
                        templist[place].Hitpoints += 1.0;
                        if (templist[place].Hitpoints > 10.0)
                        {
                            templist[place].Hitpoints = 10.0;
                        }
                        templist[place].Has_action = false;
                        events.Add("She-Wolf has eat rabbit in row " + (row + 1) + ", column " + (column + 1));
                        break;
                    }
                }
            }
            return templist;
        }
    }
}
