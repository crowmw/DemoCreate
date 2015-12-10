using System;
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
using Reository.Models;
using PagedList;
using Repository.Models.ChartModels;

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

        public ActionResult Index(int? page)
        {
            IEnumerable<Questionnaire> questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId != Guid.Empty).OrderByDescending(x=>x.TimeOfCreation);
            if(User.Identity.GetUserId() != null)
            {
                int pageSize = 5;
                int pageNumber = (page ?? 1);
                ViewBag.CurrentUserId = User.Identity.GetUserId().ToString();
                return View(questionnaire.ToPagedList(pageNumber, pageSize));
            }
            return View("../Home/Index", questionnaire);
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

            var suma = questionnaire.Vote1.VotedUsers.Count + questionnaire.Vote2.VotedUsers.Count;
            var vote1 = (decimal)questionnaire.Vote1.VotedUsers.Count / (decimal)suma * 100;
            var vote2 = (decimal)questionnaire.Vote2.VotedUsers.Count / (decimal)suma * 100;

            ViewBag.Vote1Procentage = questionnaire.Vote1.VotedUsers.Count + " ( " + decimal.Round(vote1, 1) + "% )";
            ViewBag.Vote2Procentage = questionnaire.Vote2.VotedUsers.Count + " ( " + decimal.Round(vote2, 1) + "% )";
            return View(questionnaire);
        }

        [HttpPost]
        public ActionResult Vote(string id)
        {
            try
            {
                Guid voteId = Guid.Empty;
                Guid.TryParse(id, out voteId);
                var vote = db.Vote.Find(voteId);
                var loggedUserId = User.Identity.GetUserId().ToString();
                var loggedUser = db.User.Where(x => x.Id == loggedUserId)
                                            .FirstOrDefault();

                vote.VotedUsers.Add(new Reository.Models.Choose
                {
                    ChooseId = Guid.NewGuid(),
                    vote = vote,
                    VoteId = vote.VoteId,
                    user = loggedUser,
                    UserId = loggedUserId
                });
                db.SaveChanges();

                return Json(new { success = true, voteId = vote.VoteId, questionnaireId = vote.QuestionnaireId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Nie udało się oddać głosu. \nERRORINFO: " + ex.Message });
            }
        }

        public ActionResult Create()
        {
            return View();
        }

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
                    var tbottom = (img.Height - (int)(top * res) - (int)(height * res));
                    var tright = (img.Width - (int)(left * res) - (int)(width * res));
                    img.Crop(ttop, tleft, tbottom, tright);
                    img.Resize(AvatarStoredWidth, AvatarStoredHeight);
                    img.Save(filePath);
                }
                return Json(new { success = true, fileGuid = fileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Nie udało się wrzucić zdjęcia. \nERRORINFO: " + ex.Message });
            }
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


            var suma = questionnaire.Vote1.VotedUsers.Count + questionnaire.Vote2.VotedUsers.Count;
            var vote1 = (decimal)questionnaire.Vote1.VotedUsers.Count / (decimal)suma * 100;
            var vote2 = (decimal)questionnaire.Vote2.VotedUsers.Count / (decimal)suma * 100;

            ViewBag.Vote1Procentage = questionnaire.Vote1.VotedUsers.Count + " ( " + decimal.Round(vote1, 1) + "% )";
            ViewBag.Vote2Procentage = questionnaire.Vote2.VotedUsers.Count + " ( " + decimal.Round(vote2, 1) + "% )";
            return View(questionnaire);
        }

        public ActionResult DeleteConfirmed(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Find(id);
            Vote vote1 = db.Vote.Find(questionnaire.Vote1Id);
            Vote vote2 = db.Vote.Find(questionnaire.Vote2Id);
            IEnumerable<Choose> vote1Chooses = db.Choose.Where(x => x.VoteId == vote1.VoteId);
            IEnumerable<Choose> vote2Chooses = db.Choose.Where(x => x.VoteId == vote2.VoteId);
            foreach (var vote in vote1Chooses)
            {
                db.Choose.Remove(vote);
            }

            foreach (var vote in vote2Chooses)
            {
                db.Choose.Remove(vote);
            }

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

        #region AllCharts
        public ActionResult ChartAllGender(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        public ActionResult ChartAllProvince(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        public ActionResult ChartAllAge(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        public ActionResult ChartAllEducation(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }
        #endregion

        #region Vote1Charts
        public ActionResult ChartVote1Gender(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetGenderVote1Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            List<GenderVotes> gv = new List<GenderVotes>();
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            gv.Add(new GenderVotes() { Gender = "Kobiety", Votes = questionnaire.Vote1.VotedUsers.Where(x=>x.user.Gender == "female").Count() });
            gv.Add(new GenderVotes() { Gender = "Mężczyźni", Votes = questionnaire.Vote1.VotedUsers.Where(x => x.user.Gender == "male").Count() });

            return Json(gv, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartVote1Province(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetProvinceVote1Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            List<ProvinceVotes> pv = new List<ProvinceVotes>();
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var name in db.Provinces)
            {
                dict.Add(name.ProvinceName, 0);
            }

            var vote1 = questionnaire.Vote1;

            foreach (var v in vote1.VotedUsers)
            {
                dict[v.user.Province.ProvinceName]++;
            }

            foreach (KeyValuePair<string, int> pair in dict)
            {
                pv.Add(new ProvinceVotes() { ProvinceName = pair.Key, Votes = pair.Value });
            }

            return Json(pv, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartVote1Age(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetAgeVote1Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            List<AgeVotes> av = new List<AgeVotes>();

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var name in db.AgeRange)
            {
                dict.Add(name.AgeRangeName, 0);
            }

            var vote1 = questionnaire.Vote1;

            foreach (var v in vote1.VotedUsers)
            {
                dict[v.user.AgeRange.AgeRangeName]++;
            }

            foreach (KeyValuePair<string, int> pair in dict)
            {
                av.Add(new AgeVotes() { AgeRange = pair.Key, Votes = pair.Value });
            }

            return Json(av, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartVote1Education(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetEducationVote1Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            List<EducationVotes> ev = new List<EducationVotes>();

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var name in db.Education)
            {
                dict.Add(name.EducationName, 0);
            }

            var vote1 = questionnaire.Vote1;

            foreach (var v in vote1.VotedUsers)
            {
                dict[v.user.Education.EducationName]++;
            }

            foreach (KeyValuePair<string, int> pair in dict)
            {
                ev.Add(new EducationVotes() { EducationName = pair.Key, Votes = pair.Value });
            }

            return Json(ev, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Vote2Charts
        public ActionResult ChartVote2Age(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetAgeVote2Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            List<AgeVotes> av = new List<AgeVotes>();

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var name in db.AgeRange)
            {
                dict.Add(name.AgeRangeName, 0);
            }

            var vote2 = questionnaire.Vote2;

            foreach (var v in vote2.VotedUsers)
            {
                dict[v.user.AgeRange.AgeRangeName]++;
            }

            foreach (KeyValuePair<string, int> pair in dict)
            {
                av.Add(new AgeVotes() { AgeRange = pair.Key, Votes = pair.Value });
            }

            return Json(av, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartVote2Education(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetEducationVote2Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            List<EducationVotes> ev = new List<EducationVotes>();

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var name in db.Education)
            {
                dict.Add(name.EducationName, 0);
            }

            var vote2 = questionnaire.Vote2;

            foreach (var v in vote2.VotedUsers)
            {
                dict[v.user.Education.EducationName]++;
            }

            foreach (KeyValuePair<string, int> pair in dict)
            {
                ev.Add(new EducationVotes() { EducationName = pair.Key, Votes = pair.Value });
            }

            return Json(ev, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ChartVote2Province(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetProvinceVote2Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            List<ProvinceVotes> pv = new List<ProvinceVotes>();
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (var name in db.Provinces)
            {
                dict.Add(name.ProvinceName, 0);
            }

            var vote2 = questionnaire.Vote2;

            foreach (var v in vote2.VotedUsers)
            {
                dict[v.user.Province.ProvinceName]++;
            }

            foreach (KeyValuePair<string, int> pair in dict)
            {
                pv.Add(new ProvinceVotes() { ProvinceName = pair.Key, Votes = pair.Value });
            }

            return Json(pv, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartVote2Gender(Guid id)
        {
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == id).FirstOrDefault();
            return View(questionnaire);
        }

        [HttpGet]
        public ActionResult GetGenderVote2Data(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            List<GenderVotes> gv = new List<GenderVotes>();
            Questionnaire questionnaire = db.Questionnaire.Where(x => x.QuestionnaireId == guid).FirstOrDefault();
            gv.Add(new GenderVotes() { Gender = "Kobiety", Votes = questionnaire.Vote2.VotedUsers.Where(x => x.user.Gender == "female").Count() });
            gv.Add(new GenderVotes() { Gender = "Mężczyźni", Votes = questionnaire.Vote2.VotedUsers.Where(x => x.user.Gender == "male").Count() });

            return Json(gv, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

