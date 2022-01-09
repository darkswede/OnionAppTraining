namespace OnionAppTraining.Core.Domain
{
    public class Vehicle
    {
        public string Brand { get; protected set; }
        public string Name { get; protected set; }
        public int Seats { get; protected set; }

        protected Vehicle(string brand, string name, int seats)
        {
            Brand = brand;
            Name = name;
            Seats = seats;
        }

        public static Vehicle Create(string brand, string name, int seats) => new Vehicle(brand, name, seats);
    }
}
