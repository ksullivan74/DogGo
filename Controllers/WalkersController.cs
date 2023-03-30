using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace DoGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalksRepository _walksRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IWalksRepository walksRepo, IOwnerRepository ownerRepo)
        {
            _walkerRepo = walkerRepository;
            _walksRepo = walksRepo;
            _ownerRepo = ownerRepo;
        }

        // GET: WalkersController
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();
            if (ownerId == 0)
            {
                List<Walker> allwalkers = _walkerRepo.GetAllWalkers();
                return View(allwalkers);
            }
            else
            {
                Owner owner = _ownerRepo.GetOwnerById(ownerId);
                List<Walker> boroWalkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);
                return View(boroWalkers);

            }
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetAllWalksByWalkerId(walker.Id);


            WalkerDetailView wv = new WalkerDetailView()
            {
                Walker = walker,
                Walks = walks,
            };

            return View(wv);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                return int.Parse(id);
            }
            else
            {
                return 0;
            }

        }
    }
}
