using vesalius_m.Dtos;
using vesalius_m.Models;

namespace vesalius_m.Services
{
    public class AuthService
    {
        private readonly ApplicationUserService applicationUserService;

        public AuthService(ApplicationUserService aus)
        {
            applicationUserService = aus; 
        }

        public async Task<ApplicationUser?> AuthenticateUser(LoginDto data)
        {
            ApplicationUser? user = null;
            try
            {
                bool valid = false;
                user = await applicationUserService.FindByUsernameAsync(data.Username);
                if (data.FromBiometric == 1)
                {
                    //valid = user != null ? await applicationUserService
                }

                else
                {
                    valid = user != null && applicationUserService.ValidateCredentials(user, data.Password);
                }

                if (!valid)
                {
                    throw new UnauthorizedAccessException("Invalid Credentials");
                }

                if (data.PlayerId != null)
                {

                }

                if (data.MachineId != null)
                {

                }

                if (user?.Inactive == "Y")
                {

                }
            }

            catch (Exception)
            {
                throw;
            }

            return user;
        }
    }
}
