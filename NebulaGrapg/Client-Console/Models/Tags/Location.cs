using Client.Console.Models;
using Nebula.Common;
using Nebula.Graph;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models.Tags
{
    public class Location : INebulaTag
    {
        public string ModelName => "Locations";

        public Guid VID { get; }
        public string Address { get;  }
        public short Zip { get; }
        public string City { get; }
        public string Name { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public Location(string address, short zip, string city, string name, double latitude, double longitude)
        {
            VID = Guid.NewGuid();
            Address = address;
            Zip = zip;
            City = city;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        private Location(Vertex vertex)
        {
            VID = new Guid(Encoding.UTF8.GetString(vertex.Vid.SVal));
            Address = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Address)).Value.SVal);
            Zip = (short)vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Zip)).Value.IVal;
            City = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(City)).Value.SVal);
            Name = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Name)).Value.SVal);
            Coordinate coord = vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == "Location").Value.GgVal.PtVal.Coord;
            Latitude = coord.X;
            Longitude = coord.Y;
        }
        public static List<Location> Map(List<Row> executionResponse)
        {
            List<Location> locations = new List<Location>();
            for (int i = 0; i < executionResponse.Count; i++)
            {
                locations.Add(new Location(executionResponse[i].Values[0].VVal));
            }
            return locations;
        }
        public string Create()
        {
            return $"INSERT VERTEX {ModelName}({nameof(Address)},{nameof(Zip)},{nameof(City)},{nameof(Name)},Location) VALUES \"{VID:N}\":(\"{Address}\",{Zip},\"{City}\",\"{Name}\",ST_Point({Longitude.ToString(CultureInfo.InvariantCulture)},{Latitude.ToString(CultureInfo.InvariantCulture)}))";
        }
    }
}
