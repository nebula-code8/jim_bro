namespace JimBro.Domain;

public class Equipment
{
    public long  Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Equipment(long id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    
    public Equipment(string name, string description)
    {
        Name = name;
        Description = description;
    }
}