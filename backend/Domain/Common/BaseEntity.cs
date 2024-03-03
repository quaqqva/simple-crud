namespace Backend.Domain.Common
{
    public abstract record BaseEntity
    {
        public Guid? Id { get; set; }
    }
}
