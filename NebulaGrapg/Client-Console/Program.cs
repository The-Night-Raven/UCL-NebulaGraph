using Nebula.Graph;
using NebulaNet;
using Thrift.Protocol;

namespace Client_Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (NebulaConnection client = new NebulaConnection())
            {
                await client.OpenAsync("127.0.0.1", 9669);
                AuthResponse authResponse = await client.AuthenticateAsync("root", "nebula");
                ExecutionResponse executionResponse = await client.ExecuteAsync(authResponse.Session_id, "USE Test;");

                await client.ExecuteAsync(authResponse.Session_id, "INSERT VERTEX Schools (Name, Website) VALUES 1:(\"UCL\",\"https://www.ucl.dk/\")");
                await client.ExecuteAsync(authResponse.Session_id, "INSERT VERTEX Location (InternalName, Location) VALUES 1:(\"Odense\",ST_Point(55.403450,10.379370))");
                await client.ExecuteAsync(authResponse.Session_id, "INSERT VERTEX Location (InternalName, Location) VALUES 2:(\"Vejle\",ST_Point(55.707396,9.517151))");
                
            }
        }
    }
}
