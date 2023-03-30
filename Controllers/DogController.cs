using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Models;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DoGo.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepo;

        public DogController(IDogRepository DogRepository)
        {
            _dogRepo = DogRepository;
        }

        // GET: DogController
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dog = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dog);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            return View(dog);
        }

        // GET: DogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {

            int ownerId = GetCurrentUserId();

            Dog dog = _dogRepo.GetDogById(id);
            if (dog == null)
            {
                return NotFound();
            };
            if (ownerId == dog.OwnerId)
            {
                return View(dog);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {

            dog.OwnerId = GetCurrentUserId();

            if(dog.OwnerId == dog.OwnerId)
            {
                _dogRepo.UpdateDog(dog);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            int ownerId = GetCurrentUserId();

            Dog dog = _dogRepo.GetDogById(id);
            if (dog == null)
            {
                return NotFound();
            };
            if (ownerId == dog.OwnerId)
            {
                return View(dog);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            dog.OwnerId = GetCurrentUserId();

            if (dog.OwnerId == dog.OwnerId)
            {
                    _dogRepo.DeleteDog(id);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
