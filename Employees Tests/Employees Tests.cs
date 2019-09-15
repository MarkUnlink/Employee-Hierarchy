using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Employees_Tests
{
    [TestClass]
    public class Test_Employees
    {
        [TestMethod]
        public void TestMethod1()
        {
            var text = File.ReadAllLines("../../../test.txt");

            Employees emp = new Employees(text);
            Assert.Equal(3300, employees.SalaryBudget("Employee1"));
            Assert.Equal(1000, employees.SalaryBudget("Employee2"));
        }
    }
}
