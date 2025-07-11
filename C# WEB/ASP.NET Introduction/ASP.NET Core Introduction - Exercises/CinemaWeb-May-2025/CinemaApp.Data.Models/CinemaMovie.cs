﻿namespace CinemaApp.Data.Models
{
    public class CinemaMovie
    {
        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; } = null!;
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public int AvailableTickets { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Showtimes { get; set; } = null!;

    }
}
