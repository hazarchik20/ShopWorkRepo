using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.CreateOrder
{
    using FluentValidation;
    public sealed class CreateOrderCommandValidator
        : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.BuyerId).NotEmpty();
            RuleFor(x => x.AdvertId).NotEmpty();
        }
    }

}
