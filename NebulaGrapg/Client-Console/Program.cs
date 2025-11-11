using Client.Console.Models;
using Client.Console.Services;
using Nebula.Graph;
using NebulaNet;
using Thrift.Protocol;

namespace Client.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (NebulaService service = new NebulaService("127.0.0.1", 9669, "root", "nebula"))
            {
                await service.ExecuteAsync("USE UCLTest;");
                School school = new School("UCL","https://www.ucl.dk/");
                Location UCLSeebladsgade = new Location("Seebladsgade 1", 5000, "Odense", "UCL Seebladsgade", 55.403450, 10.379370);
                Location UCLVestreEngvej = new Location("Vestre Engvej 51 C", 7100, "Vejle", "UCL Vestre Engvej", 55.707780, 9.517108);
                SchoolHasLocation schoolHasUCLSeebladsgade = new SchoolHasLocation(school, UCLSeebladsgade);
                SchoolHasLocation schoolHasUCLVestreEngvej = new SchoolHasLocation(school, UCLVestreEngvej);
                await service.CreateVertexAsync(school);
                await service.CreateVertexAsync(UCLSeebladsgade);
                await service.CreateEdgeAsync(schoolHasUCLSeebladsgade);
                ExecutionResponse executionResponse1 = await service.CreateEdgeAsync(schoolHasUCLVestreEngvej);


            }
        }
    }
}
