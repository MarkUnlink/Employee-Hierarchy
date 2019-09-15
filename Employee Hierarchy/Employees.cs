using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_Hierarchy
{
    public class Employees
    {
        public List<string> emps = new List<string>();
        public List<KeyValuePair<string, string>> employees = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, int>> salaries = new List<KeyValuePair<string, int>>();

        public Employees(string[] employeeCSV)
        {
            
            var lines = employeeCSV.Select(items => items.Split('\n'));
            var section = from line in lines select (from value in line select value);

            int CEO = 0, num=0;

            foreach (var i in section)
            {

                var x = i.GetEnumerator();
                while (x.MoveNext())
                {
                    try
                    {
                        var item = x.Current.Split(',');
                    //copies a record of all employees to list
                        emps.Add(item[0]);

                    // THis code identifies only 1 CEO and validates that there isn't another null entry
                        if (string.IsNullOrEmpty(item[0]))
                        {
                            Console.WriteLine("ERROR: Employee cannot be empty !");
                            break;
                        }
                        else if (string.IsNullOrEmpty(item[1]) && CEO == 1)
                        {
                            Console.WriteLine("ERROR: There can only be 1 CEO !");
                            break;
                        }

                    // This code verifies that the salaries are only integers

                        if (int.TryParse(item[2], out num))
                        {
                            try
                            {
                                salaries.Add(new KeyValuePair<string, int>(item[1], int.Parse(item[2])));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Salaries must be Integers !");
                            break;
                        }

                        foreach (var keyValue in employees)
                        {
                            string k = keyValue.Key;
                            string v = keyValue.Value;

                        /* THis code ensures that the employee does NOT report to more than 1 manager,
                         * by ensuring the same employee doesn't appear more than once.
                        */

                            if (k == item[0])
                            {
                                Console.WriteLine("ERROR: Employee already has a manager/ Exists !");
                                break;
                            }

                        // This code checks for circular reference and stops
                            if (item[0]== v)
                            {
                                foreach (var kv in employees)
                                {
                                    string a = kv.Key;
                                    string b = kv.Value;

                                    if (k == b)
                                    {
                                        if (item[1]==a)
                                        {
                                            Console.WriteLine("ERROR: Circular reference not allowed");
                                            break;
                                        }
                                    }
                                }
                            }

                        }

                    // This checks if the Manager is also an employee
                        if (emps.Contains(item[1]))
                            {
                                employees.Add(new KeyValuePair<string, string>(item[0], item[1]));
                            }

                        if (string.IsNullOrEmpty(item[1]) && CEO < 1)
                            {
                            
                                employees.Add(new KeyValuePair<string, string>(item[0], null));
                                CEO++;
                            }

                        else
                        {
                            //Console.WriteLine("ERROR: Manager MUST be an employee !");
                            break;
                        }

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

    // THis method adds salaries from the specified manager
        public long Budget(string manager)
        {
            var budget = 0;
            try
            {
                foreach (var sal in salaries)
                {
                    string y = sal.Key;
                    int z = sal.Value;

                    if (manager == y)
                    {
                        budget += z;
                    }
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
