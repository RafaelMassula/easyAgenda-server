namespace EasyAgendaService.Exceptions
{
    public class OperationException: Exception
    {
        public OperationException()
        {
        }
        public OperationException(string message) : base(message)
        {
        }
    }
}
