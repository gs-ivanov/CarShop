namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Issues;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class IssuesController : Controller
    {
        private readonly IValidator validator;
        private readonly CarShopDbContext data;
        private readonly IUserService users;

        public IssuesController(
            IUserService users,
            IValidator validator,
            CarShopDbContext data)
        {
            this.users = users;
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            if (!this.UserCanAccessCar(carId))
            {
                return Unauthorized();
            }

            var carIssues = this.data
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    UserIsMechanic = this.users.IsMechanic(this.User.Id),
                    Issues = c.Issues.Select(i => new IssueListingViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsFixed = i.IsFixed,
                        IsFixedInformation = i.IsFixed ? "Yes" : "Not Yet"
                    })
                })
                .FirstOrDefault();

            if (carIssues == null)
            {
                return NotFound(); //Error($"Car with Id '{carId}' does not exists!");
            }

            return View(carIssues);
        }

        [Authorize]
        public HttpResponse Add(string carID) => View(new AddIssueViewModel
        {
            CarId = carID
        });

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddIssueFormModel model)
        {
            if (!this.UserCanAccessCar(model.CarId))
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateIssue(model);

            if (modelErrors.Any())
            {
                return this.Error(modelErrors);
            }

            var issue = new Issue
            {
                Description = model.Description,
                CarId = model.CarId,
            };

            this.data.Issues.Add(issue);

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId,string carId)
        {
            var userIsMechanic = this.users.IsMechanic(this.User.Id);

            if (!userIsMechanic)
            {
                return Unauthorized();
            }

            var issue = this.data.Issues.Find(issueId);

            issue.IsFixed = true;

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            if (!this.UserCanAccessCar(carId))
            {
                return Unauthorized();
            }

            var issue = this.data.Issues.Find(issueId);

            this.data.Issues.Remove(issue);

            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        private bool UserCanAccessCar(string carId)
        {
            var userIsMechanic = this.users.IsMechanic(this.User.Id);

            if (!userIsMechanic)   // userIsClient
            {
                var userOwnsCar = this.users.OwnsCar(this.User.Id, carId);
               
                if (!userOwnsCar)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
