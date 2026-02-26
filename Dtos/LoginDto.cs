namespace vesalius_m.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; init; } = "eugene.lim@nova-hub.com";

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; } = "Abcd1234";

        public string? PlayerId { get; init; }

        public string? MachineId { get; init; }

        public bool FromAdmin { get; init; } = false;

        public int FromBiometric { get; init; } = 0;
    }
}
