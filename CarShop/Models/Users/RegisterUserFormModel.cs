namespace CarShop.Models.Users
{
    using System;

    public class RegisterUserFormModel  // Dannite idvat ot Formata - Register podadeni ot Browsera
    {
       public string Username { get; init; }

       public string Email { get; init; }

       public string Password { get; init; }

       public string ConfirmPassword { get; init; }

       public string UserType { get; init; }
    }
}
