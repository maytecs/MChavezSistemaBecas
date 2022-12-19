using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WAPI.Controllers
{
    public class AlumnoBecaController : ApiController
    {
        [Route("api/AlumnoBeca/GetAll")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            //ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.AlumnoBeca.ABGetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/AlumnoBeca/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AlumnoBeca
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/AlumnoBeca/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AlumnoBeca/5
        public void Delete(int id)
        {
        }
    }
}
