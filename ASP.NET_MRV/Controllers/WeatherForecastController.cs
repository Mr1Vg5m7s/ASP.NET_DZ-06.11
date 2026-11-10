using Microsoft.AspNetCore.Mvc;
using ASP.NET_MRV.Models;

namespace ASP.NET_MRV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {

        public static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Keyboard" },
            new Item { Id = 2, Name = "Mouse" },
            new Item { Id = 3, Name = "Monitor" }
        };


        //dz za 06.11
        [HttpGet("search")]
        public ActionResult<List<Item>> Search(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return BadRequest("Item not found");
            }
            else if(id <= 0)
            {
                return NotFound("no id");
            }
            else
            {
                return Ok(items.Where(x => x.Id == id).ToList());
            }
        }
        //
        [HttpPost]
        public ActionResult<List<Item>> Create(Item item, string Name)
        {
            items.Add(item);
            if (string.IsNullOrEmpty(Name))
            {
                return BadRequest("Name no found");
            }
            else
            {
                return CreatedAtAction(nameof(Search), new { id = item.Id }, item);
            }
        }
        //
        [HttpPut("{id:int}")]
        public ActionResult<List<Item>> Update2(Item item)
        {
            var selectedItem = items.FirstOrDefault(x => x.Id == item.Id);
            if (selectedItem == null)
            {
                return NotFound();
            }
            else if( selectedItem.Name == "")
            {
                return BadRequest("Name no found");
            }
            else
            {
                return Ok(item);
            }

        }
        //
        [HttpDelete("delete-id={id:int}")]
        public ActionResult<List<Item>> Delete(int id)
        {
            var item = items.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            items.Remove(item);

            return NoContent();
        }
    }

}
