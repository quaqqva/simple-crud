namespace backend.Database.Exceptions
{
    public class DbNotFoundException : Exception
    {
        public DbNotFoundException()
            : base("No entity found with such ID") { }
    }
}
