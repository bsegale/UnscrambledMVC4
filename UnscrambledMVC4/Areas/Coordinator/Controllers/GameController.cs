using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Areas.Coordinator.Models;
using Unscrambled.Models;
using Unscrambled.Areas.Coordinator.Functions;
using Unscrambled.Areas.Coordinator.Models.ViewModels;
using Unscrambled.Areas.Coordinator.Validations;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Validations;
using UnscrambledMVC4.Infrastructure;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;



namespace Unscrambled.Areas.Coordinator.Controllers
{ 
    public class GameController : Controller
    {
   

        private GameRepository db = new GameRepository();
        private LeaderboardRepository leaderboardRepository = new LeaderboardRepository();
        private CoordinatorLibrary lib = new CoordinatorLibrary();
        private int coordinatorID = 1;



        public ViewResult MyGames()
        {
            MyGamesPageView pageView = new MyGamesPageView();
            pageView.myGamesVM = new MyGamesViewModel();
            pageView.leftNavVM = new CoordinatorLeftNavViewModel();
            CoordinatorLibrary cl = new CoordinatorLibrary();
            pageView.leftNavVM.leftNavItems = cl.GenerateCoordinatorLeftNavItems(1);

            pageView.myGamesVM.games = db.GetGames(1).ToList();
            return View(pageView);
        }


        public ActionResult GameDetails(int id) // Game ID
        {
            Game game = db.GetGameByID(id);
            if (game.GameStateID > 1)
            {
                return RedirectToAction("ViewGameDetails", new { id = id });
            }

            return RedirectToAction("ViewGameDetails", new { id = id });
        }


        public ActionResult EditGameDetails(int id)
        {
            Game game = db.GetGameByID(id);
            if (game.GameStateID > 1)
            {
                TempData["FromEditGameDetails"] = "Y";
                return RedirectToAction("ViewGameDetails", new { id = id });
            }
            // Create page and partial view models
            EditGameDetailsPageView pageView = new EditGameDetailsPageView();
            pageView.editGameDetailsVM = new EditGameDetailsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();

            
            // Set select list items for createGameVM
            //ViewBag.GameTypes = lib.GameTypesSelectListItems();
            pageView.editGameDetailsVM.GameTypeSelectItems = lib.GameTypesSelectListItems(game.GameTypeID);
            pageView.editGameDetailsVM.numberOfRoundsSelectItems = lib.NumberOfRoundsSelectListItems(game.NumberOfRounds);
            pageView.editGameDetailsVM.IsHandicappedSelectItems = lib.IsHandicappedSelectListItems(game.IsHandicapped);


            pageView.leftNavVM.leftNavItems = lib.GenerateLeftNavItems(game);
            
            game.CoordinatorID = 1;

            //pageView.leftNavVM.GameID = game.ID;
            pageView.editGameDetailsVM.game = game;


            return View(pageView);
        }



       

        public ActionResult ViewGameDetails(int id)
        {
            // ViewBag.CoordinatorID = new SelectList(db.Coordinators, "ID", "FirstName");
            ViewBag.SectionTitle = "View Game Details";

            // Create page and partial view models
            ViewGameDetailsPageView pageView = new ViewGameDetailsPageView();
            pageView.viewGameDetailsVM = new ViewGameDetailsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();

            Game game = db.GetGameByID(id);
            pageView.leftNavVM.leftNavItems = lib.GenerateLeftNavItems(game);
            pageView.viewGameDetailsVM.game = game;
            pageView.viewGameDetailsVM.gameType = db.GetGameType(id);


            return View(pageView);
        }
        //
        // GET: /Coordinator/Game/Edit/5
 
        public ActionResult Create()
        {
            // ViewBag.CoordinatorID = new SelectList(db.Coordinators, "ID", "FirstName");
            ViewBag.SectionTitle = "Game Details";

            // Create page and partial view models
            GameDetailsPageView pageView = new GameDetailsPageView();
            pageView.createGameVM = new GameDetailsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();


            // Set select list items for createGameVM
            //ViewBag.GameTypes = lib.GameTypesSelectListItems();
            pageView.createGameVM.GameTypeSelectItems = lib.GameTypesSelectListItems(99);
            pageView.createGameVM.numberOfRoundsSelectItems = lib.NumberOfRoundsSelectListItems(99);
            pageView.createGameVM.IsHandicappedSelectItems = lib.IsHandicappedSelectListItems("");

            

            Game game = new Game();
            pageView.leftNavVM.leftNavItems = lib.GenerateLeftNavItems(game);
            
            game.CoordinatorID = 1;

            //pageView.leftNavVM.GameID = game.ID;
            pageView.createGameVM.game = game;


            return View(pageView);
        }


        // POST: /Coordinator/Game/Create

        [HttpPost]
        public ActionResult Create(Game inGame)
        {
            GameDetailsFormValidator formValidator = new GameDetailsFormValidator();
            GameDetailsPageView pageView = new GameDetailsPageView();
            pageView.createGameVM = new GameDetailsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            pageView.createGameVM.game = new Game();
            bool formHasErrors = false;


            Game gameToAdd = new Game();
            gameToAdd.CoordinatorID = 1;

            // Check Game Name
            if (!formValidator.isGameNameValid(Request["Name"]))
            {
                pageView.createGameVM.gameNameHasErrors = true;
                formHasErrors = true;
                gameToAdd.Name = Request["Name"];
            }
            else
            {
                gameToAdd.Name = Request["Name"];
            }


            // Check Number of Players
            if (!formValidator.isNumberPlayersValid(Request["NumberOfPlayersOrTeams"]))
            {
                pageView.createGameVM.numberPlayersHasErrors = true;
                formHasErrors = true;
                gameToAdd.NumberOfPlayersOrTeams = 0;
            }
            else
            {
                gameToAdd.NumberOfPlayersOrTeams = Convert.ToInt32(Request["NumberOfPlayersOrTeams"]);
            }

            // Check Game Type
            if (!formValidator.isGameTypeValid(Request["GameTypeID"]))
            {
                pageView.createGameVM.gameTypeHasErrors = true;
                formHasErrors = true;
                gameToAdd.GameTypeID = 999; // Set it to something that is out of range
            }
            else
            {
                gameToAdd.GameTypeID = Convert.ToInt32(Request["GameTypeID"]);
            }

            // Check IsHandicapped
            if (!formValidator.isHandicappedValid(Request["IsHandicapped"]))
            {
                pageView.createGameVM.isHandicappedHasErrors = true;
                formHasErrors = true;
                gameToAdd.IsHandicapped = ""; // Set it to something that is out of range
            }
            else
            {
                gameToAdd.IsHandicapped = Request["IsHandicapped"];
            }

            // Check NumberOfRounds
            if (!formValidator.isNumberOfRoundsValid(Request["NumberOfRounds"]))
            {
                pageView.createGameVM.numberOfRoundsHasErrors = true;
                formHasErrors = true;
                gameToAdd.NumberOfRounds = 0; // Set it to something that is out of range
            }
            else
            {
                gameToAdd.NumberOfRounds = Convert.ToInt32(Request["NumberOfRounds"]);
            }



            if (!formHasErrors)
            {
                // Generate a random Join Code that doesn't exist in DB.
                inGame.JoinCode = lib.GenerateJoinCode();
                int gameID;
                // Save game
                inGame.GameStateID = 1;
                inGame.CalcHandicapsFromIndex = "Y";

                db.CreateGame(inGame);
                gameID = inGame.ID;

                return RedirectToAction("SetPlayers", new { id = gameID });
            }
            
                pageView.createGameVM.game = gameToAdd;
                pageView.createGameVM.GameTypeSelectItems = lib.GameTypesSelectListItems(inGame.GameTypeID);
                pageView.createGameVM.IsHandicappedSelectItems = lib.IsHandicappedSelectListItems(Request["IsHandicapped"]);
                pageView.createGameVM.numberOfRoundsSelectItems = lib.NumberOfRoundsSelectListItems(inGame.NumberOfRounds);
                Game newGame = new Game();
                pageView.leftNavVM.leftNavItems = lib.GenerateLeftNavItems(newGame);
                return View(pageView);

        }

