using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadYourHeartOut.Data;
using ReadYourHeartOut.Models.Profiles;
using System.Web;
using ReadYourHeartOut.Utilities;
using System.Net.Http;
using ReadYourHeartOut.Models;

namespace ReadYourHeartOut.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            if (_context.Users.Count() == 0)
            {
                _context.AddRange(CompareAdd());
                _context.SaveChanges();
            }
        }

        public List<User> CompareAdd()
        {
            List<User> UsersToReturn = new List<User>();
            GetUserDataFromAPI getUserDataFromAPI = new GetUserDataFromAPI();
            foreach (User user in getUserDataFromAPI.GetUserData())
            {
            if (!_context.Users.ToList().Contains(user))
                {
                    UsersToReturn.Add(user);
                }

            }
            return UsersToReturn;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }



        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(i => i.ServicesAssignment)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserName,Email,JoinDate")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,  byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users.Include(i => i.ServicesAssignment).FirstOrDefaultAsync(m => m.UserID == id);

            if (userToUpdate == null)
            {
                User deletedUser = new User();
                await TryUpdateModelAsync(deletedUser);
                ModelState.AddModelError(string.Empty, "Unable to save changes. The user was deleted by another user.");
                ViewData["ServiceID"] = new SelectList(_context.Services, "ID", "Name", deletedUser.UserID);
                return View(deletedUser);
            }

            _context.Entry(userToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<User>(userToUpdate, "", s => s.UserName, s => s.Email))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (User)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Fuck you");

                    }
                    else
                    {
                        var databaseValues = (User)databaseEntry.ToObject();

                        if (databaseValues.UserName != clientValues.UserName)
                        {
                            ModelState.AddModelError("UserName", $"Current value: {databaseValues.UserName}");
                        }
                        if (databaseValues.Email != clientValues.Email)
                        {
                            ModelState.AddModelError("Email", $"Current value: {databaseValues.Email:c}");
                        }
                        if (databaseValues.JoinDate != clientValues.JoinDate)
                        {
                            ModelState.AddModelError("JoinDate", $"Current value: {databaseValues.JoinDate:d}");
                        }
                        

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        userToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            return View(userToUpdate);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

     






        //Trial and error
        //public async Task<UserIndexData> GetData(string actionRequired)
        //{
        //    string url = APIController.apiClient.BaseAddress + actionRequired;

        //    using (HttpResponseMessage response = await APIController.apiClient.GetAsync(url))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            UserIndexData recievedData = await response.Content.ReadAsAsync<UserIndexData>();
        //            return recievedData;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }

        //}
    }
}
