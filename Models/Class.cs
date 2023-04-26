using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }
    public class Class
    {
     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }
        //FK
        [ForeignKey("Course")]
        public int? FKCourseId { get; set; }
        //FK
        [ForeignKey("Student")]
        public int? FKStudentId { get; set; }
        //FK
        [ForeignKey("Teacher")]
        public int? FKTeacherId { get; set; }
        public Grade? Grade { get; set; }


        public Course? Course { get; set; }
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
    }
    
}
