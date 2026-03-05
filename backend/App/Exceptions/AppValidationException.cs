namespace App.Exceptions
{
    public sealed class AppValidationException : AppException
    {
        public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; }

        public AppValidationException(
            IReadOnlyDictionary<string, string[]> errorsDictionary)
            : base("Validation Error", "Uno o más errores de validación ocurrieron.")
        {
            ErrorsDictionary = errorsDictionary;
        }
    }
}