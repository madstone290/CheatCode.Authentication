using OAuth.Server.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace OAuth.Server.Controllers
{
    public class SecretController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Ok("Secret message");
        }
    }
}