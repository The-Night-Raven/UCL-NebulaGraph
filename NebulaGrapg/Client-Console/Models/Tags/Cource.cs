using Client.Console.Models;
using Nebula.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.Console.Models.Tags
{
    public class Cource : INebulaTag
    {
        public string ModelName => "Cource";

        public Guid VID { get; }
        public short ECTS { get; }
        public string Name { get; }

        public Cource(short ects, string name)
        {
            VID = Guid.NewGuid();
            ECTS = ects;
            Name = name;

        }
        private Cource(Vertex vertex)
        {
            VID = new Guid(Encoding.UTF8.GetString(vertex.Vid.SVal));
            ECTS = (short)vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(ECTS)).Value.IVal;
            Name = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Name)).Value.SVal);
        }
        public static List<Cource> Map(List<Row> executionResponse)
        {
            List<Cource> cources = new List<Cource>();
            for (int i = 0; i < executionResponse.Count; i++)
            {
                cources.Add(new Cource(executionResponse[i].Values[0].VVal));
            }
            return cources;
        }
        public string Create()
        {
            return $"INSERT VERTEX {ModelName}({nameof(ECTS)},{nameof(Name)}) VALUES \"{VID:N}\":({ECTS},\"{Name}\")";
        }
    }
}
