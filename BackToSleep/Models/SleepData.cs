//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackToSleep.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SleepData
    {
        public int ID { get; set; }

        [Required]
        public string SleepHours { get; set; }

        public string Day { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string UserID { get; set; }

        [Required]
        [Range(1,10, ErrorMessage = "You done did it!")]
        public int SleepQuality { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
