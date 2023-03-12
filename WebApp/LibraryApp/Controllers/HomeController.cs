using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Data;
using LibraryApp.Models;
using System.Web;

namespace LibraryApp.Controllers {
    public class HomeController : Controller {
        private readonly LibraryAppContext _context;

        public HomeController(LibraryAppContext context) {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index() {
            // 페이징(Paging) 관련 변수
            int listCount = 5;      // 한 번에 표시할 레코드 수
            int pageNum = 1;        // 현재 페이지 번호
            int firstRecordNum = 0;     // 현재 페이지의 첫 레코드의 기본키
            IQueryable<Book> books;     // 출력할 도서 목록을 저장할 쿼리 인터페이스

            // 쿼리 스트링을 배열 형태로 저장
            // 예) ["", "searchKind", "Title", "page", 1]
            string[] queryStr = Request.QueryString.ToString().Split(new char[] { '?', '&', '=' });
            foreach (string s in queryStr)
                Console.WriteLine(">> " + s);

            // 쿼리스트링의 파라미터가 'page'인 경우, 쿼리스트링 값(현재 페이지)을 정수 형태로 저장
            if (queryStr.Length >= 2 && queryStr[1] == "page" && queryStr[2] != null)
                pageNum = Convert.ToInt32(queryStr[2]);

            // 현재 페이지의 첫 레코드의 기본키를 갱신함
            firstRecordNum = (pageNum - 1) * listCount;

            // 현재 페이지 번호를 뷰에 전달(ViewBag)
            ViewBag.PageNum = pageNum;

            // 검색어 입력에 따른 필터링(책제목 기준)
            if (queryStr.Length >= 4 && queryStr[1] == "searchKind" && queryStr[3] == "keyword" && !string.IsNullOrWhiteSpace(queryStr[4])) {
                // URL 인코딩(퍼센트 인코딩) 형태의 문자열을 (한글 깨짐 해결)
                string queryStrDecode = HttpUtility.UrlDecode(queryStr[4]);
                switch(queryStr[2]) {
                    case "Title":
                        books = _context.Book.Where(x => x.Title.Contains(queryStrDecode));
                        break;
                    case "Writer":
                        books = _context.Book.Where(x => x.Writer.Contains(queryStrDecode));
                        break;
                    case "Publisher":
                        books = _context.Book.Where(x => x.Publisher.Contains(queryStrDecode));
                        break;
                    default:
                        books = _context.Book.Where(x => x.Title.Contains(queryStrDecode));
                        break;
                }
                ViewBag.PageNumMax = (int)((books.Count() - 1) / listCount) + 1;     // 최대 페이지 수를 뷰에 전달(ViewBag)
                books = books.OrderBy(x => x.Book_U)
                        .Skip(firstRecordNum).Take(listCount);
            }

            // 검색어 입력이 아닌 경우, 전체 레코드를 페이징하여 출력
            else {
                books = _context.Book;
                ViewBag.PageNumMax = (int)((books.Count() - 1) / listCount) + 1;      // 최대 페이지 수를 뷰에 전달(ViewBag)
                books = books.OrderBy(x => x.Book_U)
                        .Skip(firstRecordNum).Take(listCount);
            }

            // 출력할 도서 리스트를 뷰에 전달(return)
            return View(await books.ToListAsync());
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
