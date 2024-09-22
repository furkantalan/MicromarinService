using Micromarin.Domain.Interfaces.General;
using Microsoft.Extensions.Localization;


namespace Micromarin.Domain.Helpers;

public class ErrorMessageHelper : IErrorMessageHelper
{
    private readonly IStringLocalizer _localizer;

    public ErrorMessageHelper(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("ErrorMessages", "MicroService.Shared");
    }

    public string GetErrorMessage(int errorCode, params object[] args)
    {
        try
        {
            var localizedString = _localizer[errorCode.ToString(), args];
            if (localizedString.ResourceNotFound)
            {
                return $"Error code {errorCode} not found.";
            }

            return localizedString.Value;
        }
        catch (FormatException ex)
        {
            // Format hatası yakalandığında hata mesajı döndür
            return $"Error formatting message for error code {errorCode}";
        }
    }

}
