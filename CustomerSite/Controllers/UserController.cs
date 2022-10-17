﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CustomerSite.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace CustomerSite.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult ResetPassword()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
