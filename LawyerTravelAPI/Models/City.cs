using System.ComponentModel;

namespace LawyerTravelAPI.Models
{
    public class City:AreaGeografica, System.ComponentModel.INotifyPropertyChanged
    {
        public int CountryId { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Country? Country { get; set; }
        long numPositiv;
        public long NPositivi 
        {
            get { return numPositiv; }
            set
            {
                numPositiv = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NPositivi)));
            }
        }

        public City ToCity() => new City()
        {
            NPositivi = this.NPositivi,
            CountryId = this.CountryId,

        };
        

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
