namespace Repository.Models
{
    public class Vote_User
    {
        public Vote_User() { }

        public int Vote_UserId { get; set; }
        public int VoteId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Vote Vote { get; set; }
    }
}
