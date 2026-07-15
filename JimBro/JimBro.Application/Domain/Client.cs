namespace JimBro.Domain;

public class Client : User
{
    public double Height { get; private set; }
    public double Weight { get; private set; }
    public string Goal { get; private set; }
    public TrainingLocation TrainingLocation { get; private set; }
    public String HealthProblems { get; private set; }
    public List<Accessory> Accessories { get; private set; } = new();
    public List<Machine> Machines { get; private set; } = new();

    public Client(string name, string surname, Gender gender, DateOnly dateOfBirth, string phoneNumber,
        string emailAddress, string password, double height, double weight, string goal,
        TrainingLocation trainingLocation, string healthProblems) : base(name, surname, gender, dateOfBirth,
        phoneNumber, emailAddress, password, Role.Client)
    {
        Height = height;
        Weight = weight;
        Goal = goal;
        TrainingLocation = trainingLocation;
        HealthProblems = healthProblems;
    }

    public Client(long id, string name, string surname, Gender gender, DateOnly dateOfBirth, string phoneNumber,
        string emailAddress, string password, double height, double weight, string goal,
        TrainingLocation trainingLocation, string healthProblems) : base(id, name, surname, gender, dateOfBirth,
        phoneNumber, emailAddress, password, Role.Client)
    {
        Height = height;
        Weight = weight;
        Goal = goal;
        TrainingLocation = trainingLocation;
        HealthProblems = healthProblems;
    }
    
    public void AddAccessory(Accessory accessory)
    {
        if (!Accessories.Contains(accessory))
            Accessories.Add(accessory);
    }
    
    public void RemoveAccessory(Accessory accessory)
    {
        Accessories.Remove(accessory);
    }
    
    public void AddMachine(Machine machine)
    {
        if (!Machines.Contains(machine))
            Machines.Add(machine);
    }
    
    public void RemoveMachine(Machine machine)
    {
        Machines.Remove(machine);
    }
}