using System.Web;

namespace SJManagementWebApplication.Model
{
    public class ApplicationUser
    {
        public string UserId { get; set; }
        //public string UserPassword { get; set; }
    }
    public static class AuthHelper
    {

        public static bool SignIn(string userId)
        {
            HttpContext.Current.Session["User"] = CreateDefualtUser(userId);//, password);  // Mock user data
            return true;
        }
        public static void SignOut()
        {
            HttpContext.Current.Session["User"] = null;
        }
        public static bool IsAuthenticated()
        {
            return GetLoggedInUserInfo() != null;
        }

        public static ApplicationUser GetLoggedInUserInfo()
        {
            return HttpContext.Current.Session["User"] as ApplicationUser;
        }

        private static ApplicationUser CreateDefualtUser(string userId)//, string password)
        {
            return new ApplicationUser
            {
                UserId = userId,
                //UserPassword = password
            };
        }
    }
}