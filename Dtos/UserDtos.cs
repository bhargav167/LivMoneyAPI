using System;

namespace LivMoneyAPI.Dtos
{
    public class UserDtos
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}