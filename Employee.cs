using System;
using System.IO;
using System.Text;

namespace ProjectConnectDB
{
    public class Employee
    {
        static string directory = @"C:\EdsineProjects\project_payroll\ProjectPayroll\";
        static string fileName = "employeedata.csv";
        public int EmpNo {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Gender {get; set;}
        public string Department {get; set;}
        public string EmailAddress {get; set;}
        public double GrossSalary {get; set;}
        public double NetSalary {get; set;}
        public double Tax {get; set;}
        public double Bonuses {get; set;}
        public double Pension {get; set;}
        public int TotalWorkingHours {get; set;}
        
        public void calculateNetSalary() {
            Tax = Math.Round(0.075 * GrossSalary, 2);
            Pension = Math.Round(0.075 * GrossSalary, 2);
            NetSalary = Math.Round((GrossSalary + Bonuses) - Tax - Pension, 2);
        }

        public void calculateBonuses() {
            if (TotalWorkingHours >= 180) {
                Bonuses = 0.05 * GrossSalary;
            } else if (TotalWorkingHours <= 160) {
                Bonuses = -(0.02 * GrossSalary);
            }
        }

        public void storeEmployeeInfo() {

            Console.Write("Input the employee's first Name: ");
            FirstName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Input the employee's Last Name: ");
            LastName = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Input the employee's Gender: ");
            Gender = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Input the employee's Email Address: ");
            EmailAddress = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Input the employee's Department: ");
            Department = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Input the employee's gross salary: ");
            GrossSalary = double.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Input the employee's total working hours: ");
            TotalWorkingHours = int.Parse(Console.ReadLine());
            Console.WriteLine();


            calculateBonuses();
            calculateNetSalary();
            
            using (var db = new ConnectContext()) 
            {
                var employee = new Employee {
                    FirstName = FirstName, 
                    LastName = LastName, 
                    Gender = Gender, 
                    Department = Department, 
                    EmailAddress = EmailAddress,
                    GrossSalary = GrossSalary,
                    NetSalary = NetSalary,
                    Tax = Tax,
                    Bonuses = Bonuses,
                    Pension = Pension,
                    TotalWorkingHours = TotalWorkingHours
                };
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            Console.Write("Data Recorded Successfully \n");
        }

        public void updateEmployeeInfo() {
            using (var db = new ConnectContext()) {

                var employee = (from d in db.Employees Where d.FirstMidName == "Ali" select d).Single();
                employee.LastName = "Aslam";
                db.SaveChanges();
            }
        }

        public void deleteEmployeeInfo() {

        }

        public void sendEmailToEmployees() {

        }

        public void sendEmailToEmployee() {

        }

        public void generatePayRoll() {
            double totalSalaryPaid = 1, TaxCollected = 1, PensionCollected = 1, BonusesPaid = 1;
            string[] lines = File.ReadAllLines(directory + fileName);
            Console.Write("---------------------------------------------------");
            Console.WriteLine("---------------------------------------------------");
            Console.Write(String.Format("{0, -15}","No."));
            Console.Write(String.Format("{0, -15}", "First Name"));
            Console.Write(String.Format("{0, -15}", "Last Name"));
            Console.Write(String.Format("{0, -15}", "Department"));
            Console.Write(String.Format("{0, -15}", "Gross Salary"));
            Console.Write(String.Format("{0, -15}", "Tax"));
            Console.Write(String.Format("{0, -15}", "Pension"));
            Console.Write(String.Format("{0, -15}", "Bonuses"));
            Console.Write(String.Format("{0, -15}", "Net Salary"));
            Console.Write(String.Format("{0, -15}", "TW Hours"));
            Console.WriteLine(String.Format("{0, -15}", "Email"));
            Console.Write("---------------------------------------------------");
            Console.WriteLine("---------------------------------------------------");
            foreach(string line in lines) {
                string[] columns = line.Split(',');
                foreach(string column in columns) 
                {
                    Console.Write(String.Format("{0, -15}", $"{column}"));
                }
                Console.WriteLine();
                Console.Write("---------------------------------------------------");
                Console.WriteLine("---------------------------------------------------");
                totalSalaryPaid += double.Parse(columns[4]);
                TaxCollected += double.Parse(columns[5]);
                PensionCollected += double.Parse(columns[6]);
                BonusesPaid += double.Parse(columns[7]);
            }
            Console.WriteLine();
            Console.Write("---------------------------------------------------");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Total Salary: {totalSalaryPaid}".PadRight(20));
            Console.WriteLine($"Total Tax: {TaxCollected}".PadRight(20));
            Console.WriteLine($"Total Pension: {PensionCollected}".PadRight(20));
            Console.WriteLine($"Total Bonuses: {BonusesPaid}".PadRight(20));
            Console.Write("---------------------------------------------------");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine();
        }

        public void generatePaySlip() {
            Console.Write("Please input the employee's number: ");
            string EmpNo = Console.ReadLine();
            string[] lines = File.ReadAllLines(directory + fileName);
            Console.WriteLine("Input the employer's name: ");
            string employer = Console.ReadLine();
            foreach(string line in lines) {
                if(line.Contains(EmpNo)) {
                    string[] columns = line.Split(',');
                    Console.WriteLine("\n");
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("PAYSLIP");
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine($"Employer's name: {employer}");
                    Console.WriteLine(String.Format("{0, -10} {1, -10}", $"Employee's name: {columns[1]} {columns[2]}", $"| Hours Worked: {columns[9]}"));
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("EARNINGS");
                    Console.WriteLine(String.Format("{0, -10} {1, -10}", "Salary: ", columns[4]));
                    Console.Write(String.Format("{0, -10} {1, -10}", "Bonuses: ", double.Parse(columns[7])));
                    Console.WriteLine(String.Format("{0, -10} {1, -10}", "Gross Payment: ", double.Parse(columns[4]) + double.Parse(columns[7])));
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("DEDUCTIONS");
                    Console.WriteLine(String.Format("{0, -10} {1, -10}", "Tax: ", columns[5]));
                    Console.Write(String.Format("{0, -10} {1, -10}", "Pension: ", columns[6]));
                    Console.WriteLine(String.Format("{0, -10} {1, -10}" ,"Total Deductions: ", double.Parse(columns[5]) + double.Parse(columns[6])));
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine(String.Format("{0, -10} {1, -10}", "Net Payment: ", columns[8]));
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine();

                    break;
                }
                else {
                    Console.WriteLine("No employee found. Please check employee's number and try again"); 
                }
            }

        }


    }
}