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
        // All tags must have a unique VID (Primary key)
        Guid VID { get; }
        string Create();

    }
}