        [HttpPost]
        public ActionResult EditGameDetails(Game gameFromForm)
        {
            GameDetailsFormValidator formValidator = new GameDetailsFormValidator();
            EditGameDetailsPageView pageView = new EditGameDetailsPageView();
            pageView.editGameDetailsVM = new EditGameDetailsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            pageView.editGameDetailsVM.game = new Game();
            bool formHasErrors = false;


            Game gameFromDB = db.GetGameByID(gameFromForm.ID);

            gameFromDB.CoordinatorID = 1;

            // Check Game Name
            if (!formValidator.isGameNameValid(Request["Name"]))
            {
                pageView.editGameDetailsVM.gameNameHasErrors = true;
                pageView.editGameDetailsVM.gameNameErrorMessage = "Invalid game name.  Reverting to old value.";
                formHasErrors = true;
                gameFromForm.Name = Request["Name"];
            }
            else
            {
                gameFromForm.Name = Request["Name"];
            }


            // Check Number of Players
            if (!formValidator.isNumberPlayersValid(Request["NumberOfPlayersOrTeams"]))
            {
                pageView.editGameDetailsVM.numberPlayersHasErrors = true;
                pageView.editGameDetailsVM.numberOfPlayersErrorMessage = "Invalid number of players.  Reverting to old value.";
                formHasErrors = true;
                gameFromForm.NumberOfPlayersOrTeams = gameFromDB.NumberOfPlayersOrTeams;
            }
            else
            {
                gameFromForm.NumberOfPlayersOrTeams = Convert.ToInt32(Request["NumberOfPlayersOrTeams"]);
            }

            // Check Game Type
            if (!formValidator.isGameTypeValid(Request["GameTypeID"]))
            {
                pageView.editGameDetailsVM.gameTypeHasErrors = true;
                formHasErrors = true;
                gameFromForm.GameTypeID = 999; // Set it to something that is out of range
            }
            else
            {
                gameFromForm.GameTypeID = Convert.ToInt32(Request["GameTypeID"]);
            }

            // Check IsHandicapped
            if (!formValidator.isHandicappedValid(Request["IsHandicapped"]))
            {
                pageView.editGameDetailsVM.isHandicappedHasErrors = true;
                formHasErrors = true;
                gameFromForm.IsHandicapped = ""; // Set it to something that is out of range
            }
            else
            {
                gameFromForm.IsHandicapped = Request["IsHandicapped"];
            }

            // Check NumberOfRounds
            if (!formValidator.isNumberOfRoundsValid(Request["NumberOfRounds"]))
            {
                pageView.editGameDetailsVM.numberOfRoundsHasErrors = true;
                formHasErrors = true;
                gameFromForm.NumberOfRounds = gameFromDB.NumberOfRounds;
            }
            else
            {
                gameFromForm.NumberOfRounds = Convert.ToInt32(Request["NumberOfRounds"]);
            }

            if (formHasErrors)
            {
                pageView.editGameDetailsVM.game = gameFromForm;
                pageView.editGameDetailsVM.GameTypeSelectItems = lib.GameTypesSelectListItems(gameFromForm.GameTypeID);
                pageView.editGameDetailsVM.IsHandicappedSelectItems = lib.IsHandicappedSelectListItems(Request["IsHandicapped"]);
                pageView.editGameDetailsVM.numberOfRoundsSelectItems = lib.NumberOfRoundsSelectListItems(gameFromForm.NumberOfRounds);
                
                pageView.leftNavVM.leftNavItems = lib.GenerateLeftNavItems(gameFromDB);
                return View(pageView);
            }

            

            EditGameDetailsConfirmationPageView confirmationPageView = new EditGameDetailsConfirmationPageView();
            confirmationPageView.leftNavVM = new LeftNavViewModel();
            confirmationPageView.editGameDetailsConfirmationVM = new EditGameDetailsConfirmationViewModel();
            bool requiresConfirmation = false;

            if (gameFromForm.NumberOfPlayersOrTeams < gameFromDB.NumberOfPlayersOrTeams)
            {
                requiresConfirmation = true;
                confirmationPageView.editGameDetailsConfirmationVM.NumberOfPlayersToDelete = gameFromDB.NumberOfPlayersOrTeams - gameFromForm.NumberOfPlayersOrTeams;
                confirmationPageView.editGameDetailsConfirmationVM.ListOfPlayerToDelete = db.GetPlayersThatWillBeDeleted(gameFromForm.ID, confirmationPageView.editGameDetailsConfirmationVM.NumberOfPlayersToDelete);
                confirmationPageView.editGameDetailsConfirmationVM.NumberOfPlayersAfterDelete = gameFromDB.NumberOfPlayersOrTeams - confirmationPageView.editGameDetailsConfirmationVM.NumberOfPlayersToDelete;
                confirmationPageView.editGameDetailsConfirmationVM.NumberOfPlayersToStart = gameFromDB.NumberOfPlayersOrTeams;
            }
            if (gameFromForm.NumberOfRounds < gameFromDB.NumberOfRounds)
            {
                requiresConfirmation = true;
                confirmationPageView.editGameDetailsConfirmationVM.NumberOfRoundsToDelete = gameFromForm.NumberOfRounds - gameFromForm.NumberOfRounds;
                db.GetRoundsThatWillBeDeleted(gameFromForm.ID, confirmationPageView.editGameDetailsConfirmationVM.NumberOfRoundsToDelete);
                confirmationPageView.editGameDetailsConfirmationVM.NumberOfRoundsAfterDelete = gameFromDB.NumberOfRounds - confirmationPageView.editGameDetailsConfirmationVM.NumberOfRoundsToDelete;
                confirmationPageView.editGameDetailsConfirmationVM.NumberOfRoundsToStart = gameFromDB.NumberOfRounds;
            }
            if (gameFromDB.IsHandicapped != gameFromForm.IsHandicapped)
            {
                requiresConfirmation = true;
                confirmationPageView.editGameDetailsConfirmationVM.didIsHandicappedChange = true;
            }

            if (requiresConfirmation == true)
            {
                ViewBag.SectionTitle = "Edit Game Details Confirmation";
                CoordinatorLibrary cl = new CoordinatorLibrary();
                confirmationPageView.editGameDetailsConfirmationVM.GameID = gameFromForm.ID;
                confirmationPageView.editGameDetailsConfirmationVM.EditedGameDetails = gameFromForm;
                confirmationPageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(gameFromDB);
                return View("EditGameDetailsConfirmation", confirmationPageView);
            }

            // It will only get in here if the game name or game type is changing.  All other changes will
            // be taken to a confirmation screen
            if (!formHasErrors && requiresConfirmation == false)
            {
                db.EditGame(gameFromForm);
                return RedirectToAction("SetPlayers", new { id = gameFromDB.ID });
            }
            else
            {
                return RedirectToAction("DBError");
            }
                

        }
 
        
        

        

        

        //
        // GET: /Coordinator/Game/Delete/5

        
 
        public ActionResult Delete(int id)
        {
            Game game = db.GetGameByID(id);
            return View(game);
        }

        
   

        [HttpPost]
        public ActionResult DeleteGameConfirmed() // playerID
        {
            GameDetailsFormValidator validate = new GameDetailsFormValidator();
            int gameId;
            if (!validate.isGameIDValid(Request["GameID"]))
            {
                return RedirectToAction("ValidationError");
            }
            else
            {
                gameId = Convert.ToInt32(Request["GameID"]);
            }

            if (!db.DeleteGameAndEverythingElse(gameId))
            {
                return RedirectToAction("DBError");
            }
            else
            {
                return RedirectToAction("MyGames");
            }


        }

        [HttpPost]
        public ActionResult DeletePlayersConfirmation()
        {
            CoordinatorLibrary cl = new CoordinatorLibrary();
            DeletePlayersConfirmationPageView pageView = new DeletePlayersConfirmationPageView();
            pageView.deletePlayersConfirmationViewModel = new DeletePlayersConfirmationViewModel();
            pageView.leftNavViewModel = new LeftNavViewModel();
            
            
            List<Player> playersToDelete = new List<Player>();

            foreach (string key in Request.Params.Keys)
            {
                if (key.Contains("PlayerIDToDelete"))
                {
                    Player player = new Player();                    
                    player = db.GetPlayerByID(Convert.ToInt32(Request[key]));
                    playersToDelete.Add(player);
                }
            }
            int gameId = Convert.ToInt32(Request["GameID"]);
            Game game = db.GetGameByID(gameId);
            pageView.deletePlayersConfirmationViewModel.PlayersToDelete = playersToDelete;
            pageView.deletePlayersConfirmationViewModel.Game = game;
            pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);


