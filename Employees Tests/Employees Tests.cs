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
            var text = File.ReadAllLines("C:\\testBudget.csv");

            Employees emp = new Employees(text);
            Assert.AreEqual(1800, emp.Budget("Employee1"));
            Assert.AreEqual(500, emp.Budget("Employee2"));
        }

       
    }
}
