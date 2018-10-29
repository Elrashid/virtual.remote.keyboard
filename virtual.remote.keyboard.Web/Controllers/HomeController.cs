using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using remote.keyboard.Web.Models;

namespace remote.keyboard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DirectoryInfo dinfo = new DirectoryInfo(@".\data\");
            if (!dinfo.Exists)
            {
                try
                {
                    dinfo.Create();
                    var exampleFile = @"example dotnet publish.txt";
                    var fromPath = @".\" + exampleFile;
                    var toPath = @".\data\" + exampleFile;
                    System.IO.File.WriteAllText(toPath, System.IO.File.ReadAllText(fromPath));

                }
                catch (Exception ex)
                {
                }

            }

            FileInfo[] files = dinfo.GetFiles("*.txt");
            var notes = new List<Note>();
            foreach (FileInfo file in files)
            {
                notes.Add(new Note() { Name = file.Name, Text = file.OpenText().ReadLine() });
            }

            return View(notes);
        }

        // GET: codes/Details/5
        public ActionResult Details(string fileName)
        {

            var path = @".\data\" + fileName;

            return View(new Note() { Name = fileName, Text = System.IO.File.ReadAllText(path) });
        }


        public ActionResult processSelectLislPart()
        {

            Process[] processlist = Process.GetProcesses();

            var processSelectList = new List<SelectListItem>();


            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    var txt = string.Format("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                    processSelectList.Add(new SelectListItem { Value = process.Id.ToString(), Text = txt });
                }

            }

            ViewData["processSelectList"] = processSelectList;

            return PartialView();
        }


        [HttpPost]
        public string write(String txt, int? prossid)
        {
            if (prossid.HasValue) remote.keyboard.Web.Core.all.select(prossid.Value);
            txt = System.Net.WebUtility.HtmlDecode(txt);
            remote.keyboard.Web.Core.all.typing(txt);
            return "ok";
        }

        // GET: codes/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            try
            {

                var path = @".\data\" + note.Name + ".txt";
                System.IO.File.WriteAllText(path, note.Text);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



    }

    public class Note
    {
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

    }
}

