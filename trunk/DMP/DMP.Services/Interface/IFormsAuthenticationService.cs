using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMP.Services.Interface {
    public interface IFormsAuthenticationService {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }
}
