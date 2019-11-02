using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScriptureJournal.Models
{
    public class ScriptureJournalContext : DbContext
    {
        public ScriptureJournalContext (DbContextOptions<ScriptureJournalContext> options)
            : base(options)
        {
        }

        public DbSet<ScriptureJournal.Models.Scripture> Scripture { get; set; }
    }
}
