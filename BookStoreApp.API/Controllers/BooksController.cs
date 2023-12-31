﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.Models.Book;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Models.Author;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDBContext _context;
        private readonly IMapper mapper;

        public BooksController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            var booksDtos = await _context.Books
                .Include(q=>q.Author)
                .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
                .ToListAsync();
           // var bookDtos = mapper.Map<IEnumerable<BookReadOnlyDto>>(books);
            return Ok (booksDtos);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
                 var book = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if(book == null)
            {
                return NotFound();
            }
            return Ok (book);

        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }
            var book = await _context.Books.FindAsync(id); 

            if (book == null)
            {
                return NotFound();
            }

            mapper.Map(bookDto, book);

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await BookExistsAync(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto bookDto)
        {
            var book = mapper.Map<Book>(bookDto);

          if (book == null)
          {
              return Problem("Entity set 'BookStoreDBContext.Books'  is null.");
          }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> BookExistsAync(int id)
        {
            return await _context.Books.AnyAsync(e => e.Id == id);
        }
    }
}
