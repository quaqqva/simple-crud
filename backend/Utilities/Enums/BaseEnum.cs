namespace backend.Utilities.Enums
{
    public abstract class BaseEnum(string value)
    {
        public string Value { get; } = value;

        public override string ToString()
        {
            return Value;
        }
    }
}