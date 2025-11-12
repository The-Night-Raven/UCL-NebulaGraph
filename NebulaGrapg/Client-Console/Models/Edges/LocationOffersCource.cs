using Client.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models.Edges
{
    internal class LocationOffersCource : INebulaEdge
    {
        public INebulaTag From { get; }
        public INebulaTag To { get; }

        public string EdgeName => "Offers";

        public LocationOffersCource(INebulaTag from, INebulaTag to)
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
