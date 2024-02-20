using School.BL.DTO;

namespace School.BL.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task AddUser(UserDTO user);
        Task UpdateUser(UserDTO user);
        Task DeleteUser(int id);
        UserDTO GetUserById(int id);
        Task<bool> ResetPassword(ResetPasswordDTO password);
        Task<UserDTO> Login(LoginDTO login);
    }
}
