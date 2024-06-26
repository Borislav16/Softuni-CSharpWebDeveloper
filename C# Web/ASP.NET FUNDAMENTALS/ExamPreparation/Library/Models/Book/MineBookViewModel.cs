﻿using System.ComponentModel.DataAnnotations;
using Library.Data.Models;
using static Library.Data.DataConstants.Book;

namespace Library.Models.Book
{
    public class MineBookViewModel
    {
        
        public int Id { get; set; }

       
        public string ImageUrl { get; set; }

        
       
        public string Title { get; set; }

        
        public string Author { get; set; }

        
        public string Description { get; set; }

        
        public Category Category { get; set; }
    }
}
