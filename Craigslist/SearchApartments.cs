using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HyperScrape.Craigslist
{
    public class SearchApartments
    {
        public async Task<List<String>> SearchApartmentsAsync(string[] args)
        {
            HttpClient clientForListings = new HttpClient();
            var listingResponse = await clientForListings.GetAsync("https://washingtondc.craigslist.org/search/nva/apa?query=arlington&min_price=1&max_price=750&availabilityMode=0&sale_date=all+dates");
            var listingPageContents = await listingResponse.Content.ReadAsStringAsync();

            HtmlDocument listingPageDocument = new HtmlDocument();
            listingPageDocument.LoadHtml(listingPageContents);

            var listings = listingPageDocument.DocumentNode.SelectNodes("//li[contains(@class, 'result-row')]//a/@href");

            int listingIndex = 0;

            //String emailBodyBuilder = "";
            var _listing = new List<String>();
            foreach (var listing in listings)
            {
                var listingOuterhtml = listing.OuterHtml;

                if (!listingOuterhtml.Contains("https://washingtondc.craigslist.org/nva/"))
                    continue;
                //string ting0 = "<a href=\"https://washingtondc.craigslist.org/nva/apa/d/arlington-renovated-1-beds-1-baths/7157683767.html\" class=\"result-image gallery empty\"></a>";
                var listingHref = listingOuterhtml.Substring(listingOuterhtml.IndexOf("https:"));
                int removeAfterIndex = listingHref.IndexOf("\"");
                if (removeAfterIndex > 0)
                    listingHref = listingHref.Substring(0, removeAfterIndex);
                HttpClient client = new HttpClient();
                //var response = await client.GetAsync("https://washingtondc.craigslist.org/nva/apa/d/arlington-720-and-you-will-pay-part-of/7157292172.html");
                var response = await client.GetAsync(listingHref);


                var pageContents = await response.Content.ReadAsStringAsync();

                HtmlDocument pageDocument = new HtmlDocument();
                pageDocument.LoadHtml(pageContents);

                //var listings = pageDocument.DocumentNode.SelectNodes("(//ul[contains(@class, 'rows')]//li[contains(@class, 'result-row')])");
                var price = pageDocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'price')]");
                //var rooms = pageDocument.DocumentNode.SelectSingleNode("//span[contains(@class, 'housing')]");
                var postTitle = pageDocument.DocumentNode.SelectSingleNode("(//h2[contains(@class, 'postingtitle')]//span[@id='titletextonly'])");
                var userbody = pageDocument.DocumentNode.SelectSingleNode(
                    "(//section[contains(@class, 'userbody')]//section[@id = 'postingbody'])");
                var qrCode = pageDocument.DocumentNode.SelectSingleNode("//p[contains(@class, 'print-qrcode-label')]");
                var displayText = userbody.InnerText.Replace(qrCode.InnerText, "");

                //var number = listings.Count;
                //foreach(var listedPost in listings)
                //{
                //    Console.WriteLine(listedPost.InnerText);

                //}
                //string spacers = "-----------------------------------------------------------------------------------------------------------------------";
                //Console.WriteLine(spacers);

                //Console.WriteLine("Listing " + (listingIndex + 1));

                //Console.WriteLine("Price : " + price.InnerText);
                //Console.WriteLine("Rooms : " + rooms.InnerText);
                //Console.WriteLine("Post Title : " + postTitle.InnerText.Trim());
                //Console.WriteLine(displayText.Trim());
                //Console.WriteLine(spacers);

                String emailBodyBuilder = //emailBodyBuilder + Environment.NewLine +
                    "Listing " + (listingIndex + 1) + Environment.NewLine + "<br>" +
                    "Price : " + price.InnerText + Environment.NewLine + "<br>" +
                    //"Rooms : " + rooms.InnerText + Environment.NewLine + "<br>" +
                    "Post Title : " + postTitle.InnerText.Trim() + Environment.NewLine + "<br>" +
                    displayText.Trim() + Environment.NewLine + "<br>" +
                    "\r\n" + "<br>";
                //Console.ReadLine();
                _listing.Add(emailBodyBuilder);
                listingIndex++;
            }
            foreach (var _list in _listing)
            {
                Console.WriteLine(_list);
            }
            
            //return emailBodyBuilder;
            return _listing;
        }
    }
}
