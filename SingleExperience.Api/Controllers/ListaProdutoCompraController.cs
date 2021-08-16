﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SingleExperience.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleExperience.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaProdutoCompraController : ControllerBase
    {
        protected readonly SingleExperienceRepository _repo;

        public ListaProdutoCompraController(SingleExperienceRepository repo)
        {
            _repo = repo;
        }
    }
}
