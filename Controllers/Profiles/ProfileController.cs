using System.Threading.Tasks;
using AutoMapper;
using LivMoneyAPI.Dtos;
using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Repository.Crud;
using LivMoneyAPI.Repository.ProfileRepo;
using Microsoft.AspNetCore.Mvc;

namespace LivMoneyAPI.Controllers.Profiles {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfileController : ControllerBase {
        private readonly ICrudRepo _crudrepo;
        private readonly IProfileRepo _profilerepo;
        private readonly IMapper _mapper;
        public ProfileController (ICrudRepo crudrepo, IProfileRepo profilerepo, IMapper mapper) {
            _crudrepo = crudrepo;
            _profilerepo = profilerepo;
            _mapper = mapper;
        }
        // Get Profile Data By Id
        [HttpGet ("GetUserProfile/{userId}")]
        public async Task<ActionResult> GetUserProfile (int userId) {
            var authUsers = await _profilerepo.getProfileById (userId);

            //Getting User Role In Application
            var userRole = await _profilerepo.GetAuthRole (authUsers.Id);

            var returnData = new UserDtos () {
                Email = authUsers.Email,
                Name = authUsers.Name,
                UserName = authUsers.UserName,
                Token = authUsers.Token,
                ImageUrl = authUsers.ImageUrl,
                RoleId = userRole.RoleId,
                RoleName = userRole.Role.RoleName
            };
            return Ok (returnData);
        }
    }
}