using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using Slugify;
using ThucTap.Models;

namespace ThucTap.Controllers
{
	public class LienHeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	

	}
}
