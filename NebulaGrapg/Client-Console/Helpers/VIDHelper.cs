using Nebula.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Helpers
{
    public static class VIDHelper
    {
        public static string GetVIDs(List<Edge> edges)
        {
            if (edges.Count == 0)
            {
                return string.Empty;
            }
            List<string> ids = new List<string>();
            for(int i = 0; i < edges.Count; i++)
            {
                ids.Add(Encoding.UTF8.GetString(edges[i].Src.SVal));
                ids.Add(Encoding.UTF8.GetString(edges[i].Dst.SVal));
            }
            ids = ids.Distinct().ToList();
            StringBuilder sb = new StringBuilder($"\"{ids[0]}\"");
            for (int i = 1; i < ids.Count; i++)
            {
                sb.Append($", \"{ids[i]}\"");
            }
            return sb.ToString();
        }
    }
}
