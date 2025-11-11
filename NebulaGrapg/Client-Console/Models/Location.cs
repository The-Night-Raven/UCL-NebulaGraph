using Nebula.Common;
using Nebula.Graph;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models
{
    public class Location : INebulaTag
    {
        public string ModelName => "Locations";

        public Guid Index { get; set; }
        public string Address { get; set; }
        public short Zip { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Location(string address, short zip, string city, string name, double latitude, double longitude)
        {
            Index = Guid.NewGuid();
            Address = address;
            Zip = zip;
            City = city;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        private Location(Row row)
        {
            Index = new Guid(Encoding.UTF8.GetString(row.Values[0].SVal));
            Address = Encoding.UTF8.GetString(row.Values[1].SVal);
            Zip = ((short)row.Values[2].IVal);
            City = Encoding.UTF8.GetString(row.Values[3].SVal);
            Name = Encoding.UTF8.GetString(row.Values[4].SVal);
            Coordinate coord = row.Values[5].GgVal.PtVal.Coord;
            Latitude = coord.X; 
            Longitude = coord.Y;
        }
        public List<Location> Map(ExecutionResponse executionResponse)
        {
            List<Location> schools = new List<Location>();
            for (int i = 0; i < executionResponse.Data.Rows.Count; i++)
            {
                schools.Add(new Location(executionResponse.Data.Rows[i]));
            }
            return schools;
        }
        public string Create()
        {
            return $"INSERT VERTEX {ModelName}({nameof(Address)},{nameof(Zip)},{nameof(City)},{nameof(Name)},Location) VALUES \"{Index:N}\":(\"{Address}\",{Zip},\"{City}\",\"{Name}\",ST_Point({Longitude.ToString(CultureInfo.InvariantCulture)},{Latitude.ToString(CultureInfo.InvariantCulture)}))";
        }
    }
}
