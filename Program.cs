using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;



namespace ExtensionDeleter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool recursive = false;
            ExDel exDel = new ExDel();
            try {

                // args[0] - starting folder (c:\), args[1] - extension (zip), args[2] - recursive (r)

                if (args.Length == 3 && !string.IsNullOrEmpty(args[2])) {
                    if (args[2].ToLower() == "r") {
                        recursive = true;
                    }
                }

                exDel.DeleteExtensions(args[0], args[1], recursive);

            } catch (IndexOutOfRangeException) {
                Console.WriteLine("Not all required arguments provided - quitting...");
            }
        }
    }
}
