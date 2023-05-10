namespace EasyAgendaService.Utilities
{
  public class Wrappers<T>
  {
    public T Data { get; set; }
    public bool Succeded { get; set; }
    public string[] Erros { get; set; }
    public string Message { get; set; }

    public Wrappers() { }

    public Wrappers(T data)
    {
      Data = data;
      Succeded = true;
      Erros = Array.Empty<string>();
      Message = string.Empty;
    }

  }
}
