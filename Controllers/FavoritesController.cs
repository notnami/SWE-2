// Controllers/FavoritesController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFitnessBudAPI.Data;
using MyFitnessBudAPI.Models;

namespace MyFitnessBudAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoritesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequest request)
        {
            var user = await _userManager.GetUserAsync(User);

            var favorite = new Favorite
            {
                OpenFoodFactsId = request.OpenFoodFactsId,
                UserId = user.Id
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return Created("", new { message = "Favorite added" });
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites()
        {
            var user = await _userManager.GetUserAsync(User);

            var favorites = await _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Select(f => new
                {
                    f.Id,
                    f.OpenFoodFactsId,
                    f.CreatedAt
                })
                .ToListAsync();

            return Ok(favorites);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
                return NotFound();

            if (favorite.UserId != user.Id)
                return Forbid();

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Favorite removed" });
        }
    }

    public class FavoriteRequest
    {
        public string OpenFoodFactsId { get; set; }
    }
}