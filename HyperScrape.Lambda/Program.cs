using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HyperScrape.Craigslist;
using HyperScrape.SMTP;

namespace HyperScrape
{
    class Program
    {
        static void Main(string[] args)
        {          
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();               
        }

        public async static Task MainAsync(string[] args)
        {
            Email email = new Email();
            //SearchApartments searchApartments = new SearchApartments();
            //await searchApartments.SearchApartmentsAsync(args);
            await email.SendEmail();
        }



    }
}