            return View(pageView);
        }

        public ActionResult EditGameDetailsConfirmation()
        {
            
            EditGameDetailsConfirmationPageView pageView = new EditGameDetailsConfirmationPageView();
            pageView.editGameDetailsConfirmationVM = new EditGameDetailsConfirmationViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            
        
            return View(pageView);
        }

        [HttpPost]
        public ActionResult EditGameConfirmed(Game gameFromForm) // playerID
        {
           
            if (!db.EditGame(gameFromForm))
            {
                return RedirectToAction("DBError");
            }

            return RedirectToAction("GameDetails", new { id = gameFromForm.ID });
    
        }
        

        //
        // POST: /Coordinator/Game/DeletePlayer
        // This controller will do the following:
        // 1) Get the player instance
        // 2) Get the game intance
        // 3) Reduce the Game.NumberOfPlayersOrTeams by 1
        // 4) Delete the player from the DB

        [HttpPost]
        public ActionResult DeletePlayersConfirmed() // playerID
        {
            CoordinatorLibrary cl = new CoordinatorLibrary();
            int gameId = Convert.ToInt32(Request["GameID"]);
            Game game = db.GetGameByID(gameId);
            

            foreach (string key in Request.Params.Keys)
            {
                if (key.Contains("PlayerIDToDelete"))
                {
                    Player player = new Player();
                    player = db.GetPlayerByID(Convert.ToInt32(Request[key]));
                    if (!db.DeletePlayerFromGame(game,player))
                    {
                        return RedirectToAction("DBError");
                    }
                }
            }

            return RedirectToAction("SetPlayers", new { id = game.ID });

           
        }


       


        public ActionResult DBError(int id)
        {
            return View();
        }

        public ActionResult ValidationError()
        {
            return View();
        }

        public ActionResult GameError()
        {
            return View();
        }

        

