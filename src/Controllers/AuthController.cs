using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using src.Domain.DTOs.Token;
using src.Domain.DTOs.Tokens;
using src.Domain.DTOs.Users;
using src.Domain.Models.Users;
using src.Infrastructure.Security;
using src.Persistence.Services.Interfaces.Users;

namespace src.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthController(IMapper mapper, IAuthenticationService authenticationService, IUserService userService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsDtos userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.UserName, userCredentials.Password);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            var accessToken = _mapper.Map<AccessToken, TokenDtos>(response.Token);
            return Ok(accessToken);
        }

        [Route("/api/register")]
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsDtos userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserCredentialsDtos, User>(userCredentials);
            
            var response = await _userService.CreateUserAsync(user, ApplicationRole.User);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResponse = _mapper.Map<User, UserDtos>(response.User);
            return Ok(userResponse);
        }

        [Route("/api/adminregister")]
        [HttpPost]
        public async Task<IActionResult> CreateAdminAsync([FromBody] UserCredentialsDtos userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserCredentialsDtos, User>(userCredentials);
            
            var response = await _userService.CreateUserAsync(user, ApplicationRole.Administrator);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResponse = _mapper.Map<User, UserDtos>(response.User);
            return Ok(userResponse);
        }

        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenDtos refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserName);
            if(!response.Success)
            {
                return BadRequest(response.Message);
            }
        
            var token = _mapper.Map<AccessToken, TokenDtos>(response.Token);
            return Ok(token);
        }

        [Route("/api/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenDtos revokeTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
            return NoContent();
        }
    }
}