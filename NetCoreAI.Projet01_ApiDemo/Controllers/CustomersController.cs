using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Projet01_ApiDemo.Context;
using NetCoreAI.Projet01_ApiDemo.Entities;

namespace NetCoreAI.Projet01_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CustomersList()
        {
            var value = _context.Customers.ToList();
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Müşteri ekleme işlemi başarılı");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var result = _context.Customers.Find(id);
            _context.Customers.Remove(result);
            _context.SaveChanges();
            return Ok("Müşteri başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var result = _context.Customers.Find(id);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return Ok("Müşteri başarıyla güncellendi");
        }
    }
}
