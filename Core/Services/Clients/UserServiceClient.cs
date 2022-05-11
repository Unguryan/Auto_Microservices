﻿using Core.EFCore.Models;
using Grpc.Core;
using Grpc.Net.Client;
using Interfaces.Models;
using Interfaces.Services.Clients;
using Interfaces.Services.Protos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Clients
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly UserGRPCService.UserGRPCServiceClient _client;

        public UserServiceClient()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5004");
            _client = new UserGRPCService.UserGRPCServiceClient(channel);
        }

        public async Task<IUser> AuthUser(string userName, string password)
        {
            var req = new AuthUserRequest()
            {
                UserName = userName,
                Password = password
            };
            var res = await _client.AuthUserAsync(req);
            var user = new User_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                Phone = res.Phone
            };

            return user;
        }

        public async Task<IUser> DeleteUser(int id)
        {
            var req = new DeleteUserRequest()
            {
                Id = id
            };
            var res = await _client.DeleteUserAsync(req);
            return new User_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                Phone = res.Phone
            };
        }

        public async Task<IUser> GetUserById(int id)
        {
            var req = new GetUserByIdRequest()
            {
                Id = id
            };
            var res = await _client.GetUserByIdAsync(req);
            return new User_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                Phone = res.Phone
            };
        }

        public async Task<IEnumerable<IUser>> GetUsers()
        {
            var req = new GetUsersRequest()
            {
            };
            using var res = _client.GetUsers(req);

            var list = new List<IUser>();

            await foreach (var data in res.ResponseStream.ReadAllAsync())
            {
                var temp = new User_DAL()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Phone = data.Phone
                };
                list.Add(temp);
            }
            return list;
        }

        public async Task NotifyUser(int userId, int orderId)
        {
            var req = new NotifyUserRequest()
            {
                UserId = userId,
                OrderId = orderId
            };
            await _client.NotifyUserAsync(req);
        }

        public async Task<IUser> RegUser(string userName, string password, string name, string phone)
        {
            var req = new RegUserRequest()
            {
                UserName = userName,
                Password = password,
                Name = name,
                Phone = phone
            };
            var res = await _client.RegUserAsync(req);
            return new User_DAL()
            {
                Id = res.Id,
                Name = res.Name,
                Phone = res.Phone
            };
        }
    }
}
