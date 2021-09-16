using System;

namespace LivMoneyAPI.Dtos
{
    public class UserDtos
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string ImageUrl { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        
        
        public DateTime CreatedDate { get; set; }
    }
}