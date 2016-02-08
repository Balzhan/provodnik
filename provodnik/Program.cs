using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace provodnik
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\kbtu15";
                //@"C:\Users\ww\Documents"; //"C:\\Documents and Settings\\UserXP\\Desktop";
            DirectoryInfo dir = new DirectoryInfo(path);

            List<FileSystemInfo> items = new List<FileSystemInfo>();
            items.AddRange(dir.GetDirectories());
            items.AddRange(dir.GetFiles());

            int index = 0;

            while (true)
            {
                for (int i = 0; i < items.Count; ++i)
                {
                    if (i == index)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    if (items[i].GetType() == typeof(FileInfo))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine(items[i].Name);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }


                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (index > 0) index--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (index < items.Count - 1) index++;
                        break;
                    case ConsoleKey.Enter:
                        if (items[index].GetType() == typeof(DirectoryInfo))
                        {
                            path = items[index].FullName;
                            dir = new DirectoryInfo(path);
                            items.Clear();
                            items.AddRange(dir.GetDirectories());
                            items.AddRange(dir.GetFiles());
                            index = 0;
                        }
                        if (items[index].GetType() == typeof(FileInfo))
                        {
                            FileStream stream = new FileStream(items[index].FullName, FileMode.Open, FileAccess.Read);
                            StreamReader reader = new StreamReader(stream);

                            string line = reader.ReadToEnd();
                            Console.Clear();
                            Console.WriteLine(line);
                            reader.Close();
                            stream.Close();
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();

                        break;
                    case ConsoleKey.LeftArrow:
                        if (items[index].GetType() == typeof(DirectoryInfo))
                        {
                            path = Path.GetDirectoryName(items[index].FullName);
                            dir = new DirectoryInfo(path);
                            items.Clear();
                            items.AddRange(dir.GetDirectories());
                            items.AddRange(dir.GetFiles());
                            index = 0;
                        }
                        break;
                }
                Console.Clear();

            }
        }
    }
}
