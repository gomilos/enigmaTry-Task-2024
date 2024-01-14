namespace ClientFinancialDocument.Domain.Shared
{
    public record Error
    {
        public static Error None = new(string.Empty, string.Empty, ErrorType.Failure);

        public static Error NullValue = new("Error.NullValue", "Null value was provided", ErrorType.Failure);

        private Error(string code, string name, ErrorType errorType)
        {
            Code = code;
            Name = name;
            ErrorType = errorType;
        }

        public string Code { get; }
        public string Name { get; }
        public ErrorType ErrorType { get; }

        public static Error Failure(string code, string name) => new(code, name, ErrorType.Failure);
        public static Error Validation(string code, string name) => new(code, name, ErrorType.Validation);
        public static Error NotFound(string code, string name) => new(code, name, ErrorType.NotFound);
        public static Error Forbiden(string code, string name) => new(code, name, ErrorType.Forbiden);
    }

    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Forbiden = 3
    }
}