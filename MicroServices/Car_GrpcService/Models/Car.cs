using Interfaces.Models;

namespace Car_GrpcService.Models
{
    public class Car : ICar
    {
        public Car(int idUser, string model)
        {
            IdUser = idUser;
            Model = model;
        }

        public Car(int id, int idUser, string model)
        {
            Id = id;
            IdUser = idUser;
            Model = model;
        }

        public int Id { get; }

        public int IdUser { get; }

        public string Model { get; }
    }
}
