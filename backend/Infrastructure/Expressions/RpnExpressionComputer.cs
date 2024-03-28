using System.Text.RegularExpressions;

namespace Backend.Infrastructure.Expressions;

public static class RpnExpressionComputer
{
    private static readonly Dictionary<string, ExpressionOperationInfo> baseOperationsInfo =
        new() { ["("] = new ExpressionOperationInfo { Cost = 0 } };

    public static TResult Compute<TResult>(
        string expression,
        Dictionary<string, ExpressionOperationInfo> operationInfos,
        string operandPattern,
        Func<string, TResult> operandCallback,
        Func<string, TResult, TResult, TResult> operatorCallback
    )
    {
        var expressionComponents = ParseToComponents(
            expression,
            operationInfos,
            operandPattern
        );
        Stack<TResult> stack = new();

        try
        {
            foreach (var component in expressionComponents)
                if (component.Type == ExpressionComponentType.Operand)
                {
                    stack.Push(operandCallback(component.Value));
                }
                else
                {
                    var operationName = component.Value;
                    if (operationName == "(")
                        throw new ArgumentException(
                            "Invalid expression string: not all brackets are closed"
                        );

                    TResult operand1 = stack.Pop(),
                        operand2 = operationInfos[operationName].Type == OperationType.Binary
                            ? stack.Pop()
                            : default!;
                    stack.Push(operatorCallback(operationName, operand1, operand2));
                }

            return stack.Pop();
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException(
                "Invalid expression string: your expression doesn't match expected pattern"
            );
        }
    }

    private static List<ExpressionComponent> ParseToComponents(
        string expression,
        Dictionary<string, ExpressionOperationInfo> operationInfos,
        string operandPattern
    )
    {
        List<ExpressionComponent> result = [];
        Stack<string> operators = new();

        var operandRegex = new Regex(
            "^" + operandPattern,
            RegexOptions.Multiline | RegexOptions.CultureInvariant
        );
        operationInfos = operationInfos.Union(baseOperationsInfo).ToDictionary();

        var charCount = expression.Length;
        for (var i = 0; i < charCount; i++)
        {
            var operation = operationInfos.Keys.FirstOrDefault(
                operation =>
                    expression.Substring(i, Math.Min(operation.Length, charCount - i))
                    == operation
            );
            if (operation != null)
            {
                if (
                    operators.Count == 0
                    || operationInfos[operation!].Cost > operationInfos[operators.Peek()].Cost
                    || operation == "("
                )
                {
                    operators.Push(operation!);
                    i += operation!.Length;
                }
                else
                {
                    result.Add(
                        new ExpressionComponent
                        {
                            Value = operators.Pop(),
                            Type = ExpressionComponentType.Operation
                        }
                    );
                }

                i -= 1;
            }
            else if (expression[i] == ')')
            {
                try
                {
                    while (operators.Peek() != "(")
                        result.Add(
                            new ExpressionComponent
                            {
                                Value = operators.Pop(),
                                Type = ExpressionComponentType.Operation
                            }
                        );
                    operators.Pop();
                }
                catch (InvalidOperationException)
                {
                    throw new ArgumentException(
                        "Invalid expression string: not all brackets are closed"
                    );
                }
            }
            else
            {
                var matches = operandRegex.Matches(expression[i..]);
                if (matches.Count == 0)
                    continue;

                var simpleExpression = matches.MaxBy(match => match.Length)!.Value;

                result.Add(
                    new ExpressionComponent
                    {
                        Value = simpleExpression,
                        Type = ExpressionComponentType.Operand
                    }
                );
                i += simpleExpression.Length - 1;
            }
        }

        while (operators.Count != 0)
            result.Add(
                new ExpressionComponent
                {
                    Value = operators.Pop(),
                    Type = ExpressionComponentType.Operation
                }
            );
        return result;
    }
}