        public ActionResult SetPlayers(int id) // id is GameID
        {
            Game gameCheck = db.GetGameByID(id);
            if (gameCheck.GameStateID > 1)
            {
                return RedirectToAction("ViewPlayers", new { id = id });
            }
            SetPlayersPageView pageView = new SetPlayersPageView();
            pageView.setPlayersVM = new SetPlayersViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            CoordinatorLibrary cl = new CoordinatorLibrary();
            
            

            int gameId = id;
            Game game = db.GetGameRoundsAndPlayersByID(id);


            pageView.setPlayersVM.Game = game;
            pageView.setPlayersVM.Players = game.Players;

            int playerCount = 1;
            pageView.setPlayersVM.playerNameErrors = new List<KeyValuePair<string,bool>>();
            pageView.setPlayersVM.playerIndexErrors = new List<KeyValuePair<string,bool>>();

            foreach (var player in pageView.setPlayersVM.Players)
            {
                pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + playerCount.ToString(), false));
                pageView.setPlayersVM.playerIndexErrors.Add(new KeyValuePair<string, bool>("PlayerName" + playerCount.ToString(), false));
            }

                    

            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);
            
            return View(pageView);
          
            
        }

        public ActionResult SetHandicaps(int id) // id is RoundID       
        {
            ViewBag.SectionTitle = "Set Player Handicaps";
            SetRoundHandicapsPageView pageView = new SetRoundHandicapsPageView();
            pageView.setRoundHandicapsVM = new SetRoundHandicapsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            CoordinatorLibrary cl = new CoordinatorLibrary();




            Game game = db.GetGameByRoundID(id);

            if (game.CalcHandicapsFromIndex == "Y")
            {
                return RedirectToAction("ChangeSetHandicapsConfirmation", new { id = id });
            }
            pageView.setRoundHandicapsVM.Game = game;

            IEnumerable<RoundPlayerHandicap> roundPlayerHandicaps = db.GetRoundPlayerHandicapsByRoundID(id);

            List<RoundPlayerViewModel> rPVMList = new List<RoundPlayerViewModel>();

            foreach (var roundPlayerHandicap in roundPlayerHandicaps)
            {
                RoundPlayerViewModel rPVM = new RoundPlayerViewModel();
                rPVM.Handicap = roundPlayerHandicap.Handicap;
                rPVM.ID = roundPlayerHandicap.ID;
                rPVM.PlayerID = roundPlayerHandicap.PlayerID;
                rPVM.PlayerName = roundPlayerHandicap.Player.Name;
                rPVM.RoundID = roundPlayerHandicap.RoundID;
                rPVMList.Add(rPVM);
            }

            pageView.setRoundHandicapsVM.RoundPlayersViewModels = rPVMList;

            pageView.setRoundHandicapsVM.RoundID = id;

            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);

            return View(pageView);


        }


        [HttpPost]
        public ActionResult SetHandicaps()
        {
            // Initialize the View Model
            ViewBag.SectionTitle = "Set Player Handicaps";
            SetRoundHandicapsPageView pageView = new SetRoundHandicapsPageView();
            pageView.setRoundHandicapsVM = new SetRoundHandicapsViewModel();
            pageView.leftNavVM = new LeftNavViewModel();


            CoordinatorLibrary cl = new CoordinatorLibrary();
            bool pageHasErrors = false;

            String gameIDString = Request["GameID"]; // Get GameID
            int gameID = Convert.ToInt32(gameIDString); // Make it an iteger
            int numberOfPlayers = Convert.ToInt32(Request["NumberOfPlayersOrTeams"]); // Get Number of Players
            int roundID = Convert.ToInt32(Request["RoundID"]);

            // Redirect back to ViewPlayerHandicaps on Cancel
            if (Request["SubmitForm"] == "Cancel")
            {
                return RedirectToAction("ViewHandicaps", new { id = roundID });
            }

            String playerHandicapString;


            RoundPlayerValidations validator = new RoundPlayerValidations();
            List<RoundPlayerViewModel> rPVMList = new List<RoundPlayerViewModel>();

            for (int i = 1; i <= numberOfPlayers; i++)
            {
                RoundPlayerViewModel rPVM = new RoundPlayerViewModel();

                rPVM.ID = Convert.ToInt32(Request["RoundPlayerID" + i.ToString()]);
                rPVM.PlayerName = Request["RoundPlayerName" + i.ToString()];
                rPVM.PlayerID = Convert.ToInt32(Request["RoundPlayerPlayerID" + i.ToString()]);
                rPVM.RoundID = roundID;
                playerHandicapString = Request["RoundPlayerHandicap" + i.ToString()];
                bool isHandicapGood = validator.isHandicapValid(playerHandicapString);
                if (isHandicapGood)
                {
                    rPVM.Handicap = Convert.ToInt32(playerHandicapString);
                    RoundPlayerHandicap rPH = db.GetRoundPlayerHandicapByID(rPVM.ID);
                    rPH.Handicap = rPVM.Handicap;
                    if (!db.UpdateRoundPlayerHandicap(rPH))
                    {
                        return RedirectToAction("DBError", new { id = gameID });
                    }
                    else if (!isHandicapGood)
                    {
                        rPVM.Handicap = null;
                        rPVM.HandicapErrorMessage = "Invalid Handicap";
                        pageHasErrors = true;
                    }
                }

            }

            if (pageHasErrors)
            {
                Game game = db.GetGameByRoundID(roundID);
                pageView.setRoundHandicapsVM.Game = game;
                pageView.setRoundHandicapsVM.RoundID = roundID;
                pageView.setRoundHandicapsVM.RoundPlayersViewModels = rPVMList;
                pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);
                return View(pageView);
            }

            return RedirectToAction("ViewHandicaps", new { id = roundID });

        }

        public ActionResult ViewRound(int id) // id is RoundID
        {
            int roundId = id;
            Game game = db.GetGameByRoundID(roundId);

            CoordinatorLibrary cl = new CoordinatorLibrary();
            ViewRoundPageView pageView = new ViewRoundPageView();
            pageView.viewRoundVM = new ViewRoundViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);

            // Setup the RoundViewModel
            Round round = db.GetRoundAndHolesByID(id);
            RoundViewModel roundVM = new RoundViewModel();
            roundVM.Back9Total = round.Back9Total;
            roundVM.Front9Total = round.Front9Total;
            roundVM.FullRoundTotal = round.FullRoundTotal;
            roundVM.GameID = round.GameID;
            roundVM.HolesInRound = round.HolesInRound;
            roundVM.ID = round.ID;
            roundVM.RoundNumberInGame = round.RoundNumberInGame;
            roundVM.RoundStateID = round.RoundStateID;
            roundVM.Slope = round.Slope;
            
            pageView.viewRoundVM.RoundViewModel = roundVM;


            // Setup the Hole View Models
            pageView.viewRoundVM.HoleViewModels = new List<HoleViewModel>();
            var holeViewModelList = new List<HoleViewModel>();
            foreach (var hole in round.Holes)
            {
                HoleViewModel holeViewModel = new HoleViewModel();
                holeViewModel.ID = hole.ID;
                holeViewModel.RoundID = hole.RoundID;
                // holeViewModel.ErrorMessage = "";
                holeViewModel.HoleNumber = hole.HoleNumber;
                if (hole.Par != null)
                {
                    holeViewModel.Par = hole.Par;
                }
                if (hole.Handicap != null)
                {
                    holeViewModel.Handicap = hole.Handicap;
                }

                if (round.HolesInRound == 18)
                {
                    holeViewModel.HoleHandicapList = cl.HandicapSelectItems18(hole.Handicap);
                }
                else if (round.HolesInRound == 9)
                {
                    holeViewModel.HoleHandicapList = cl.HandicapSelectItems9(hole.Handicap);
                }
                holeViewModel.ParList = cl.ParSelectItems(hole.Par);
                holeViewModelList.Add(holeViewModel);
            }
            pageView.viewRoundVM.HoleViewModels = holeViewModelList;

            return View(pageView);
        }


        public ActionResult ViewHandicaps(int id) // id is RoundID
        {
           
            ViewRoundHandicapsPageView pageView = new ViewRoundHandicapsPageView();
            CoordinatorLibrary cl = new CoordinatorLibrary();

            Game game = db.GetGameByRoundID(id);

            if (game.CalcHandicapsFromIndex == "Y")
            {
                if (!db.CalculateHandicapsFromSlopeAndIndex(id))
                {
                    return RedirectToAction("DBError");
                }
            }
            

            pageView.viewRoundHandicapsVM.RoundPlayersViewModels = db.GetRoundPlayerHandicapsByRoundID(id);
            pageView.viewRoundHandicapsVM.Game = db.GetGameByRoundID(id);
            
            



            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(pageView.viewRoundHandicapsVM.Game);

            return View(pageView);


        }


        public ActionResult ViewCalculatedHandicaps(int id) // id is RoundID
        {

            ViewCalculatedRoundHandicapsPageView pageView = new ViewCalculatedRoundHandicapsPageView();
            CoordinatorLibrary cl = new CoordinatorLibrary();

            Game game = db.GetGameByRoundID(id);

            if (game.CalcHandicapsFromIndex == "Y")
            {
                if (!db.CalculateHandicapsFromSlopeAndIndex(id))
                {
                    return RedirectToAction("DBError");
                }
            }


            pageView.viewRoundHandicapsVM.RoundPlayersViewModels = db.GetRoundPlayerHandicapsByRoundID(id);
            pageView.viewRoundHandicapsVM.Game = db.GetGameByRoundID(id);





            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(pageView.viewRoundHandicapsVM.Game);

            return View(pageView);


        }



        public ActionResult SetPlayerHandicap(int id, int playerId) // id is Round Player ID
        {
            ViewBag.SectionTitle = "Manually Set Player Handicap";
            SetPlayerHandicapPageView pageView = new SetPlayerHandicapPageView();
            CoordinatorLibrary cl = new CoordinatorLibrary();


            RoundPlayerHandicap roundPlayerHandicap = db.GetRoundPlayerHandicapByRoundIdAndPlayerId(id, playerId);
            roundPlayerHandicap.CalcHandicapFromIndex = "N";
            if (!db.UpdateRoundPlayerHandicap(roundPlayerHandicap))
            {
                return RedirectToAction("DBError");
            }

            RoundPlayerViewModel roundPlayerViewModel = new RoundPlayerViewModel();
            roundPlayerViewModel.ID = roundPlayerHandicap.ID;
            roundPlayerViewModel.Handicap = roundPlayerHandicap.Handicap;
            roundPlayerViewModel.PlayerID = roundPlayerHandicap.PlayerID;
            roundPlayerViewModel.PlayerName = roundPlayerHandicap.Player.Name;
            roundPlayerViewModel.RoundID = roundPlayerHandicap.RoundID;
            


            Game game = db.GetGameByRoundID(roundPlayerHandicap.RoundID);


            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);
            pageView.setPlayerHandicapVM.Game = game;
            pageView.setPlayerHandicapVM.RoundPlayerViewModel = roundPlayerViewModel;

           return View(pageView);


        }


        [HttpPost]
        public ActionResult SetPlayerHandicap()
        {

            

            RoundPlayerViewModel roundPlayerViewModel = new RoundPlayerViewModel();
            RoundPlayerValidations roundPlayerValidation = new RoundPlayerValidations();

            roundPlayerViewModel.ID = Convert.ToInt32(Request["RoundPlayerID"]);
            roundPlayerViewModel.PlayerID = Convert.ToInt32(Request["PlayerID"]);
            roundPlayerViewModel.RoundID = Convert.ToInt32(Request["RoundID"]);

            // Redirect back to ViewPlayerHandicaps on Cancel
            if (Request["SubmitForm"] == "Cancel")
            {
                return RedirectToAction("ViewHandicaps", new { id = roundPlayerViewModel.RoundID });
            }

            if (roundPlayerValidation.isHandicapValid(Request["PlayerHandicap"]))
            {
                RoundPlayerHandicap roundPlayerHandicap = db.GetRoundPlayerHandicapByID(roundPlayerViewModel.ID);
                roundPlayerHandicap.Handicap = Convert.ToInt32(Request["PlayerHandicap"]);
                if (db.UpdateRoundPlayerHandicap(roundPlayerHandicap))
                {
                    return RedirectToAction("ViewHandicaps", new { id = roundPlayerViewModel.RoundID });
                }
                else
                {
                    return RedirectToAction("DBError");
                }

            }
            else
            {
                RoundPlayerHandicap roundPlayerHandicap = db.GetRoundPlayerHandicapByID(roundPlayerViewModel.ID);
                roundPlayerViewModel.HandicapErrorMessage = "Invalid Handicap";
                roundPlayerViewModel.Handicap = roundPlayerHandicap.Handicap;
                roundPlayerViewModel.PlayerName = roundPlayerHandicap.Player.Name;
                roundPlayerViewModel.RoundID = roundPlayerHandicap.RoundID;

                SetPlayerHandicapPageView pageView = new SetPlayerHandicapPageView();
                //pageView.leftNavVM = new LeftNavViewModel();
                pageView.setPlayerHandicapVM.RoundPlayerViewModel = roundPlayerViewModel;
                CoordinatorLibrary cl = new CoordinatorLibrary();
                pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(db.GetGameByRoundID(roundPlayerViewModel.RoundID ));

                return View(pageView);


            }




        }

        public ViewResult SetFoursomes(int id) // id is Round ID
        {
            int roundId = id;
            ViewBag.SectionTitle = "Set Foursomes";
            CoordinatorLibrary cl = new CoordinatorLibrary();
            SetFoursomesPageView pageView = new SetFoursomesPageView();
            pageView.setFoursomesVM = new SetFoursomesViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
           

            
            Game game = db.GetGameByRoundID(roundId);

           
               

            pageView.setFoursomesVM.GameID = game.ID;
            
            
            pageView.setFoursomesVM.Foursomes = db.GetFoursomes(roundId);
            pageView.setFoursomesVM.RoundID = roundId;
            pageView.setFoursomesVM.NumberOfPlayersOrTeams = game.NumberOfPlayersOrTeams;
            pageView.setFoursomesVM.GameID = game.ID;
            pageView.setFoursomesVM.emptyFoursomes = db.GetDeletableFoursomes(roundId);

            IEnumerable<RoundFoursome> foursomes = pageView.setFoursomesVM.Foursomes;
            pageView.setFoursomesVM.FoursomeNumberSelectListItems = lib.FoursomeNumberSelectListItems(foursomes);



            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);
            return View(pageView);


        }

        public ActionResult AddFoursomeConfirmation(int id) // id is round id
        {
            CoordinatorLibrary cl = new CoordinatorLibrary();
            AddFoursomeConfirmationPageView pageView = new AddFoursomeConfirmationPageView();
            pageView.addFoursomeConfirmationViewModel = new AddFoursomeConfirmationViewModel();
            pageView.leftNavViewModel = new LeftNavViewModel();

            List<RoundFoursome> roundFoursomes = db.GetFoursomes(id).ToList();
            pageView.addFoursomeConfirmationViewModel.currentNumberOfFoursomes = roundFoursomes.Count();
            pageView.addFoursomeConfirmationViewModel.newNumberOfFoursomes = pageView.addFoursomeConfirmationViewModel.currentNumberOfFoursomes + 1;

            pageView.addFoursomeConfirmationViewModel.RoundID = id;
            Game game = new Game();
            game = db.GetGameByRoundID(id); // id is round id
            pageView.addFoursomeConfirmationViewModel.Game = game;
            pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);


            return View(pageView);
        }


        [HttpPost]
        public ActionResult AddFoursomeConfirmed()
        {
            int roundId = Convert.ToInt32(Request["RoundID"]);
            Round round = db.GetRoundByID(roundId);
            RoundFoursome roundFoursome = db.AddFoursome(round);
            if (roundFoursome == null)
            {
                return RedirectToAction("DBError");
            }

            return RedirectToAction("SetFoursomes", new { id = roundId });

        }

        public ActionResult DeleteFoursomes(int id) // id is round id
        {
            int roundId = id;
            List<RoundFoursome> deletableFoursomes = db.GetDeletableFoursomes(roundId);
            
            if (deletableFoursomes.Count() == 0)
            {
                TempData["FromDeleteFoursome"] = "Y";
                return RedirectToAction("SetFoursomes", new { id = roundId });
            }
            
            CoordinatorLibrary cl = new CoordinatorLibrary();
            DeleteFoursomesPageView pageView = new DeleteFoursomesPageView();
            pageView.deleteFoursomesViewModel = new DeleteFoursomesViewModel();
            pageView.leftNavViewModel = new LeftNavViewModel();

            Game game = new Game();
            game = db.GetGameByRoundID(roundId); // id is round id
            pageView.deleteFoursomesViewModel.RoundID = roundId;
            pageView.deleteFoursomesViewModel.Game = game;
            pageView.deleteFoursomesViewModel.deletableFoursomes = deletableFoursomes;
            pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);


            return View(pageView);
        }


        [HttpPost]
        public ActionResult DeleteFoursomes()
        {
            CoordinatorLibrary cl = new CoordinatorLibrary();
            int gameId = Convert.ToInt32(Request["GameID"]);
            int roundId = Convert.ToInt32(Request["RoundID"]);
            Game game = db.GetGameByID(gameId);


            foreach (string key in Request.Params.Keys)
            {
                if (key.Contains("FoursomeIDToDelete"))
                {
                    RoundFoursome roundFoursome = new RoundFoursome();
                    roundFoursome = db.GetFoursomeByID(Convert.ToInt32(Request[key]));
                    if (!db.DeleteFoursomeFromRound(roundFoursome))
                    {
                        return RedirectToAction("DBError");
                    }
                }
            }

            return RedirectToAction("SetFoursomes", new { id = roundId });


        }
        public ViewResult DeletePlayers(int id) // id is GameID
        {
            CoordinatorLibrary cl = new CoordinatorLibrary();
            DeletePlayersPageView pageView = new DeletePlayersPageView();
            pageView.deletePlayersViewModel = new DeletePlayersViewModel();
            pageView.leftNavViewModel = new LeftNavViewModel();
            

            int gameId = id;
            Game game = db.GetGameAndPlayersByID(id);
            

            pageView.deletePlayersViewModel.Game = game;
            pageView.deletePlayersViewModel.Players = game.Players;
            pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);
            
            return View(pageView);



        }

        public ViewResult  AddPlayers(int id) // id is GameID
        {
            CoordinatorLibrary cl = new CoordinatorLibrary();
            AddPlayersPageView pageView = new AddPlayersPageView();
            pageView.addPlayersViewModel = new AddPlayersViewModel(); 
            pageView.leftNavViewModel = new LeftNavViewModel();
            

            int gameId = id;
            Game game = db.GetGameRoundsAndPlayersByID(id);

            pageView.addPlayersViewModel.Game = game;
            pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);


            return View(pageView);



        }

        public ViewResult ViewPlayers(int id) // id is GameID
        {
            ViewBag.SectionTitle = "Player List";
            CoordinatorLibrary cl = new CoordinatorLibrary();
            ViewPlayersPageView pageView = new ViewPlayersPageView();
            pageView.viewPlayerVM = new ViewPlayersViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            

            int gameId = id;
            Game game = db.GetGameAndPlayersByID(id);
            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);

            pageView.viewPlayerVM.Game = game;
            pageView.viewPlayerVM.Players = game.Players;
            // pageView.viewPlayerVM.playerCounter = 0;
            pageView.viewPlayerVM.Round = db.GetFirstRound(gameId);
            
            return View(pageView);



        }

        [HttpPost]
        public ActionResult AddPlayers()
        {
            int gameId = Convert.ToInt32(Request["GameID"]);
            Game game = db.GetGameByID(gameId);

            AddPlayersValidations formInput = new AddPlayersValidations();

            if (!formInput.isGameIDValid(Request["GameID"]))
            {
                return RedirectToAction("ValidationError");
            }

           

            if (!formInput.isNumberOfPlayersValid(Request["NumberOfPlayersToAdd"]))
            {
                CoordinatorLibrary cl = new CoordinatorLibrary();
                AddPlayersPageView pageView = new AddPlayersPageView();
                pageView.addPlayersViewModel = new AddPlayersViewModel();
                pageView.leftNavViewModel = new LeftNavViewModel();
                pageView.addPlayersViewModel.Game = game;
                pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);
                pageView.addPlayersViewModel.numberPlayersHasErrors = true;
                return View(pageView);
            }

            if ((Convert.ToInt32(Request["NumberOfPlayersToAdd"]) + game.NumberOfPlayersOrTeams) > 120)
            {
                CoordinatorLibrary cl = new CoordinatorLibrary();
                AddPlayersPageView pageView = new AddPlayersPageView();
                pageView.addPlayersViewModel = new AddPlayersViewModel();
                pageView.leftNavViewModel = new LeftNavViewModel();
                pageView.addPlayersViewModel.Game = game;
                pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);
                pageView.addPlayersViewModel.numberPlayersHasErrors = true;
                return View(pageView);
            }


            int numberOfPlayersToAdd = Convert.ToInt32(Request["NumberOfPlayersToAdd"]);
            int gameID = Convert.ToInt32(Request["GameID"]);

            if (db.UpdateNumberOfPlayers(gameID, numberOfPlayersToAdd))
            {
                return RedirectToAction("SetPlayers", new { id = gameID });
            }
            else
            {
                return RedirectToAction("DBError");
            }

            
            

        }

        [HttpPost]
        public ActionResult SetPlayers()
        {


            // In case there are issues, prepare the view model
            ViewBag.SectionTitle = "Game Players";
            SetPlayersPageView pageView = new SetPlayersPageView();
            pageView.setPlayersVM = new SetPlayersViewModel();
            pageView.leftNavVM = new LeftNavViewModel();
            bool hasErrors = false;



            String gameIDString = Request["GameID"]; // Get GameID
            int gameID = Convert.ToInt32(gameIDString); // Make it an iteger
            int numberOfPlayers = Convert.ToInt32(Request["NumberOfPlayersOrTeams"]); // Get Number of Players
            String playerIDString;
            String playerNameString;
            String playerIndexString;
            CoordinatorLibrary cl = new CoordinatorLibrary();

            // var rounds = db.GetRoundsByGameID(gameID);

            Game gameFromDB = db.GetGameByID(gameID);


            pageView.setPlayersVM.playerNameErrors = new List<KeyValuePair<string, bool>>();
            pageView.setPlayersVM.playerIndexErrors = new List<KeyValuePair<string, bool>>();

            if (gameFromDB.IsHandicapped == "Y")
            {

 
                if (Request["AutoCalcHandicap"] == "Y")
                {
                    db.UpdateGameCalcHandicapFlagtoY(gameID);
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerIDString = Request["PlayerID" + i.ToString()];
                        playerNameString = Request["PlayerName" + i.ToString()];
                        playerIndexString = Request["PlayerIndex" + i.ToString()];
                        PlayerValidations pv = new PlayerValidations();
                        bool nameIsValid = pv.isNameValid(playerNameString);
                        bool playerIndexIsValid = pv.isIndexValid(playerIndexString);



                        if (nameIsValid && playerIndexIsValid)
                        {
                            Player player = new Player();
                            player.Name = playerNameString;
                            if (playerNameString == "")
                            {
                                player.PlayerIndex = null;
                            }
                            else
                            {
                                player.PlayerIndex = Convert.ToDecimal(playerIndexString);
                            }
                            player.GameID = gameID;
                            player.ID = Convert.ToInt32(playerIDString);
                            if (!db.UpdatePlayer(player))
                            {
                                return RedirectToAction("DBError", new { id = gameID });
                            }
                            pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + i.ToString(), !nameIsValid));
                            pageView.setPlayersVM.playerIndexErrors.Add(new KeyValuePair<string, bool>("PlayerIndex" + i.ToString(), !playerIndexIsValid));

                        }
                        else
                        {
                            pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + i.ToString(), !nameIsValid));
                            if (playerNameString == "")
                            {
                                pageView.setPlayersVM.playerIndexErrors.Add(new KeyValuePair<string, bool>("PlayerIndex" + i.ToString(), false));
                            }
                            else
                            {
                                pageView.setPlayersVM.playerIndexErrors.Add(new KeyValuePair<string, bool>("PlayerIndex" + i.ToString(), !playerIndexIsValid));
                            }
                            hasErrors = true;
                        }
                    }
                }
                else if (Request["AutoCalcHandicap"] == "N")
                {
                    db.UpdateGameCalcHandicapFlagtoN(gameID);
                    for (int i = 1; i <= numberOfPlayers; i++)
                    {
                        playerIDString = Request["PlayerID" + i.ToString()];
                        playerNameString = Request["PlayerName" + i.ToString()];
                        playerIndexString = Request["PlayerIndex" + i.ToString()];


                        PlayerValidations pv = new PlayerValidations();
                        bool nameIsValid = pv.isNameValid(playerNameString);
                        if (nameIsValid)
                        {
                            Player player = new Player();
                            player.ID = Convert.ToInt32(playerIDString);
                            player.Name = playerNameString;
                            player.PlayerIndex = null;
                            player.GameID = gameID;
                            if (!db.UpdatePlayer(player))
                            {
                                return RedirectToAction("DBError", new { id = gameID });
                            }
                            pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + i.ToString(), !nameIsValid));

                        }
                        else
                        {
                            pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + i.ToString(), !nameIsValid));
                            hasErrors = true;
                        }
                    }
                }
            }
            else if (gameFromDB.IsHandicapped == "N")
            {


                for (int i = 1; i <= numberOfPlayers; i++)
                {
                    playerIDString = Request["PlayerID" + i.ToString()];
                    playerNameString = Request["PlayerName" + i.ToString()];
                    PlayerValidations pv = new PlayerValidations();
                    bool nameIsValid = pv.isNameValid(playerNameString);


                    if (nameIsValid)
                    {
                        Player player = new Player();
                        player.Name = playerNameString;
                        player.GameID = gameID;
                        player.ID = Convert.ToInt32(playerIDString);
                        if (!db.UpdatePlayer(player))
                        {
                            return RedirectToAction("DBError", new { id = gameID });
                        }
                        pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + i.ToString(), !nameIsValid));
                    }
                    else
                    {
                        pageView.setPlayersVM.playerNameErrors.Add(new KeyValuePair<string, bool>("PlayerName" + i.ToString(), !nameIsValid));
                        hasErrors = true;
                    }
                }

            }

            //db.UpdatePlayerHandicaps(gameID);

            if (hasErrors)
            {
                Game game = db.GetGameRoundsAndPlayersByID(gameID);
                pageView.setPlayersVM.Game = game;
                pageView.setPlayersVM.Players = game.Players;
                pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);

                int pcount = 1;
                foreach (var p in pageView.setPlayersVM.Players)
                {
                    if (p.Name == "")
                    {
                        p.Name = Request["PlayerName" + pcount.ToString()];
                    }
                    pcount++;
                }
                return View(pageView);
            }

            if (Request["SubmitForm"] == "SaveAndStay")
            {
                return RedirectToAction("SetPlayers", new { id = gameID });
            }
            else if (Request["SubmitForm"] == "SaveAndDone")
            {
                Round firstRound = db.GetFirstRoundInGameByGameID(gameID);
                return RedirectToAction("SetRound", new { id = firstRound.ID }); 
            }
            return RedirectToAction("ViewHandicaps", new { id = gameID });
    }


        [HttpPost]
        public ActionResult SetFoursomes()
        {
            String roundIDString = Request["RoundID"]; // Get GameID
            int roundID = Convert.ToInt32(roundIDString); // Make it an iteger

            String gameIDString = Request["GameID"]; // Get GameID
            int gameID = Convert.ToInt32(gameIDString); // Make it an iteger

            int numberOfPlayers = Convert.ToInt32(Request["NumberOfPlayersOrTeams"]); // Get Number of Players


            String FoursomePlayerIDString;
            String RoundFoursomeIDString;
            CoordinatorLibrary cL = new CoordinatorLibrary();

            


            for (int i = 1; i <= numberOfPlayers; i++)
            {
                RoundFoursome roundFoursome = new RoundFoursome();
                FoursomePlayerIDString = Request["FoursomePlayerID" + i.ToString()];
                RoundFoursomeIDString = Request["RoundFoursomeID" + i.ToString()];


                if (!String.IsNullOrEmpty(FoursomePlayerIDString) && !String.IsNullOrEmpty(RoundFoursomeIDString))
                {
                    GameDetailsFormValidator formValidator = new GameDetailsFormValidator();
                    if (formValidator.isPlayerFoursomeValid(RoundFoursomeIDString))
                    {
                        int foursomePlayerId = Convert.ToInt32(FoursomePlayerIDString);
                        
                        int roundFoursomeId = Convert.ToInt32(RoundFoursomeIDString);


                        db.UpdateFoursomePlayerNumber(foursomePlayerId, roundFoursomeId);
                    }
                }
            }
                
                

                return RedirectToAction("SetFoursomes", new { id = roundID });
           
        }

        public ViewResult WebTest()
        {

            // ViewBag.CoordinatorID = new SelectList(db.Coordinators, "ID", "FirstName");
            ViewBag.GameTypes = lib.GameTypesSelectListItems(99);
            ViewBag.NumRounds = lib.NumberOfRoundsSelectListItems(99);
            ViewBag.IsHandicappedSelectItems = lib.IsHandicappedSelectListItems("");

            Game game = new Game();
            game.CoordinatorID = 1;

            return View(game);

        }

       


 

        public ViewResult SetRound(int id)
        {
            ViewBag.SectionTitle = "Course Details";
            // Declare the page view model
            SetRoundPageView pageViewModel = new SetRoundPageView();
            pageViewModel.leftNavVM = new LeftNavViewModel();
            pageViewModel.setRoundVM = new SetRoundViewModel();
            CoordinatorLibrary cl = new CoordinatorLibrary();

            // First set the round view model
            // Get round from DB and put in roundViewModel
            Round round = db.GetRoundAndHolesByID(id);

            // Build the RoundViewModel
            pageViewModel.setRoundVM.RoundViewModel = new RoundViewModel();
            pageViewModel.setRoundVM.RoundViewModel.ID = round.ID;
            pageViewModel.setRoundVM.RoundViewModel.RoundNumberInGame = round.RoundNumberInGame;
            pageViewModel.setRoundVM.RoundViewModel.Slope = round.Slope;
            pageViewModel.setRoundVM.RoundViewModel.SlopeErrorMessage = "";
            pageViewModel.setRoundVM.RoundViewModel.HolesInRound = round.HolesInRound;
            pageViewModel.setRoundVM.RoundViewModel.GameID = round.GameID;
            pageViewModel.setRoundVM.RoundViewModel.Front9Total = round.Front9Total;
            pageViewModel.setRoundVM.RoundViewModel.Back9Total = round.Back9Total;
            pageViewModel.setRoundVM.RoundViewModel.FullRoundTotal = round.FullRoundTotal;
            


            pageViewModel.setRoundVM.HoleViewModels = new List<HoleViewModel>();
            var holeViewModelList = new List<HoleViewModel>();
            foreach (var hole in round.Holes)
            {
                HoleViewModel holeViewModel = new HoleViewModel();
                holeViewModel.ID = hole.ID;
                holeViewModel.RoundID = hole.RoundID;
                // holeViewModel.ErrorMessage = "";
                holeViewModel.HoleNumber = hole.HoleNumber;
                if (hole.Par != null)
                {
                    holeViewModel.Par = hole.Par;
                }
                if (hole.Handicap != null)
                {
                    holeViewModel.Handicap = hole.Handicap;
                }
                
                if (round.HolesInRound == 18)
                {
                    holeViewModel.HoleHandicapList = cl.HandicapSelectItems18(hole.Handicap);
                }
                else if (round.HolesInRound == 9)
                {
                    holeViewModel.HoleHandicapList = cl.HandicapSelectItems9(hole.Handicap);
                }
                holeViewModel.ParList = cl.ParSelectItems(hole.Par);
                holeViewModelList.Add(holeViewModel);
            }
            pageViewModel.setRoundVM.HoleViewModels = holeViewModelList;
            
            // Set the Handicap drop down list
            var items = new List<SelectListItem>();
            // Add the drop down item for something not selected
            items.Add(new SelectListItem() { Text = "--", Value = "", Selected = false });

            for (int i = 1; i <= round.HolesInRound; i++)
            {
                items.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }

            pageViewModel.setRoundVM.HoleHandicapList = items;


            var selectItems = new List<SelectListItem>();
            // Add the drop down item for something not selected
            selectItems.Add(new SelectListItem() { Text = "--", Value = "", Selected = false });
            selectItems.Add(new SelectListItem() { Text = "3", Value = "3" });
            selectItems.Add(new SelectListItem() { Text = "4", Value = "4" });
            selectItems.Add(new SelectListItem() { Text = "5", Value = "5" });

            pageViewModel.setRoundVM.ParList = selectItems;


            Game gameFromDB = db.GetGameByID(round.GameID);
            pageViewModel.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(gameFromDB);
            

           
            return View(pageViewModel);
        }



        

        [HttpPost]
        public ActionResult SetRound()
        {

            int front9Total = 0;
            int back9Total = 0;
            int fullRoundTotal = 0;

            // Setup View Model
            SetRoundPageView pageView = new SetRoundPageView();
            pageView.leftNavVM = new LeftNavViewModel();
            pageView.setRoundVM = new SetRoundViewModel();
            pageView.setRoundVM.RoundViewModel = new RoundViewModel();

            Round roundToUpdate = new Round(); // Will hold submitted the holes coming from form
            
            RoundValidations roundValidation = new RoundValidations();
            CoordinatorLibrary cl = new CoordinatorLibrary();

            // SECTION - Get the slope from the form.
            // Validate the slope is between 55-155
            // roundToUpdate will hold the Round that will eventually be submitted to the DB.
            // RoundViewModel will hold the values in case they need to be sent back to the View
            int RoundID = Convert.ToInt32(Request["RoundID"]);
            int Slope;
            roundToUpdate = db.GetRoundByID(RoundID);

            // Validate Slope submitted is not null and it is between 55 - 155.
            if (roundValidation.isSlopeValid(Request["Slope"]))
            {
                Slope = Convert.ToInt32(Request["Slope"]);
                roundToUpdate.Slope = Slope;
                pageView.setRoundVM.RoundViewModel.Slope = roundToUpdate.Slope;
                pageView.setRoundVM.RoundViewModel.SlopeErrorMessage = "";
            }
            else
            {
                pageView.setRoundVM.RoundViewModel.SlopeErrorMessage = "Slope out of range.";
                pageView.setRoundVM.PageHasErrors = true;
                pageView.setRoundVM.RoundViewModel.Slope = cl.ExtractIntOrMake0(Request["Slope"]);
            }

            

            String parString;
            String handicapString;
            String holeIDString;

            //Hole hole = roundToUpdate.Holes.Where(h => h.ID == Convert.ToInt32(Request["HoleID" + i.ToString()])).SingleOrDefault();

            List<HoleViewModel> holesFromForm = new List<HoleViewModel>();
            int numberOfHoles = Convert.ToInt32(Request["HolesInRound"]);

            for (var i=1; i<=numberOfHoles; i++)
            {
                HoleViewModel holeViewModel = new HoleViewModel();
                holeViewModel.HoleNumber = i;
                parString = Request["ParForHole" + i.ToString()];
                handicapString = Request["HandicapForHole" + i.ToString()];
                holeIDString = Request["HoleID" + i.ToString()];

                if (roundValidation.isParValid(parString))
                {
                    holeViewModel.Par = Convert.ToInt32(parString);
                    holeViewModel.ParList = cl.ParSelectItems(holeViewModel.Par);
                    if (i <= 9)
                    {
                        if (!String.IsNullOrEmpty(parString))
                        {
                            front9Total += Convert.ToInt32(parString);
                            fullRoundTotal += Convert.ToInt32(parString);
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(parString))
                        {
                            back9Total += Convert.ToInt32(parString);
                            fullRoundTotal += Convert.ToInt32(parString);
                        }
                    }
                }
                else
                {
                    holeViewModel.ParErrorMessage = "Par missing or out of range.";
                    pageView.setRoundVM.PageHasErrors = true;
                    holeViewModel.ParList = cl.ParSelectItems(-99);
                }

                if (roundValidation.isHandicapValid(handicapString, numberOfHoles))
                {
                    holeViewModel.Handicap = Convert.ToInt32(handicapString);
                }
                else
                {
                    holeViewModel.HandicapErrorMessage = "Handicap missing or out of range.";
                    pageView.setRoundVM.PageHasErrors = true;
                }
                holeViewModel.ID = Convert.ToInt32(holeIDString);
                holeViewModel.RoundID = roundToUpdate.ID;

                if (numberOfHoles == 9)
                {
                    holeViewModel.HoleHandicapList = cl.HandicapSelectItems9(holeViewModel.Handicap);
                }
                else
                {
                    holeViewModel.HoleHandicapList = cl.HandicapSelectItems18(holeViewModel.Handicap);
                }


                holesFromForm.Add(holeViewModel);

            }

            roundToUpdate.Front9Total = front9Total;
            roundToUpdate.Back9Total = back9Total;
            roundToUpdate.FullRoundTotal = fullRoundTotal;

            

            pageView.setRoundVM.RoundViewModel.Front9Total = front9Total;
            pageView.setRoundVM.RoundViewModel.Back9Total = back9Total;
            pageView.setRoundVM.RoundViewModel.FullRoundTotal = fullRoundTotal;
            pageView.setRoundVM.RoundViewModel.ID = roundToUpdate.ID;
            pageView.setRoundVM.RoundViewModel.RoundNumberInGame = roundToUpdate.RoundNumberInGame;

            pageView.setRoundVM.RoundViewModel.HolesInRound = roundToUpdate.HolesInRound;
            pageView.setRoundVM.RoundViewModel.GameID = roundToUpdate.GameID;
            

            if (!db.UpdateRound(roundToUpdate))
            {
                return RedirectToAction("DBError", new { id = roundToUpdate.GameID });
            }
            

            // check for duplicate handicaps
            foreach (var hole in holesFromForm)
            {
                
                foreach (var iHole in holesFromForm)
                {
                    if (hole.Handicap == iHole.Handicap && hole.HoleNumber != iHole.HoleNumber)
                    {
                        hole.HandicapErrorMessage = "Duplicate handicap with hole " + iHole.HoleNumber.ToString();
                        pageView.setRoundVM.PageHasErrors = true;
                    }
                }
            }

            foreach (var holeVM in holesFromForm)
            {
                if (holeVM.HandicapErrorMessage == "")
                {
                    Hole holeMinizedFromViewModel = new Hole();
                    holeMinizedFromViewModel.ID = holeVM.ID;
                    holeMinizedFromViewModel.Handicap = holeVM.Handicap;
                    holeMinizedFromViewModel.HoleNumber = holeVM.HoleNumber;
                    holeMinizedFromViewModel.RoundID = holeVM.RoundID;
                    holeMinizedFromViewModel.Par = holeVM.Par;
                    if (!db.UpdateHole(holeMinizedFromViewModel))
                    {
                        return RedirectToAction("DBError", new { id = roundToUpdate.GameID });
                    }
                }

            }

            pageView.setRoundVM.RoundViewModel.Front9Total = front9Total;
            pageView.setRoundVM.RoundViewModel.Back9Total = back9Total;
            pageView.setRoundVM.RoundViewModel.FullRoundTotal = fullRoundTotal;
            pageView.setRoundVM.HoleViewModels = holesFromForm;
            
             


            if (pageView.setRoundVM.PageHasErrors == true)
            {
                Game gameFromDB = db.GetGameByID(roundToUpdate.GameID);
                pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(gameFromDB);
                return View(pageView);
            }
            else
            {
                return RedirectToAction("ViewHandicaps", new { id = roundToUpdate.ID });
            }

          
           

        }



        public ActionResult ChangeRoundTo9Holes(int id)
        {

            if (db.UpdateRoundTo9Holes(id))
            {
                return RedirectToAction("SetRound", new { id = id });
            }
            else
            {
                return RedirectToAction("DBError");
            } 
            

        }

        public ActionResult ChangeRoundTo18Holes(int id)
        {

            if (db.UpdateRoundTo18Holes(id))
            {
                return RedirectToAction("SetRound", new { id = id });
            }
            else
            {
                return RedirectToAction("DBError");
            }

        }

        public ActionResult ChangeRoundPlayerHandicapToAutomatic(int id)
        {
            RoundPlayerHandicap roundPlayerHandicap = db.GetRoundPlayerHandicapByID(id);
            roundPlayerHandicap.CalcHandicapFromIndex = "Y";
            if (db.UpdateRoundPlayerHandicap(roundPlayerHandicap))
            {
                return RedirectToAction("ViewHandicaps", new { id = roundPlayerHandicap.RoundID });
            }
            else
            {
                return RedirectToAction("DBError");
            }
        }

        

        public ActionResult ChangeSetHandicapsConfirmation(int id) // Round ID
        {
            ViewBag.SectionTitle = "Change to Manual Handicap Entry Confirmation";
            CoordinatorLibrary cl = new CoordinatorLibrary();
            ChangeSetHandicapsConfirmationPageView pageView = new ChangeSetHandicapsConfirmationPageView();
            pageView.changeSetHandicapsVM = new ChangeSetHandicapsConfirmationViewModel();
            pageView.leftNavVM = new LeftNavViewModel();

            Game game = db.GetGameByRoundID(id);

            pageView.changeSetHandicapsVM.game = game;
            pageView.changeSetHandicapsVM.RoundID = id;
            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);

            return View(pageView);
           
        }

        [HttpPost]
        public ActionResult ChangeSetHandicapsConfirmed()
        {
            int roundId = Convert.ToInt32(Request["RoundID"]);
            if (Request["SubmitForm"] == "Cancel")
            {
                return RedirectToAction("ViewHandicaps", new { id = roundId });
            }

            if (db.UpdateGameCalcHandicapFlagtoN(Convert.ToInt32(Request["GameID"])))
            {
                return RedirectToAction("SetHandicaps", new { id = roundId });
            }
            else
            {
                return RedirectToAction("DBError");
            }
                



          


        }

        public ActionResult ChangeHandicapsToAutoCalcConfirmation(int id) // Round ID
        {
            ViewBag.SectionTitle = "Change to Automatic Handicap Calculation Confirmation";
            CoordinatorLibrary cl = new CoordinatorLibrary();
            ChangeHandicapsToAutoCalcConfirmationPageView pageView = new ChangeHandicapsToAutoCalcConfirmationPageView();
            pageView.changeHandicapsToAutoCalcVM = new ChangeHandicapsToAutoCalcConfirmationViewModel();
            pageView.leftNavVM = new LeftNavViewModel();

            Game game = db.GetGameByRoundID(id);

            pageView.changeHandicapsToAutoCalcVM.game = game;
            pageView.changeHandicapsToAutoCalcVM.RoundID = id;
            pageView.leftNavVM.leftNavItems = cl.GenerateLeftNavItems(game);

            return View(pageView);

        }

        [HttpPost]
        public ActionResult ChangeHandicapsToAutoCalcConfirmation()
        {
            int roundId = Convert.ToInt32(Request["RoundID"]);
            if (Request["SubmitForm"] == "Cancel")
            {
                return RedirectToAction("ViewHandicaps", new { id = roundId });
            }

            if (db.UpdateGameCalcHandicapFlagtoY(Convert.ToInt32(Request["GameID"])))
            {
                return RedirectToAction("ViewHandicaps", new { id = roundId });
            }
            else
            {
                return RedirectToAction("DBError");
            }


        }

        public ViewResult ChangeHoles(int id)
        {
            Round round = db.GetRoundByID(id);
            return View(round);
        }

        [HttpPost]
        public ActionResult ChangeHoles()
        {
            int roundID = Convert.ToInt32(Request["RoundID"]);

            // If user wants to switch to 9 holes
            if (Request["SubmitForm"] == "Switch To 9")
            {
                if (db.DeleteBackNine(roundID))
                {
                    // Delete holes 10-18 and redirect to Set Round
                    return RedirectToAction("SetRound", new { id = roundID });
                }
                else
                {
                    return RedirectToAction("DBError");
                } 
            }
            else if (Request["SubmitForm"] == "Switch To 18")
            {
                // Add holes 10-18 and redirect to Set Round
                if (db.AddBackNine(roundID))
                {
                    return RedirectToAction("SetRound", new { id = roundID });
                }
                else
                {
                    return RedirectToAction("DBError");
                } 
            }
            else if (Request["SubmitForm"] == "Never Mind")
            {
                // Add holes 10-18 and redirect to Set Round
                return RedirectToAction("SetRound", new { id = roundID });
            }
            Round round = db.GetRoundByID(roundID);
            return View(round);
        }


        public ActionResult StartRound(int id)
        {
            // Check Game for Errors
            CoordinatorLibrary cl = new CoordinatorLibrary();
            Round round = db.GetRoundByID(id);
            Game game = db.GetGameByID(round.GameID);
            List<GameValidationError> errorList = db.IsGameReady(game, round);

            // If there are errors, return errors to the StartRound View
            if (errorList.Count > 0)
            {
                StartRoundPageView pageViewModel = new StartRoundPageView();
                pageViewModel.leftNavViewModel = new LeftNavViewModel();
                pageViewModel.errorList = new List<GameValidationError>();
                pageViewModel.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);
                pageViewModel.gameId = game.ID;
                pageViewModel.errorList = errorList;            
                return View(pageViewModel);
            }
            else
            {
                StartRoundConfirmationPageView confirmationPageView = new StartRoundConfirmationPageView();
                confirmationPageView.leftNavViewModel = new LeftNavViewModel();
                confirmationPageView.warningList = new List<GameValidationWarning>();
                confirmationPageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);
                confirmationPageView.Game = game;
                confirmationPageView.Round = round;
                confirmationPageView.warningList = db.DoesGameHaveWarnings(game, round);
                return View("StartRoundConfirmation", confirmationPageView);
                
            }

        }

        [HttpPost]
        public ActionResult StartRoundConfirmed()
        {
            int roundID = Convert.ToInt32(Request["RoundID"]);
            // Delete empty foursomes
            // *********************************************************************
            if (!db.DeleteEmptyFoursomesFromRound(roundID))
            {
                return RedirectToAction("DBError");
            }


            // Set Game Leaderboard but only if it the first round in the game
            Round round = db.GetRoundByID(roundID);
            Game game = db.GetGameByRoundID(roundID);
            if (round.RoundNumberInGame == 1)
            {
                if (!db.StartGame(game, round))
                {
                    return RedirectToAction("DBError");
                }
            }

            if (!db.StartRound(game,round))
            {
                return RedirectToAction("DBError");
            }

            return RedirectToAction("RoundLive", new { id = round.ID });
        }

        public ActionResult RoundLive(int id) // round id
        {
            int roundId = id;
            CoordinatorLibrary cl = new CoordinatorLibrary();
            Game game = db.GetGameByRoundID(roundId);
            RoundLivePageView pageView = new RoundLivePageView();
            pageView.leftNavViewModel = new LeftNavViewModel();
            pageView.roundLiveViewModel = new RoundLiveViewModel();
            pageView.leftNavViewModel.leftNavItems = cl.GenerateLeftNavItems(game);
            pageView.roundLiveViewModel.Game = game;
            pageView.roundLiveViewModel.Round = db.GetRoundByID(roundId);

             

            return View(pageView);
        }

    }
}