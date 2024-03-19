namespace CarShop.Controllers
{
    using System.Linq;
    using CarShop.Models.Users;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private readonly IValidator validator;

        public UsersController(IValidator validator)
        {
            this.validator = validator;
        }

        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUser(model);

            if (modelErrors.Count > 0)
            {
                return BadRequest();
            }

            return View();
        }

    }
}
