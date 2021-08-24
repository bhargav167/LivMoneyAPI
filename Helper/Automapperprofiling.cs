using AutoMapper;
using LivMoneyAPI.Dtos;
using LivMoneyAPI.Model.Authentication;

namespace LivMoneyAPI.Helper
{
    public class Automapperprofiling:Profile
    {
         public Automapperprofiling () {
            CreateMap<UserDtos, AuthUser> ();
        } 
    }
}