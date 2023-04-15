namespace Survey.Microservices.Architecture.Domain.Entities.v1
{
    public class Survey : BaseEntity
    {
        public string Question { get; set; }
        public IEnumerable<string> AvailableAnswers { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
