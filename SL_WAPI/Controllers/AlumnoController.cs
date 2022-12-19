using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WAPI.Controllers
{
    public class AlumnoController : ApiController
    {
        // GET: api/Materia
        [Route("api/Alumno/GetAll")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.Alumno.GetAllLQ();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: api/Materia/5
        [Route("api/Alumno/GetById/{idAlumno}")]
        [HttpGet]
        public IHttpActionResult GetById(int idAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            ML.Result result = BL.Alumno.GetById(idAlumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<UsuarioController>
        [Route("api/Alumno/Add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody] ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.AddLQ(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<UsuarioController>/5
        [Route("api/Alumno/Update/{IdAlumno}")]
        [HttpPut]
        public IHttpActionResult Put(int IdAlumno, [FromBody] ML.Alumno alumno)
        {
            //if (alumno.IdAlumno != null && alumno.IdAlumno != 0)

            alumno.IdAlumno = IdAlumno;
                ML.Result result = BL.Alumno.UpdateLQ(alumno);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            
            //else
            //{
            //    return BadRequest("Especifique el dato a actualizar");
            //}
        }

        // DELETE api/<UsuarioController>/5
        [Route("api/Alumno/Delete/{idAlumno}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idAlumno)
        {
            ML.Result result = BL.Alumno.DeleteLQ(idAlumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
