namespace JimBro.Domain;

public class WorkoutExercise
{
    public long Id { get; private set; }
    public int Sets {get; private set;}
    public int Reps {get; private set;}
    public DateOnly CompletionDate { get; private set; }
    public Exercise Exercise { get; private set; }
    public Workout Workout { get; private set; }

    public WorkoutExercise(long id, int sets, int reps, DateOnly completionDate, Exercise exercise, Workout workout)
    {
        Id = id;
        Sets = sets;
        Reps = reps;
        CompletionDate = completionDate;
        Exercise = exercise;
        Workout = workout;
    }
    
    public WorkoutExercise(int sets, int reps, DateOnly completionDate, Exercise exercise, Workout workout)
    {
        Sets = sets;
        Reps = reps;
        CompletionDate = completionDate;
        Exercise = exercise;
        Workout = workout;
    }
}