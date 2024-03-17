namespace CarShop.Services
{
    using CarShop.Models.Users;
    using System;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public bool ValidateUserRegistration(RegisterUserFormModel model)
        {
            if (model.Username.Length < UserMinUsername)
            {
                return false;
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                return false;
            }
            if (model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                return false;
            }
            if (model.UserType!=UserTypeClient && model.UserType != UserTypeMechanic)
            {
                return false;
            }

            return true;
        }
    }
}
