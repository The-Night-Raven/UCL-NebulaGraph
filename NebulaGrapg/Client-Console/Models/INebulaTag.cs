using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Models
{
    public interface INebulaTag
    {
        string ModelName { get; }
        Guid Index { get; }
        string Create();

    }
}
