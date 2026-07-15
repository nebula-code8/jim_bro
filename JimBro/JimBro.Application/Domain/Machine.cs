namespace JimBro.Domain;

public class Machine
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Machine(long id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public Machine(string name, string description)
    {
        Name = name;
        Description = description;
    }
}