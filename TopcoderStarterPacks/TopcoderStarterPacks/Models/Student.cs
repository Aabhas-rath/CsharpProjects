using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
