﻿using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        //public ActionResult Index()
        //{

        //    var upcommingCourses = _dbContext.Courses
        //        .Include(c => c.Lecturer)
        //        .Include(c => c.Category)
        //        .Where(c => c.DateTime > DateTime.Now && c.IsCanceled==false);

        //        var viewModel1 = new CoursesViewModel
        //        {
        //            UpcommingCourses = upcommingCourses,
        //            ShowAction = User.Identity.IsAuthenticated
        //        };
        //        return View(viewModel1);
        //}                   

        public ActionResult Index(String Search)
        {

            var upcommingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now && c.IsCanceled == false);


            if (!String.IsNullOrEmpty(Search))
            {
                ViewBag.Search = Search;
                 upcommingCourses = _dbContext.Courses
                 .Include(s => s.Lecturer)
                 .Include(s => s.Category)
                 .Where(s =>
                 s.Lecturer.Name.Contains(Search) || s.Category.Name.Contains(Search) &&
                 s.DateTime > DateTime.Now &&
                 s.IsCanceled == false);
            }


            var viewModel1 = new CoursesViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel1);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}