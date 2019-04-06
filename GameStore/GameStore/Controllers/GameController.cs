using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new GameStoreDb())
            {
                var allGames = db.Games.ToList();
                return View(allGames);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Game game)
        {
            using (var db = new GameStoreDb())
            {
                db.Games.Add(game);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = new GameStoreDb())
            {
                var gameToEdit = db.Games.FirstOrDefault(x => x.Id == id);
                if (gameToEdit == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(gameToEdit);
            }
        }

        [HttpPost]
        public IActionResult Edit(Game game)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            using (var db = new GameStoreDb())
            {
                var gameToEdit = db.Games.FirstOrDefault(x => x.Id == game.Id);
                gameToEdit.Name = game.Name;
                gameToEdit.Platform = game.Platform;
                gameToEdit.Price = game.Price;
                gameToEdit.Dlc = game.Dlc;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var db = new GameStoreDb())
            {
                var taskToDelete = db.Games.FirstOrDefault(x => x.Id == id);
                if (taskToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(taskToDelete);
            }
        }

        [HttpPost]
        public IActionResult Delete(Game game)
        {
            using (var db = new GameStoreDb())
            {
                db.Games.Remove(game);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}