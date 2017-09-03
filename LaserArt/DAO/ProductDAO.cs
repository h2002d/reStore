using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LaserArt.DAO
{
    public class ProductDAO : DAO
    {
        public static Product saveProduct(Product newProduct)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SaveProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        if (newProduct.Id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", newProduct.Id);

                        command.Parameters.AddWithValue("@ProductTitle", newProduct.ProductTitle);
                        command.Parameters.AddWithValue("@ProductDescription", newProduct.ProductDescription);
                        command.Parameters.AddWithValue("@ImageSource", newProduct.ImageSource);
                        command.Parameters.AddWithValue("@CategoryId", newProduct.CategoryId);
                        command.Parameters.AddWithValue("@Price", newProduct.Price);
                        command.Parameters.AddWithValue("@PriceDiscounted", newProduct.PriceDiscounted);

                        command.ExecuteNonQuery();
                        return newProduct;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }
        public static List<Product> getProducts(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProducts", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id",id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.Price =Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"]==DBNull.Value? 0:Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);

                            newProductList.Add(newProduct);
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
        public static List<Product> getProductsByCategoryId(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProductsByCategory", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryId",id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"]==DBNull.Value? 0:Convert.ToDecimal(rdr["PriceDiscounted"]);

                            newProductList.Add(newProduct);
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
        public static List<Product> getProductsByOrderId(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProductsByOrderid", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OrderId", id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);

                            newProductList.Add(newProduct);
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
