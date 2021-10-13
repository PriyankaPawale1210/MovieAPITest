using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovie _imovie;
        public MoviesController(IMovie imovie)
        {
            this._imovie = imovie;
        }

        [HttpGet]
        [Route("stats")]
        public IActionResult Get()
        {
            var stats = this._imovie.Get();
            return Ok(stats);
        }
    }
}
