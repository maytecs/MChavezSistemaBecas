using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result AddLQ(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    DL.Alumno alumnoDL = new DL.Alumno();

                    alumnoDL.NombreAlumno = alumno.NombreAlumno;
                    alumnoDL.ApellidoPaterno = alumno.ApellidoPaterno;
                    alumnoDL.ApellidoMaterno = alumno.ApellidoMaterno;
                    alumnoDL.Genero = alumno.Genero;
                    alumnoDL.Edad = alumno.Edad;

                    context.Alumnoes.Add(alumnoDL);
                    context.SaveChanges();
                }
                result.Correct = true;
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = "Ocurrio un error al conectar al cargar la informacion" + result.Ex;
            }
            return result;
        }
        //Editar para poder hacer otro commit

        public static ML.Result UpdateLQ(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    var query = (
                        from alumnoLQ in context.Alumnoes
                        where alumnoLQ.IdAlumno == alumno.IdAlumno
                        select alumnoLQ).SingleOrDefault();

                    if (query != null)
                    {
                        //DL.Alumno alumnoDL = new DL.Alumno();

                        query.IdAlumno = alumno.IdAlumno;
                        query.NombreAlumno = alumno.NombreAlumno;
                        query.ApellidoPaterno = alumno.ApellidoPaterno;
                        query.ApellidoMaterno = alumno.ApellidoMaterno;
                        query.Genero = alumno.Genero;
                        query.Edad = alumno.Edad;

                        //context.Alumnoes.Add(alumnoDL);
                        context.SaveChanges();
                    }
                }
                result.Correct = true;
            }
            catch(Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = "Ocurrio un error al cargar la informacion" + result.Ex;
            }
            return result;
        }

        public static ML.Result GetAllLQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    var query = (from alumnoLQ in context.Alumnoes

                                 select new
                                 {
                                     IdAlumno = alumnoLQ.IdAlumno,
                                     NombreAlumno = alumnoLQ.NombreAlumno,
                                     ApellidoPaterno = alumnoLQ.ApellidoPaterno,
                                     ApellidoMaterno = alumnoLQ.ApellidoMaterno,
                                     Genero = alumnoLQ.Genero,
                                     Edad = alumnoLQ.Edad
                                 }
                                 ).ToList();
                    result.Objects = new List<object>();

                    if(query != null && query.ToList().Count > 0)
                    {
                        foreach (var row in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = row.IdAlumno;
                            alumno.NombreAlumno = row.NombreAlumno;
                            alumno.ApellidoPaterno = row.ApellidoPaterno;
                            alumno.ApellidoMaterno = row.ApellidoMaterno;
                            alumno.Genero = (bool)row.Genero;
                            alumno.Edad = (int)row.Edad;

                            result.Objects.Add(alumno);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se encontraron datos para mostrar";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Message = Ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    var query = (from alumnoLQ in context.Alumnoes
                                 where alumnoLQ.IdAlumno == IdAlumno

                                 select new
                                 {
                                     IdAlumno = alumnoLQ.IdAlumno,

                                     NombreAlumno = alumnoLQ.NombreAlumno,
                                     ApellidoPaterno = alumnoLQ.ApellidoPaterno,
                                     ApellidoMaterno = alumnoLQ.ApellidoMaterno,
                                     Genero = alumnoLQ.Genero,
                                     Edad = alumnoLQ.Edad

                                 }
                                 ).SingleOrDefault();

                    if(query != null)
                    {
                        ML.Alumno alumno = new ML.Alumno();

                        alumno.IdAlumno = query.IdAlumno;

                        alumno.NombreAlumno = query.NombreAlumno;
                        alumno.ApellidoPaterno = query.ApellidoPaterno;
                        alumno.ApellidoMaterno = query.ApellidoMaterno;
                        alumno.Genero = (bool)query.Genero;
                        alumno.Edad = (int)query.Edad;

                        result.Object = alumno;
                        result.Correct = true;
                    }
                }
                result.Correct = true;
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.Ex = Ex;
                result.Message = "Ocurrio un error al cargar la informacion" + result.Ex;
            }
            return result;
        }


        public static ML.Result DeleteLQ(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            //ML.Alumno alumno = new ML.Alumno();

            try
            {
                using (DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    var query = (from alumnoLQ in context.Alumnoes
                                 where alumnoLQ.IdAlumno == IdAlumno
                                 select alumnoLQ
                                 ).First();

                    context.Alumnoes.Remove(query);
                    context.SaveChanges();
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al cargar la información" + result.Ex;
                throw;
            }
            return result;

        }

    }
}
