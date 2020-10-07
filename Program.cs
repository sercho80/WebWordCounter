using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace WebWordCounter
{
    public class Program
    {
        public static XXXXXXXXXX Task Main()
        {
            string url = null;
            string w = null;
            int n = 0;

            Console.WriteLine("WEB WORD COUNTER\n");
            Console.WriteLine("Programa que cuenta\ncuántas veces aparece\nen una web que se indique\nla palabra que se quiera.\n");

            while(true)
            {
                Console.WriteLine("Escribe la url o exit:");
                url = Console.ReadLine();
                if (String.Equals(url.Trim().ToUpper(),"EXIT")) break;
                if (!url.StartsWith("http")) continue;

                Task<string[]> task1 = Task.Run(() => CreateWordArray(url));

                Console.WriteLine("Escribe la palabra:");
                w = Console.ReadLine();
                
                string[] words = task1.Result;                
                
                XXXXXXXXXX Task.Run(() => {
                    n = GetCountForWord(words, w);
                });
                
                Console.WriteLine("La palabra aparece {0} veces.", n);
            }

            Console.WriteLine("FIN\n");

        }

        private static int GetCountForWord(string[] words, string term)
        {
            var findWord = from word in words
                           where word.ToUpper().Contains(term.ToUpper())
                           select word;
            return findWord.Count();
        }

        static string[] CreateWordArray(string uri)
        {
            string s = new WebClient().DownloadString(uri);
            return s.Split(
                new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/', '\\', '<', '>', '(', ')', '"', '\'', '&', '»' },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}


// > dotnet run
// WEB WORD COUNTER
//
// Programa que cuenta
// cuántas veces aparece
// en una web que se indique
// la palabra que se quiera.
//
// Escribe la url o exit:
// https://es.wikipedia.org/wiki/Microsoft_Windows
// Escribe la palabra:
// linux
// La palabra aparece 22 veces.
// Escribe la url o exit:
// https://es.wikipedia.org/wiki/GNU/Linux
// Escribe la palabra:
// Windows
// La palabra aparece 8 veces.
// Escribe la url o exit:
// exit
// FIN

