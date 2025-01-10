using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FormulariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FormulariosController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
