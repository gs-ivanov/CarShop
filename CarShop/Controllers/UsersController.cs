namespace CarShop.Controllers
{
    using CarShop.Models.Users;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private readonly IValidator validator;

        public UsersController()
        {
            this.validator = validator;
        }
        public HttpResponse Register()=> View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var data = validator.ValidateUserRegistration(model);
            return View();
        }

    }
}
