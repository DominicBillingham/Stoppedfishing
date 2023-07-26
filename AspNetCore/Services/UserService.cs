namespace StoppedFishing.Services
{
    public class UserService
    {
        public int? currentUserId = null;

        public void SetCurrentUser(int UserId)
        {
            currentUserId = UserId;
        }

    }
}
