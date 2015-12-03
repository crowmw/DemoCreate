﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web.Helpers;
using Reository.Models.DAL;
using System.Text.RegularExpressions;
using System.Drawing;

namespace DemoCreate.Controllers
{
    public class QuestionnaireController : Controller
    {
        private DCContext db = new DCContext();

        private const int AvatarStoredWidth = 400;  // ToDo - Change the size of the stored avatar image
        private const int AvatarStoredHeight = 400; // ToDo - Change the size of the stored avatar image
        private const int AvatarScreenWidth = 400;  // ToDo - Change the value of the width of the image on the screen

        private const string TempFolder = "/Temp";
        private const string MapTempFolder = "~" + TempFolder;
        private const string UploadedImagesPath = "/UploadImages";

        private readonly string[] _imageFileExtensions = { ".jpg", ".png", ".gif", ".jpeg" };

        public ActionResult Index()
        {
            IEnumerable<Questionnaire> questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId != Guid.Empty).OrderByDescending(x=>x.TimeOfCreation);
            return View(questionnaire);
        }


        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto(string cakesImage, HttpPostedFileBase photo)
        {
            string path = "~/Uploads" + cakesImage;

            if (photo != null)
                photo.SaveAs(path);

            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeletePhoto(string photoFileName)
        //{
        //    //Session["DeleteSuccess"] = "No";
        //    var photoName = "";
        //    photoName = photoFileName;
        //    string fullPath = Request.MapPath("~/Images/Cakes/" + photoName);

        //    if (System.IO.File.Exists(fullPath))
        //    {
        //        System.IO.File.Delete(fullPath);
        //        //Session["DeleteSuccess"] = "Yes";
        //    }
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Questionnaire questionnaire)
        {
            var loggedUser = UserDAL.GetUserByID(User.Identity.GetUserId().ToString());
            questionnaire.TimeOfCreation = DateTime.Now;
            questionnaire.UserId = loggedUser.Id;

            if (ModelState.IsValid)
            {
                QuestionnaireDAL.Add(questionnaire);
                return RedirectToAction("Index");
            }

            return View(questionnaire);
        }

        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionnaire questionnaire = QuestionnaireDAL.GetQuestionnaireById(id);
            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionnaireId,Title,TimeOfCreation")] Questionnaire questionnaire)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(questionnaire).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionnaire);
        }

        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Questionnaire questionnaire = db.Questionnaire.Find(id);

            if (questionnaire == null)
            {
                return HttpNotFound();
            }
            return View(questionnaire);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Find(id);
            Vote vote1 = db.Vote.Find(questionnaire.Vote1Id);
            Vote vote2 = db.Vote.Find(questionnaire.Vote2Id);
            db.Vote.Remove(vote1);
            db.Vote.Remove(vote2);
            db.Questionnaire.Remove(questionnaire);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (DCContext db = new DCContext())
                { db.Dispose(); }
            }
            base.Dispose(disposing);
        }

        public ActionResult UploadImage()
        {
            return RedirectToAction(actionName: "_UploadImage",
                controllerName: "UploadImage");
        }

        [HttpPost]
        public ActionResult Save(string t, string l, string h, string w, string o, string fileData)
        {
            try
            {
                // Calculate dimensions
                var top = Convert.ToInt32(t.Replace("-", "").Replace("px", ""));
                var left = Convert.ToInt32(l.Replace("-", "").Replace("px", ""));

                var height = Convert.ToInt32(h.Replace("-", "").Replace("px", ""));
                var width = Convert.ToInt32(w.Replace("-", "").Replace("px", ""));

                var originHeight = Convert.ToInt32(o.Replace("px", ""));

                var base64Data = Regex.Match(fileData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                byte[] binData = Convert.FromBase64String(base64Data);
                var type = base64Data.GetType();
                //var stream = new MemoryStream(binData);

                // Generate unique file name
                var fileName = Guid.NewGuid().ToString();
                var serverPath = HttpContext.Server.MapPath(UploadedImagesPath);
                var filePath = Path.Combine(serverPath, fileName);

                using (var ms = new MemoryStream(binData))
                {
                    var img = new WebImage(binData);
                    var res = (double)img.Height / (double)originHeight;

                    var ttop = (int)(res * top);
                    var tleft = (int)(res * left);
                    var tbottom = (img.Height - (int)(top* res)-(int)(height* res));
                    var tright = (img.Width - (int)(left* res) - (int)(width * res));
                    img.Crop(ttop, tleft, tbottom, tright);
                    img.Resize(AvatarStoredWidth, AvatarStoredHeight);
                    img.Save(filePath);
                }
                return Json(new { success = true, fileGuid = fileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Unable to upload file.\nERRORINFO: " + ex.Message });
            }
        }
    }
}

