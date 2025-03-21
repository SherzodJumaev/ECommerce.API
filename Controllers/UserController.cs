﻿using ECommerce.API.Data;
using ECommerce.API.DTOs.UserDTOs;
using ECommerce.API.Helpers;
using ECommerce.API.Interfaces;
using ECommerce.API.Mappers.UserMaps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly AuthSettings _authSettings;
        private readonly ITokenService _tokenService;
        private readonly ICustomerRepository _customerRepository;

        public UserController(
            ApplicationDBContext context,
            IOptions<AuthSettings> authSettings,
            ICustomerRepository customerRepository,
            ITokenService tokenService)
        {
            _context = context;
            _authSettings = authSettings.Value;
            _tokenService = tokenService;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userDto">The user data for registration.</param>
        /// <returns>A message indicating the result of the registration.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
            {
                return BadRequest("Username already exists.");
            }

            try
            {
                userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                userDto.Role = string.IsNullOrEmpty(userDto.Role) ? "Customer" : userDto.Role;

                var userModel = userDto.FromUserDtoToUser();

                await _context.Users.AddAsync(userModel);
                await _context.SaveChangesAsync();

                if (!userDto.Role.ToLower().Equals("admin"))
                {
                    var newUserModel = userDto.FromUserDtoToCustomer();

                    await _customerRepository.CreateAsync(newUserModel);
                }

                Log.Information($"User {userModel.Username} created with ID {userModel.Id}");

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to create User {userDto.Username}");
                throw;
            }
        }

        /// <summary>
        /// Authenticates the user and returns a JWT token.
        /// </summary>
        /// <param name="userDto">The user credentials for login.</param>
        /// <returns>A JWT token if login is successful, or an error message if not.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var dbUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == userDto.Username);

                if (dbUser == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, dbUser.Password))
                {
                    return Unauthorized("Invalid username or password.");
                }

                var token = _tokenService.GenerateJwtToken(dbUser);

                Log.Information($"Token generated successfully.");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to generate Token");
                throw;
            }
        }
    }
}
