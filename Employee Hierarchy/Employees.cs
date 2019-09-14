using System;
using System.Linq;
using System.Collections.Generic;

namespace Employee_Hierarchy
{
    public class Employees
    {

        private Tree<Emp> empTree;
        private Dictionary<string, Emp> employee;

        public Employees(string[] employeesCSV)
        {
            int salary = 0, CEO = 0;

            empTree = new Tree<Emp>();
            employee = new Dictionary<string, Emp>();

            var lines = employeesCSV.Select(items => items.Split('\t'));
            var section = from line in lines select (from value in line select value);


            foreach (var i in section)
            {

                var x = i.GetEnumerator();
                while (x.MoveNext())
                {
                    try
                    {
                        var item = x.Current.Split(',');

                 // This code ensures that the salary in the CSV is a valid integer
                        if (int.TryParse(item[2], out salary))
                        {
                            var emp = new Emp(item[0], item[1], salary);
                            try
                            {
                                employee.Add(emp.Id, emp);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }

                 // THis code adds the employee to the DAG if not present
                            if (!empTree.HasVertex(emp))
                            {
                                empTree.AddVertex(emp);
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Salary is NOT an integer!");
                        }

                 // THis code 
                        if (string.IsNullOrEmpty(item[0]))
                        {
                            Console.WriteLine("Employee cannot have empty Id, skipping ...");
                            continue;
                        }

                        if (string.IsNullOrEmpty(item[1]) && CEO < 1)
                        {
                            CEO++;
                        }
                        else if (string.IsNullOrEmpty(item[1]) && CEO == 1)
                        {
                            Console.WriteLine("There can only be 1 ceo in the organization, skipping ...");
                            continue;
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                x.Dispose();

            }

            foreach (KeyValuePair<string, Emp> kvp in employee)
            {
                if (!string.IsNullOrEmpty(kvp.Value.Manager))
                {
                // This code checks for circular referencing
                    bool doubleLinked = false;
                    foreach (Emp employee in empTree.DepthFirstWalk(kvp.Value).ToArray())
                    {
                        if (employee.Equals(kvp.Value.Manager))
                        {
                            doubleLinked = true;
                            break;
                        }
                    }
                // THis code ensures that employees have only one manager
                    if (empTree.IncomingEdges(kvp.Value).ToArray().Length < 1 && !doubleLinked)
                    {
                        empTree.AddEdge(employee[kvp.Value.Manager], kvp.Value);
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Double linking not allowed");
                    }
                }

            }

        }

    // THis Code returns the salary budget for the manager
        public long Budget(string sal)
        {
            var budget = 0;
            try
            {
                var employeesInPath = empTree.DepthFirstWalk(employee[sal]).GetEnumerator();
                while (employeesInPath.MoveNext())
                {
                    budget += employeesInPath.Current.Salary;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return budget;
        }
    }
 
}