using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Gamezone.Models.ViewModels;
using UnscrambledMVC4.Infrastructure;

namespace UnscrambledMVC4.Areas.Gamezone.Controllers
{
    public class LiveController : Controller
    {
        

        public ActionResult Leaderboard(int id)
        {
            LeaderboardRepository lr = new LeaderboardRepository();

            PageViewGameLeaderboard pageView = new PageViewGameLeaderboard();
            pageView.GameLeaderboard = new GameLeaderboard();
            pageView.PlayersOnLeaderboard = new List<PlayerOnGameLeaderboard>();

            pageView.GameLeaderboard = lr.GetLeaderboard(id);

           

            pageView.PlayersOnLeaderboard = lr.GetPlayersOnGameLeaderboard(pageView.GameLeaderboard.ID, 1);

            return View(pageView);
        }

        public ActionResult Score(int id, int groupId, int holeNumber)
        {
            holeNumber = holeNumber != null ? holeNumber : 1;
            LeaderboardRepository lr = new LeaderboardRepository();

            PageViewScore pageView = new PageViewScore();


            
            pageView.PlayersOnScore = new List<PlayerOnScore>();

            // pageView.GameLeaderboard = lr.GetLeaderboard(id);

            pageView.PlayersOnScore = lr.GetScorecardHoleForPairing(id, groupId, holeNumber);


            // pageView.PlayersOnLeaderboard = lr.GetPlayersOnGameLeaderboard(pageView.GameLeaderboard.ID, 1);

            return View(pageView);
        }

        //
        // GET: /Gamezone/Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Gamezone/Default1/Create

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
        // POST: /Gamezone/Default1/Edit/5

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
        // GET: /Gamezone/Default1/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Gamezone/Default1/Delete/5

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
