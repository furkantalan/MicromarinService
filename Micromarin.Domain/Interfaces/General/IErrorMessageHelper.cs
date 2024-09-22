


namespace Micromarin.Domain.Interfaces.General;

public interface IErrorMessageHelper
{
    string GetErrorMessage(int errorCode, params object[] args);
}
