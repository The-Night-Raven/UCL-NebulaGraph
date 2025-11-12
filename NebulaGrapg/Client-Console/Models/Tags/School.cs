using Client.Console.Models;
using Nebula.Common;
using Nebula.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models.Tags
{
    public class School : INebulaTag
    {
        public string ModelName => "Schools";

        public Guid VID { get; }

        public string Name { get; }
        public string Website { get; }
        public School(string name, string website)
        {
            VID = Guid.NewGuid();
            Name = name;
            Website = website;
        }
        private School(Vertex vertex)
        {
            VID = new Guid(Encoding.UTF8.GetString(vertex.Vid.SVal));
            Website = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Website)).Value.SVal);
            Name = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Name)).Value.SVal);
        }
        public static List<School> Map(List<Row> executionResponse)
        {
            List<School> schools = new List<School>();
            for (int i = 0; i < executionResponse.Count; i++)
            {
                schools.Add(new School(executionResponse[i].Values[0].VVal));
            }
            return schools;
        }

        public string Create()
        {
            return $"INSERT VERTEX {ModelName}({nameof(Name)},{nameof(Website)}) VALUES \"{VID:N}\":(\"{Name}\",\"{Website}\")";
        }
    }
}
