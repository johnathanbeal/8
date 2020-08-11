using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using HyperScrape.SMTP;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HyperScrape.Lambda
{
    public class Function
    {
        
        /// <summary>
        /// Scrapes a list of Craigslist adds and sends it to a specified email
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async static Task FunctionHandler()
        {
            Email email = new Email();
            await email.SendEmail();
            //Console.WriteLine(input);
        }
    }
}
