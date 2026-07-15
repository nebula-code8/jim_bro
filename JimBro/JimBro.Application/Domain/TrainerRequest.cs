namespace JimBro.Domain;

public enum RequestStatus
{
    Pending,
    Accepted,
    Rejected
}

public class TrainerRequest
{
    public long Id  { get; private set; }
    public long ClientId { get; private set; }
    public long TrainerId  { get; private set; }
    public RequestStatus Status { get; private set; }

    public TrainerRequest(long id, long clientId, long trainerId, RequestStatus status)
    {
        this.Id   = id;
        this.ClientId = clientId;
        this.TrainerId = trainerId;
        this.Status = status;
    }
    
    public TrainerRequest(long clientId, long trainerId, RequestStatus status)
    {
        this.ClientId = clientId;
        this.TrainerId = trainerId;
        this.Status = status;
    }
}
