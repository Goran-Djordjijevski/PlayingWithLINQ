﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateSimpleXml();
            ReadSimpleXml();

            Console.ReadLine();
        }

        private static void ReadSimpleXml()
        {
            XDocument doc = XDocument.Load("modules.xml");

            XElement root = doc.Root;

            var elements = root.Descendants();
            foreach (var element in elements)
            {
                string value = element.Value;
                Console.WriteLine(value);
            }
        }

        public static void CreateSimpleXml()
        {
            // Create new XML file
            XDocument doc = new XDocument(
                    new XElement(
                            "Modules",
                            new XElement("Module", "Introduction to LINQ"),
                            new XElement("Module", "LINQ and C#")
                        )
                );

            doc.Save(fileName: "modules.xml");
        }
    }
}