using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shop_app.Models;
using System.Security.Claims;

namespace shop_app.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class APIOrderController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public APIOrderController(ProductContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Order model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User is not authenticated");
            }

            // Призначаємо користувача замовлення
            model.UserId = userId;
            model.OrderDate = DateTime.UtcNow;

            // Додаємо замовлення до бази даних
            _context.Orders.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order created successfully", orderId = model.Id });
        }
    }
}
