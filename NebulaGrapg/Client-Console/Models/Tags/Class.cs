using Client.Console.Models;
using Nebula.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models.Tags
{
    internal class Class : INebulaTag
    {
        public string ModelName => "Cource";

        public Guid VID { get; }
        public short ECTS { get; }
        public DateOnly StartDate { get; }
        public string Name { get; }

        public Class(string name, short ects, DateOnly startDate)
        {
            VID = Guid.NewGuid();
            Name = name;
            ECTS = ects;
            StartDate = startDate;

        }
        private Class(Vertex vertex)
        {
            VID = new Guid(Encoding.UTF8.GetString(vertex.Vid.SVal));
            ECTS = (short)vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(ECTS)).Value.IVal;
            Name = Encoding.UTF8.GetString(vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(Name)).Value.SVal);
            Date dVal = vertex.Tags[0].Props.FirstOrDefault(x => Encoding.UTF8.GetString(x.Key) == nameof(StartDate)).Value.DVal;
            StartDate = new DateOnly(dVal.Year, dVal.Month, dVal.Day);
        }
        public static List<Class> Map(List<Row> executionResponse)
        {
            List<Class> classes = new List<Class>();
            for (int i = 0; i < executionResponse.Count; i++)
            {
                classes.Add(new Class(executionResponse[i].Values[0].VVal));
            }
            return classes;
        }
        public string Create()
        {
            return $"INSERT VERTEX {ModelName}({nameof(Name)},{nameof(ECTS)},{nameof(StartDate)}) VALUES \"{VID:N}\":(\"{Name}\",{ECTS},\"{StartDate}\")";
        }
    }
}
