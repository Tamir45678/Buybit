namespace Balance.Entities
{
    public class UserBalance
    {
        public int Id { get; set; }
        public int Budget { get; set; } 

        public UserBalance(int id, int budget)
        {
            Id = id;
            Budget = budget;
        }

        public UserBalance() { }
    }
}
