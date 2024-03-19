namespace CarShop.Services
{
    using CarShop.Models.Users;
    using System;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel user);
    }
}
