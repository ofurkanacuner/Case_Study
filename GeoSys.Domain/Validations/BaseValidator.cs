#region - Using

using FluentValidation;
using GeoSys.Domain.ViewModels;

#endregion

namespace GeoSys.Domain.Validations
{
    public class BaseValidator<TValidate>
        : AbstractValidator<TValidate> where TValidate
        : BaseViewModel
    {

    }
}