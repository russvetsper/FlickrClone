using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//this will be required for a user to signin
namespace FlickrClone.ViewModels
{
    public class LoginViewModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
