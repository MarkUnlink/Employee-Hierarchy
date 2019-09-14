using System;

namespace Employee_Hierarchy
{
    public class Emp : IComparable<Emp>
    {
        public string Id { get; set; }
        public int Salary { get; set; }

        public string Manager { get; set; }

        public Emp(string id, string manager, int salary)
        {
            Id = id;
            Salary = salary;
            Manager = manager;
        }

        public int CompareTo(Emp other)
        {
            if (other == null) return -1;
            return string.Compare(this.Id, other.Id,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
