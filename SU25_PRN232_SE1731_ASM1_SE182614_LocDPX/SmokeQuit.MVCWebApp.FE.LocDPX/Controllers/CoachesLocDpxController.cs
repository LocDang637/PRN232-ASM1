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

        public async Task<IActionResult> Index(string? fullName, string? email, int? pageNumber)
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
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

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

        // GET: CoachesLocDpx/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoachesLocDpx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoachesLocDpx coach)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "CoachLocDpx", coach))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                        {
                            ModelState.AddModelError("Email", "A coach with this email already exists.");
                        }
                    }
                }
            }

            return View(coach);
        }

        // Additional CRUD methods...
    }
}