using System;
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
            // Create new XML file
            XDocument doc = new XDocument(
                    new XElement(
                            "Modules",
                            new XElement("Module", "Introduction to LINQ"),
                            new XElement("Module", "LINQ and C#")
                        )
                );

            Console.ReadLine();
        }
    }
}
