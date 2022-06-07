using LawyerTravelAPI.Persistence.Configuration;
using LawyerTravelAPI.Models;
using System;
using System.Collections.Generic;
using LawyerTravelAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LawyerTravelAPI.Utils
{
    public static class SeedData
    {
        
        public static async Task SeedDatabase(DatabaseCxt dbCtx)
        {

            Clear(dbCtx.City);
            Clear(dbCtx.Country);
            Clear(dbCtx.Continent);



            List<City> ItalyCities = new List<City>()
            {
                new City() { Name = "Milano", NAbitanti = 10000000, NPositivi = 100000},
                new City() { Name = "Roma", NAbitanti = 10500000, NPositivi = 100000},
                new City() { Name = "Padova", NAbitanti = 13000000, NPositivi = 10000},
                new City() { Name = "Firenze", NAbitanti = 11100000, NPositivi = 20000 }
            };
            List<City> AlbaniaCities = new List<City>()
            {
               new City() { Name = "Tirana", NAbitanti = 10000000, NPositivi = 20000},
               new City() { Name = "Elbasan", NAbitanti = 10500000, NPositivi = 105000},
               new City() { Name = "Lezhe", NAbitanti = 13000000, NPositivi = 1023000},
               new City() { Name = "Kavaje", NAbitanti = 11100000,  NPositivi = 105000 }
            };

            List<City> ChinaCities = new List<City>()
            {
                new City() { Name = "Shangai", NAbitanti = 10000000, NPositivi = 340000},
                new City() { Name = "Beijing", NAbitanti = 10500000, NPositivi = 500000},
                new City() { Name = "Wuhan", NAbitanti = 13000000, NPositivi = 1000},
                new City() { Name = "Fuzhou", NAbitanti = 11100000, NPositivi = 9000 }
            };
            List<City> koreanCities = new List<City>()
            {
               new City() { Name = "Seul", NAbitanti = 10000000, NPositivi = 100000},
               new City() { Name = "Busan", NAbitanti = 10500000, NPositivi = 100000},
               new City() { Name = "Incheon", NAbitanti = 13000000, NPositivi = 10000},
               new City() { Name = "Ulsan", NAbitanti = 11100000, NPositivi = 10000 }
            };


            List<Country> EuropeanCountry = new List<Country>()
            {
                new Country() {Name = "Italy", NAbitanti = 10000000, cities = ItalyCities},
                new Country() {Name = "Albania", NAbitanti = 10000000, cities = AlbaniaCities},

            };
            List<Country> AsianCountry = new List<Country>()
            {
                new Country() {Name = "China", NAbitanti = 10000000, cities = ChinaCities},
                new Country() {Name = "Korea", NAbitanti = 10000000, cities = koreanCities},

            };


            Continent _continent = new Continent()
            {
                Name = "Europe",
                NAbitanti = 751000000,
                countries = new List<Country>(),
            };
            Continent _continent1 = new Continent()
            {
                Name = "Asia",
                NAbitanti = 4716448660,
                countries = new List<Country>(),
            };

            foreach (City city in ItalyCities)
            {
                city.AreaColor();
            }
            foreach (City city in AlbaniaCities)
            {
                city.AreaColor();
            }
            foreach (City city in ChinaCities)
            {
                city.AreaColor();
            }
            foreach (City city in koreanCities)
            {
                city.AreaColor();
            }

            foreach (Country country in EuropeanCountry)
            {
                country.NumberPositive();
                country.AreaColor();
            }
            foreach (Country country in AsianCountry)
            {
                country.NumberPositive();
                country.AreaColor();
            }

            ItalyCities[0].Country = EuropeanCountry[0];
            ItalyCities[0].NPositivi = 100000;


            _continent.countries.AddRange(EuropeanCountry);
            _continent1.countries.AddRange(AsianCountry);



            _continent.NumberPositive();
            _continent.AreaColor();

            _continent1.NumberPositive();
            _continent1.AreaColor();


            dbCtx.Continent.Add(_continent);
            dbCtx.Continent.Add(_continent1);

            try
            {
                await dbCtx.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            if (dbSet.Any())
            {
                dbSet.RemoveRange(dbSet.ToList());
            }
        }

    }
}


