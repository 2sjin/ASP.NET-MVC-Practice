using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;

namespace LibraryApp.Controllers {
    public class HomeController : Controller {
        private readonly LibraryAppContext _context;

        public HomeController(LibraryAppContext context) {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index() {
            // 페이징(Paging)
            int listCount = 5;      // 한 번에 표시할 레코드 수
            int pageNum = 1;        // 현재 페이지 번호

            // 쿼리 스트링을 배열 형태로 저장
            // 예) ["?page", 1]
            string[] queryStr = Request.QueryString.ToString().Split("=");  

            // 쿼리스트링에 저장된 현재 페이지를 정수 형태로 저장
            if (queryStr[0] == "?page" && queryStr[1] != null)
                pageNum = Convert.ToInt32(queryStr[1]);

            // 페이지 정보를 뷰에 전달
            ViewBag.PageNum = pageNum;
            ViewBag.PageNumMax = (int) ((_context.Book.Count() - 1) / listCount) + 1;

            // listCount개 만큼만 레코드 출력
            int startNum = (pageNum - 1) * listCount;
            var books = _context.Book.OrderBy(x => x.Book_U)
                        .Skip(startNum).Take(listCount).ToListAsync();

            // 검색어 입력에 따른 필터링(책제목 기준)
            // 1. 검색어 중 한글은 인식되지 않음(퍼센트 인코딩 형태로 인식됨)
            // 2. 짧은 시간 안에 검색을 연속 수행하면 오류 발생
            // (An unhandled exception occurred while processing the request.)
            if (queryStr[0] == "?keyword" && !string.IsNullOrWhiteSpace(queryStr[1]))
                books = _context.Book.Where(x => x.Title.Contains(queryStr[1]))
                        .OrderBy(x => x.Book_U)
                        .Skip(startNum).Take(listCount).ToListAsync();

            return View(await books);
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Book == null)
                return NotFound();

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Book_U == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // GET: Home/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book) {
            if (ModelState.IsValid) {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Book == null)
                return NotFound();

            var book = await _context.Book.FindAsync(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Book_U,Title,Writer,Summary,Publisher,Published_date")] Book book) {
            if (id != book.Book_U)
                return NotFound();

            if (ModelState.IsValid) {
                try {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!BookExists(book.Book_U))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Book == null)
                return NotFound();

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Book_U == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Book == null)
                return Problem("Entity set 'LibraryAppContext.Book'  is null.");

            var book = await _context.Book.FindAsync(id);
            if (book != null)
                _context.Book.Remove(book);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id) {
          return _context.Book.Any(e => e.Book_U == id);
        }
    }
}
