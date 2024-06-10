namespace LINQExamples_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Select and Where operators - Method Syntax
            //List<Employee> employeeList = Data.GetEmployees();
            //List<Department> departmentList = Data.GetDepartments();

            //var results = employeeList.Select (e => new
            //{
            //    FullName = e.FirstName + " " + e.LastName,
            //    AnnualSalary = e.AnnualSalary,
            //}).Where(e => e.AnnualSalary >= 50000);
            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            //}
            ////Console.ReadKey();
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            // Select and where operators = Query Syntax
            //var results = from emp in employeeList
            //              where emp.AnnualSalary >= 50000
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary
            //              };

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            //}

            //query is not used here due to deferred execution
            //var results = from emp in employeeList.GetHighSalariedEmployees()
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary
            //              };
            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Sandesh",
            //    LastName = "Rijal",
            //    AnnualSalary = 100000.56m,
            //    IsManager = true,
            //    DepartmentId = 2
            //});

            ////Query is executed here when used in interation
            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            //}

            //Immediate Execution Example
            //var results = (from emp in employeeList.GetHighSalariedEmployees()
            //               select new
            //               {
            //                   FullName = emp.FirstName + " " + emp.LastName,
            //                   AnnualSalary = emp.AnnualSalary
            //               }).ToList();
            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Sandesh",
            //    LastName = "Rijal",
            //    AnnualSalary = 100000.56m,
            //    IsManager = true,
            //    DepartmentId = 2
            //});
            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}");
            //}

            //Join Operation Example - Method Syntax
            //var results = departmentList.Join(employeeList, department => department.Id, employee => employee.DepartmentId, (Department, Employee) => new
            //{
            //    FullName = Employee.FirstName + "" + Employee.LastName,
            //    AnnualSalary = Employee.AnnualSalary,
            //    departmentName = Department.LongName
            //});
            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20}{item.AnnualSalary,10}\t {item.departmentName}");
            //}

            //Join Operation Example - Query Syntax
            //var results = from dept in departmentList
            //              join emp in employeeList
            //              on dept.Id equals emp.DepartmentId
            //              select new
            //              {
            //                  FullName = emp.FirstName + "" + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //                  DepartmentName = dept.LongName
            //              };

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName} \t {item.AnnualSalary} \t {item.DepartmentName}"
            //        );
            //}

            //Group Join operator  - method syntax

            //var results = departmentList.GroupJoin(employeeList, dept => dept.Id, emp => emp.DepartmentId, (dept, employeesGroup) => new
            //{
            //    Employees = employeesGroup,
            //    DepartmentName = dept.LongName
            //});

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"Department Name : {item.DepartmentName}");
            //    foreach (var emp in item.Employees)
            //        Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
            //}

            //Group join operator - query syntax
            var results = from dept in departmentList
                          join emp in employeeList
                          on dept.Id equals emp.DepartmentId
                          into employeeGroup
                          select new
                          {
                              Employees = employeeGroup,
                              DepartmentName = dept.LongName
                          };

            foreach (var item in results)
            {
                Console.WriteLine($"Department Name: {item.DepartmentName}");
                foreach (var emp in item.Employees)
                    Console.WriteLine($"\t {emp.FirstName} {emp.LastName}");
            }

            Console.ReadKey();
        }
    }

    public static class EnumberableCollectionExtensionMethods
    {
        public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)

        {
            foreach (Employee emp in employees)
            {
                Console.WriteLine($"Accessing Employee: {emp.FirstName + " " + emp.LastName}");
                if (emp.AnnualSalary >= 50000)
                    yield return emp;
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "Anish",
                LastName = "Shrestha",
                AnnualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 2,
                FirstName = "Ghyamjo",
                LastName = "Lama",
                AnnualSalary = 70000.3m,
                IsManager = true,
                DepartmentId = 2
            };
            employees.Add(employee);

            employee = new Employee
            {
                Id = 3,
                FirstName = "Uttam",
                LastName = "Magaju",
                AnnualSalary = 80000.3m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);

            employee = new Employee
            {
                Id = 4,
                FirstName = "Uttam",
                LastName = "Joshi",
                AnnualSalary = 90000.3m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);

            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);

            return departments;
        }
    }
}