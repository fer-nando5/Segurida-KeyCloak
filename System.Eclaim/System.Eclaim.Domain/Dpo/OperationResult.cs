namespace System.Eclaim.Domain.Dpo
{
    public class OperationResult
    {
        public bool Success { get; }
        public string[] Errors { get; }

        private OperationResult(bool succes, IEnumerable<string>? errors = null)
        {
            Success = succes;
            Errors = errors?.ToArray() ?? Array.Empty<string>();
        }

        public static OperationResult Ok() => new(true);
        public static OperationResult Fail(IEnumerable<string> errors) => new(false, errors);

    }
}
