using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
//作用和60页一样
using SimpleCMS.Validations;

namespace SimpleCMS.Models
{
    //60页
    [Table("T_Content")]
    public class Content
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Image { get; set; }

        [MaxLength(500)]
        [Column(TypeName = "nvarchar")]
        public string Summary { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Body { get; set; }

        [Required]
        [DefaultDateTimeValue("Now")]
        public DateTime? Created { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Hits { get; set; }

        [Required]
        [Range(0, 1)]
        public byte State { get; set; }

        [Required]
        [DefaultValue(0)]
        public int SortOrder { get; set; }

        //61页,实现1对多关系
        public virtual ICollection<Content> contents { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}