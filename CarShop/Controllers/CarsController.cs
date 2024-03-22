namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Models.Cars;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly IValidator validator;
        private readonly CarShopDbContext data;
        private readonly IUserService userService;

        public CarsController(
            IValidator validator,
            IUserService userService,
            CarShopDbContext data)
        {
            this.validator = validator;
            this.data = data;
            this.userService = userService;
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CarAddFormModel model)
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var modelErrors = this.validator.ValidateCar(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this.data.Add(car);

            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            List<CarListingViewModel> cars;

            if (this.userService.IsMechanic(this.User.Id))
            {
                cars = this.data
                     .Cars
                     .Where(c => c.Issues.Any(i => !i.IsFixed))
                    .Select(c => new CarListingViewModel()
                    {
                        Id = c.Id,
                        Model = c.Model,
                        Year = c.Year,
                        Image = c.PictureUrl,
                        PlateNumber = c.PlateNumber,
                        FixedIssues = c.Issues
                        .Where(i => i.IsFixed).Count(),
                        ReminingIssues = c.Issues
                        .Where(i => !i.IsFixed).Count(),
                    })
                     .ToList();
            }
            else
            {
                cars = this.data
                   .Cars
                   .Where(c => c.OwnerId == this.User.Id)
                   .Select(c => new CarListingViewModel()
                   {
                       Id = c.Id,
                       Model = c.Model,
                       Year = c.Year,
                       Image = c.PictureUrl,
                       PlateNumber = c.PlateNumber,
                       FixedIssues = c.Issues
                       .Where(i => i.IsFixed).Count(),
                       ReminingIssues = c.Issues
                       .Where(i => !i.IsFixed).Count(),
                   })
                   .ToList();

            }
            return View(cars);
        }
    }
}
