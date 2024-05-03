using i_think_so.Application.Models.DTO;
using i_think_so.Application.Models.Request;
using i_think_so.Application.Services.Token;
using i_think_so.Domain.Entities;
using i_think_so.Infrastructure.Database;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Security.Cryptography;
using System.Text;

namespace i_think_so.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private const int MinPasswordLength = 6;
        private readonly IMongoCollection<User> _usersCollection;
        private readonly byte[] _secret;
        private readonly HMACSHA512 _hmac;
        private readonly ITokenService _tokenService;
        public AuthService(IOptions<SecurityOptions> _options, MongoContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _secret = Encoding.UTF8.GetBytes(_options.Value.Secret);
            _hmac = new HMACSHA512(_secret);
            _usersCollection = context.Users;
        }

        public async Task RegisterAsync(RegisterRequest user, CancellationToken cancellationToken)
        {
            if (CheckValidPassword(user.Password))
                throw new Exception("Password is not valid. It should be at least 8 characters long and contain at least one letter.");

            var existingUser = await _usersCollection.Find(u => u.Username == user.Username).FirstOrDefaultAsync();
            if (existingUser != null)
                throw new Exception("User with this username already exists.");
      
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = HashPassword(user.Password)
            };

            await _usersCollection.InsertOneAsync(newUser, new InsertOneOptions
            {}, cancellationToken);
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var user = await _usersCollection.Find(u => u.Username == loginRequest.Username).SingleOrDefaultAsync();
            if (user == null || !VerifyPasswordHash(loginRequest.Password, user.PasswordHash))
            {
                throw new Exception("Invalid username or password.");
            }

            return _tokenService.GenerateToken(user.Username);
        }

        public async Task<UserDTO> SelfAsync(HttpContext httpContext, CancellationToken cancellationToken)
        {
            var username = httpContext.User.Identity!.Name;

            var user = await _usersCollection.Find(u => u.Username == username).SingleOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            return new UserDTO
            {
                Id = user.Id!,
                Username = user.Username,
                Email = user.Email,
                AccessToken = token
            };
        }

        public bool VerifyPasswordHash(string password, byte[] hashedPassword)
        {
            var computedHash = HashPassword(password);
            return computedHash.SequenceEqual(hashedPassword);
        }

        public byte[] HashPassword(string password)
        {
            var computedHash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash;
        }
        private bool CheckValidPassword(string password)
        {
            return (string.IsNullOrWhiteSpace(password) || password.Length < MinPasswordLength || !password.Any(char.IsLetter));
        }

    }
}
