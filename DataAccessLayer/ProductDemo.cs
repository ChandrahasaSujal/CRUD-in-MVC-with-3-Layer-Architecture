using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ProductDemo
    {
        string str = ConfigurationManager.ConnectionStrings["ProductsConnection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        //Read
        public IEnumerable<Product> ProductsList
        {
            get
            {
                List<Product> products = new List<Product>();
                con = new SqlConnection(str);
                con.Open();
                cmd = new SqlCommand("spGetProducts", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    Product product = new Product();
                    product.ProductId = Convert.ToInt32(dr["ProductId"]);
                    product.Name = dr["Name"].ToString();
                    product.Price = Convert.ToDecimal(dr["Price"]);
                    products.Add(product);
                }
                dr.Close();
                con.Close();
                return products;
            }
        }

        //Create
        public void InsertProduct(Product product)
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("spInsertProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter paramName = new SqlParameter();
            paramName.ParameterName = "@Name";
            paramName.Value = product.Name;
            cmd.Parameters.Add(paramName);
            SqlParameter paramPrice = new SqlParameter();
            paramPrice.ParameterName = "@Price";
            paramPrice.Value = product.Price;
            cmd.Parameters.Add(paramPrice);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Update
        public void UpdateProduct(Product product)
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("spUpdateProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter productId = new SqlParameter("@ProductId", product.ProductId);
            cmd.Parameters.Add(productId);
            SqlParameter name = new SqlParameter("@Name", product.Name);
            cmd.Parameters.Add(name);
            SqlParameter price = new SqlParameter("@Price", product.Price);
            cmd.Parameters.Add(price);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //Delete
        public void DeleteProduct(int id)
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("spDeleteProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductId", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
