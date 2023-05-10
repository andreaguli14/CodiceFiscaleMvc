using BusinessLogic;
using DataLayer;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodiceFiscale.Models;

namespace CodiceFiscale.Controllers;

public class CFController : Controller
{
    
    private readonly ICodiceFiscale _calcolo;
    private readonly ComuniContext _comuni;

    public CFController(ICodiceFiscale codice, ComuniContext comuni)
    {
        _calcolo = codice;
        _comuni = comuni;
    }

    public IActionResult Index()
    {

        PersonaDataViewModel x = new();
        x.CodiceFiscale = "";
   
        return View("Index",x);

    }

    [HttpPost]
    public IActionResult Index(PersonaDataViewModel x)
    {

        x.Istat = _comuni.Comunis.FirstOrDefault(y => y.Comune == x.Istat.Remove(0, 2)).Code;
        x.CodiceFiscale = _calcolo.CalcolaCodiceFiscale(x);

        return View("Index", x);
    }



    public JsonResult GetComuni(string? id)
    {
        var comuni = _comuni.Comunis.ToList();

        if (!string.IsNullOrEmpty(id))
        {
             comuni = _comuni.Comunis.Where(x => x.Sigla == id).ToList();
        }
     

        return new JsonResult(comuni);
    }


    public JsonResult GetProvince()
    {

        var province = _comuni.Comunis.Distinct().ToList();

        return new JsonResult(province);
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

