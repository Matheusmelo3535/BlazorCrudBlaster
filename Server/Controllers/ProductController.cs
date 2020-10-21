using Mysql_blazor2.Server;
using Microsoft.AspNetCore.Mvc;
using Mysql_blazor2.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly AppDbContext db;

    public ProductController(AppDbContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> Get()
    {
        var products = await db.Products.ToListAsync();
        return Ok(products);
    }

    

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Post([FromBody] Product product)
    {
        try
        {
            var newProduct = new Product
            {
                nome = product.nome,
                descricao = product.descricao,
                preco = product.preco
            };

            db.Add(newProduct);
            await db.SaveChangesAsync();//INSERT INTO
            return Ok();
        }
        catch (Exception e)
        {
            return View(e);
        }
    }

    [HttpGet]
    [Route("GetId")]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        // procura no banco na tabela products se tem algum Id igual e retorna o produto com todas suas informações
        var produto = await db.Products.SingleOrDefaultAsync(p => p.Id == Convert.ToInt32(id));
        return Ok(produto);
    }

    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Put([FromBody] Product produto)
    {
        if (!ModelState.IsValid){

            return BadRequest(ModelState);

        }

        db.Entry(produto).State = EntityState.Modified;
        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw (ex);
        }
        return NoContent();
    }


    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult <User>> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var produto = await db.Products.FindAsync(id);
        if(produto == null)
        {
            return NotFound();
        }

        db.Products.Remove(produto);
        await db.SaveChangesAsync();
        return Ok(produto);
    }

}