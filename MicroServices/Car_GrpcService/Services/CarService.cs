using Car_GrpcService.Models;
using Grpc.Core;
using Interfaces.Models;
using Interfaces.Services;
using Interfaces.Services.Protos;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Car_GrpcService.Services
{
    public class CarService : CarGRPCService.CarGRPCServiceBase
    {
        private readonly ILogger<CarService> _logger;

        private readonly IBaseContextWrapper<ICar> _context;

        public CarService(ILogger<CarService> logger, IBaseContextWrapper<ICar> context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<CarModel> AddCar(AddCarRequest request, ServerCallContext context)
        {
            var model = new Car(request.IdUser, request.Model);
            var res = await _context.Add(model);
            //Task.WaitAll();

            if (res == null)
            {
                return null;
            }

            return new CarModel()
            {
                Id = res.Id,
                IdUser = res.IdUser,
                Model = res.Model
            };
        }

        public override async Task<CarModel> DeleteCar(DeleteCarRequest request, ServerCallContext context)
        {
            var res = await _context.Remove(request.Id);
            //Task.WaitAll();

            if (res == null)
            {
                return null;
            }

            return new CarModel()
            {
                Id = res.Id,
                IdUser = res.IdUser,
                Model = res.Model
            };
        }

        public override async Task<CarModel> GetCarById(GetCarByIdRequest request, ServerCallContext context)
        {
            var res = await _context.GetById(request.Id);
            //Task.WaitAll();

            if(res == null)
            {
                return null;
            }

            return new CarModel()
            {
                Id = res.Id,
                IdUser = res.IdUser,
                Model = res.Model
            };
        }

        public override async Task GetCarsByUserId(GetCarByUserIdRequest request, IServerStreamWriter<CarModel> responseStream, ServerCallContext context)
        {
            foreach (var response in (await _context.Get()).Where(car => car.IdUser == request.IdUser))
            {
                var model = new CarModel()
                {
                    Id = response.Id,
                    IdUser = response.IdUser,
                    Model = response.Model
                };

                await responseStream.WriteAsync(model);
            }
        }
    }
}
