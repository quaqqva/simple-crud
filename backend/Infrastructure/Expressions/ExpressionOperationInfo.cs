namespace Backend.Infrastructure.Expressions;

public enum OperationType
{
    Unary,
    Binary
}

public struct ExpressionOperationInfo
{
    public OperationType Type { get; init; }

    public int Cost { get; init; }
}