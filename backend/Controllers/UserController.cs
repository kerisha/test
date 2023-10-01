using Microsoft.AspNetCore.Mvc;
using data;
using data.models;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {

    }

    // GET: /User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        using(var _context = new UserContext()){
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }

    // GET: /User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        using(var _context = new UserContext()){
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }  
    }

    // POST: /User
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        using(var _context = new UserContext()){
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        
    }

    // PUT: /User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        using(var _context = new UserContext()){
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id, _context))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        return NoContent();
    }

    // DELETE: /User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        using(var _context = new UserContext()){
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    private bool UserExists(int id, UserContext userContext)
    {
        return userContext.Users.Any(e => e.Id == id);
    }

}
