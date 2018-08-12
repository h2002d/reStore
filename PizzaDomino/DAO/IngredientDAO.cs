using PizzaDomino.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PizzaDomino.DAO
{
    public class IngredientDAO: DAO
    {
        internal int saveIngredient(Ingredients ingredients)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_CreateIngredients", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (ingredients.IngredientId == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", ingredients.IngredientId);
                        command.Parameters.AddWithValue("@GoodId", ingredients.GoodId);
                        command.Parameters.AddWithValue("@Price", ingredients.Price);
                        command.Parameters.AddWithValue("@Name", ingredients.Name);
                        return Convert.ToInt32(command.ExecuteScalar());
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