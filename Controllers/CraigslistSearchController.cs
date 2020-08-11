using HyperScrape.Craigslist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperScrape.Controllers
{
    public class CraigslistSearchController
    {
        public async Task<List<string>> Post()
        {
            SearchApartments searchApartments = new SearchApartments();
            var ads = await searchApartments.SearchApartmentsAsync(new string[] { });
            return ads;
        }
    }
}
