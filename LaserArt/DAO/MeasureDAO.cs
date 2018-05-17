using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LaserArt.DAO
{
    public class MeasureDAO:DAO
    {
        public List<Measure> getMeasure(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetMeasure", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Measure> newProductList = new List<Measure>();
                        while (rdr.Read())
                        {
                            Measure newMeasure = new Measure();
                            newMeasure.Id = Convert.ToInt32(rdr["Id"]);
                            newMeasure.Name = rdr["Name"].ToString();
                            newMeasure.IsDecimal = Convert.ToBoolean(rdr["IsDecimal"]);
                            newProductList.Add(newMeasure);
                        }
                        return newProductList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

    }
}