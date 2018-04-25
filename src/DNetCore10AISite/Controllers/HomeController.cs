using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.InternalAbstractions;
using Microsoft.Extensions.DependencyModel;

namespace DNetCore10AISite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var runtimeId = RuntimeEnvironment.GetRuntimeIdentifier();
            var assemblies = DependencyContext.Default.GetRuntimeAssemblyNames(runtimeId);

            ViewBag.Assemblies = assemblies;

            ViewBag.Modules = Process.GetCurrentProcess().Modules;

            ViewBag.EnvironmentVariables = Environment.GetEnvironmentVariables();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private async void GetResponseStream()
        {
            var req = WebRequest.Create("http://bing.com");
            var response = await req.GetResponseAsync();

            using (var s = response.GetResponseStream())
            {
                byte[] content = new byte[512];
                s.Read(content, 0, content.Length);
                var contentS = System.Text.Encoding.UTF8.GetString(content);
                Console.WriteLine(contentS);
                ViewBag.Content = contentS;
            }
        }
    }
}
