using System;
using System.Collections.Generic;
using System.Text;

namespace WPFDBConnection
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentNo { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Status { get; set; } = "Active";

       
    }

    public class Module
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Title { get; set; } = "";
        public int Credits { get; set; } = 10; 
        public string Lecturer { get; set; } = "";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "Active";
    }
}
