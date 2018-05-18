using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Post Task { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
