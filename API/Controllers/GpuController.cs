using CompCompany1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompCompany1.Controllers
{
    [ApiController]
    [Route("/gpus")]
    public class GpuController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            var db = new CompContext();
            var gpu = db.Gpus.SingleOrDefault(s => s.Id == id);
            if (gpu == null)
                return NotFound();
            return Ok(gpu);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var db = new CompContext();
            return Ok(db.Gpus);
        }
        [HttpPost]
        public IActionResult Add(Gpu gpu)
        {
            var db = new CompContext();
            db.Gpus.Add(gpu);
            db.SaveChanges();
            return Ok(gpu);
        }
        [HttpPut]
        public IActionResult Edit(Gpu gpu)
        {
            var db = new CompContext();
            db.Gpus.Update(gpu);
            db.SaveChanges();
            return Ok(gpu);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var db = new CompContext();
            var gpu = db.Gpus.SingleOrDefault(s => s.Id == id);
            if (gpu == null)
                return NotFound();
            return Ok(gpu);
        }

        public IActionResult Update(Gpu gpu)
        {
            CompContext compContext = new CompContext();
            compContext.Gpus.Update(gpu);
            return Ok(gpu);
        }
    }
}
