using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFitnessBud.Data;
using MyFitnessBud.Models;

namespace MyFitnessBud.Controllers
{
    [ApiController]
    [Route("api/favorites")]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public FavoritesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public class SaveFavoriteRequest
        {
            public string LoggedInUser { get; set; }
            public string ProductCode { get; set; }
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string Allergens { get; set; }
            public bool IngredientsAvailable { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> SaveFavorite([FromBody] SaveFavoriteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.LoggedInUser) ||
                string.IsNullOrWhiteSpace(request.ProductCode) ||
                string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest(new { message = "Missing required favorite data." });
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.LoggedInUser);

            if (user == null)
            {
                return NotFound(new { message = "User not found in database." });
            }

            var existingCache = await _db.SnackCaches
                .FirstOrDefaultAsync(s => s.ProductCode == request.ProductCode);

            if (existingCache == null)
            {
                existingCache = new SnackCache
                {
                    ProductCode = request.ProductCode,
                    Name = request.Name,
                    ImageUrl = request.ImageUrl,
                    Allergens = request.Allergens,
                    IngredientsAvailable = request.IngredientsAvailable,
                    LastFetched = DateTime.UtcNow
                };

                _db.SnackCaches.Add(existingCache);
            }
            else
            {
                existingCache.Name = request.Name;
                existingCache.ImageUrl = request.ImageUrl;
                existingCache.Allergens = request.Allergens;
                existingCache.IngredientsAvailable = request.IngredientsAvailable;
                existingCache.LastFetched = DateTime.UtcNow;
            }

            var existingFavorite = await _db.Favorites
                .FirstOrDefaultAsync(f =>
                    f.UserId == user.UserId &&
                    f.ProductCode == request.ProductCode);

            if (existingFavorite == null)
            {
                _db.Favorites.Add(new Favorite
                {
                    UserId = user.UserId,
                    ProductCode = request.ProductCode
                });
            }

            await _db.SaveChangesAsync();
            return Ok(new { message = "Favorite saved." });
        }

        [HttpDelete("{productCode}")]
        public async Task<IActionResult> RemoveFavorite(string productCode, [FromQuery] string loggedInUser)
        {
            if (string.IsNullOrWhiteSpace(loggedInUser))
                return BadRequest(new { message = "Missing user." });

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loggedInUser);

            if (user == null)
                return NotFound(new { message = "User not found." });

            var favorite = await _db.Favorites
                .FirstOrDefaultAsync(f => f.UserId == user.UserId && f.ProductCode == productCode);

            if (favorite == null)
                return NotFound(new { message = "Favorite not found." });

            _db.Favorites.Remove(favorite);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Favorite removed." });
        }

        [HttpGet]
        public async Task<IActionResult> GetFavorites([FromQuery] string loggedInUser)
        {
            if (string.IsNullOrWhiteSpace(loggedInUser))
                return BadRequest(new { message = "Missing user." });

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == loggedInUser);

            if (user == null)
                return NotFound(new { message = "User not found." });

            var favorites = await (
                from f in _db.Favorites
                join s in _db.SnackCaches on f.ProductCode equals s.ProductCode
                where f.UserId == user.UserId
                orderby f.SavedAt descending
                select new
                {
                    s.ProductCode,
                    s.Name,
                    s.ImageUrl,
                    s.Allergens,
                    s.IngredientsAvailable,
                    f.SavedAt
                }
            ).ToListAsync();

            return Ok(favorites);
        }
    }
}