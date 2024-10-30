using LikeButtonApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LikeController : ControllerBase
{
    private readonly LikeDbContext _context;

    public LikeController(LikeDbContext context)
    {
        _context = context;
    }

    // Get the current number of likes
    [HttpGet("{id}/likes")]
    public async Task<IActionResult> GetLikes(int id)
    {
        var likeButton = await _context.LikeButtons.FindAsync(id);
        if (likeButton == null) return NotFound();

        return Ok(new LikesResponse { Likes = likeButton.Likes });
    }

    // Increment likes by 1
    [HttpPost("{id}/likes")]
    public async Task<IActionResult> PostLike(int id)
    {
        var likeButton = await _context.LikeButtons.FindAsync(id);
        if (likeButton == null) return NotFound();

        likeButton.Likes += 1;
        await _context.SaveChangesAsync();

        return Ok(new LikesResponse { Likes = likeButton.Likes });
    }
}
