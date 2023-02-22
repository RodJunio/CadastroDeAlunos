using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;

namespace mvc.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ILogger<AlunosController> _logger;

        public AlunosController(ILogger<AlunosController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            
            return View(Alunos.Todos());
        }
        [Route("/AdicionarAluno")]
        [HttpGet]
        public IActionResult AdicionarAluno()
        {            
            
            return View();
        }

        [Route("/AdicionarAluno")]
        [HttpPost]
        public IActionResult AdicionarAluno(Alunos aluno)
        {            
            aluno.Salvar();
            return Redirect("~/Alunos");
        }

        [Route("/Alunos/{id}/ExcluirAluno")]
        [HttpGet]
        public IActionResult ExcluirAluno(int id)
        {           
             
            Alunos.DeletePorId(id);
            return Redirect("~/Alunos");
        }

        [Route("/Alunos/editaraluno/{id}")]
        [HttpGet]
        public IActionResult EditarAluno(int id, Alunos aluno)
        {   
            ViewBag.Id = aluno.Id;
            ViewBag.Nome = aluno.Nome;
            ViewBag.Matricula = aluno.Matricula;
            ViewBag.Nota = aluno.Nota;
            return View(Alunos.BuscarAlunoId(id));
        }       

        [Route("/EditarAluno")]
        [HttpPost]
        public IActionResult EditarAluno(Alunos aluno)
        {               
            
            aluno.Salvar();
            return Redirect("/Alunos");
        }
    }

}
