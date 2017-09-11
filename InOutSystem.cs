using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ecosystem
{
    class InOutSystem
    {
        public void UserDialogue()
        {
            String file_name = "";
            List<String> loadedParameters;
            XMLReader Reader = new XMLReader();
            while (true)
            {
                Console.WriteLine("Enter the name of the uploaded file");
                file_name = Console.ReadLine();
                if (!file_name.Contains("xml"))
                {
                    Console.WriteLine("Wrong format of file!");
                    Console.WriteLine();
                    continue;
                }
                loadedParameters = Reader.ReadFromFile(@"tests\" + file_name);
                if (loadedParameters == null)
                {
                    continue; ;
                }
                break;
            }
            Modeller Model = new Modeller();
            Model.Init(loadedParameters);
            ConsoleKeyInfo cons;
            do
            {
                cons = Console.ReadKey();
                if ((cons.Key == ConsoleKey.Spacebar) || (cons.Key == ConsoleKey.Enter))
                {
                    Model.ModelStep();
                }

                else if (cons.Key == ConsoleKey.E)
                {
                    Model.Visual.ShowEvents(Model.Visual.StepEventsTable);
                }
                else if (cons.Key == ConsoleKey.I)
                {
                    Model.Visual.ShowFieldInformation(Model.Curr_Island);
                }
                else if (cons.Key == ConsoleKey.F)
                {
                    Model.Visual.ShowEvents(Model.Visual.FullEventsTable);
                }
                else if (cons.Key != ConsoleKey.Escape)
                {
                    Console.WriteLine("Wrong option!");
                    Console.WriteLine();
                }
            } while (cons.Key != ConsoleKey.Escape);
            bool flag = true;
            while (flag)
            {
                char user_choice;
                Console.WriteLine("Save changes to the file? (y/n)");
                try
                {
                    user_choice = char.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong option!");
                    continue;
                }
                switch (user_choice)
                {
                    case 'y':
                        Console.WriteLine("Enter name of file to save");
                        String write_file = "";
                        while (true)
                        {
                            write_file = Console.ReadLine();
                            if (write_file.Contains("."))
                            {
                                if (!write_file.Contains(".xml"))
                                {
                                    Console.WriteLine("Wrong format of file!");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                write_file += ".xml";
                                break;
                            }
                        }
                        XMLWriter Writer = new XMLWriter();
                        Writer.WriteToFile(write_file, Model);
                        flag = false;
                        break;
                    case 'n':
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Wrong option, try again!");
                        Console.WriteLine();
                        continue;
                }
            }
        }
    }
}
