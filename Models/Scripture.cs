using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ScriptureJournal.Models
{
    public class Scripture
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set ; }

        [RegularExpression(@"^[A-Z0-9]+[a-zA-Z""'\s-]*$", ErrorMessage = "Please start with a number or letter!")]
        [Required]
        [StringLength(30)]
        public string Book { get; set; }

        
        [Required]
        public Int16 Chapter { get; set; }

        [RegularExpression(@"^[0-9]+[Z0-9""'\s-]*$", ErrorMessage = "Please start with a number!")]
        [Required]
        [StringLength(30)]
        public String Verse { get; set; }


        public String Notes { get; set; }

    }
}
