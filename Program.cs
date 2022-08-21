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
        public static async Task Main()
        {
            string url = null, w = null;
            int n = 0;

            Console.WriteLine("WEB WORD COUNTER\nA program that count how many times a word appears in a web");

            while(true)
            {
                Console.WriteLine("\nType the URL or exit:");
                url = Console.ReadLine();
                if (String.Equals(url.Trim().ToUpper(),"EXIT")) break;
                if (!url.StartsWith("http")) continue;

                Task<string[]> task1 = Task.Run(() => CreateWordArray(url));

                Console.WriteLine("\nType the word you want to search:");
                w = Console.ReadLine();
                
                string[] words = task1.Result;                
                
                await Task.Run(() => {
                    n = GetCountForWord(words, w);
                });
                
                Console.WriteLine("\nThe word appears {0} times.\n", n);
            }

            Console.WriteLine("END\n");
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