﻿using BusinessLogic;
using DataLayer;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodiceFiscale.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

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



    public async Task<IActionResult> GetComuni(string? id)
    {
        List<Comuni> comuni = new();

        if (!string.IsNullOrEmpty(id))
        {
               comuni = await _comuni.Comunis.Where(x => x.Sigla == id).ToListAsync();
        }
        else
        {
            comuni = await _comuni.Comunis.ToListAsync();
        }
     

        return  Ok(comuni);
    }


    public async Task<IActionResult> GetProvince()
    {

        return  Ok(await _comuni.Comunis.Distinct().ToListAsync());
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
