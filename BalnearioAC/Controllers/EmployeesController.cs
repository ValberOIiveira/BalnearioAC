using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BalnearioAC.Database;
using BalnearioAC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BalnearioAC.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EmployeesController : ControllerBase
    {
        private readonly Conexao _context;

        public EmployeesController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Funcionario nao pode ser nulo");
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee.Id > 0 ? Ok(employee) : BadRequest("Erro ao cadastrar funcionario");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest("Funcionario nao pode ser nulo ou ID invalido");
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound("Funcionario nao encontrado");
            }
            existingEmployee.IdUser = employee.IdUser;
            existingEmployee.Role = employee.Role;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.AdmissionDate = employee.AdmissionDate;
            existingEmployee.User = employee.User;

            await _context.SaveChangesAsync();

            return Ok(existingEmployee);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Funcionario nao encontrado");
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok("Funcionario removido com sucesso");

        }
    }
}