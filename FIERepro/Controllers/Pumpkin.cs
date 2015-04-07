namespace FIERepro.Controllers
{
    public class Pumpkin
    {
        public int Id { get; private set; }

        public decimal Weight { get; private set; }

        public string Color { get; private set; }

        public Pumpkin(int id, decimal weight, string color)
        {
            Id = id;
            Weight = weight;
            Color = color;
        }
    }
}
