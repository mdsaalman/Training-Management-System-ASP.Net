using CourseEnroll.Models;
using Microsoft.Data.SqlClient;

namespace CourseEnroll.DataAccessLayer
{
    public class CategoryGateway
    {
        public string _connString { get; set; }

        public CategoryGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Category> GetList()
        {
            List<Category> Category = new List<Category>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Category";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category newCategory = new Category();
                    newCategory.Id = Convert.ToInt32(reader["Id"].ToString());
                    newCategory.Name = reader["Name"].ToString();
                    newCategory.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    Category.Add(newCategory);
                }
                conn.Close();
            }
            return Category;
        }
        public int Add(Category Category)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into Category (Name,IsActive) values (@Name,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Category.Name);
                cmd.Parameters.AddWithValue("@IsActive", Category.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }

        }
        public Category GetById(int id)
        {
            Category Category = new Category();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Category where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Category.Id = Convert.ToInt32(reader["Id"].ToString());
                    Category.Name = reader["Name"].ToString();
                    Category.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return Category;
        }
        public int Update(Category Category)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "update Category set Name=@Name,IsActive=@IsActive where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", Category.Name);
                cmd.Parameters.AddWithValue("@IsActive", Category.IsActive);
                cmd.Parameters.AddWithValue("@Id", Category.Id);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "delete from Category where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                return response;
            }
        }
    }
}
