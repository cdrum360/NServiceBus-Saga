using System.Text;
using System.Web.Mvc;
using UserManagement.Commands;


namespace Client.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Json(new { text = "Hello world." });
        }

        //  /Home/CreateUser?name=Charles&email=cdrum360@live.com

        public ActionResult CreateUser(string name, string email)
        {
            var cmd = new CreateUser
            {
                Name = name,
                EmailAddress = email
            };

            ServiceBus.Bus.Send(cmd);

            return Json(new { sent = cmd });
        }


        //  /Home/VerifyUser?email=cdrum360@live.com&code={VerificationCode}

        public ActionResult VerifyUser(string email, string code)
        {
            var cmd = new VerifyUserEmail
            {
                EmailAddress = email,
                VerificationCode = code
            };

            ServiceBus.Bus.Send(cmd);

            return Json(new { sent = cmd });
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
        }
    }
}