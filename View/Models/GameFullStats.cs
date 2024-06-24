namespace DeathCounter.View.Models
{
    public class GameFullStats
    {
        public string GameName { get; set; } = string.Empty;
        public int TotalDeaths { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}