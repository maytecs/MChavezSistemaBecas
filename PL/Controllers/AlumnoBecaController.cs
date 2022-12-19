using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoBecaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.AlumnoBeca alumno = new ML.AlumnoBeca();

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                    var responseTask = client.GetAsync("AlumnoBeca/GetAll");

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.AlumnoBeca resultItemList = JsonConvert.DeserializeObject<ML.AlumnoBeca>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }

            if (result.Correct)
            {
                alumno.AlumnoBecas = result.Objects;
                return View(alumno);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta ";
                return PartialView("Modal");
            }

        }

        [HttpPost]
        public ActionResult Form(ML.AlumnoBeca alumnoBeca)
        {
            if (alumnoBeca.IdAlumnoBeca == 0)
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                        var postTask = client.PostAsJsonAsync<ML.AlumnoBeca>("AlumnoBeca/Add", alumnoBeca);

                        postTask.Wait();

                        var resultServicio = postTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Ocurrio un error al agregar la informacion";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha agregado exitosamente";
                }
                else
                {
                    ViewBag.Messsage = "Error:  " + result.Message;
                }
            }
            else
            {
                ML.Result result = new ML.Result();

                try
                {
                    using (var cliente = new HttpClient())
                    {
                        cliente.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);

                        var postask = cliente.PostAsJsonAsync<ML.AlumnoBeca>("Alumno/Update/" + alumnoBeca.IdAlumno, alumnoBeca);

                        postask.Wait();

                        var resultServicio = postask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "Ocurrio un error al actualizar la informacion";
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Message = ex.Message;
                }
                if (result.Correct)
                {
                    ViewBag.Message = "El dato se ha actualizado";
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            return PartialView("Modal");
        }
    }
}