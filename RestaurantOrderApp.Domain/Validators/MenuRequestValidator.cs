using FluentValidation;
using RestaurantOrderApp.Domain.Core.Bus;
using RestaurantOrderApp.Domain.Core.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.Validators
{
    public class MenuRequestValidator : BaseValidator<string>

    {
        public MenuRequestValidator(IMediatorHandler bus) : base(bus)
        {
        }

        public void ValidateMenuInputRequest(string request) {
            ValidateInputRequest();
            ValidationResults = Validate(request);
            NotifyValidationErrors();
        }

        protected void ValidateInputRequest()
        {
            RuleFor(menu => menu)
                .NotEmpty()
                .Matches("^[a-z]{1,50}[ 0-9,]*")
                .WithName("menuRequest")
                .WithMessage("Input invalid type of day format or dish type");
        }
    }
}
