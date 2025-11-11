using Nebula.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Console.Models
{
    public class School : INebulaModel
    {
        public string ModelName => "Schools";

        public int Index { get; set; }

        public string Name { get; set; }
        public Point Point{ get; set; }
        public Nebula.Graph. MyProperty { get; set; }

        public string Create()
        {
            throw new NotImplementedException();
        }
    }
}
