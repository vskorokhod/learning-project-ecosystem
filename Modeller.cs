using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    class Modeller
    {
        private Island curr_Island;
        private int island_size = 0;
        private int steps_number = 0;
        private Visualiser visual = new Visualiser();

        public Island Curr_Island
        {
            get
            {
                return curr_Island;
            }
            set
            {
                curr_Island = value;
            }
        }

        public int Island_size
        {
            get
            {
                return island_size;
            }
            set
            {
                island_size = value;
            }
        }

        public Visualiser Visual
        {
            get
            {
                return visual;
            }
            set
            {
                visual = value;
            }
        }

        public void Init(List<String> initConditions)
        {
            String temp = "";
            int n = 0;
            int row = 0;
            int column = 0;
            int fr = 0;
            int fc = 0;
            int lr = 0;
            int lc = 0;
            String Animal = "";
            island_size = int.Parse(initConditions[0]);
            temp = initConditions[1];
            n = temp.IndexOf(' ');
            fr = int.Parse(temp.Substring(0, n + 1)) - 1;
            temp = temp.Remove(0, n + 1);
            n = temp.IndexOf(' ');
            fc = int.Parse(temp.Substring(0, n + 1)) - 1;
            temp = temp.Remove(0, n + 1);
            n = temp.IndexOf(' ');
            lr = int.Parse(temp.Substring(0, n + 1)) - 1;
            temp = temp.Remove(0, n + 1);
            lc = int.Parse(temp) - 1;
            Curr_Island = new Island(island_size, fr, fc, lr, lc);
            int iter = 2;
            while (iter != initConditions.Count)
            {
                temp = initConditions[iter];
                n = temp.IndexOf(' ');
                row = int.Parse(temp.Substring(0, n + 1));
                temp = temp.Remove(0, n + 1);
                n = temp.IndexOf(' ');
                column = int.Parse(temp.Substring(0, n + 1));
                temp = temp.Remove(0, n + 1);
                Animal = temp;
                if (Animal == "Rabbit")
                {
                    Curr_Island.Fields[row - 1, column - 1].Add(new Rabbit(AnimalType.Rabbit));
                }
                else if (Animal == "Wolf")
                {
                    Curr_Island.Fields[row - 1, column - 1].Add(new Wolf(AnimalType.Wolf));
                }
                else if (Animal == "She-Wolf")
                {
                    Curr_Island.Fields[row - 1, column - 1].Add(new She_Wolf(AnimalType.She_Wolf));
                }
                iter++;
            }
            Visual.ShowIsland(Curr_Island, island_size);
        }

        private Island AnimalMoves(Island OldIsland, int size, int fr, int fc, int lr, int lc)
        {
            Island NewIsland = new Island(size, fr, fc, lr, lc);
            for (int i = 0; i < island_size; i++)
            {
                for (int j = 0; j < island_size; j++)
                {
                    foreach (Animal anim in OldIsland.Fields[i, j])
                    {
                        int[] old_coords = new int[] { i, j };
                        int[] new_coords = new int[2];
                        new_coords = anim.Move(Curr_Island, old_coords, island_size);
                        NewIsland.Fields[new_coords[0], new_coords[1]].Add(anim);
                        anim.Hitpoints -= 0.2;
                    }
                }
            }
            return NewIsland;
        }

        private Island AnimalActions(Island OldIsland, int size, int fr, int fc, int lr, int lc)
        {
            Island NewIsland = new Island(size, fr, fc, lr, lc);
            for (int i = 0; i < island_size; i++)
            {
                for (int j = 0; j < island_size; j++)
                {
                    for (int k = 0; k < OldIsland.Fields[i, j].Count; k++)
                    {
                        NewIsland.Fields[i, j] = OldIsland.Fields[i, j][k].Action(OldIsland.Fields[i, j], i, j, k, Visual.StepEventsTable);
                    }
                }
            }
            return NewIsland;
        }

        public void ModelStep()
        {
            for (int i = 0; i < island_size; i++)
            {
                for (int j = 0; j < island_size; j++)
                {
                    foreach (Animal anim in Curr_Island.Fields[i, j])
                    {
                        anim.Has_action = true;
                        anim.Is_pregnant = false;
                    }
                }
            }
            steps_number++;
            Visual.StepEventsTable.Clear();
            Curr_Island = AnimalMoves(Curr_Island, island_size, Curr_Island.Safe_zone.first_row, Curr_Island.Safe_zone.first_column, Curr_Island.Safe_zone.last_row, Curr_Island.Safe_zone.last_column);
            Curr_Island = AnimalActions(Curr_Island, island_size, Curr_Island.Safe_zone.first_row, Curr_Island.Safe_zone.first_column, Curr_Island.Safe_zone.last_row, Curr_Island.Safe_zone.last_column);
            Visual.ShowIsland(Curr_Island, island_size);
            Visual.FullEventsTable.Add("Step " + steps_number);
            if (Visual.StepEventsTable.Count == 0)
            {
                Visual.FullEventsTable.Add("There were no events");
            }
            else
            {
                foreach (String str in Visual.StepEventsTable)
                {
                    Visual.FullEventsTable.Add(str);
                }
            }
        }
    }
}
