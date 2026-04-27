namespace Chiro.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string  Username { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsGroupLeader { get; set; }
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public string  PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
