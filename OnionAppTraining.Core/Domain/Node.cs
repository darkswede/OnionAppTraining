using System;

namespace OnionAppTraining.Core.Domain
{
    public class Node
    {
        public string Address { get; protected set; }
        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Node(string address, double longitude, double latitude)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw new Exception($"Address is empty");
            }

            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLongitude(double longitude)
        {
            if (double.IsNaN(longitude))
            {
                throw new Exception("Longitude must be a number");
            }
            if (Longitude == longitude)
            {
                return;
            }

            Longitude = longitude;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLatitude(double latitude)
        {
            if (double.IsNaN(latitude))
            {
                throw new Exception("Latitude must be a number");
            }
            if (Latitude == latitude)
            {
                return;
            }

            Latitude = latitude;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Node Create(string address, double longitude, double latitude) => new Node(address, longitude, latitude);
    }
}
