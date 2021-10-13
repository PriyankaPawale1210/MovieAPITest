using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Interface;
using MovieAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        private readonly IMetaData _imetadata;
        public MetaDataController(IMetaData imetadata)
        {
            this._imetadata = imetadata;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var metadata = _imetadata.Get(id);
                return Ok(metadata);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post(MetadataModel model)
        {
            var result = _imetadata.Post(model);

            if (result) return Ok();
            return BadRequest();
        }
    }
}
