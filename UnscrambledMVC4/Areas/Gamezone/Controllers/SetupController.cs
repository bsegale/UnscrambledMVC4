using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;
using UnscrambledMVC4.Infrastructure;

namespace UnscrambledMVC4.Areas.Gamezone.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Gamezone/Setup/

        public ActionResult Index()
        {

            // Get list of games and rounds
            GameRepository gr = new GameRepository();
            
            var games = gr.GetGames(1);

            


            return View(games);
        }

        //
        // GET: /Gamezone/Setup/Details/5

        public ActionResult RoundSetup(int id)
        {
            LeaderboardRepository lr = new LeaderboardRepository();

            // var foursomes = lr.GetFoursomes(id);
            return View();
        }

        //
        // GET: /Gamezone/Setup/Create

        public ActionResult GenerateLeaderboard()
        {

            return View();
        }

        //
        // POST: /Gamezone/Setup/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Gamezone/Setup/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Gamezone/Setup/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Gamezone/Setup/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Gamezone/Setup/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
