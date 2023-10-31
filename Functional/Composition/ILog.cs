namespace Functional.Composition;

public interface ILog
{
    void Log(String message);
    void Log(Exception exception);
}