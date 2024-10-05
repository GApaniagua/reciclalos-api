using Infrastructure.Persistence.Models;

public class Game
{
    public int ID { get; set; }
    public string Name { get; set; }
    public virtual List<Question> Questions { get; set; }
}
