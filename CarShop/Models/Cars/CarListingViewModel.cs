﻿namespace CarShop.Models.Cars
{
    using System;

    public class CarListingViewModel
    {
        public string Id { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public string Image { get; init; }

        public string PlateNumber { get; init; }

        public int FixedIssues { get; init; }

        public int ReminingIssues { get; init; }
    }
}
