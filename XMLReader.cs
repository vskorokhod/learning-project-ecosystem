using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ecosystem
{
    class XMLReader
    {
        private String StringHandling(String str, String head)
        {
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("/", "");
            str = str.Replace(head, "");
            str = str.Trim();
            return str;
        }

        public List<String> ReadFromFile(String filename)
        {
            List<String> templist = new List<String>();
            try
            {
                System.IO.StreamReader from_file = new System.IO.StreamReader(filename);
                while (from_file.EndOfStream != true)
                {
                    String tempstr = from_file.ReadLine();
                    if (!tempstr.Contains("<?") && (!tempstr.Contains("<!")))
                    {
                        if (tempstr.Contains("Size"))
                        {
                            tempstr = StringHandling(tempstr, "Size");
                            templist.Add(tempstr);
                        }
                        else if (tempstr.Contains("SafeZone"))
                        {
                            tempstr = StringHandling(tempstr, "SafeZone");
                            templist.Add(tempstr);
                        }
                        else if (tempstr.Contains("Animal"))
                        {
                            tempstr = tempstr.Trim();
                            if (tempstr.Contains("<Animal>"))
                            {
                                templist.Add("");
                            }
                            else if (!tempstr.Equals("</Animal>"))
                            {
                                int n1 = tempstr.IndexOf("=");
                                int n2 = tempstr.IndexOf("column");
                                templist.Add(tempstr.Substring(n1 + 1, n2 - (n1 + 1)));
                                tempstr = tempstr.Remove(0, n2);
                                n1 = tempstr.IndexOf("=");
                                n2 = tempstr.IndexOf(">");
                                templist[templist.Count - 1] += (tempstr.Substring(n1 + 1, n2 - (n1 + 1)) + " ");
                                tempstr = tempstr.Remove(0, n2 + 1);
                                n1 = tempstr.IndexOf("<");
                                templist[templist.Count - 1] += tempstr.Substring(0, n1);
                            }
                        }
                        else if (tempstr.Contains("Row"))
                        {
                            tempstr = StringHandling(tempstr, "Row");
                            templist[templist.Count - 1] += (tempstr + " ");
                        }
                        else if (tempstr.Contains("Column"))
                        {
                            tempstr = StringHandling(tempstr, "Column");
                            templist[templist.Count - 1] += (tempstr + " ");
                        }
                        else if (tempstr.Contains("Type"))
                        {
                            tempstr = StringHandling(tempstr, "Type");
                            templist[templist.Count - 1] += tempstr;
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File does not exist!");
                Console.WriteLine();
                return null;
            }
            return templist;
        }
    }
}
