namespace LivMoneyAPI.Model.Authentication.AppRole
{
    public class UserRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public AuthRole Role { get; set; }
        public int AuthUserId { get; set; }
        public AuthUser AuthUser { get; set; }
    }
}