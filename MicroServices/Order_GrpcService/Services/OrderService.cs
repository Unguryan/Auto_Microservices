using Grpc.Core;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Protos;
using Microsoft.Extensions.Logging;
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
