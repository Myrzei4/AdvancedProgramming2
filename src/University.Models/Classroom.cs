using System;
using System.Collections.Generic;

namespace University.Models
{
    public class Classroom
    {
        public string ClassroomID { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
        public bool Projector { get; set; }
        public bool Whiteboard { get; set; }
        public bool Microphone { get; set; }
        public string Description { get; set; } = string.Empty;

       // public virtual ICollection<Subject>? Subjects { get; set; } = null;
    }
}
