using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WPP_DB_PART
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION");

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Ошибка: Переменная PRACTICE_DB_CONNECTION не найдена.");
                return;
            }

            Console.WriteLine("=== ОТЧЕТ ПО ОТДЕЛАМ ===");
            await GenerateDepartmentReport(connectionString);

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadLine();
        }

        static async Task GenerateDepartmentReport(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = @"
                        SELECT d.DepartmentName, 
                               COUNT(e.EmployeeId) AS EmpCount, 
                               AVG(e.Salary) AS AvgSalary 
                        FROM Departments d 
                        LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId 
                        GROUP BY d.DepartmentName
                        ORDER BY d.DepartmentName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("В базе нет данных для отчета.");
                            return;
                        }

                        Console.WriteLine("                                                  ");
                        Console.WriteLine($"{"Отдел",-20} | {"Сотрудников",12} | {"Ср. зарплата",12}");
                        Console.WriteLine("                                                  ");

                        while (await reader.ReadAsync())
                        {
                            string deptName = reader.GetString(0);

                            int empCount = reader.GetInt32(1);

                            string avgSalaryStr;
                            if (reader.IsDBNull(2))
                            {
                                avgSalaryStr = "N/A";
                            }
                            else
                            {
                                decimal avgSalary = reader.GetDecimal(2);
                                avgSalaryStr = Math.Round(avgSalary, 2).ToString("F2") + " руб.";
                            }
                            Console.WriteLine($"{deptName,-20} | {empCount,12} | {avgSalaryStr,12}");
                        }
                        Console.WriteLine("                                                         ");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Ошибка SQL Server: {ex.Message}");
            }
        }
    }
}