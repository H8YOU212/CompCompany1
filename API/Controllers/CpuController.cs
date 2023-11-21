using CompCompany1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompCompany1.Controllers
{
    [ApiController]
    [Route("/cpus")]
    public class CpuController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            CompContext compContext = new CompContext();
            return Ok(compContext.Cpus);
        }

        [HttpPost]
        public IActionResult Add(Cpu cpu)
        {
            CompContext compContext = new CompContext();
            compContext.Cpus.Add(cpu);
            compContext.SaveChanges();
            return Ok(cpu);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetId(int Id)
        {
            CompContext compContext = new CompContext();
            Cpu? cpu = compContext.Cpus.FirstOrDefault(x => x.Id == Id);
            if (cpu == null) { return NotFound(); }
            return Ok();
        }
        [HttpPost]
        public IActionResult Update(Cpu cpu)
        {
            CompContext compContext = new CompContext();
            compContext.Cpus.Update(cpu);
            return Ok(cpu);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int Id)
        {
            CompContext compContext = new CompContext();
            Cpu? cpu = compContext.Cpus.FirstOrDefault(x => x.Id == Id);
            if (Id == null) { return NotFound(); }
            compContext.Cpus.Remove(cpu);
            compContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(Cpu cpu)
        {
            var db = new CompContext();
            db.Cpus.Update(cpu);
            db.SaveChanges();
            return Ok(cpu);
        }
    }
}
