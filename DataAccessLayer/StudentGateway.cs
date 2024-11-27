using CourseEnroll.Models.VM;
using CourseEnroll.Models;
using Microsoft.Data.SqlClient;

namespace CourseEnroll.DataAccessLayer
{
    public class StudentGateway
    {
        public string _connString { get; set; }

        public StudentGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Student> GetList()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Student";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(reader["Id"].ToString());
                    student.Name = reader["Name"].ToString();
                    student.Mobile = reader["Mobile"].ToString();
                    student.Email = reader["Email"].ToString();
                    student.Education = reader["Education"].ToString();
                    student.Institute = reader["Institute"].ToString();
                    student.RegDate = Convert.ToDateTime(reader["RegDate"].ToString());
                    student.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                    students.Add(student);
                }
                conn.Close();
            }

            return students;
        }
        public Student GetById(int id)
        {
            Student student = new Student();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from Student where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    student.Id = Convert.ToInt32(reader["Id"].ToString());
                    student.Name = reader["Name"].ToString();
                    student.Mobile = reader["Mobile"].ToString();
                    student.Email = reader["Email"].ToString();
                    student.Education = reader["Education"].ToString();
                    student.Institute = reader["Institute"].ToString();
                    student.RegDate = Convert.ToDateTime(reader["RegDate"].ToString());
                    student.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());

                }
                conn.Close();
            }
            return student;
        }
        public int Update(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = @"update Student set 
                                    Name=@Name,
                                    Mobile=@Mobile,
                                    Email=@Email,
                                    Education=@Education,
                                    Institute=@Institute,
                                    RegDate=@RegDate,
                                    IsActive=@IsActive
                                    where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Education", student.Education);
                cmd.Parameters.AddWithValue("@Institute", student.Institute);
                cmd.Parameters.AddWithValue("@RegDate", student.RegDate);
                cmd.Parameters.AddWithValue("@IsActive", student.IsActive);
                cmd.Parameters.AddWithValue("@Id", student.Id);

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
                string query = "delete from Student where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();

                conn.Close();
                return response;
            }
        }       

        public int Add(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "insert into Student (Name,Mobile,Email,Education,Institute,RegDate,IsActive) values (@Name,@Mobile,@Email,@Education,@Institute,@RegDate,@IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Education", student.Education);
                cmd.Parameters.AddWithValue("@Institute", student.Institute);
                cmd.Parameters.AddWithValue("@RegDate", student.RegDate);
                cmd.Parameters.AddWithValue("@IsActive", student.IsActive);

                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();

                return response;
            }
        }
    }

}
