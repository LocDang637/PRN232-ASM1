using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;
using SmokeQuit.Repositories.LocDPX.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace SmokeQuit.MVCWebApp.FE.LocDPX.Controllers
{
    [Authorize]
    public class ChatsLocDpxController : Controller
    {
        private string APIEndPoint = "https://localhost:7260/api/";

        public async Task<IActionResult> Index(string? message, string? messageType, string? sentBy, int? pageNumber)
        {
            var search = new SearchChatRequest
            {
                Message = message ?? "",
                MessageType = messageType ?? "",
                SentBy = sentBy ?? "",
                CurrentPage = pageNumber ?? 1,
                PageSize = 10
            };

            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "ChatsLocDpx/Search", search))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<PaginationResult<List<ChatsLocDpx>>>(content);

                        if (result != null)
                        {
                            var pagedList = new StaticPagedList<ChatsLocDpx>(
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

            return View(new List<ChatsLocDpx>().ToPagedList(search.CurrentPage, search.PageSize));
        }

        // GET: ChatsLocDpx/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CoachId"] = new SelectList(await GetCoaches(), "CoachesLocDpxid", "FullName");
            ViewData["UserId"] = new SelectList(await GetUsers(), "UserAccountId", "UserName");
            return View();
        }

        // POST: ChatsLocDpx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChatsLocDpx chat)
        {
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                    var chatDto = new
                    {
                        Message = chat.Message,
                        MessageType = chat.MessageType,
                        UserId = chat.UserId,
                        CoachId = chat.CoachId,
                        SentBy = chat.SentBy,
                        AttachmentUrl = chat.AttachmentUrl,
                        IsRead = chat.IsRead,
                        ResponseTime = chat.ResponseTime
                    };

                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "ChatsLocDpx", chatDto))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }

            ViewData["CoachId"] = new SelectList(await GetCoaches(), "CoachesLocDpxid", "FullName", chat.CoachId);
            ViewData["UserId"] = new SelectList(await GetUsers(), "UserAccountId", "UserName", chat.UserId);
            return View(chat);
        }

        // GET: ChatsLocDpx/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            ChatsLocDpx chat = null;

            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                var response = await httpClient.GetAsync(APIEndPoint + "ChatsLocDpx/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    chat = JsonConvert.DeserializeObject<ChatsLocDpx>(content);
                }
            }

            if (chat == null) return NotFound();

            ViewData["CoachId"] = new SelectList(await GetCoaches(), "CoachesLocDpxid", "FullName", chat.CoachId);
            ViewData["UserId"] = new SelectList(await GetUsers(), "UserAccountId", "UserName", chat.UserId);
            return View(chat);
        }

        // Additional methods for Edit, Delete, etc.
        private async Task<List<CoachesLocDpx>> GetCoaches()
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                using (var response = await httpClient.GetAsync(APIEndPoint + "CoachLocDpx"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<CoachesLocDpx>>(content);
                        return result ?? new List<CoachesLocDpx>();
                    }
                }
            }
            return new List<CoachesLocDpx>();
        }

        private async Task<List<SystemUserAccount>> GetUsers()
        {
            using (var httpClient = new HttpClient())
            {
                var tokenString = HttpContext.Request.Cookies.FirstOrDefault(c => c.Key == "TokenString").Value;
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenString);

                using (var response = await httpClient.GetAsync(APIEndPoint + "SystemUserAccount"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<SystemUserAccount>>(content);
                        return result ?? new List<SystemUserAccount>();
                    }
                }
            }
            return new List<SystemUserAccount>();
        }
    }
}