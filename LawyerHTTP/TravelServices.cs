using LawyerTravelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LawyerHTTP
{
    public delegate void DelegateCountry(string text);
    public static class TravelServices
    {
        static HttpClient client = new HttpClient();

        static async Task<Country> GetCountryByNameAsync(string Name)
        {
            Country country = null;
            HttpResponseMessage response = await client.GetAsync($"/api/AreaGeographica/Country/{Name}");
            if (response.IsSuccessStatusCode)
            {
                country = await response.Content.ReadAsAsync<Country>();
            }
            return country;
        }

        static async void Message(Country country, DelegateCountry deleg)
        {

            if (country.Area == AREA.Red.ToString())
            {
                deleg("You country of choice is : ");
                Console.ForegroundColor = ConsoleColor.Red;
                deleg(country.Name);
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" .Covid situation currently is: ");
                Console.ForegroundColor = ConsoleColor.Red;
                deleg($"{country.NPositivi}");
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" postive cases.");
            }
            else if (country.Area == AREA.Orange.ToString())
            {
                deleg("You country of choice is : ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                deleg(country.Name);
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" .Covid situation currently is: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                deleg($"{country.NPositivi}");
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" postive cases.");
            }
            else if (country.Area == AREA.Yellow.ToString())
            {
                deleg("You country of choice is : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                deleg(country.Name);
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" .Covid situation currently is: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                deleg($"{country.NPositivi}");
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" postive cases.");
            }
            else if (country.Area == AREA.White.ToString())
            {
                deleg("You country of choice is : ");
                Console.ForegroundColor = ConsoleColor.Blue;
                deleg(country.Name);
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" .Covid situation currently is: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                deleg($"{country.NPositivi}");
                Console.ForegroundColor = ConsoleColor.White;
                deleg(" postive cases.");
            }
        }


        public static async Task RunAsync(string countryName , DelegateCountry deleg)
        {

            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Country country = null;
                country = await GetCountryByNameAsync(countryName);
                Message(country, deleg);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        

    }
}
