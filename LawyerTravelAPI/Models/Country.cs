using LawyerTravelAPI.Contracts;
using System.Collections.Generic;
using System.ComponentModel;

namespace LawyerTravelAPI.Models
{
    public class Country : AreaGeografica, IPositiveCases
    {
        
        public virtual Continent? Continent { get; set; }
        public virtual List<City>? cities { get; set; }
        public int ContinentId { get; set; }


        public void NumberPositive()
        {
            long nPositivi = 0;
            foreach (City city in cities)
            {
                nPositivi = nPositivi + city.NPositivi;
            }
            NPositivi = nPositivi;
        }

        public override void AreaColor()
        {
            if (NPositivi >= 500000)
            {
                Area = AREA.Red.ToString();
            }
            else if (NPositivi >= 100000 && NPositivi < 500000)
            {
                Area = AREA.Orange.ToString();
            }
            else if (NPositivi >= 10000 && NPositivi < 100000)
            {
                Area = AREA.Yellow.ToString();
            }
            else if (NPositivi < 10000)
            {
                Area = AREA.White.ToString();
            }
        }

        public Country ToCountry() => new Country()
        {
            NPositivi = this.NPositivi,
            ContinentId = this.ContinentId,

        };

        public Country(City city)
        {
            city.PropertyChanged += NPositiveChangedEvent;
        }

        public Country()
        {

        }

        public void NPositiveChangedEvent(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "NPositive")
            {
                NumberPositive();
                AreaColor();


            }
        }
    }
}
