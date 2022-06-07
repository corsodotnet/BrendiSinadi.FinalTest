using LawyerTravelAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace LawyerTravelAPI.Controllers
{
    public class SaveCityResource
    {
        [Required]
        public long NPositivi { get; set; }
       
        public City ToCity() => new City()
        {
            NPositivi = this.NPositivi,
        };
        
    }

    public class SaveCityResource2
    {

        [Required]
        public long NPositivi { get; set; }
        public string Name { get; set; }
        public long NAbitanti { get; set; }
        public int CountryId { get; set; }
        public string Area { get; set; }
        public City ToCity2() => new City()
        {
            Name = this.Name,
            NAbitanti = this.NAbitanti,
            NPositivi = this.NPositivi,
            CountryId = this.CountryId,
            Area = this.CityArea(),
        };
        public string CityArea()
        {
            string area = "";
            if (NPositivi >= 500000)
            {
                return AREA.Red.ToString();
            }
            else if (NPositivi >= 100000 && NPositivi < 500000)
            {
                return AREA.Orange.ToString();
            }
            else if (NPositivi >= 10000 && NPositivi < 100000)
            {
                return AREA.Yellow.ToString();
            }
            else if (NPositivi < 10000)
            {
                return AREA.White.ToString();
            }
            return area;
        }
    }
}
