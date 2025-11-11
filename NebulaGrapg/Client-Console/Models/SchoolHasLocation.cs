using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models
{
    public class SchoolHasLocation : INebulaEdge
    {
        public INebulaTag From { get; }
        public INebulaTag To { get; }

        public string EdgeName => "Has";

        public SchoolHasLocation(INebulaTag from, INebulaTag to)
        {
            From = from;
            To = to;
        }

        public string Create()
        {
            return $"INSERT EDGE {EdgeName} () VALUES \"{From.Index:N}\"->\"{To.Index:N}\":();";
        }
    }
}
