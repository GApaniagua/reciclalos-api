namespace Application.GameCore;

public class AnswerDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
}

public class GameViewDto
{
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public string ImageUrl { get; set; }
    public List<AnswerDto> Answers { get; set; }
}