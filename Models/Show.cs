using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecureSeat.Models
{
    [Table("Shows")]
    public class Show
    {
        //id property, used for primary key in database
        public int Id { get; set; }

        // location with validation attributes
        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters")]
        public string Location { get; set; }

        // date with validation attributes
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        // title with validation attributes
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
        public string Title { get; set; }

        // description with validation attributes
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        // category with validation attributes
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        // time with validation attributes
        [Required(ErrorMessage = "Time is required")]
        [DataType(DataType.Time)]
        [Display(Name = "Event Time")]
        public DateTime Time { get; set; }

        // owner with validation attributes
        [Required(ErrorMessage = "Owner is required")]
        [StringLength(50, ErrorMessage = "Owner cannot be longer than 50 characters")]
        [Display(Name = "Event Owner")]
        public string Owner { get; set; }

        //date created with display attribute
        [Display(Name = "Date Created")]
        public DateTime? dateCreated { get; set; }

        //url for image with validation attributes
        [Display(Name = "Image URL")]   
        [StringLength(300, ErrorMessage = "Image URL cannot be longer than 300 characters")]
        public string? ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Event Image")]
        public IFormFile? ImageFile { get; set; }
    }
}


