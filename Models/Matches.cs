namespace BodyaBet.Models
{
    public class Matches
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public int? TournamentId { get; set; }
        public int HomeTeamId { get; set; }
        public int GuestTeamId { get; set; }

        public virtual Tournament Tournament { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Team GuestTeam { get; set; }

    }
}
