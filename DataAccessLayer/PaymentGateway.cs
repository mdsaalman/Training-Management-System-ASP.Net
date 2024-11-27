using CourseEnroll.Models;
using CourseEnroll.Models.VM;
using Microsoft.Data.SqlClient;
using NuGet.Configuration;

namespace CourseEnroll.DataAccessLayer
{
    public class PaymentGateway
    {


        public string _connString { get; set; }

        public PaymentGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<PaymentVM> GetList()
        {
            List<PaymentVM> payments = new List<PaymentVM>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = @"SELECT 
s.Id,
                                    s.Name, c.TotalFee, ISNULL(SUM(p.PayAmount), 0) AS Paid 
                                    FROM CourseReg c 
                                        inner JOIN Student s ON s.Id = c.StudentId 
                                        left JOIN CoursePayment p ON s.Id = p.StudentId 
                                    GROUP BY  s.Id, s.Name, c.TotalFee";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PaymentVM pay = new PaymentVM();
                    pay.Id = Convert.ToInt32(reader["Id"].ToString());
                    pay.Name = reader["Name"].ToString();
                    pay.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());
                    pay.Paid = Convert.ToDecimal(reader["Paid"].ToString());
                    pay.Due = 0;
                    payments.Add(pay);
                }
                conn.Close();
            }

            return payments;
        }
        public List<CoursePayment> PayList(int StudentId)
        {
            List<CoursePayment> payments = new List<CoursePayment>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "SELECT RefNo, PayAmount, PaymentDate FROM  CoursePayment where StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CoursePayment pay = new CoursePayment();
                    pay.RefNo = reader["RefNo"].ToString();
                    pay.PaymentDate = Convert.ToDateTime(reader["PaymentDate"].ToString());
                    pay.PayAmount = Convert.ToDecimal(reader["PayAmount"].ToString());
                    payments.Add(pay);
                }
                conn.Close();
            }

            return payments;
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

        public int Add(CoursePayment pay)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into CoursePayment (RefNo,StudentId,PayAmount,PaymentDate) values (@RefNo,@StudentId,@PayAmount,@PaymentDate)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RefNo", pay.RefNo);
                cmd.Parameters.AddWithValue("@StudentId", pay.StudentId);
                cmd.Parameters.AddWithValue("@PayAmount", pay.PayAmount);
                cmd.Parameters.AddWithValue("@PaymentDate", pay.PaymentDate);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();

                return response;
            }
        }
    }
}
