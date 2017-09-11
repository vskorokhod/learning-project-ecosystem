using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    class Visualiser
    {
        private List<String> stepEventsTable = new List<String>();
        private List<String> fullEventsTable = new List<String>();

        public List<String> StepEventsTable
        {
            get
            {
                return stepEventsTable;
            }
            set
            {
            }
        }

        public List<String> FullEventsTable
        {
            get
            {
                return fullEventsTable;
            }
            set
            {
            }
        }

        public void ShowIsland(Island tempIsland, int n)
        {
            Console.Clear();
            Console.Write("   ");
            for (int i = 1; i <= n; i++)
            {
                Console.Write("{0, -3}", i);
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0, -3}", (i + 1));
                for (int j = 0; j < n; j++)
                {
                    if (tempIsland.Fields[i, j].Count == 0)
                    {
                        if (((i == tempIsland.Safe_zone.first_row) && (j >= tempIsland.Safe_zone.first_column) && (j <= tempIsland.Safe_zone.last_column)) || ((i == tempIsland.Safe_zone.last_row) && (j >= tempIsland.Safe_zone.first_column) && (j <= tempIsland.Safe_zone.last_column)) || ((j == tempIsland.Safe_zone.first_column) && (i >= tempIsland.Safe_zone.first_row) && (i <= tempIsland.Safe_zone.last_row)) || ((j == tempIsland.Safe_zone.last_column) && (i >= tempIsland.Safe_zone.first_row) && (i <= tempIsland.Safe_zone.last_row)))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("*  ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        else
                        {
                            Console.Write("*  ");
                        }
                    }
                    else
                    {
                        if (tempIsland.Fields[i, j][0].Type == AnimalType.Rabbit)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("R  ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (tempIsland.Fields[i, j][0].Type == AnimalType.She_Wolf)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("S  ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        if (tempIsland.Fields[i, j][0].Type == AnimalType.Wolf)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("W  ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    if (j == (n - 1))
                    {
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("R");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" - Rabbit");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("W");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" - Wolf");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("S");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" - She-Wolf");
            Console.WriteLine();
            Console.WriteLine("Space/Enter - one step of modelling");
            Console.WriteLine("I - show information about any field");
            Console.WriteLine("E - show events on this step");
            Console.WriteLine("F - show full list of events");
            Console.WriteLine("Escape - exit");
            Console.WriteLine();
        }

        public void ShowFieldInformation(Island tempIsland)
        {
            int row, column;
            Console.WriteLine();
            Console.WriteLine("Enter the row number");
            try
            {
                row = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong option!");
                return;
            }
            Console.WriteLine("Enter the column number");
            try
            {
                column = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong option!");
                return;
            }
            if (tempIsland.Fields[row - 1, column - 1].Count == 0)
            {
                Console.WriteLine("This field is empty");
            }
            else
            {
                foreach (Animal anim in tempIsland.Fields[row - 1, column - 1])
                {
                    Console.WriteLine(anim.Type + ": " + "hitpoints = {0:f1}", anim.Hitpoints);
                }
            }
        }

        public void ShowEvents(List<String> EventsList)
        {
            Console.WriteLine();
            if (EventsList.Count == 0)
            {
                Console.WriteLine("There were no events");
            }
            else
            {
                foreach (String str in EventsList)
                {
                    Console.WriteLine(str);
                }
            }
        }
    }
}
