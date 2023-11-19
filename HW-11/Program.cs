using System;
using System.Linq;

interface IEmployee
{
    string ToString();
}

struct Employee : IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public char Gender { get; set; }

    public override string ToString()
    {
        return $"Name: {FirstName} {LastName}, Hire Date: {HireDate:d}, Position: {Position}, Salary: {Salary:C}, Gender: {Gender}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the number of employees: ");
        int numberOfEmployees = int.Parse(Console.ReadLine());

        Employee[] employees = new Employee[numberOfEmployees];

        // Заполнение массива сотрудников
        for (int i = 0; i < numberOfEmployees; i++)
        {
            employees[i] = ReadEmployeeFromConsole();
        }

        // a. Вывести полную информацию обо всех сотрудниках
        DisplayAllEmployees(employees);

        // b. Вывести полную информацию о сотрудниках, выбранной должности
        Console.WriteLine("Enter the position to display employees: ");
        string selectedPosition = Console.ReadLine();
        DisplayEmployeesByPosition(employees, selectedPosition);

        // c. Найти менеджеров с зарплатой больше средней зарплаты клерков
        DisplayManagersAboveAverageClerkSalary(employees);

        // d. Вывести информацию о сотрудниках, принятых позже определенной даты
        Console.WriteLine("Enter the hire date to filter employees (MM/dd/yyyy): ");
        DateTime hireDateFilter = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);
        DisplayEmployeesHiredAfterDate(employees, hireDateFilter);

        // e. Вывести информацию о сотрудниках по полу
        Console.WriteLine("Enter the gender to filter employees (M/F), or press Enter for all: ");
        char genderFilter = Console.ReadLine().ToUpper().FirstOrDefault();
        DisplayEmployeesByGender(employees, genderFilter);
    }

    // Методы для выполнения задач
    static void DisplayAllEmployees(Employee[] employees)
    {
        Console.WriteLine("\nAll Employees:");
        foreach (var employee in employees)
        {
            Console.WriteLine(employee.ToString());
        }
    }

    static void DisplayEmployeesByPosition(Employee[] employees, string position)
    {
        Console.WriteLine($"\nEmployees with Position '{position}':");
        foreach (var employee in employees)
        {
            if (employee.Position.Equals(position, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(employee.ToString());
            }
        }
    }

    static void DisplayManagersAboveAverageClerkSalary(Employee[] employees)
    {
        decimal averageClerkSalary = employees.Where(e => e.Position.Equals("Clerk", StringComparison.OrdinalIgnoreCase))
                                             .Average(e => e.Salary);

        var managersAboveAverage = employees.Where(e => e.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase)
                                                    && e.Salary > averageClerkSalary)
                                           .OrderBy(e => e.LastName)
                                           .ToArray(); // Добавлено для преобразования в массив

        Console.WriteLine("\nManagers with Salary Above Average Clerk Salary:");
        foreach (var manager in managersAboveAverage)
        {
            Console.WriteLine(manager.ToString());
        }
    }

    static void DisplayEmployeesHiredAfterDate(Employee[] employees, DateTime hireDateFilter)
    {
        var filteredEmployees = employees.Where(e => e.HireDate > hireDateFilter)
                                         .OrderBy(e => e.LastName);

        Console.WriteLine($"\nEmployees Hired After {hireDateFilter:d}:");
        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee.ToString());
        }
    }

    static void DisplayEmployeesByGender(Employee[] employees, char genderFilter)
    {
        var filteredEmployees = employees;

        if (genderFilter == 'M' || genderFilter == 'F')
        {
            filteredEmployees = employees.Where(e => e.Gender == genderFilter)
                                         .OrderBy(e => e.LastName)
                                         .ToArray(); // Добавлено для преобразования в массив
        }

        Console.WriteLine($"\nEmployees{(genderFilter == 0 ? "" : " (" + (genderFilter == 'M' ? "Male" : "Female") + ")")}:\n");
        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee.ToString());
        }
    }

    static Employee ReadEmployeeFromConsole()
    {
        Console.WriteLine("\nEnter employee details:");

        Console.Write("First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Hire Date (MM/dd/yyyy): ");
        DateTime hireDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", null);

        Console.Write("Position: ");
        string position = Console.ReadLine();

        Console.Write("Salary: ");
        decimal salary = decimal.Parse(Console.ReadLine());

        Console.Write("Gender (M/F): ");
        char gender = Console.ReadLine().ToUpper().FirstOrDefault();

        return new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            HireDate = hireDate,
            Position = position,
            Salary = salary,
            Gender = gender
        };
    }
}
