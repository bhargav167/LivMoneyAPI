using System;

namespace LivMoneyAPI.Model.Authentication
{
    public class AuthRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public AuthRole()
        {
            CreatedDate=DateTime.Now;
        }
    }
}