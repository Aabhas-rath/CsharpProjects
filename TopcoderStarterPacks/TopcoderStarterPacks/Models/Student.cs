using Models.Interfaces;

namespace Models
{
    public class Student : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rollno { get; set; }
        public float Marks { get; set; }

    }
}
