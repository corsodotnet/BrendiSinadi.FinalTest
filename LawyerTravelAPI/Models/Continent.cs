using LawyerTravelAPI.Contracts;
using System.Collections.Generic;

namespace LawyerTravelAPI.Models
{
    public class Continent:AreaGeografica, IPositiveCases
    {
        
        public virtual List<Country>? countries { get; set; }


        public void NumberPositive()
        {
            long nPositivi = 0;
            foreach (Country country in countries)
            {
                nPositivi = nPositivi + country.NPositivi;
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

    }
}
