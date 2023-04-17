namespace MinimalAPI.Auth;

public class UserRepository : IUserRepository
{
    private List<UserDTO> _users = new()
    {
        new UserDTO("John", "123"),
        new UserDTO("Monica", "123"),
        new UserDTO("Nancy", "123"),
    };
    public UserDTO GetUser(UserModel userModel) =>
        _users.FirstOrDefault(u =>
            string.Equals(u.UserName, userModel.UserName) &&
            string.Equals(u.Password, userModel.Passford)) ??
            throw new Exception();
}
