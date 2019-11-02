using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ScriptureJournalContext>>()))
            {
                // Look for any scripture reference.
                if (context.Scripture.Any())
                {
                    return;   // DB has been seeded
                }

                context.Scripture.AddRange(
                        new Scripture
                        {
                            Title = "How to love Him",
                            DateAdded = DateTime.Parse("2019-10-12"),
                            Book = "John",
                            Chapter = 14,
                            Verse = "15",
                            Notes = "It seems easy, but when we do not obey, is it like we completely do not love him?"
                        },

                        new Scripture
                        {
                            Title = "Sinning Vs. Transgressing",
                            DateAdded = DateTime.Parse("2019-10-13"),
                            Book = "1 Samuel",
                            Chapter = 16,
                            Verse = "7",
                            Notes = "I feel like it is talking about our intentions in our actions. But how much does that excuse us when we make a wrong choice"
                        },

                        new Scripture
                        {
                            Title = "Forgiveness is Achievable",
                            DateAdded = DateTime.Parse("2019-10-23"),
                            Book = "Doctrine and Covenants",
                            Chapter = 58,
                            Verse = "42-43",
                            Notes = "It is good to know if we repent we will be forgiven. Combine that with how often we will be forgiven it seems reassuring"
                        }
                    );

                context.SaveChanges();
            }
        }
    }
}
