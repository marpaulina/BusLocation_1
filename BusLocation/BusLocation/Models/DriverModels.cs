using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BusLocation.Models
{
    public class DriverModels
    {
       [Key]
       public int Id { get; set; }

       [Required]
       public string Name { get; set; }

       [Required]
       public string Surname { get; set; }

       [Required]
       public string Nick { get; set; }

       [Required]
       [DataType(DataType.Password)]
       [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public DriverModels()
        {
            //Empty 
        }

        DriverModels(string name, string surname, string nick, string password)
        {
            Name = name;
            Surname = surname;
            Nick = nick;
            Password = password;
        }
        public void Update(DriverModels driver)
        {
            Name = driver.Name;
            Surname = driver.Surname;
            Nick = driver.Nick;
            Password = driver.Password;
        }

        public bool Empty
        {
            get
            {
                return (
                        string.IsNullOrWhiteSpace(Name) &&
                        string.IsNullOrWhiteSpace(Surname) &&
                        string.IsNullOrWhiteSpace(Nick) &&
                        string.IsNullOrWhiteSpace(Password));
                }
        }

    }
}