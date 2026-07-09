namespace JimBro.Domain;

public class Client : User
{
    public double Height { get; private set; }
    public double Weight { get; private set; }
    public String HealthProblems { get; private set; }
    
    public Client(string name, string surname, Gender gender, DateOnly dateOfBirth, string phoneNumber, string emailAddress, string password, double height, double weight, string healthProblems) : base(name, surname, gender, dateOfBirth, phoneNumber, emailAddress, password, Role.Client)
    {
        Height =  height;
        Weight = weight;
        HealthProblems = healthProblems;
    }

    public Client(long id, string name, string surname, Gender gender, DateOnly dateOfBirth, string phoneNumber, string emailAddress, string password, double height, double weight, string healthProblems) : base(id, name, surname, gender, dateOfBirth, phoneNumber, emailAddress, password, Role.Client)
    {
        Height =  height;
        Weight = weight;
        HealthProblems = healthProblems;
    }
}