using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Shop.Controllers
{
    /// <summary>
    /// 站点
    /// </summary>
    public class SiteConfig
    {
        public string Name { get; set; } = "";
        public string Info { get; set; } = "";
    }

    public class HomeController : Controller
    {
        public SiteConfig Config;

        public HomeController(IOptions<SiteConfig> option)
        {
            Config = option.Value;
        }

        public IActionResult Index()
        {
            return View(Config);
        }
        

    }
}
