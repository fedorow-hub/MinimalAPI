namespace MinimalAPI.Auth;

public interface IUserRepository
{
    UserDTO GetUser(UserModel userModel);
}
