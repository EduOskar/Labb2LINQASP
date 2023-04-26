using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb2LINQ.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //FK
        [ForeignKey("Teacher")]
        public int FK_teacher { get; set; }
        public Teacher? teacher { get; set; }

        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}
