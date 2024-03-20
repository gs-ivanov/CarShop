namespace CarShop.Controllers
{
    using CarShop.Models.Cars;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System;

    public class CarsController:Controller
    {
        public HttpResponse Add() => View();

        [HttpPost]
        public HttpResponse Add(AddCarFormModel car)
        {

            return Redirect("/Cars/All");
        }

        public HttpResponse All()
        {
            return View();
        }
    }
}
