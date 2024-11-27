using CourseEnroll.Models;
using CourseEnroll.Models.VM;
using Microsoft.Data.SqlClient;
using NuGet.Configuration;

namespace CourseEnroll.DataAccessLayer
{
    public class CourseRegGateway
    {


        public string _connString { get; set; }

        public CourseRegGateway(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<CourseRegVM> GetList()
        {
            List<CourseRegVM> courses = new List<CourseRegVM>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = @"select CR.Id, CR.RefNo, CR.CourseRegDate, CR.TotalFee, s.Name StudentName from CourseReg CR 
                                    inner join Student s on s.Id = CR.StudentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CourseRegVM course = new CourseRegVM();
                    course.Id = Convert.ToInt32(reader["Id"].ToString());
                    course.RefNo = reader["RefNo"].ToString();
                    course.StudentName = reader["StudentName"].ToString();
                    course.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());
                    course.CourseRegDate = Convert.ToDateTime(reader["CourseRegDate"].ToString());
                    courses.Add(course);
                }
                conn.Close();
            }

            return courses;
        }
        public CourseRegMasterVM GetById(int id)
        {
            CourseRegMasterVM VM = new CourseRegMasterVM();
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = "select * from CourseReg where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    VM.CourseReg.Id = Convert.ToInt32(reader["Id"].ToString());
                    VM.CourseReg.RefNo = reader["RefNo"].ToString();
                    VM.CourseReg.StudentId = Convert.ToInt32(reader["StudentId"].ToString());
                    VM.CourseReg.CourseRegDate = Convert.ToDateTime(reader["CourseRegDate"].ToString());
                    VM.CourseReg.TotalFee = Convert.ToDecimal(reader["TotalFee"].ToString());

                }
                conn.Close();

                query = "select * from CourseRegDetails where CourseRegId=@CourseRegId";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseRegId", id);
                conn.Open();

                SqlDataReader readerDetails = cmd.ExecuteReader();
                while (readerDetails.Read())
                {
                    CourseRegDetails CRD = new CourseRegDetails();
                    CRD.Id = Convert.ToInt32(readerDetails["Id"].ToString());
                    CRD.CourseId = Convert.ToInt32(readerDetails["CourseId"].ToString());
                    CRD.CourseFee = Convert.ToDecimal(readerDetails["CourseFee"].ToString());
                    VM.CourseRegDetails.Add(CRD);

                }
                conn.Close();
            }
            return VM;
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
                string query = "delete from CourseReg where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int response = cmd.ExecuteNonQuery();
                conn.Close();
                if (response > 0)
                {
                    query = "delete from CourseRegDetails where CourseRegId=@CourseRegId";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CourseRegId", id);
                    conn.Open();
                    int response2 = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return response;
            }
        }
      
        public int Add(CourseRegMasterVM VM)
        {           

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                int CourseRegId = 0;

                string query = @"INSERT INTO CourseReg
                                (
                                    RefNo,
                                    StudentId,
                                    TotalFee,
                                    CourseRegDate
                                )
                                VALUES
                                (   
		    	                    @RefNo, 
			                        @StudentId, 
			                        @TotalFee,
                                    @CourseRegDate
		                        ) 
                            SELECT SCOPE_IDENTITY() Id;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RefNo", VM.CourseReg.RefNo);
                cmd.Parameters.AddWithValue("@StudentId", VM.CourseReg.StudentId);
                cmd.Parameters.AddWithValue("@TotalFee", VM.CourseReg.TotalFee);
                cmd.Parameters.AddWithValue("@CourseRegDate", VM.CourseReg.CourseRegDate);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CourseRegId = Convert.ToInt32(reader["Id"].ToString());
                }
                conn.Close();

                if (CourseRegId > 0)
                {
                    foreach (var item in VM.CourseRegDetails)
                    {
                        query = @"INSERT INTO CourseRegDetails
                                    (
                                        CourseRegId,
                                        CourseId,
                                        CourseFee
                                    )
                                VALUES
                                    (
		                                @CourseRegId, 
                                        @CourseId, 
                                        @CourseFee
			                        )";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@CourseRegId", CourseRegId);
                        cmd.Parameters.AddWithValue("@CourseId", item.CourseId);
                        cmd.Parameters.AddWithValue("@CourseFee", item.CourseFee);
                        conn.Open();
                        var res = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                else
                {
                    return 0;
                }

                return 1;
            }
        }

        public List<CourseVM> CourseList(int StudentId)
        {
            List<CourseVM> courses = new List<CourseVM>();

            using (SqlConnection conn = new SqlConnection(_connString))
            {
                string query = @"select c.Name, c.CourseFee,c.CourseDuration from CourseRegDetails CD 
                                        inner join CourseReg CR on CR.Id=CD.CourseRegId
                                        inner join Course C on c.Id = CD.CourseId where CR.StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", StudentId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CourseVM course = new CourseVM();
                    course.Name = reader["Name"].ToString();
                    course.CourseDuration = reader["CourseDuration"].ToString();
                    course.CourseFee = Convert.ToDecimal(reader["CourseFee"].ToString());
                    courses.Add(course);
                }
                conn.Close();
            }

            return courses;
        }
    }
}
