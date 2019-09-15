using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Employee_Hierarchy;

namespace Employees_Tests
{
    [TestClass]
    public class Test_Employees
    {
        [TestMethod]
        public void TestBudget()
        {
            var text = File.ReadAllLines("D:\\test.csv");

            Employees emp = new Employees(text);
            Assert.Equals(3300, emp.Budget("Employee1"));
            Assert.Equals(1000, emp.Budget("Employee2"));
        }
    }
}
