
using System.ComponentModel.DataAnnotations;


namespace P01_StudentSystem.Data.Models
{

    public class Student
    {
       

        [Key]
        public int StudentId { get; set; }


        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }


        public DateTime? Birthday { get; set; }


        public virtual ICollection<Homework> Homeworks { get; set; }
        public virtual ICollection<StudentCourse> StudentsCourses { get; set; }
    }


}









