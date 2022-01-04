using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
       private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        //Home page
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.dishes_db;
            return View();
        }

        //Displays the form to create new dish (NewDish) 
        [HttpGet("new")]
        public IActionResult NewDish()
        {
            return View();
        }
        // Adding Dish
        [HttpPost("addDish")]
        public IActionResult AddDish(Dish newDish)
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Form to update dish
        [HttpGet("update/{dishId}")]
        public IActionResult UpdateDish(int dishId)
        {
            Dish RetrievedDish = _context.dishes_db.FirstOrDefault(dish => dish.DishId == dishId);
            ViewBag.selectedDish = RetrievedDish;
            return View();
        }

        // Post action to update dish 
         [HttpPost("update/{dishId}")]
        public IActionResult UpdateDishPost(Dish updatedDish, int dishId)
        {
            Dish RetrievedDish = _context.dishes_db.FirstOrDefault(dish => dish.DishId == dishId);
            
            RetrievedDish.Name = updatedDish.Name;
            RetrievedDish.Chef = updatedDish.Chef;
            RetrievedDish.Calories = updatedDish.Calories;
            RetrievedDish.Tastiness = updatedDish.Tastiness;
            RetrievedDish.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // View a selected dish 
        [HttpGet("/{dishId}")]
        public IActionResult ViewDish(int dishId)
        {
            Dish RetrievedDish = _context.dishes_db.FirstOrDefault(dish => dish.DishId == dishId);
            ViewBag.selectedDish = RetrievedDish;
            return View();
        }

        // Delete a dish
        [HttpGet("delete/{dishId}")]
        public IActionResult DeleteDish(int dishId)
        {
            Dish RetrievedDish = _context.dishes_db.FirstOrDefault(dish => dish.DishId == dishId);
            _context.dishes_db.Remove(RetrievedDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
