using System.ComponentModel.DataAnnotations.Schema;

public class AddedTime
{
    public int Id { get; set; }
    public DateTime StartAddedTime { get; set; }
    public DateTime EndAddedTime { get; set; }

    [NotMapped]
    public TimeSpan Duration => EndAddedTime - StartAddedTime;
}
