﻿namespace Osiansdrystonewalls.com.Models.ViewModels
{
    public class CreateAccountRequest
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PostCode { get; set; }
        public required string Password { get; set; }

    }
}
