namespace EasyAgenda.Exceptions
{
  public class FileSizeLimitException : OperationException
  {
    public long FileSize { get; }
    public long FileSizeLimit { get; }

    public FileSizeLimitException(long fileSize, long fileSizeLimit) : this($"The size file {fileSize} exceeded size max {fileSizeLimit} MB.")
    {
      FileSize = fileSize;
      FileSizeLimit = fileSizeLimit;
    }

    public FileSizeLimitException(string message) : base(message)
    {

    }
  }
}
