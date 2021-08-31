using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CultureInfoDetails1.Models
{
    public class CultureSpecification
    {
        public string Name { get; set; }
        public string GroupSeparator { get; set; }
        public string CultureCode { get; set; }
        public string DisplayName { get; set; }
        public string DecimalSeparator  { get; set; }

        public string GroupSeparatorUnicode { get; set; }

        public string DecimalSeparatorUnicode { get; set; }
    }
}
