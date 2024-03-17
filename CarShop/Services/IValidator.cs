namespace CarShop.Services
{
    using CarShop.Models.Users;
    using System;

    public interface IValidator
    {
        bool ValidateUserRegistration(RegisterUserFormModel model);
    }
}
