using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BalnearioAC.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalnearioAC.Models;


namespace BalnearioAC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly Conexao _context;

        public EmployeeController(Conexao context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await _context.Employees
                .Include(e => e.User)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("O cadastro não pode ser concluido");
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee.Id > 0 ? Ok(employee) : BadRequest("Erro ao cadastrar funcionário");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest("O funcionário não pode ser nulo ou com ID inválido");
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound("Funcionário não encontrado");
            }

            existingEmployee.Id = employee.Id;
            existingEmployee.IdUser = employee.IdUser;
            existingEmployee.Role = employee.Role;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.AdmissionDate = employee.AdmissionDate;

            await _context.SaveChangesAsync();

            return Ok(existingEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound("Funcionário não encontrado");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok("Funcionário excluído");
        }
    }
}