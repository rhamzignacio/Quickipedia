using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quickipedia.Services;
using Quickipedia.Models;

namespace Quickipedia.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserList()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveUser(UserModel user)
        {
            string serverReponse = "";

            if (user != null)
                UserService.SaveUser(user, out serverReponse);

            return Json(serverReponse);
        }

        [HttpPost]
        public JsonResult GetUserList()
        {
            string serverResponse = "";

            var users = UserService.GetUsers(out serverResponse);

            return Json(new { users = users });
        }
    }
}