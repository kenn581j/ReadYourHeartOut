using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReadYourHeartOut.Data;
using ReadYourHeartOut.Models.Profiles;

namespace ReadYourHeartOut.Controllers
{
    public class ServicesController : Controller
    {
        private readonly UserContext _context;

        public ServicesController(UserContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            string url = String.Format("https://localhost:44382/api/Services");
            WebRequest requestObject = WebRequest.Create(url);
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();

            string recievedData = null;
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                recievedData = streamReader.ReadToEnd();
            }

            var jSonObject = JsonConvert.DeserializeObject<Service>(recievedData);
            // ikke helt korrekt, tjekker ikke for overenstemmelser
            if (_context.Services.Count() == 0)
            {
                _context.Services.AddRange(jSonObject);
            }

           return View(await _context.Services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,ServiceName,Cost,RowVersion")] Service service)
        {
            string url = String.Format("https://localhost:44382/api/Services");
            WebRequest requestPostObject = WebRequest.Create(url);
            requestPostObject.ContentType ="application/json";

            using (StreamWriter streamWriter = new StreamWriter(requestPostObject.GetRequestStream()))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(service);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(service);



                    //HttpWebResponse httpResponse = (HttpWebResponse)requestPostObject.GetResponse();

                    //using (Stream stream = httpResponse.GetResponseStream())
                    //{
                    //    StreamReader streamReader = new StreamReader(stream);
                    //}

                    //if (ModelState.IsValid)
                    //{  
                    //    _context.Add(service);
                    //    await _context.SaveChangesAsync();
                    //    return RedirectToAction(nameof(Index));
                    //}
                    //return View(service);
                }
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceID,ServiceName,Cost,RowVersion")] Service service)
        {
            if (id != service.ServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceID == id);
        }
    }
}
