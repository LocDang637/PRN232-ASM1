using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;
using SmokeQuit.Repositories.LocDPX.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace SmokeQuit.MVCWebApp.FE.LocDPX.Controllers
{
    [Authorize]
    public class CoachesLocDpxController : Controller
    {
        private string APIEndPoint = "https://localhost:7260/api/";

        // GET: CoachesLocDpx - Return view only, data will be loaded via AJAX
        public IActionResult Index()
        {
            return View();
        }

        // GET: CoachesLocDpx/Create - Return view only
        public IActionResult Create()
        {
            return View();
        }

        // GET: CoachesLocDpx/Edit/5 - Return view only, data will be loaded via AJAX
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            ViewBag.CoachId = id;
            return View();
        }

        // GET: CoachesLocDpx/Delete/5 - Return view only, data will be loaded via AJAX
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            ViewBag.CoachId = id;
            return View();
        }

        // GET: CoachesLocDpx/Details/5 - Return view only, data will be loaded via AJAX
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            ViewBag.CoachId = id;
            return View();
        }

        // Public views remain unchanged for anonymous access
        [AllowAnonymous]
        public async Task<IActionResult> PublicIndex(string? fullName, string? email, int? pageNumber)
        {
            var search = new SearchCoachRequest
            {
                FullName = fullName ?? "",
                Email = email ?? "",
                CurrentPage = pageNumber ?? 1,
                PageSize = 10
            };

            using (var httpClient = new HttpClient())
            {
                // No token needed for public access
                using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "CoachLocDpx/Search", search))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PaginationResult<List<CoachesLocDpx>>>(content);

                        if (result != null)
                        {
                            var pagedList = new StaticPagedList<CoachesLocDpx>(
                                result.Items,
                                result.CurrentPage,
                                result.PageSize,
                                result.TotalItems
                            );
                            return View(pagedList);
                        }
                    }
                }
            }

            return View(new List<CoachesLocDpx>().ToPagedList(search.CurrentPage, search.PageSize));
        }

        // Public Details - No authentication required  
        [AllowAnonymous]
        public async Task<IActionResult> PublicDetails(int? id)
        {
            if (id == null) return NotFound();

            CoachesLocDpx coach = null;

            using (var httpClient = new HttpClient())
            {
                // No token needed for public access
                var response = await httpClient.GetAsync(APIEndPoint + "CoachLocDpx/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    coach = JsonConvert.DeserializeObject<CoachesLocDpx>(content);
                }
            }

            if (coach == null) return NotFound();
            return View(coach);
        }
    }
}