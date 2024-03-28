namespace Backend.Infrastructure.Expressions;

public enum ExpressionComponentType
{
    Operand,
    Operation
}

public struct ExpressionComponent
{
    public string Value { get; init; }

    public ExpressionComponentType Type { get; init; }
}