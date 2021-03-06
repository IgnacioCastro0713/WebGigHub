﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGigHub.Controllers;
using WebGigHub.Core.Models;

namespace WebGigHub.Core.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required] public string Venue { get; set; }

        [Required] [FutureDate] public string Date { get; set; }

        [Required] [ValidTime] public string Time { get; set; }

        [Required] public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, Task<ActionResult>>> create = (c => c.Create(this));
                Expression<Func<GigsController, Task<ActionResult>>> update = (c => c.Update(this));
                var method = (Id != 0) ? update : create;
                var action = (method.Body as MethodCallExpression)?.Method.Name;
                return action;
            }
        }

        public DateTime GetDateTime() => DateTime.Parse($"{Date} {Time}");
    }
}