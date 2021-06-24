using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index() 
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Repositories/All");
            }

            return View();
        }
    }
}
