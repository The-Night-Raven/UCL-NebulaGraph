using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Console.Models
{
    public interface INebulaModel
    {
        string ModelName { get; }
        int Index { get; set; }
        string Create();

    }
}
