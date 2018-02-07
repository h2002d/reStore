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

                        command.Parameters.AddWithValue("@Color", newProduct.Color);
                        command.Parameters.AddWithValue("@Color1", newProduct.Color1==null? "": newProduct.Color1);
                        command.Parameters.AddWithValue("@Color2", newProduct.Color2 == null ? "" : newProduct.Color2);
                        command.Parameters.AddWithValue("@Color3", newProduct.Color3 == null ? "" : newProduct.Color3);
                        command.Parameters.AddWithValue("@Color4", newProduct.Color4 == null ? "" : newProduct.Color4);
                        command.Parameters.AddWithValue("@Color5", newProduct.Color5 == null ? "" : newProduct.Color5);
                        command.Parameters.AddWithValue("@Color6", newProduct.Color6 == null ? "" : newProduct.Color6);

                        command.Parameters.AddWithValue("@CategoryId", newProduct.CategoryId);
                        command.Parameters.AddWithValue("@Price", newProduct.Price);
                        command.Parameters.AddWithValue("@PriceDiscounted", newProduct.PriceDiscounted);

                        newProduct.Id=Convert.ToInt32(command.ExecuteScalar());
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
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Color = rdr["Color"].ToString();
                            newProduct.Color1 = rdr["Color1"].ToString();
                            newProduct.Color2 = rdr["Color2"].ToString();
                            newProduct.Color3 = rdr["Color3"].ToString();
                            newProduct.Color4 = rdr["Color4"].ToString();
                            newProduct.Color5 = rdr["Color5"].ToString();
                            newProduct.Color6 = rdr["Color6"].ToString();
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
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Color = rdr["Color"].ToString();
                            newProduct.Color1 = rdr["Color1"].ToString();
                            newProduct.Color2 = rdr["Color2"].ToString();
                            newProduct.Color3 = rdr["Color3"].ToString();
                            newProduct.Color4 = rdr["Color4"].ToString();
                            newProduct.Color5 = rdr["Color5"].ToString();
                            newProduct.Color6 = rdr["Color6"].ToString();
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
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();
                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4= rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Color = rdr["Color"].ToString();
                            newProduct.Color1 = rdr["Color1"].ToString();
                            newProduct.Color2 = rdr["Color2"].ToString();
                            newProduct.Color3 = rdr["Color3"].ToString();
                            newProduct.Color4 = rdr["Color4"].ToString();
                            newProduct.Color5 = rdr["Color5"].ToString();
                            newProduct.Color6 = rdr["Color6"].ToString();

                            newProduct.Price = Convert.ToDecimal(rdr["Price"]);
                            newProduct.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                            newProduct.PriceDiscounted = rdr["PriceDiscounted"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PriceDiscounted"]);
                            CardModel model = new CardModel();
                            model.ProductQuantity = Convert.ToInt32(rdr["Quantity"]);
                            model.product = newProduct;
                            model.Specification = ProductSpecification.GetProductSpecificationById(Convert.ToInt32(rdr["SpecificationId"])).First();
                            model.Color = rdr["Color"].ToString();
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
                        command.Parameters.AddWithValue("@Query", string.Format("%{0}%",query));
                        SqlDataReader rdr = command.ExecuteReader();
                        List<Product> newProductList = new List<Product>();
                        while (rdr.Read())
                        {
                            Product newProduct = new Product();
                            newProduct.Id = Convert.ToInt32(rdr["Id"]);
                            newProduct.ProductTitle = rdr["ProductTitle"].ToString();
                            newProduct.ProductDescription = rdr["ProductDescription"].ToString();

                            newProduct.ImageSource = rdr["ImageSource"].ToString();
                            newProduct.ImageSource1 = rdr["ImageSource1"].ToString();
                            newProduct.ImageSource2 = rdr["ImageSource2"].ToString();
                            newProduct.ImageSource3 = rdr["ImageSource3"].ToString();
                            newProduct.ImageSource4 = rdr["ImageSource4"].ToString();
                            newProduct.ImageSource5 = rdr["ImageSource5"].ToString();
                            newProduct.ImageSource6 = rdr["ImageSource6"].ToString();
                            newProduct.Color = rdr["Color"].ToString();
                            newProduct.Color1 = rdr["Color1"].ToString();
                            newProduct.Color2 = rdr["Color2"].ToString();
                            newProduct.Color3 = rdr["Color3"].ToString();
                            newProduct.Color4 = rdr["Color4"].ToString();
                            newProduct.Color5 = rdr["Color5"].ToString();
                            newProduct.Color6 = rdr["Color6"].ToString();
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
