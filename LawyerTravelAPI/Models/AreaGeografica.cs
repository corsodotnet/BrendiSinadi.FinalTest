namespace LawyerTravelAPI.Models
{
    public abstract class AreaGeografica
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long NAbitanti { get; set; }
        public string Area { get; set; }
        public long NPositivi { get; set; }
        public abstract void AreaColor() ;
    }
}
