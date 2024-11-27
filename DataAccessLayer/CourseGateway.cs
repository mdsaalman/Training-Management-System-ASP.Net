using CourseEnroll.Models;
using CourseEnroll.Models.VM;
using Microsoft.Data.SqlClient;
using NuGet.Configuration;

namespace CourseEnroll.DataAccessLayer
{
    public class CourseGateway
    {


        public string _connString { get; set; }

        public CourseGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<CourseVM> GetList()
        {
            List<CourseVM> courses = new List<CourseVM>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select i.*,c.Name CategoryName from Course i left join Category c on c.Id = i.CategoryId";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CourseVM course = new CourseVM();
                    course.Id = Convert.ToInt32(reader["Id"].ToString());
                    course.Name = reader["Name"].ToString();
                    course.Description = reader["Description"].ToString();
                    course.CourseDuration = reader["CourseDuration"].ToString();
                    course.Category = reader["CategoryName"].ToString();
                    course.CourseFee = Convert.ToDecimal(reader["CourseFee"].ToString());
                    course.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    courses.Add(course);
                }
                conn.Close();
            }

            return courses;
        }
        public Course GetById(int id)
        {
            Course course = new Course();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Course where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    course.Id = Convert.ToInt32(reader["Id"].ToString());
                    course.Name = reader["Name"].ToString();
                    course.Description = reader["Description"].ToString();
                    course.CourseDuration = reader["CourseDuration"].ToString();
                    course.CategoryId = Convert.ToInt32(reader["CategoryId"].ToString());
                    course.CourseFee = Convert.ToDecimal(reader["CourseFee"].ToString());
                    course.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return course;
        }
        public int Update(Course course)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = @"update Course set 
                                    Name=@Name,
                                    Description=@Description,
                                    CategoryId=@CategoryId,
                                    CourseDuration=@CourseDuration,
                                    CourseFee=@CourseFee,
                                    IsActive=@IsActive
                                    where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", course.Name);
                cmd.Parameters.AddWithValue("@CategoryId", course.CategoryId);
                cmd.Parameters.AddWithValue("@Description", course.Description);
                cmd.Parameters.AddWithValue("@CourseDuration", course.CourseDuration);
                cmd.Parameters.AddWithValue("@CourseFee", course.CourseFee);
                cmd.Parameters.AddWithValue("@IsActive", course.IsActive);
                cmd.Parameters.AddWithValue("@Id", course.Id);

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
                string query = "delete from Course where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();

                conn.Close();
                return response;
            }
        }
        
        public List<Category> GetCategoryList()
        {
            List<Category> category = new List<Category>();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Category";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Category category1 = new Category();
                    category1.Id = Convert.ToInt32(reader["Id"].ToString());
                    category1.Name = reader["Name"].ToString();
                    category.Add(category1);
                }
                conn.Close();
            }
            return category;
        }

        public int Add(Course item)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into Course (Name,CategoryId,CourseDuration,Description,CourseFee,IsActive) values (@Name,@CategoryId,@CourseDuration,@Description,@CourseFee,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@CourseDuration", item.CourseDuration);
                cmd.Parameters.AddWithValue("@Description", item.Description);
                cmd.Parameters.AddWithValue("@CourseFee", item.CourseFee);
                cmd.Parameters.AddWithValue("@IsActive", item.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();

                return response;
            }
        }
    }
}
