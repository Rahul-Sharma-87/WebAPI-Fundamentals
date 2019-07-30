using System;
using System.Linq;

namespace DataAcessLayer.Models {
    public interface ISecurityProvider {
        bool Login(string userName, string password);
    }

    public class BasicSecurityProvider :BaseContextProvider, ISecurityProvider {

        public BasicSecurityProvider(PracticeSQLEntities dbContext):base(dbContext) {
        }

        public bool Login(string userName, string password) {
           return DbContext.Users.Any(x => x.Username.Equals(
                         userName, StringComparison.InvariantCultureIgnoreCase) &&
                     x.Password.Equals(password));
        }
    }
}
