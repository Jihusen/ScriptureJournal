using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScriptureJournal.Models;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Models.ScriptureJournalContext _context;

        public IndexModel(ScriptureJournal.Models.ScriptureJournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Scripture> Scripture { get;set; }
        
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Books { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            BookSort = String.IsNullOrEmpty(sortOrder) ? "book_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;

            IQueryable<Scripture> scriptureIQ = from s in _context.Scripture
                                                select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                scriptureIQ = scriptureIQ.Where(s => s.Notes.Contains(searchString) || s.Book.Contains(searchString));
            }

            switch (sortOrder)
            { 
                case "book_desc":
                    scriptureIQ = scriptureIQ.OrderByDescending(s => s.Book);
                    break;
                case "Date":
                    scriptureIQ = scriptureIQ.OrderBy(s => s.DateAdded);
                    break;
                case "date_desc":
                    scriptureIQ = scriptureIQ.OrderByDescending(s => s.DateAdded);
                    break;
                default:
                    scriptureIQ = scriptureIQ.OrderBy(s => s.Book);
                    break;
            }


            // Use LINQ to get list of books.
            IQueryable<string> bookQuery = from m in _context.Scripture
                                            orderby m.Book
                                            select m.Book;

            var scriptures = from m in _context.Scripture
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ScriptureBook))
            {
                scriptures = scriptures.Where(x => x.Book == ScriptureBook);
            }
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());

            Scripture = await scriptureIQ.AsNoTracking().ToListAsync();
        }
    }
}
