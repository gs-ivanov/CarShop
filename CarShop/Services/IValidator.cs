namespace CarShop.Services
{
    using CarShop.Models.Users;
    using CarShop.Models.Cars;
    using System;
    using System.Collections.Generic;
    using CarShop.Models.Issues;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel user);

        ICollection<string> ValidateCar(AddCarFormModel car);

        ICollection<string> ValidateIssue(AddIssueFormModel issue);

    }
}
