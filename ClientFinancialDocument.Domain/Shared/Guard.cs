namespace ClientFinancialDocument.Domain.Shared
{
    public static class Guard
    {
        public static void IsNotNulOrWhiteSpace(string? value, string? paramName, string? message = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(message ?? "The value can't be null", paramName);
            }
        }
    }
}