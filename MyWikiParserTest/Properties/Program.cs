using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWikiParser;

namespace MyWiki
{
    class Program
    {
        static void Main(string[] args)
        {
           
            
            string[] URLS=new string[10];

			URLS[0] = @"https://ru.wikipedia.org/wiki/Чужой_(фильм)";
			URLS[1] = @"https://ru.wikipedia.org/wiki/Тельма_и_Луиза";
			URLS[2] = @"https://ru.wikipedia.org/wiki/Версия_Браунинга_(фильм)";
			URLS[3] = @"https://ru.wikipedia.org/wiki/Неприятности_с_обезьянкой";
			URLS[4] = @"https://ru.wikipedia.org/wiki/Хороший_год_(фильм)";
			URLS[5] = @"https://ru.wikipedia.org/wiki/Обличитель";
			URLS[6] = @"https://ru.wikipedia.org/wiki/Верные_друзья_(фильм)";
			URLS[7] = @"https://ru.wikipedia.org/wiki/Красная_палатка_(фильм)";
			URLS[8] = @"https://ru.wikipedia.org/wiki/Летят_журавли";
			URLS[9] = @"https://ru.wikipedia.org/wiki/Четыре_комнаты";

			string URL = "";
            string ID = "";
            string Name = "";
            string Year = "";
            string Genre = "";
            string Director = "";

            for (int q = 0; q < 10; q++)
            {
                WikiParser.GetAllData(URLS[q], ref ID, ref URL, ref Name, ref Year, ref Genre, ref Director);

                Console.WriteLine(URL);
                Console.WriteLine("***");
                Console.WriteLine(ID);
                Console.WriteLine("***");
                Console.WriteLine(Name);
                Console.WriteLine("***");
                Console.WriteLine(Year);
                Console.WriteLine("***");
                Console.WriteLine(Genre);
                Console.WriteLine("***");
                Console.WriteLine(Director);
                Console.WriteLine("---------------------------------------------------------------");
            }
            

     
            Console.ReadLine();

        }
    }
}
