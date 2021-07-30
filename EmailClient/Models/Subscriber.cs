using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Models
{
    class Subscriber:PageModel
    {
        [BindProperty]
        public string firstname { get; set; }
        [BindProperty]
        public string lastname { get; set; }
        [BindProperty]
        public string productname { get; set; }
    }
}
