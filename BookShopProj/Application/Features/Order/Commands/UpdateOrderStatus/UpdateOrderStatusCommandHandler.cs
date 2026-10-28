using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Order.Commands.UpdateOrderStatus
{
    using Application.Abstractions.Persistence.Order;
    using Application.Common.Results;
    using MediatR;
    public sealed class UpdateOrderStatusCommandHandler
        : IRequestHandler<UpdateOrderStatusCommand, Result>
    {
        private readonly IOrderRepository _repository;

        public UpdateOrderStatusCommandHandler(
            IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(
            UpdateOrderStatusCommand request,
            CancellationToken cancellationToken)
        {
            var updated = await _repository.ChangeStatusAsync(
                request.OrderId,
                request.UserId,
                request.Status,
                cancellationToken);

            if (!updated)
            {
                return Result.Failure(
                    Error.Conflict(
                        "Order.InvalidStatusTransition",
                        "Неможливо змінити статус замовлення."));
            }

            return Result.Success();
        }
    }

}
