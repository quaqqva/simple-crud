using Backend.Domain.Common;

namespace Backend.Application.Enums;

public class SortOrder : BaseEnum
{
    private SortOrder(string value)
        : base(value)
    {
    }

    public static SortOrder Ascending { get; } = new("ASC");

    public static SortOrder Descending { get; } = new("DESC");

    public static SortOrder FromValue(string value)
    {
        var formattedValue = value.ToUpper();
        if (formattedValue != "ASC" && formattedValue != "DESC")
            throw new ArgumentException(
                $"Value '{value}' does not match any of avaliable sort orders"
            );
        return formattedValue == "ASC" ? Ascending : Descending;
    }
}