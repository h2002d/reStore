using PizzaDomino.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PizzaDomino.DAO
{
    public class GoodsDAO: DAO
    {
        internal List<Goods> getGoodById(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetGoodById", sqlConnection))
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
                        List<Goods> goodsList = new List<Goods>();
                        while (rdr.Read())
                        {
                            Goods good = new Goods();
                            good.Id = Convert.ToInt32(rdr["Id"]);
                            good.Description = rdr["Description"].ToString();
                            good.Name = rdr["Name"].ToString();
                            good.ImageSource = rdr["ImageSource"].ToString();
                            good.Price = Convert.ToDecimal(rdr["Price"].ToString());
                            good.CategoryId = Convert.ToInt32(rdr["CategoryId"]);

                            goodsList.Add(good);
                        }
                        return goodsList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        internal List<Goods> getGoodByCategoryId(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetGoodByCategory", sqlConnection))
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
                        List<Goods> goodsList = new List<Goods>();
                        while (rdr.Read())
                        {
                            Goods good = new Goods();
                            good.Id = Convert.ToInt32(rdr["Id"]);
                            good.Description = rdr["Description"].ToString();
                            good.Name = rdr["Name"].ToString();
                            good.Price = Convert.ToDecimal(rdr["Price"].ToString());
                            good.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            good.ImageSource = rdr["ImageSource"].ToString();

                            goodsList.Add(good);
                        }
                        return goodsList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        internal int saveGood(Goods good)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_CreateGoods", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (good.Id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", good.Id);
                        command.Parameters.AddWithValue("@Description", good.Description);
                        command.Parameters.AddWithValue("@CategoryId", good.CategoryId);
                        command.Parameters.AddWithValue("@ImageSource", good.ImageSource);
                        command.Parameters.AddWithValue("@Price", good.Price);
                        command.Parameters.AddWithValue("@Name", good.Name);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        internal void deleteCharity(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_AdminDeleteGood", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
                        //command.Parameters.AddWithValue("@DateBirth", user.DateBirth);
                        command.ExecuteNonQuery();
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