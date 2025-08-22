using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test 
            Koncert koncert = new Koncert(1, "Bukinski", DateTime.Now, "Mladenovo", 200.0);
            Console.WriteLine("Koncert br.{0} {1} {2:dd.MM.yyyy} {3} {4}", koncert.Id, koncert.Naziv, koncert.VremePocetka, koncert.Lokacija, koncert.CenaKarte);
            Console.ReadLine();
        }
    }
}
