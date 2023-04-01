using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saturn.Account
{
    public class TestAppService : SaturnAppService
    {

        public TestAppService()
        {
        }

        [Authorize]
        public bool GetTestAuth()
        {
            return true;
        }

        public bool GetTestWithoutAuth()
        {
            return true;
        }
    }
}
