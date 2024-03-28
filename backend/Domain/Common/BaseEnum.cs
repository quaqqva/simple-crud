namespace Backend.Domain.Common;

public abstract class BaseEnum(string value)
{
    public string Value { get; } = value;

    public override string ToString()
    {
        return Value;
    }
}