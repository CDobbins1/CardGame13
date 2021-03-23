namespace CardGame13.Game
{
    public class Player
    {
        public string? Name { get; set; }

        public int PlayerNumber { get; set; }

        public int CardCount { get; set; } = 13;

        public override string ToString()
        {
            return $"{Name}\nCards Remaining: {CardCount}";
        }
    }
}
