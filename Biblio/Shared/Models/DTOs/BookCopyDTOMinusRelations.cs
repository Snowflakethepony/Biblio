﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Shared.Models.DTOs
{
    public class BookCopyDTOMinusRelations
    {
        public int BookCopyId { get; set; }
        public bool IsAvailable { get; set; }
        public int ShelfNumber { get; set; }
        public DateTime? BorrowedAt { get; set; }
        public DateTime? ReturnBy { get; set; }
        public int? TimesRerented { get; set; }

        /// <summary>
        /// JUST HERE TO IMMULATE A REAL SCAN PROPERTY ON PHYSICAL BOOKS! 
        /// Used in system to loan, return and rerent by scanning and locating the book instance inside the system. 
        /// This is just randomly generated at BookCopy instance creation to an 8 letter string.
        /// </summary>
        public string RFID { get; set; }

        public int BookId { get; set; }
        public string BorrowerId { get; set; }
        public int OriginLibraryId { get; set; }
        public int CurrentLibraryId { get; set; }
    }
}
