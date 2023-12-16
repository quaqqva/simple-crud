namespace backend.Database.Exceptions
{
    public class DbIntegrityException: Exception
    {
        public DbIntegrityException(): base("Entity can't be updated because of foreigns constraints") {}
    }
}