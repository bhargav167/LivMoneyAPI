using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LivMoneyAPI.Dtos;
using LivMoneyAPI.Extension.AuthReponces;
using LivMoneyAPI.Helper;
using LivMoneyAPI.Model.Authentication;
using LivMoneyAPI.Repository.AuthenticationRepo;
using LivMoneyAPI.Repository.Crud;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LivMoneyAPI.Controllers.Authentication {
    [ApiController]
    [Route ("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly ICrudRepo _crudrepo;
        private readonly IAuthRepo _authrepo;
        private readonly IMapper _mapper;
        private AuthResponces _responces;
        public AuthController (ICrudRepo crudrepo, IAuthRepo authrepo, IMapper mapper, AuthResponces responces) {
            _crudrepo = crudrepo;
            _authrepo = authrepo;
            _mapper = mapper;
            _responces = responces;
        }

        //Register Method api/Job/AddJob
        [HttpPost ("AddAuthUser")]
        public async Task<IActionResult> AddAuthUser ([FromBody] AuthUser authUser) {
            // Checking Duplicate Entry
            try {
                if (await _authrepo.IsAuthUserExist (authUser.Email) && authUser.ModOfRegistration == "GOOGLE") {
                    var authUsers = await _authrepo.getAuthUserByEmail (authUser.Email);
                    if (authUsers == null)
                        return NoContent ();

                    _responces.Status = 200;
                    _responces.Success = true;
                    _responces.Status_Message = "User with this email already exist!";
                    var userDtos = new UserDtos () {
                        Email = authUsers.Email,
                        Name = authUsers.Name,
                        UserName = authUsers.UserName,
                        Token = authUsers.Token,
                        ImageUrl = authUsers.ImageUrl,
                        CreatedDate = authUsers.CreatedDate
                    };

                    _responces.data = userDtos;

                    return Ok (_responces);
                }
                if (await _authrepo.IsAuthUserExist (authUser.Email) && authUser.ModOfRegistration == "CustomLogin") {
                    _responces.Status = 200;
                    _responces.Success = false;
                    _responces.Status_Message = "User with this email already exist!";
                    _responces.data = null;

                    return Ok (_responces);
                }
                // validate request
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                //Create random UserName on server
                var createdUserName = RandomUserName.CreateUserName (authUser.Name);
                authUser.UserName = createdUserName;

                // Genetate Token key
                IdentityOptions _opt = new IdentityOptions ();

                var tokenDecriptor = new SecurityTokenDescriptor {
                    Subject = new System.Security.Claims.ClaimsIdentity (new Claim[] {
                    new Claim ("Id", authUser.Id.ToString ()),
                    new Claim (_opt.ClaimsIdentity.EmailClaimType, authUser.Email)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes (50),
                    SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("super secret key")), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandeler = new JwtSecurityTokenHandler ();
                var securityToken = tokenHandeler.CreateToken (tokenDecriptor);
                var token = tokenHandeler.WriteToken (securityToken);
                //Add latest token to database
                authUser.Token = token;

                if (authUser.ModOfRegistration == "GOOGLE")
                    authUser.IsEmailConfirm = true;

                authUser.ImageUrl = "https://mpng.subpng.com/20180429/bdq/kisspng-money-bag-computer-icons-dollar-sign-clip-art-5ae64a6cf14455.5668874915250417729882.jpg";
                var CreatedUser = await _authrepo.AddAuth (authUser);

                _responces.Status = 200;
                _responces.Success = true;
                _responces.Status_Message = "User created successfully!";
                var userDtos1 = new UserDtos () {
                    Email = CreatedUser.Email,
                    Name = CreatedUser.Name,
                    UserName = CreatedUser.UserName,
                    Token = CreatedUser.Token,
                    ImageUrl = CreatedUser.ImageUrl,
                    CreatedDate = CreatedUser.CreatedDate
                };
                _responces.data = userDtos1;
                return Ok (_responces);
            } catch (System.Exception ex) {
                throw new System.Exception ("Error in saving User data.", ex);
            }
        }

        //Login Method
        [HttpPost ("Login")]
        public async Task<IActionResult> Login ([FromBody] LoginModal loginModel) {
            // validate request
            if (!ModelState.IsValid)
                return BadRequest (ModelState);

            try {
                var LoggedInUser = await _authrepo.UserLogin (loginModel.Email, loginModel.Password);
                if (LoggedInUser == null) {
                    _responces.Status = 209;
                    _responces.Success = true;
                    _responces.Status_Message = $"User not avalable for {loginModel.Email} email!";
                    _responces.data = null;
                    return Ok (_responces);
                }
                _responces.Status = 200;
                _responces.Success = true;
                _responces.Status_Message = "User loggedIn successfully!";
                var userDtos = new UserDtos () {
                    Email = LoggedInUser.Email,
                    Name = LoggedInUser.Name,
                    UserName = LoggedInUser.UserName,
                    Token = LoggedInUser.Token,
                    ImageUrl = LoggedInUser.ImageUrl,
                    CreatedDate = LoggedInUser.CreatedDate
                };
                _responces.data = userDtos;
                return Ok (_responces);
            } catch (System.Exception ex) {
                throw new Exception ($"Error in Login! {ex}");
            }

        }
    }
}