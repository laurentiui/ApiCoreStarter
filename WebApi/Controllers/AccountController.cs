﻿using Data.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        

        public AccountController(ILogger<AccountController> logger, IConfiguration config, IUserService userService)
        {
            _logger = logger;
            _config = config;
            _userService = userService;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// -
        /// </remarks>
        /// <returns></returns>
        //[Authorize]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthentificatedUserDto>> Login([FromBody] UserLoginDto userLoginDto)
        {
            Data.Domain.Entity.User user = null;

            try
            {
                user = await _userService.LoginAsync(userLoginDto.Email, userLoginDto.Password);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError($"failed log ${userLoginDto.Email}: {ex.Message}");
                return Unauthorized();
            }

            var authentificatedUserDto = new AuthentificatedUserDto()
            {
                Username = user.Username,
                Token = GenerateJSONWebToken(user)
            };


            return Ok(authentificatedUserDto);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// -
        /// </remarks>
        /// <returns></returns>
        //[Authorize]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthentificatedUserDto>> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
        {
            Data.Domain.Entity.User user = null;

            //try
            //{
            //    user = await _userService.LoginAsync(userLoginDto.Email, userLoginDto.Password);
            //}
            //catch (ArgumentException ex)
            //{
            //    _logger.LogError($"failed log ${userLoginDto.Email}: {ex.Message}");
            //    return Unauthorized();
            //}

            var authentificatedUserDto = new AuthentificatedUserDto()
            {
                Username = user.Username,
                Token = GenerateJSONWebToken(user)
            };


            return Ok(authentificatedUserDto);
        }

        private string GenerateJSONWebToken(Data.Domain.Entity.User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email) 
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
