using BibleVerse.Models;
using BibleVerse.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using BibleVerse.Services.Utility;
using System.Web.Script.Serialization;

namespace BibleVerse.Controllers
{
    public class HomeController : Controller
    {
        private readonly static MyLogger logger = MyLogger.GetInstance();
        public static BibleDatabase bible = new BibleDatabase();
        public static BibleModel verse;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BibleForm()
        {
            verse = new BibleModel();
            return View("BibleForm");
        }
        public ActionResult FindVerse()
        {
            verse = new BibleModel();
            return View("FindVerse");
        }
        [HttpPost]
        public ActionResult EnterScripture(BibleModel _verse)
        {
            if (ModelState.IsValid)
            { //checking model state
                BibleModel form = new BibleModel(_verse.Testament, _verse.Book, _verse.Chapter, _verse.Verse, _verse.Text);
                bible.AddScriptureToDatabase(form);
                logger.ILoggerInfo("Scripture Added:" + _verse.Testament + " " + _verse.Book + " " + _verse.Chapter + " " + _verse.Verse + " "
                     + _verse.Text);
                return RedirectToAction("Index");
            }
            else
            {
                logger.Error("User gave invalid Entry");
            }
            return View("BibleForm", _verse);
        }
        [HttpPost]
        public ActionResult RetrieveScripture(BibleModel _verse)
        {
            BibleModel form = new BibleModel(_verse.Testament, _verse.Book, _verse.Chapter, _verse.Verse);;
            string view = "FindVerse";
            if (ModelState.IsValid)
            { //checking model state
                
                string found = bible.FindScripture(form);
                if (found != "")
                {
                    form.Text = found;
                    logger.ILoggerInfo("Scripture Found:" + _verse.Testament + " " + _verse.Book + " " + _verse.Chapter);
                    view = "Found";
                }
                else
                {
                    logger.ILoggerInfo("Scripture:" + _verse.Testament + " " + _verse.Book + " " + _verse.Chapter + " is not in the database.");
                    view = "NotFound";
                }
            }
            else
            {
                logger.Error("User gave invalid Entry");
            }
            return View(view, form);



        }

    }
}