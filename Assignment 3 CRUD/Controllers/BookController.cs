﻿using Assignment_3_CRUD___Model.Models;
using Assignment_3_CRUD___Model.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Assignment_3_CRUD___Model.Controllers
{
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        // Display all books
        [HttpGet]
        public IActionResult Index()
        {
            return View(_bookRepository.GetAllBooks());
        }

        // Display book details
        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return book != null ? View(book) : NotFound();
        }

        //Show create form
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        //Add a new book
        [HttpPost("Create")]
        public IActionResult Create(Book newBook)
        {
            if (!ModelState.IsValid) return View(newBook);

            _bookRepository.AddBook(newBook);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Show edit form
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var book = FindOrFail(id);
            return book != null ? View(book) : NotFound();
        }

        // ✅ Update book details
        [HttpPost("Edit/{id}")]
        public IActionResult Edit(int id, Book updatedBook)
        {
            if (!ModelState.IsValid) return View(updatedBook);

            var book = FindOrFail(id);
            if (book == null) return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.PublishedDate = updatedBook.PublishedDate;
            book.Genre = updatedBook.Genre;
            book.Availability = updatedBook.Availability;

            _bookRepository.UpdateBook(book);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Show delete confirmation
        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var book = FindOrFail(id);
            return book != null ? View(book) : NotFound();
        }

        // ✅ Confirm and delete book
        [HttpPost("Delete/{id}")]
        public IActionResult ConfirmDelete(int id)
        {
            var book = FindOrFail(id);
            if (book == null) return NotFound();

            _bookRepository.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }

        // 🔹 **Helper Method**: Find book or return null
        private Book FindOrFail(int id) => _bookRepository.GetBookById(id);
    }
}
