namespace backend.Database.Exceptions
{
    public class DbIntegrityException: Exception
    {
        public DbIntegrityException(): base("Entity can't be created/updated because of foreign constraints") {}
    }
}