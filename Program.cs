using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;

namespace C43_G03_OOP02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 3&4 Employees array and sorting it..

            Employee[] employees = new Employee[3];

            Console.WriteLine("Enter details for 3 employees:");

            for (int i = 0; i < employees.Length; i++)
            {
                try
                {
                    Console.WriteLine($"\nEmployee {i + 1} Details:"); // Consider Admin who will input el data, thet does not violate security.

                    Console.Write("Enter ID: ");
                    int id = int.Parse(Console.ReadLine());

                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter Gender (M/F): ");
                    string gender = Console.ReadLine();

                    Console.Write("Enter Security Level (Guest/Developer/Secretary/DBA): ");
                    SecurityLevel securityLevel = (SecurityLevel)Enum.Parse(typeof(SecurityLevel), Console.ReadLine(), true);

                    Console.Write("Enter Salary: ");
                    decimal salary = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter Hiring Date (Day): ");
                    int day = int.Parse(Console.ReadLine());

                    Console.Write("Enter Hiring Date (Month): ");
                    int month = int.Parse(Console.ReadLine());

                    Console.Write("Enter Hiring Date (Year): ");
                    int year = int.Parse(Console.ReadLine());

                    HiringDate hiringDate = new HiringDate(day, month, year);

                    employees[i] = new Employee(id, name, gender, securityLevel, salary, new DateTime(year, month, day));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please re-enter employee details.");
                    i--;
                }
            }

            // the sorting function is exisiting in region for ques 1.

            Console.WriteLine("\nEmployee Details Before Sorting:");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
                Console.WriteLine("*******************************");
            }

            Console.WriteLine("\nAfter Sorting:");
            Array.Sort(employees, Employee.CompareByHiringDate); // CompareByHiringDate exist within class Emplyee.

            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee);
            }

            int boxingCount = 0;
            foreach (Employee employee in employees)
            {
                object obj = employee;    // Boxing 
                Employee unboxed = (Employee)obj;    // Unboxing
                boxingCount++;    
            }

            Console.WriteLine("************************");
            Console.WriteLine("************************");
            Console.WriteLine($"Boxing and unboxing occurred {boxingCount} times.");
        }

            #endregion

            #region 1. Design and implement a Class for the employees 

        internal enum SecurityLevel
        {
            Guest,Developer,Secretary,DBA
        }

        internal class Employee
        {
            private int ID { get; set; }
            private string Name { get; set; }
            private string Gender { get; set; }
            private SecurityLevel SecurityPrivilege { get; set; }
            private decimal Salary { get; set; }
            private DateTime HireDate { get; set; }

            public Employee(int id, string name, string gender, SecurityLevel securityPrivilege, decimal salary, DateTime hireDate)
            {
                this.ID = id;
                this.Name = name;
                this.Gender = gender;
                this.SecurityPrivilege = securityPrivilege;
                this.Salary = salary;
                this.HireDate = hireDate;
            }

            public static int CompareByHiringDate(Employee e1, Employee e2) // used in ques 3.
            {
                return e1.HireDate.CompareTo(e2.HireDate);
            }
            public override string ToString()
            {
                return $"ID: {ID}, Name: {Name}, Gender: {Gender}, " +
                       $"Security Level: {SecurityPrivilege}, " +
                       $"Salary: {string.Format("{0:C}", Salary)}, Hire Date: {HireDate:yyyy-MM-dd}";
            }
        }

        #endregion

        #region 2. Develop a Class to represent the Hiring Date Data:

        internal class HiringDate
        {
            private int day;
            private int month;
            private int year;

            public HiringDate(int day, int month, int year)
            {
                if (IsValidDate(day, month, year))
                {
                    this.day = day;
                    this.month = month;
                    this.year = year;
                }
                else
                {
                    throw new ArgumentException("Invalid date. Please provide a valid day, month, and year.");
                }
            }

            public int Day
            {
                get => day;
                set
                {
                    if (IsValidDate(value, month, year))
                    {
                        day = value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid day.");
                    }
                }
            }

            public int Month
            {
                get => month;
                set
                {
                    if (IsValidDate(day, value, year))
                    {
                        month = value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid month.");
                    }
                }
            }

            public int Year
            {
                get => year;
                set
                {
                    if (IsValidDate(day, month, value))
                    {
                        year = value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid year.");
                    }
                }
            }

            public void DisplayHiringDate()
            {
                Console.WriteLine($"Hiring Date: {day:D2}/{month:D2}/{year}");
            }

            private static bool IsValidDate(int day, int month, int year)
            {
                if (year < 1950 || year > 2025) return false;

                if (month < 1 || month > 12) return false;
                int daysInMonth = DateTime.DaysInMonth(year, month);

                return day >= 1 && day <= daysInMonth;
            }
        }

        #endregion

        #region 5-Design a program for a library management system where:

        internal class Book
        {
            private string Title { get; set; }
            private string Author { get; set; }
            private string ISBN { get; set; }

            public Book(string title, string author, string isbn)
            {
                this.Title = title;
                this.Author = author;
                this.ISBN = isbn;
            }

            public virtual string ToString()
            {
                return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}";
            }
        }

        internal class EBook : Book
        {
            public double FileSize { get; set; }

            public EBook(string title, string author, string isbn, double fileSize)
                : base(title, author, isbn)
            {
                FileSize = fileSize;
            }

            public override string ToString()
            {
                return base.ToString() + $", File Size: {FileSize} MB (EBook)";
            }
        }

        internal class PrintedBook : Book
        {
            public int PageCount { get; set; }

            public PrintedBook(string title, string author, string isbn, int pageCount)
                : base(title, author, isbn)
            {
                PageCount = pageCount;
            }

            public override string ToString()
            {
                return base.ToString() + $", Page Count: {PageCount} (Printed Book)";
            }
        }

        /// In this example, inheritance simplifies the design by eliminating code redundancy. 
        /// We have three classes: `Book`, `EBook`, and `PrintedBook`. 
        /// By using inheritance, we don't need to re-define common attributes like `Title`, `Author`, `IsAvailable`, and `ISBN` for `EBook` and `PrintedBook`, 
        /// as they automatically inherit these properties from the base class `Book`. 

        /// Each derived class can then introduce its own unique attributes, such as `FileSize` for `EBook` and `PageCount` for `PrintedBook`. 
        /// Moreover, inheritance, in conjunction with abstraction, ensures that every child class contains necessary functions (e.g., `Display`) 
        /// and enforces the implementation of these functions according to the specific class.


        #endregion

    }
}


