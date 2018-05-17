using LaserArt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LaserArt.DAO
{
    public class ParentCategoryDAO:DAO
    {
        public static List<ParentCategory> getCategories(int? id)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetParentCategory", sqlConnection))
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
                        List<ParentCategory> newParentCategoryList = new List<ParentCategory>();
                        while (rdr.Read())
                        {
                            ParentCategory newParentCategory = new ParentCategory();
                            newParentCategory.Id = Convert.ToInt32(rdr["Id"]);
                            newParentCategory.ParentCategoryName = rdr["ParentCategoryName"].ToString();
                            newParentCategory.ImageSource = rdr["ImageSource"].ToString();

                            newParentCategoryList.Add(newParentCategory);
                        }
                        return newParentCategoryList;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        internal static List<Category> getChildCategories(int? id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetChildCategoriesByParentId", sqlConnection))
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
                        List<Category> newCategoryList = new List<Category>();
                        while (rdr.Read())
                        {
                            Category newCategory = new Category();
                            newCategory.Id = Convert.ToInt32(rdr["Id"]);
                            newCategory.CategoryName = rdr["CategoryName"].ToString();
                            newCategory.ImageSource = rdr["ImageSource"].ToString();
                            newCategory.ParentId = Convert.ToInt32(rdr["ParentId"]);
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

        internal static void DeleteParentCategoryByID(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_DeleteParentCategory", sqlConnection))
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

        public static ParentCategory saveParentCategory(ParentCategory newParentCategory)
        {

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_SaveParentCategory", sqlConnection))
                {
                    try
                    {
                        sqlConnection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        if (newParentCategory.Id == null)
                            command.Parameters.AddWithValue("@Id", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Id", newParentCategory.Id);
                        command.Parameters.AddWithValue("@ParentCategoryName", newParentCategory.ParentCategoryName);
                        command.Parameters.AddWithValue("@ImageSource", newParentCategory.ImageSource);

                        command.ExecuteNonQuery();

                        return newParentCategory;
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