using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecosystem
{
    class XMLWriter
    {
        public void WriteToFile(String filename, Modeller WriteModel)
        {
            using (System.IO.StreamWriter to_file = new System.IO.StreamWriter(@"tests\" + filename, false))
            {
                to_file.WriteLine("<Island>");
                to_file.WriteLine("\t<Size>" + WriteModel.Island_size + "</Size>");
                to_file.WriteLine("\t<SafeZone>" + (WriteModel.Curr_Island.Safe_zone.first_row + 1) + " " + (WriteModel.Curr_Island.Safe_zone.first_column + 1) + " " + (WriteModel.Curr_Island.Safe_zone.last_row + 1) + " " + (WriteModel.Curr_Island.Safe_zone.last_column + 1) + "</SafeZone>");
                for (int i = 0; i < WriteModel.Island_size; i++)
                {
                    for (int j = 0; j < WriteModel.Island_size; j++)
                    {
                        if (WriteModel.Curr_Island.Fields[i, j].Count != 0)
                        {
                            foreach (Animal anim in WriteModel.Curr_Island.Fields[i, j])
                            {
                                to_file.WriteLine("\t<Animal row=" + (i + 1) + " column=" + (j + 1) + ">" + anim.Type + "</Animal>");
                            }
                        }
                    }
                }
                to_file.WriteLine("</Island>");
                to_file.Close();
            }
        }
    }
}
