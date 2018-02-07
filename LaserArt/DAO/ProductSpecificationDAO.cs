using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LaserArt.DAO
{
    public class ProductSpecificationDAO:DAO
    {
        public List<ProductSpecification> getProductSpecification(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProductSpecificationById", sqlConnection))
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
                        List<ProductSpecification> newCategoryList = new List<ProductSpecification>();
                        while (rdr.Read())
                        {
                            ProductSpecification newCategory = new ProductSpecification();
                            newCategory.id= Convert.ToInt32(rdr["Id"]);
                            newCategory.ProductId = Convert.ToInt32(rdr["ProductId"]);
                            newCategory.SpecificationName = rdr["SpecificationName"].ToString();
                            newCategory.Price = Convert.ToDecimal(rdr["Price"]);

                            newCategoryList.Add(newCategory);
                        }
                        return newCategoryList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        public List<ProductSpecification> getProductSpecificationById(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetProductSpecificationBySpecId", sqlConnection))
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
                        List<ProductSpecification> newCategoryList = new List<ProductSpecification>();
                        while (rdr.Read())
                        {
                            ProductSpecification newCategory = new ProductSpecification();
                            newCategory.id = Convert.ToInt32(rdr["Id"]);
                            newCategory.ProductId = Convert.ToInt32(rdr["ProductId"]);
                            newCategory.SpecificationName = rdr["SpecificationName"].ToString();
                            newCategory.Price = Convert.ToDecimal(rdr["Price"]);

                            newCategoryList.Add(newCategory);
                        }
                        return newCategoryList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        internal void DeleteSpecificationByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_DeleteProductSpecification", sqlConnection))
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
        public ProductSpecification saveProduct(ProductSpecification specific)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SaveProductSpecification", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", specific.ProductId);
                        command.Parameters.AddWithValue("@Price", specific.Price);
                        command.Parameters.AddWithValue("@SpecificationName", specific.SpecificationName);
                        command.ExecuteNonQuery();

                        return specific;
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