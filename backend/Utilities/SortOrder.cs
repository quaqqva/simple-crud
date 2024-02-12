namespace backend.Utilities
{
    public class SortOrder
    {
        public static SortOrder Ascending { get; } = new("ASC");

        public static SortOrder Descending { get; } = new("DESC");

        public static SortOrder FromValue(string value)
        {
            string formattedValue = value.ToUpper();
            if (formattedValue != "ASC" && formattedValue != "DESC")
                throw new ArgumentException(
                    $"Value '{value}' does not match any of avaliable sort orders"
                );
            return formattedValue == "ASC" ? SortOrder.Ascending : SortOrder.Descending;
        }

        public string Value { get; init; }

        private SortOrder(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
