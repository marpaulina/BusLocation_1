﻿using System;
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
       [Display(Name = "Imie")]
       [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Błędne imie (tylko litery).")]
       public string Name { get; set; }

       [Required]
       [Display(Name = "Nazwisko")]
       [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Błędne nazwisko (tylko litery).")]
       public string Surname { get; set; }

       [Required]
       [Display(Name = "Login")]
       [RegularExpression("^[a-zA-Z0-9]+([._]?[a-zA-Z0-9]+)*$", ErrorMessage = "Login musi zawierać conajmniej jedną liczbę, litere, znak specjalny (min. 8 znaków).")]
       public string Nick { get; set; }

       [Required]
       [Display(Name = "Hasło")]
       [DataType(DataType.Password)]
       [StringLength(100, ErrorMessage = "Błędne hasło, minimalna ilość znakóe 6.", MinimumLength = 6)]
       public string Password { get; set; }

       public int RouteID { get; set; }
       public int BusStopID { get; set; }
       public TimeSpan TimeFromBusStop { get; set; }
       public double Latitude { get; set; }
       public double Longitude { get; set; }
       public virtual List<UserRequestModels> UsersRequests { get; set; }

        Repository repo = new Repository();

        public DriverModels()
        {
            //Empty 
        }

        DriverModels(string name, string surname, string nick, string password)
        {
            UsersRequests = new List<UserRequestModels>();
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
        public void Update(int userRequestID)
        {
            UserRequestModels userRequest = repo.GetUserRequestByID(userRequestID);
            UsersRequests.Remove(userRequest);
        }
        public void Update(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
        public void Update(int routeID, int busStopID, TimeSpan time)
        {
            RouteID = routeID;
            BusStopID = busStopID;
            TimeFromBusStop = time;
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