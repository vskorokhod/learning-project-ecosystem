using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    class Wolf : Animal
    {
        public Wolf(AnimalType T)
            : base(T)
        {
        }

        override public int[] Move(Island tempIsland, int[] coords, int size)
        {
            bool find_victim = false;
            bool find_female = false;
            int[] copy = new int[2];
            int[] fem = new int[2];
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
                        else if (anim.Type == AnimalType.She_Wolf)
                        {
                            find_female = true;
                            fem[0] = copy[0];
                            fem[1] = copy[1];
                        }
                    }
                }
                if (find_victim)
                {
                    break;
                }
            }
            if ((!find_victim) && (find_female))
            {
                coords[0] = fem[0];
                coords[1] = fem[1];
            }
            if ((!find_victim) && (!find_female))
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
            int female_position = -1;
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
                        events.Add("Wolf has eat rabbit in row " + (row + 1) + ", column " + (column + 1));
                        break;
                    }
                    else if (templist[iter].Type == AnimalType.She_Wolf)
                    {
                        if (templist[iter].Is_pregnant == false)
                        {
                            female_position = iter;
                        }
                    }
                }
            }
            if ((templist[place].Has_action) && (female_position != -1))
            {
                if ((templist[place].Hitpoints >= 8.0) && (templist[female_position].Hitpoints >= 8.0))
                {
                    templist[female_position].Is_pregnant = true;
                    var buf = new byte[1];
                    var rand = new System.Security.Cryptography.RNGCryptoServiceProvider();
                    rand.GetBytes(buf);
                    int gender = (buf[0] % 2) + 1;
                    switch (gender)
                    {
                        case 1:
                            templist.Add(new Wolf(AnimalType.Wolf));
                            templist[templist.Count - 1].Hitpoints = templist[female_position].Hitpoints;
                            events.Add("New wolf has appeared in row " + (row + 1) + ", column " + (column + 1));
                            templist[place].Has_action = false;
                            break;
                        case 2:
                            templist.Add(new She_Wolf(AnimalType.She_Wolf));
                            templist[templist.Count - 1].Hitpoints = templist[female_position].Hitpoints;
                            events.Add("New she-wolf has appeared in row " + (row + 1) + ", column " + (column + 1));
                            templist[place].Has_action = false;
                            break;
                    }
                }
            }
            return templist;
        }
    }
}
