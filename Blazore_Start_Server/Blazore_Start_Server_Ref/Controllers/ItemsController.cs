using Blazore_Start_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazore_Start_Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {


        private readonly ILogger<ItemsController> _logger;
        private readonly ApplicationContext _db;

        public ItemsController(ILogger<ItemsController> logger, ApplicationContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public IAsyncEnumerable<Item> Get()
        {
            return _db.Items.AsAsyncEnumerable();
        }

        [HttpGet("{id}")]
        public async Task<Item> Get(int id)
        {
            return await _db.Items.FindAsync(id);
        } 

        [HttpPost]
        public async Task<IActionResult> Put(Item model)
        {
            Item item =  new Item();
            item.Title = model.Title;
            await _db.Items.AddAsync(item);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            Item item = await  _db.Items.FindAsync(id);
            item.Status = true;
            _db.Items.Update(item);
           await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Item item = await _db.Items.FindAsync(id);
            _db.Items.Remove(item);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
