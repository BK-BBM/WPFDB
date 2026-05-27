using MySql.Data.MySqlClient;

namespace WPFDBConnection
{
    public class StudentCRUD
    {
        //this is our controller -> get data from DB (model) , display it on the student.xaml(view)

        /*
         In the DB-> Select * From Students 
         */
        public List<Student> GetAll(string q = "")
        {
            var list = new List<Student>();

            using var conn = DB.Open();

            string sql = """

                SELECT * From students 
                Where @q=''
                OR student_no LIKE @like
                OR first_name LIKE @like
                OR last_name LIKE @like
                OR email LIKE @like
                OR status LIKE @like
                """;

            //this block prepares query to be run in SQL
            using var command = new MySqlCommand(sql, conn);
            command.Parameters.AddWithValue("@q", q);
            command.Parameters.AddWithValue("@like", $"%{q}%");

            //Executes query 
            using var reader = command.ExecuteReader();

            //displays (maps it to the correct property) the data as the "cursor" object is going through 
            //the DB
            while (reader.Read())
            {
                list.Add(Map(reader));
            }

            // map sql columns to model properties 
            return list;
        } // Select * From Students Where first_name LIKE = %'value'%

        /* INSERT, UPDATE & DELETE Methods
         */

        public void Insert(Student s) {
            //Insert Into Student (columnName) Values ('Value');

            //create a connection to DB
            using var conn = DB.Open();
            string sql = """
                INSERT Into students (student_no,first_name, last_name,
                email, phone, dateofbirth, status)
                Values(@no, @fn, @ln,@email,@phone,@dob,@status)
                """;
            using var command = new MySqlCommand(sql, conn);

            Bind(command, s);
            command.ExecuteNonQuery();
            
            //because id is AUTO INCREMENTING (we get it from the DB) 
            //LastInsertedId will return the "biggest" value
            //biggest value = last inserted id because we increment with 1.
            s.Id = (int)command.LastInsertedId;
        }

        public void Update(Student s) {

            using var conn = DB.Open();
            string sql = """
                UPDATE students SET 
                student_no = @no, first_name = @fn, last_name = @ln,
                email = @email, phone = @phone, dateofbirth = @dob, 
                status = @status
                WHERE id = @id
                """;

            using var command = new MySqlCommand(sql, conn);

            Bind(command, s);
            command.Parameters.AddWithValue("@id", s.Id);
            command.ExecuteNonQuery();
        }

        public void Delete(int id) { 
            using var conn = DB.Open();
            using var command = new 
                MySqlCommand("DELETE FROM students WHERE id=@id", conn);

            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }


        //Map and Bind Methods
        private static Student Map(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("id"),
            StudentNo = reader.GetString("student_no"),
            FirstName = reader.GetString("first_name"),
            LastName = reader.GetString("last_name"),
            Email = reader.GetString("email"),
            Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ?"" 
            : reader.GetString("phone"),
            DateOfBirth = reader.IsDBNull(reader.GetOrdinal("dateofbirth"))? null
            : reader.GetDateTime("dateofbirth"),
            Status = reader.GetString("status"),
        };


        private static void Bind(MySqlCommand command, Student student)
        {
            command.Parameters.AddWithValue("@no", student.StudentNo);
            command.Parameters.AddWithValue("@fn", student.FirstName);
            command.Parameters.AddWithValue("@ln", student.LastName);
            command.Parameters.AddWithValue("@email", student.Email);
            command.Parameters.AddWithValue("@phone", student.Phone);
            command.Parameters.AddWithValue("@dob", (object?)student.DateOfBirth ?? 
                DBNull.Value);
            command.Parameters.AddWithValue("@status", student.Status);
        }

    }
}
