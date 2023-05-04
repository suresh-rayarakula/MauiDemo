using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly string _connectionString;

        public EmployeeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        // GET: api/Employee
        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Employee_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", "SELECTALL");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Employee> employees = new List<Employee>();

                        while (reader.Read())
                        {
                            Employee employee = new Employee();
                            employee.EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                            employee.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            employee.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            employee.Email = reader.GetString(reader.GetOrdinal("Email"));
                            employee.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            employee.Address = reader.GetString(reader.GetOrdinal("Address"));
                            employee.HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate"));
                            employee.Salary = reader.GetDecimal(reader.GetOrdinal("Salary"));

                            employees.Add(employee);
                        }

                        return employees;
                    }
                }
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Employee_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", "SELECT");
                    command.Parameters.AddWithValue("@EmployeeID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Employee employee = new Employee();
                            employee.EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                            employee.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            employee.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            employee.Email = reader.GetString(reader.GetOrdinal("Email"));
                            employee.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                            employee.Address = reader.GetString(reader.GetOrdinal("Address"));
                            employee.HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate"));
                            employee.Salary = reader.GetDecimal(reader.GetOrdinal("Salary"));

                            return employee;
                        }

                        return NotFound();
                    }
                }
            }
        }

        // POST: api/Employee
        [HttpPost]
        public void PostEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Employee_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", "INSERT");
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);

                    command.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void PutEmployee(int id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Employee_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", "UPDATE");
                    command.Parameters.AddWithValue("@EmployeeID", id);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);

                    command.ExecuteNonQuery();
                }
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("Employee_CRUD", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Action", "DELETE");
                    command.Parameters.AddWithValue("@EmployeeID", id);

                    command.ExecuteNonQuery();
                }
            }
        }



    }
            }
