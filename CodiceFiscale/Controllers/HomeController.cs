using BusinessLogic;
using DataLayer;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodiceFiscale.Models;

namespace CodiceFiscale.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        PersonaDataViewModel x = new();
        x.FirstName = "Andrea";
        x.LastName = "Gulisano";
        x.Birthday = DateTime.Parse("2003-11-14");
        x.Istat = "Catania";
        x.Gender = 'M';

       
        CF calcolo = new();
       
        return View("Index", calcolo.CalcolaCodiceFiscale(x));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

