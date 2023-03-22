using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameChain
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("names.txt");
            string line;
            List<string> names = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] splits = line.Split(' ');
                if (splits.Length == 2)
                {
                    names.Add(line);
                }
            }

            List<string> longestChain = new List<string>();
            List<string> currentChain = new List<string>();
            List<string> usedNames = new List<string>();
            for (int i = 0; i < names.Count; i++)
            {
                usedNames.Clear();
                if (currentChain.Count == 0)
                {
                    currentChain.Add(names[i]);
                    usedNames.Add(names[i]);
                }

                bool exhausted = false;
                while (!exhausted)
                {
                    for (int j = 0; j < names.Count; j++)
                    {
                        string currentName = names[j];
                        string lastLink = currentChain[currentChain.Count - 1];
                        string[] lastLinkParts = lastLink.Split(' ');

                        string[] nameParts = currentName.Split(' ');
                        if (nameParts[0] == lastLinkParts[1] && !currentChain.Contains(currentName) && !usedNames.Contains(currentName))
                        {
                            currentChain.Add(currentName);
                            usedNames.Add(currentName);
                            j = -1;
                            continue;
                        }
                        else if (j == names.Count - 1)
                        {
                            if (currentChain.Count > longestChain.Count)
                            {
                                // deep copy
                                longestChain.Clear();
                                for (int k = 0; k < currentChain.Count; k++)
                                {
                                    longestChain.Add(currentChain[k]);
                                }
                            }
                            // remove last one from the chain and keep going
                            currentChain.RemoveAt(currentChain.Count - 1);
                            if (currentChain.Count == 0)
                            {
                                exhausted = true;
                                j = names.Count;
                            }
                            else
                            {
                                j = -1;
                                continue;
                            }
                        }
                    }
                } // end while
            } // end for

            Console.WriteLine("Length: " + longestChain.Count);
            for (int i = 0; i < longestChain.Count; i++)
            {
                Console.WriteLine(longestChain[i]);
            }

            Console.ReadLine();
        }
    }
}