using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Wxt.SportsStore.Domain.Concrete;
using Wxt.SportsStore.WebApp.Abstract;

namespace Wxt.SportsStore.WebApp.Concrete
{
    public class DbAuthProvider : IAuthProvider
    {
        private EFDbContext _context;
        public DbAuthProvider(EFDbContext context)
        {
            _context = context;
        }

        public bool Authenticate(string username, string password)
        {
            var loginUser = _context.LoginUsers.FirstOrDefault(u => u.UserName == username);

            if (loginUser == null)
            {
                return false;
            }

            if (loginUser.Password.Equals(password, StringComparison.Ordinal))
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return true;
            }
            return false;
        }
    }
}