using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Data;
using Users.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}