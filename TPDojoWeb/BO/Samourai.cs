namespace TPDojoWeb.BO
{
    public class Samourai
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public int Force { get; set; }

        public virtual Arme? Arme { get; set; }
        public List<ArtMartial>? ArtMartials{ get; set; }
    }
}
