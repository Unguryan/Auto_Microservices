using Grpc.Core;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Protos;
using Microsoft.Extensions.Logging;
using Order_GrpcService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order_GrpcService.Services
{
    public class OrderService : OrderGRPCService.OrderGRPCServiceBase
    {
        private readonly ILogger<OrderService> _logger;

        private readonly IBaseContextWrapper<IOrder> _context;

        private readonly IClientFactory _clientFactory;

        public OrderService(ILogger<OrderService> logger, IBaseContextWrapper<IOrder> context, IClientFactory clientFactory)
        {
            _logger = logger;
            _context = context;
            _clientFactory = clientFactory;
        }

        public override async Task<OrderModel> AddOrder(AddOrderRequest request, ServerCallContext context)
        {
            var completedWork = new Dictionary<int, int>();
            foreach (var item in request.CompletedWork)
            {
                completedWork.Add(item.Key, item.Value);
            }
            var model = new Order(request.Name, request.IdStation, request.IdUser, DateTime.Parse(request.CreatedAt), DateTime.MinValue,  completedWork);
            var temp = await _context.Add(model);

            if (temp == null)
            {
                return null;
            }

            var res = new OrderModel()
            {
                Id = temp.Id;
                Name = temp.Name;
                IdStation = temp.IdStation;
                IdUser = temp.IdUser;
                CreatedAt = temp.CreatedAt;
                Closed = temp.Closed;
                CompletedWork = completedWork;
        };

            foreach (var item in temp.TypeOfWork)
            {
                res.TypeOfWork.Add((int)item.Key, item.Value);
            }
            return base.AddOrder(request, context);
        }

        public override async Task<OrderModel> CloseOrder(CloseOrderRequest request, ServerCallContext context)
        {
            return base.CloseOrder(request, context);
        }

        public override async Task<OrderModel> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            return base.DeleteOrder(request, context);
        }

        public override async Task<OrderModel> GetOrderById(GetOrderByIdRequest request, ServerCallContext context)
        {
            return base.GetOrderById(request, context);
        }

        public override async Task GetOrders(GetOrdersRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            return base.GetOrders(request, responseStream, context);
        }

        public override async Task GetOrdersByCarId(GetOrderByIdRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            return base.GetOrdersByCarId(request, responseStream, context);
        }

        public override async Task GetOrdersByOrderStationId(GetOrderByIdRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            return base.GetOrdersByOrderStationId(request, responseStream, context);
        }

        public override async Task GetOrdersByUserId(GetOrderByIdRequest request, IServerStreamWriter<OrderModel> responseStream, ServerCallContext context)
        {
            return base.GetOrdersByUserId(request, responseStream, context);
        }
    }
}
