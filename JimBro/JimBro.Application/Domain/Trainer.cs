namespace JimBro.Domain;

public class Trainer : User
{
    public string Specialization { get; private set; }
    public string Biography { get; private set; }
    public string License  { get; private set; }

    public Trainer(string name, string surname, Gender gender, DateOnly dateOfBirth, string phoneNumber,
        string emailAddress, string password, string sprecialization, string biography, string license) : base(name,
        surname, gender, dateOfBirth, phoneNumber, emailAddress, password, Role.Trainer)
    {
        Specialization = sprecialization;
        Biography = biography;
        License = license;
    }
    
    public Trainer(long id, string name, string surname, Gender gender, DateOnly dateOfBirth, string phoneNumber,
        string emailAddress, string password, string sprecialization, string biography, string license) : base(id, name,
        surname, gender, dateOfBirth, phoneNumber, emailAddress, password, Role.Trainer)
    {
        Specialization = sprecialization;
        Biography = biography;
        License = license;
    }
}