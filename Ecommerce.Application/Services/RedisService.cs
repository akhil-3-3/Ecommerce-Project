using System.Text.Json;
using Ecommerce.Application.DTOs;
using StackExchange.Redis;

namespace Ecommerce.Application.Services
{
    public class RedisService
    {
        private readonly IDatabase _database;


        public RedisService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }



        public async Task SetPendingUserAsync(
            string email,
            PendingUserDto user)
        {
            var json = JsonSerializer.Serialize(user);


            await _database.StringSetAsync(
                $"pending:{email}",
                json,
                TimeSpan.FromMinutes(10));
        }



        public async Task<PendingUserDto?> GetPendingUserAsync(
            string email)
        {
            var value = await _database.StringGetAsync(
                $"pending:{email}");


            if (!value.HasValue)
                return null;


            return JsonSerializer.Deserialize<PendingUserDto>(
                value!);
        }



        public async Task RemovePendingUserAsync(
            string email)
        {
            await _database.KeyDeleteAsync(
                $"pending:{email}");
        }
    }
}