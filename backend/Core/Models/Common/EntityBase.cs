namespace Core.Models.Common
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.UtcNow;
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateDelete { get; set; }
        public Guid UserCreate { get; set; }
        public Guid? UserUpdate { get; set; }
        public Guid? UserDelete { get; set; }
    }
}