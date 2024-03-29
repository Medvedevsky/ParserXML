﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace TestTask2
{
    class Program
    {
        public static bool TryDataParsing(string pathToFile, out Dictionary<string, byte> fieldAddresObject,
                                                          out List<List<(byte, string)>> listAddresObjectFIAS)
        {
            fieldAddresObject = new Dictionary<string, byte>();
            listAddresObjectFIAS = new List<List<(byte, string)>>();

            try
            {
                using (XmlReader reader = XmlReader.Create(new FileStream(pathToFile, FileMode.Open)))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
                        {
                            var addressObject = new List<(byte, string)>(fieldAddresObject.Count);

                            while (reader.MoveToNextAttribute())
                            {
                                if (!fieldAddresObject.ContainsKey(reader.Name))
                                    fieldAddresObject.Add(reader.Name, (byte)fieldAddresObject.Values.Count);

                                addressObject.Add((fieldAddresObject[reader.Name], reader.Value));
                            }

                            listAddresObjectFIAS.Add(addressObject);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            listAddresObjectFIAS.TrimExcess();
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к файлу: ");
            string pathToFile = Console.ReadLine();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            bool res = TryDataParsing(pathToFile, out _, out _);

            stopwatch.Stop();


            Console.WriteLine($"Анализ прошел: {(res == true ? "удачно" : "не удачно")}");


            Console.WriteLine($"Прошло времени: { stopwatch.ElapsedMilliseconds} миллисекунд");

            Console.ReadKey();
        }
    }
}