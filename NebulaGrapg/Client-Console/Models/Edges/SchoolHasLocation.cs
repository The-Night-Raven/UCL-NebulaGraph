using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Console.Models;

namespace Client.Console.Models.Edges
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
            return $"INSERT EDGE {EdgeName} () VALUES \"{From.VID:N}\"->\"{To.VID:N}\":();";
        }
    }
}
