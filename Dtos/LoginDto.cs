namespace vesalius_m.Dtos
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginDto
    {
        [DefaultValue("eugene.lim@nova-hub.com")]
        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; }

        [DefaultValue("Abcd1234")]
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        public string? PlayerId { get; set; }

        public string? MachineId { get; set; }

        [DefaultValue("false")]
        public bool FromAdmin { get; set; }

        public int FromBiometric { get; set; }
    }
}
