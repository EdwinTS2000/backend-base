namespace App.Exceptions
{
    public sealed class AppValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

        public AppValidationException(
            IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Ocurrieron errores de validación.")
        {
            ErrorsDictionary = errorsDictionary;
        }
    }
}