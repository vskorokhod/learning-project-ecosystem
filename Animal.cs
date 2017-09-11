using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    enum AnimalType { Rabbit, Wolf, She_Wolf };

    class Animal
    {
        private bool has_action = false;
        private bool is_pregnant = false;
        private bool is_alive = true;
        private double hitpoints = 10.0;
        private AnimalType type;

        public bool Has_action
        {
            get
            {
                return has_action;
            }
            set
            {
                has_action = value;
            }
        }

        public bool Is_pregnant
        {
            get
            {
                return is_pregnant;
            }
            set
            {
                is_pregnant = value;
            }
        }

        public bool Is_alive
        {
            get
            {
                return is_alive;
            }
            set
            {
                is_alive = value;
            }
        }

        public double Hitpoints
        {
            get
            {
                return hitpoints;
            }
            set
            {
                hitpoints = value;
            }
        }

        public AnimalType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public Animal(AnimalType T)
        {
            this.type = T;
        }

        protected int[] GetSequence()
        {
            int count = 8;
            int next_number = 0;
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int[] res = new int[8];
            var buf = new byte[1];
            var rand = new System.Security.Cryptography.RNGCryptoServiceProvider();
            while (count > 1)
            {
                rand.GetBytes(buf);
                next_number = (buf[0] % count);
                res[8 - count] = numbers[next_number];
                for (int i = next_number; i < 7; i++)
                {
                    numbers[i] = numbers[i + 1];
                }
                count--;
            }
            res[7] = numbers[0];
            return res;
        }

        private int[] StepUpAndLeft(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == 0))
            {
                coords[0]++;
                coords[1]++;
            }
            else if ((coords[0] == 0) && (coords[1] == (size - 1)))
            {
                coords[0]++;
                coords[1]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == 0))
            {
                coords[0]--;
                coords[1]++;
            }
            else if ((coords[0] == 0) && (coords[1] != 0) && (coords[1] != (size - 1)))
            {
                coords[0]++;
                coords[1]--;
            }
            else if ((coords[1] == 0) && (coords[0] != 0) && (coords[0] != (size - 1)))
            {
                coords[0]--;
                coords[1]++;
            }
            else
            {
                coords[0]--;
                coords[1]--;
            }
            return coords;
        }

        private int[] StepUp(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == 0))
            {
                coords[0]++;
            }
            else if ((coords[0] == 0) && (coords[1] == (size - 1)))
            {
                coords[0]++;
            }
            else if ((coords[0] == 0) && (coords[1] != 0) && (coords[1] != (size - 1)))
            {
                coords[0]++;
            }
            else
            {
                coords[0]--;
            }
            return coords;
        }

        private int[] StepUpAndRight(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == 0))
            {
                coords[0]++;
                coords[1]++;
            }
            else if ((coords[0] == 0) && (coords[1] == (size - 1)))
            {
                coords[0]++;
                coords[1]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == (size - 1)))
            {
                coords[0]--;
                coords[1]--;
            }
            else if ((coords[0] == 0) && (coords[1] != 0) && (coords[1] != (size - 1)))
            {
                coords[0]++;
                coords[1]++;
            }
            else if ((coords[1] == (size - 1)) && (coords[0] != 0) && (coords[0] != (size - 1)))
            {
                coords[0]--;
                coords[1]--;
            }
            else
            {
                coords[0]--;
                coords[1]++;
            }
            return coords;
        }

        private int[] StepLeft(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == 0))
            {
                coords[1]++;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == 0))
            {
                coords[1]++;
            }
            else if ((coords[1] == 0) && (coords[0] != 0) && (coords[0] != (size - 1)))
            {
                coords[1]++;
            }
            else
            {
                coords[1]--;
            }
            return coords;
        }

        private int[] StepRight(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == (size - 1)))
            {
                coords[1]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == (size - 1)))
            {
                coords[1]--;
            }
            else if ((coords[1] == (size - 1)) && (coords[0] != 0) && (coords[0] != (size - 1)))
            {
                coords[1]--;
            }
            else
            {
                coords[1]++;
            }
            return coords;
        }

        private int[] StepDownAndLeft(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == 0))
            {
                coords[0]++;
                coords[1]++;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == 0))
            {
                coords[0]--;
                coords[1]++;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == (size - 1)))
            {
                coords[0]--;
                coords[1]--;
            }
            else if ((coords[1] == 0) && (coords[0] != 0) && (coords[0] != (size - 1)))
            {
                coords[0]++;
                coords[1]++;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] != 0) && (coords[1] != (size - 1)))
            {
                coords[0]--;
                coords[1]--;
            }
            else
            {
                coords[0]++;
                coords[1]--;
            }
            return coords;
        }

        private int[] StepDown(int[] coords, int size)
        {
            if ((coords[0] == (size - 1)) && (coords[1] == 0))
            {
                coords[0]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == (size - 1)))
            {
                coords[0]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] != 0) && (coords[1] != (size - 1)))
            {
                coords[0]--;
            }
            else
            {
                coords[0]++;
            }
            return coords;
        }

        private int[] StepDownAndRight(int[] coords, int size)
        {
            if ((coords[0] == 0) && (coords[1] == (size - 1)))
            {
                coords[0]++;
                coords[1]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == 0))
            {
                coords[0]--;
                coords[1]++;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] == (size - 1)))
            {
                coords[0]--;
                coords[1]--;
            }
            else if ((coords[0] == (size - 1)) && (coords[1] != 0) && (coords[1] != (size - 1)))
            {
                coords[0]--;
                coords[1]++;
            }
            else if ((coords[1] == (size - 1)) && (coords[0] != 0) && (coords[0] != (size - 1)))
            {
                coords[0]++;
                coords[1]--;
            }
            else
            {
                coords[0]++;
                coords[1]++;
            }
            return coords;
        }

        virtual public int[] Move(Island temp_island, int[] coords, int size)
        {
            var buf = new byte[1];
            var rand = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rand.GetBytes(buf);
            int step = (buf[0] % 9) + 1;
            switch (step)
            {
                case 1:
                    coords = StepUpAndLeft(coords, size);
                    break;
                case 2:
                    coords = StepUp(coords, size);
                    break;
                case 3:
                    coords = StepUpAndRight(coords, size);
                    break;
                case 4:
                    coords = StepLeft(coords, size);
                    break;
                case 5:
                    break;
                case 6:
                    coords = StepRight(coords, size);
                    break;
                case 7:
                    coords = StepDownAndLeft(coords, size);
                    break;
                case 8:
                    coords = StepDown(coords, size);
                    break;
                case 9:
                    coords = StepDownAndRight(coords, size);
                    break;
            }
            return coords;
        }

        virtual public List<Animal> Action(List<Animal> templist, int row, int column, int place, List<String> events)
        {
            if (templist[place].hitpoints < 0.1)
            {
                events.Add(templist[place].type + " in row " + (row + 1) + ", column " + (column + 1) + " has died");
                templist[place].is_alive = false;
                return templist;
            }
            else
            {
                return templist;
            }
        }
    }
}
