namespace Client.Console.Models
{
    public interface INebulaEdge
    {
        INebulaTag From { get; }
        INebulaTag To { get; }
        string EdgeName { get; }

        string Create();
    }
}