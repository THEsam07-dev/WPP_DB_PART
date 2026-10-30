using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WPP_DB_PART
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionSTRING = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION");

            if (string.IsNullOrEmpty(connectionSTRING))
            {
                Console.WriteLine("Ошибка: Переменная PRACTICE_DB_CONNECTION не найдена.");
                return;
            }

            Console.WriteLine("+сотрудник(тест)");
            await AddWorker(connectionSTRING, "Александр Иванов", 25000, 1);
            Console.WriteLine("проверка списка сотрудников(тест)");
            await PrintALLWorkers(connectionSTRING);

            static async Task AddWorker(string connectionSTRING, string nameW, decimal salaryW, int deptID)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionSTRING))
                    {
                        await connection.OpenAsync();

                        string query = @"
                        INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId) 
                        VALUES (@name, @salary, GETDATE(), @deptId)";

                        using (SqlCommand sql = new SqlCommand(query, connection))
                        {
                            sql.Parameters.AddWithValue("@name", nameW);
                            sql.Parameters.AddWithValue("@salary", salaryW);
                            sql.Parameters.AddWithValue("@deptId", deptID);

                            int rowsAffected = await sql.ExecuteNonQueryAsync();
                            Console.WriteLine($"Успех: Добавлен сотрудник '{nameW}'. Затронуто строк: {rowsAffected}");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Ошибка SQL Server при добавлении: {ex.Message}");
                }
            }

            static async Task PrintALLWorkers(string connectionSTRING)
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionSTRING))
                {
                    await sqlConnection.OpenAsync();
                    string query = "SELECT TOP 5 EmployeeName, Salary FROM Employees ORDER BY EmployeeId DESC";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Console.WriteLine($"{reader.GetString(0)} - {reader.GetDecimal(1)} руб.");
                        }
                    }
                }
            }
        }
    }
}