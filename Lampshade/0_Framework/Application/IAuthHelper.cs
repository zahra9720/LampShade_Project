namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        public void SignOut();
        bool IsAuthenticated();
        public void SignIn(AuthViewModel authViewModel);
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
    }
}
