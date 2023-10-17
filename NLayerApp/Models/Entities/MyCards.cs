using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class MyCards : BaseEntity<int>
    {
        [Unicode(true)]
        [StringLength(128)]
        public string Name {  get; set; }

        public int BankId { get; set; }

        [ForeignKey("BankId")]
        public Banks Bank { get; set; }

    }
}
