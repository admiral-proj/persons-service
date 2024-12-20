using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class Parent_2_Child
    {
        public int Id { get; set; }

        [Required]
        public int ParentId { get; set; }

        public int ChildId { get; set; }
    }
}
