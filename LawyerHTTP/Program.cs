using LawyerHTTP;
using LawyerTravelAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientHttp.Lesson
{
    class Program
    {
        static void Main()
        {
            Lawyer lawyer = new Lawyer();
            lawyer.OrderFlight("Korea");

        }
 
        public class Lawyer
        {
            public string Name { get; set; }
            DelegateCountry deleg;

            public Lawyer()
            {
                deleg = FeedBack;
            }


            public void OrderFlight(string countryName)
            {
                TravelServices.RunAsync(countryName, deleg).GetAwaiter().GetResult();
            }
            static void FeedBack(string message)
            {
                Console.Write(message);
            }
        }

    }
}