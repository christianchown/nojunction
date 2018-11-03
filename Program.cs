using System;
using System.IO;

namespace nojunction
{
    class Program
    {
        static bool verbose = false;

        static void ConvertJunction(string dir)
        {
            string from = JunctionPoint.GetTarget(dir);
            if (from != "")
            {
                string target = from.TrimEnd('\\');
                try
                {
                    File.Copy(target, dir + "-junction", true);
                    Directory.Delete(dir);
                    File.Move(dir + "-junction", dir);
                    Console.WriteLine("Created " + dir);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Conversion of " + target + " -> " + dir + " failed: " + ex.ToString());
                    Console.WriteLine("");
                }
                catch (IOException ioex)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Conversion of " + target + " -> " + dir + " failed (IO): " + ioex.ToString());
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("No target for " + dir);
            }
        }

        static void ProcessDir(string dir)
        {
            try
            {
                string[] subs = Directory.GetDirectories(dir);
                if (verbose)
                {
                    Console.WriteLine("Processing " + dir + ((subs.Length > 0) ? " - " + subs.Length + " subdir(s)" : ""));
                }
                foreach (string sub in subs)
                {
                    if (JunctionPoint.Exists(sub))
                    {
                        if (verbose)
                        {
                            Console.WriteLine("<JUNCTION>: " + sub);
                        }
                        ConvertJunction(sub);
                    }
                    else
                    {
                        ProcessDir(sub);
                    }
                }
            }
            catch (IOException)
            {
                if (verbose)
                {
                    Console.WriteLine("Maybe.... <JUNCTION>: " + dir);
                }
                if (JunctionPoint.Exists(dir))
                {
                    ConvertJunction(dir);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("nojunction...");
            Console.WriteLine("");
            string dir = Directory.GetCurrentDirectory() + "\\node_modules";
            foreach (string arg in args)
            {
                if ((arg == "verbose") || (arg == "-v"))
                {
                    verbose = true;
                }
                else
                {
                    dir = arg;
                }
            }
            ProcessDir(dir);
        }
    }
}
