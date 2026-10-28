using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.UpdateOrderStatus
{
    using Domain.ValueObjects;
    using FluentValidation;
    public sealed class UpdateOrderStatusCommandValidator
        : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.Status)
                .IsInEnum()
                .Must(x => x != OrderStatus.Processing);
        }
    }

}
