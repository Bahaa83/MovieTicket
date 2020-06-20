using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient.Memcached;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    public class MovieController:Controller
    {
        public Context _context;
        public MovieController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Movie> Get()
        {
            return _context.movies.ToList();
        }

        //GET:api/movie/ID
        [HttpGet("{ID}")]
        public async Task <ActionResult<Movie>> GetMovieWithId(int id) 
        {
            var movie = await _context.movies.FindAsync(id);
            if(movie == null) 
            {
                return NotFound();
            }
            return movie;
        }

        //PUT:api/movie/ID
        [HttpPut("{ID}")]
        public async Task<IActionResult> EditMovie(int id, Movie movie) 
        {
            if(id != movie.ID) 
            {
                return BadRequest();
            }
            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExist(id)) 
                {
                    return NotFound();
                }
                else 
                {
                    throw;
                }
                
            }
            return NoContent();
        }


        public bool MovieExist(int id) 
        {
           
            if(_context.movies.Any(m => m.ID == id)) 
            {
                return true;
            }
            return false;
        }

        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteMovie(int id) 
        {
            var movie = await _context.movies.FindAsync(id);

            if(movie == null) 
            {
                return NotFound(); 
            }

            _context.movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        /* 
       
              * */
       /* public IActionResult Index()
        {
            return View();
        }
        */
        [HttpPost]
        
        public IActionResult PostMovie([FromBody] Movie movie) 
        {
            
            if (!ModelState.IsValid) 
            {
                return BadRequest("Not Valid");
            }


            _context.movies.Add(movie);
            _context.SaveChanges();

            return Ok("Success!");
        }

    }
}
