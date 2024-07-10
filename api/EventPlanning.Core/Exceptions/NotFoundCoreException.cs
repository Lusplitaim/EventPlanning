namespace EventPlanning.Core.Exceptions
{
    internal class NotFoundCoreException : Exception
    {
        public NotFoundCoreException() : base()
        {
        }

        public NotFoundCoreException(string? message, Exception? inner) : base(message, inner)
        {
        }
    }
}
