using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
    }
}