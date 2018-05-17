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
                        command.Parameters.AddWithValue("@ImageSource1", newProduct.ImageSource1);
                        command.Parameters.AddWithValue("@ImageSource2", newProduct.ImageSource2);
                        command.Parameters.AddWithValue("@ImageSource3", newProduct.ImageSource3);
                        command.Parameters.AddWithValue("@ImageSource4", newProduct.ImageSource4);
                        command.Parameters.AddWithValue("@ImageSource5", newProduct.ImageSource5);
                        command.Parameters.AddWithValue("@ImageSource6", newProduct.ImageSource6);
                        command.Parameters.AddWithValue("@CategoryId", newProduct.CategoryId);
                        command.Parameters.AddWithValue("@Price", newProduct.Price);
                        command.Parameters.AddWithValue("@PriceDiscounted", newProduct.PriceDiscounted);
                        command.Parameters.AddWithValue("@MeasureId", newProduct.MeasureId);
                        command.Parameters.AddWithValue("@IsOut", newProduct.IsOut);
                        command.Parameters.AddWithValue("@IsSpecial", newProduct.IsSpecial);

                        newProduct.Id = Convert.ToInt32(command.ExecuteScalar());
                        return newProduct;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }

        internal static List<Product> getSpecialProducts(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetSpecialProducts", sqlConnection))
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
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);

                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);
                            newProduct.IsSpecial = Convert.ToBoolean(rdr["IsSpecial"]);
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

        internal static List<Product> getOutProducts(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetOutProducts", sqlConnection))
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
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);

                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);
                            newProduct.IsSpecial = Convert.ToBoolean(rdr["IsSpecial"]);
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
                            command.Parameters.AddWithValue("@Id", id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);
                            newProduct.IsSpecial = Convert.ToBoolean(rdr["IsSpecial"]);

                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
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

        internal static List<Product> getDiscountedProducts()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetDiscountedProducts", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);

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
                        command.Parameters.AddWithValue("@CategoryId", id);
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);

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
        public static List<CardModel> getProductsByOrderId(int id)
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
                        List<CardModel> newProductList = new List<CardModel>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);

                            CardModel model = new CardModel();
                            model.ProductQuantity = Convert.ToDecimal(rdr["Quantity"]);
                            model.product = newProduct;
                            newProductList.Add(model);
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
        public static List<Product> getProductsByQuery(string query)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SearchProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Query", string.Format("%{0}%", query));
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.MeasureId = Convert.ToInt32(rdr["MeasureId"]);
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.IsOut = Convert.ToBoolean(rdr["IsOut"]);

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

        public static void deleteProduct(int id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_DeleteProduct", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Id", id);
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
