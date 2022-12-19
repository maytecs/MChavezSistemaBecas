using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result BecaGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.MChavezSistemaDeBecasEntities context = new DL.MChavezSistemaDeBecasEntities())
                {
                    var query = (from becaLQ in context.Becas
                                 select new
                                 {
                                     IdBeca = becaLQ.IdBeca,
                                     NombreBeca = becaLQ.NombreBeca
                                 }
                                );
                    result.Objects = new List<object>();

                    if(query != null && query.ToList().Count > 0)
                    {
                        foreach(var row in query)
                        {
                            ML.Beca beca = new ML.Beca();

                            beca.IdBeca = row.IdBeca;
                            beca.NombreBeca = row.NombreBeca;

                            result.Objects.Add(beca);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Ocurrio un error al cargar la informacion";
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
