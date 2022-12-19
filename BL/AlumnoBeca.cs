using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoBeca
    {

        public static ML.Result ABGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    var query = (from AlumnoBecaLQ in context.AlumnoBecas
                                 select new
                                 {
                                     IdAlumnoBeca = AlumnoBecaLQ.IdAlumnoBeca,
                                     IdAlumno = AlumnoBecaLQ.IdAlumno,
                                     IdBeca = AlumnoBecaLQ.IdBeca
                                 }
                                 ).ToList();
                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var row in query)
                        {
                            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();

                            alumnoBeca.IdAlumnoBeca = row.IdAlumnoBeca;

                            alumnoBeca.Alumno = new ML.Alumno();
                            alumnoBeca.Alumno.IdAlumno = row.IdAlumno.Value;

                            alumnoBeca.Beca = new ML.Beca();
                            alumnoBeca.Beca.IdBeca = row.IdBeca.Value;

                            result.Objects.Add(alumnoBeca);
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

    }
}
