using Client.Console.Services;
using Client.Console.Models.Edges;
using Client.Console.Models.Tags;
using Nebula.Graph;
using NebulaNet;
using System.Text;
using Thrift.Protocol;
using Client.Console.Helpers;

namespace Client.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (NebulaService service = new NebulaService("127.0.0.1", 9669, "root", "nebula"))
            {
                // Connect
                await service.ExecuteAsync("USE UCLTest;");

                // Create data
                School school = new School("UCL", "https://www.ucl.dk/");
                Location UCLSeebladsgade = new Location("Seebladsgade 1", 5000, "Odense", "UCL Seebladsgade", 55.403450, 10.379370);
                Location UCLVestreEngvej = new Location("Vestre Engvej 51 C", 7100, "Vejle", "UCL Vestre Engvej", 55.707780, 9.517108);
                SchoolHasLocation schoolHasUCLSeebladsgade = new SchoolHasLocation(school, UCLSeebladsgade);
                SchoolHasLocation schoolHasUCLVestreEngvej = new SchoolHasLocation(school, UCLVestreEngvej);
                Cource softwareudvikling = new Cource(60, "Softwareudvikling");
                Cource webudvikling = new Cource(60, "Webeudvikling");
                LocationOffersCource UCLSeebladsgadeOffersSoftwareudvikling = new LocationOffersCource(UCLSeebladsgade, softwareudvikling);
                LocationOffersCource UCLVestreEngvejOffersSoftwareudvikling = new LocationOffersCource(UCLVestreEngvej, softwareudvikling);
                LocationOffersCource UCLSeebladsgadeOffersWebeudvikling = new LocationOffersCource(UCLSeebladsgade, webudvikling);
                LocationOffersCource UCLVestreEngvejOffersWebeudvikling = new LocationOffersCource(UCLVestreEngvej, webudvikling);
                await service.CreateVertexAsync(school);
                await service.CreateVertexAsync(UCLSeebladsgade);
                await service.CreateVertexAsync(UCLVestreEngvej);
                await service.CreateVertexAsync(softwareudvikling);
                await service.CreateVertexAsync(webudvikling);
                await service.CreateEdgeAsync(schoolHasUCLSeebladsgade);
                await service.CreateEdgeAsync(schoolHasUCLVestreEngvej);
                await service.CreateEdgeAsync(UCLSeebladsgadeOffersSoftwareudvikling);
                await service.CreateEdgeAsync(UCLVestreEngvejOffersSoftwareudvikling);
                await service.CreateEdgeAsync(UCLSeebladsgadeOffersWebeudvikling);
                await service.CreateEdgeAsync(UCLVestreEngvejOffersWebeudvikling);

                // Read data
                ExecutionResponse edgeResponse = await service.ExecuteAsync("MATCH ()-[e]->() RETURN e LIMIT 100;");
                string vids = VIDHelper.GetVIDs(edgeResponse.Data.Rows.Where(x => x.Values[0].EVal is not null).Select(x => x.Values[0].EVal).ToList());
                ExecutionResponse sourceResponse = await service.ExecuteAsync($"FETCH PROP ON * {vids} YIELD vertex as v;");
                List<School> schools = School.Map(sourceResponse.Data.Rows.Where(x => Encoding.UTF8.GetString(x.Values[0].VVal.Tags[0].Name) == "Schools").ToList());
                List<Location> locations = Location.Map(sourceResponse.Data.Rows.Where(x => Encoding.UTF8.GetString(x.Values[0].VVal.Tags[0].Name) == "Locations").ToList());
                List<Cource> cources = Cource.Map(sourceResponse.Data.Rows.Where(x => Encoding.UTF8.GetString(x.Values[0].VVal.Tags[0].Name) == "Cource").ToList());

            }
        }
    }
}
