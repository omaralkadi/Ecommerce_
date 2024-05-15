using AmazonApis.Dtos;
using AmazonApis.Errors;
using AmazonApis.Extensions;
using AmazonCore.Entities.Identity;
using AmazonCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AmazonApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;
        private readonly IMapper _mapper;

        public ITokenService _token { get; }

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager, ITokenService token, IMapper mapper)
        {
            _userManager = userManager;
            _signManager = signManager;
            _token = token;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            var User = new AppUser
            {
                Email = register.Email,
                UserName = register.DisplayName,
                DisplayName = register.DisplayName,
                PhoneNumber = register.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(User, register.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            var returnedUser = new UserDto
            {
                Email = register.Email,
                DisplayName = register.DisplayName,
                Token = await _token.CreateToken(User)

            };

            return Ok(returnedUser);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var User = await _userManager.FindByEmailAsync(login.Email);
            if (User == null) { return Unauthorized(new ApiResponse(401)); }

            var Result = await _signManager.CheckPasswordSignInAsync(User, login.Password, false);
            if (!Result.Succeeded) { return Unauthorized(new ApiResponse(401)); }

            var returnedUser = new UserDto
            {
                DisplayName = User.DisplayName,
                Email = login.Email,
                Token = await _token.CreateToken(User)
            };

            return Ok(returnedUser);
        }


        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);

            var returnedUser = new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _token.CreateToken(user)
            };

            return Ok(returnedUser);

        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {

            var user = await _userManager.FindUserWithAddressAsync(User);
            var ReturnedAddress = _mapper.Map<AddressDto>(user.Address);

            return Ok(ReturnedAddress);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress = _mapper.Map<Address>(addressDto);
            user.Address = MappedAddress;
            user.Address.id = user.Address.id;

            var Result=await _userManager.UpdateAsync(user);
            if(!Result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(addressDto);
        }

        [HttpPost("CheckEmailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var email = await _userManager.Users.Where(e => e.Email == Email).FirstOrDefaultAsync();
            if (email != null) return true;
            return Ok(false);

        }

    }
}
