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

    


}