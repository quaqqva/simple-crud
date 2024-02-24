namespace backend.Utilities.Enums
{
    public class SortOrder: BaseEnum
    {
        public static SortOrder FromValue(string value)
        {
            string formattedValue = value.ToUpper();
            if (formattedValue != "ASC" && formattedValue != "DESC")
                throw new ArgumentException(
                    $"Value '{value}' does not match any of avaliable sort orders"
                );
            return formattedValue == "ASC" ? SortOrder.Ascending : SortOrder.Descending;
        }

        public static SortOrder Ascending { get; } = new("ASC");

        public static SortOrder Descending { get; } = new("DESC");

        private SortOrder(string value): base(value) {}
    }
}
