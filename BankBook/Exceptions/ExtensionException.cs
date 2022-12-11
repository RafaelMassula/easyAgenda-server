namespace EasyAgenda.Exceptions
{
    internal class ExtensionException : OperationException
    {
        public ExtensionException() { }
        public ExtensionException(string message) : base(message)
        {

        }
    }
}