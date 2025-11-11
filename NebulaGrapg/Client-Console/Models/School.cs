using Nebula.Common;
using Nebula.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models
{
    public class School : INebulaTag
    {
        public string ModelName => "Schools";

        public Guid Index { get; }

        public string Name { get; }
        public string Website { get; }
        public School(string name, string website)
        {
            Index = Guid.NewGuid();
            Name = name;
            Website = website;
        }
        private School(Row row)
        {
            Index = new Guid(Encoding.UTF8.GetString(row.Values[0].SVal));
            Website = Encoding.UTF8.GetString(row.Values[1].SVal);
        }
        public List<School> Map(ExecutionResponse executionResponse)
        {
            List<School> schools = new List<School>();
            for (int i = 0; i < executionResponse.Data.Rows.Count; i++)
            {
                schools.Add(new School(executionResponse.Data.Rows[i]));
            }
            return schools;
        }

        public string Create()
        {
            return $"INSERT VERTEX {ModelName}({nameof(Name)},{nameof(Website)}) VALUES \"{Index:N}\":(\"{Name}\",\"{Website}\")";
        }
    }
}
