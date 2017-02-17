using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;

namespace MyWikiParser
{
	public class WikiParser
	{
		public static string GetImdbUrl(string WikiUrl)
		{
            
             return GetImdbUrlFromTable(GetBasicTable(WikiUrl));
		}

        static string GetImdbUrlFromTable(HtmlNode BasicTable)
        {
            string FullHtml = "";

            var trs = BasicTable.ChildNodes.Where(v => v.InnerHtml.Contains("IMD"));

            FullHtml = trs.ElementAt(0).InnerHtml;

            #region ParserRegion

            bool FlagA = false;
            bool FlagB = false;
            string Temp = "";

            for (int q = 0; q < FullHtml.Length; q++)
            {
                if (FullHtml[q] == 'h' && FullHtml[q + 1] == 't' && FullHtml[q + 2] == 't')
                {
                    FlagA = true;
                }

                if (FlagA)
                {
                    if (!"0123456789".Contains(FullHtml[q]))
                    {
                        if (!FlagB)
                        {
                            Temp += FullHtml[q].ToString();
                        }
                        else
                        {
                            FlagA = false;
                            break;
                        }
                    }
                    else
                    {
                        FlagB = true;
                        Temp += FullHtml[q].ToString();
                    }

                }

            }

            #endregion

            return Temp;
        }

        public static string GetName(string WikiUrl)
        {
            return GetNameFromTable(GetBasicTable(WikiUrl));
        }

        static string GetNameFromTable(HtmlNode BasicTable)
        {
            var trs = BasicTable.ChildNodes.Where(v => v.InnerHtml.Contains("center"));
            string Name = trs.ElementAt(0).InnerText;

            string NewName = "";

            for (int q = 0; q < Name.Length; q++)
            {
                if (Name[q] != '\n')
                {
                    NewName += Name[q].ToString();
                }
            }
            return NewName;
        }

        public static string GetYear(string WikiUrl)
        {
            return GetYearFromTable(GetBasicTable(WikiUrl));
        }

        static string GetYearFromTable(HtmlNode BasicTable)
        {
 
            return OnlyDigit(GetInnerTextContains(BasicTable, "Год"));
        }

        public static string GetGenre(string WikiUrl)
        {
            return GetGenreFromTable(GetBasicTable(WikiUrl));
        }

        static string GetGenreFromTable(HtmlNode BasicTable)
        {

            return MakeCleanString(GetInnerTextContains(BasicTable,"Жанр"),"Жанр");
        }

        static string GetDirectorFromTable(HtmlNode BasicTable)
        {

            return MakeCleanString(GetInnerTextContains(BasicTable,"Режиссёр"),"Режиссёр");
        }

        public static bool GetImdbData(string WikiUrl, ref string ID, ref string URL)
        {
            
            URL = GetImdbUrl(WikiUrl);

            ID = OnlyDigit(URL);
            return true;
        }

        public static bool GetAllData(string WikiUrl, ref string ID, ref string URL, ref string Name, ref string Year, ref string Genre, ref string Director)
        {
            HtmlNode Basic = GetBasicTable(WikiUrl);
            if (Basic != null)
            {
                URL = GetImdbUrlFromTable(Basic);
                ID = OnlyDigit(URL);
                Name = GetNameFromTable(Basic);
                Year = GetYearFromTable(Basic);
                Genre = GetGenreFromTable(Basic);
                Director = GetDirectorFromTable(Basic);                
                return true;
            }
            else
            {
                return false;
            }            
        }

        static string MakeCleanString(string WetString, string Extra)
        {

            if (string.IsNullOrWhiteSpace(WetString))
                return "";

            string Temp = WetString.Replace(Extra, "");
            string NewTemp = "";
            

            for (int q = 0; q < Temp.Length; q++)
            {
                if (Temp[q]!='\n')
                {
                   
                 NewTemp += Temp[q].ToString();                   
                  
                }
                else
                {
                    if (q != 0 && Temp[q - 1] != ' ')
                    {
                        NewTemp += " ";
                    }
                }
            }


            if (!char.IsLetter(NewTemp[NewTemp.Length - 2]))
            {
                NewTemp=NewTemp.Substring(0,NewTemp.Length-2);
            }

            return ReturnCleanString(NewTemp);
        }

        static HtmlNode GetBasicTable(string WikiUrl, bool TryAnother)
        {           
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

            HtmlNode Table = null;
            using (WebClient wClient = new WebClient())
            {
                wClient.Proxy = null;
                wClient.Encoding = encode;

                HtmlDocument html = new HtmlDocument();

                html.LoadHtml(wClient.DownloadString(WikiUrl));

                var trNodes = html.GetElementbyId("mw-content-text").ChildNodes.Where(x => x.Name == "table");

                Table = trNodes.ElementAt(0);               
            }

            return Table;
        }

        static string OnlyDigit(string WetString)
        {
            string Temp = "";
            

            for (int q = 0; q < WetString.Length; q++)
            {
                if ("0123456789".Contains(WetString[q]))
                {
                    Temp += WetString[q].ToString();
                }
            }

            
            return Temp;
        }

        static string ReturnCleanString(string St)
        {
           
            string Temp = "";

            Temp=St.Replace("  "," ");


            if (Temp[0] == ' ')
            {
                Temp = Temp.Substring(1);
            }

            return Temp;
        }

        static string GetInnerTextContains(HtmlNode BasicNode, string Target)
        {

       
            try
            {
                var tre = BasicNode.ChildNodes.Where(v => v.InnerText.Contains(Target));

                if (tre != null)
                {
                    return tre.ElementAt(0).InnerText;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
    


	}
}
