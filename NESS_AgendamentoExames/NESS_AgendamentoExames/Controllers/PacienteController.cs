using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NESS_AgendamentoExames.Models;
using NESS_AgendamentoExames.Repository;
using NESS_AgendamentoExames.Repository.Interfaces;

namespace NESS_AgendamentoExames.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IRepository<Paciente> _repository;
        private readonly IRepository<Consulta> _repositoryConsulta;

        public PacienteController(IRepository<Paciente> repository, IRepository<Consulta> repositoryConsulta)
        {
            _repository = repository;
            _repositoryConsulta = repositoryConsulta;
        }

        // GET: Paciente
        public IActionResult Index()
        {
            try
            {
                List<Paciente> listaPacientes = _repository.GetAll();

                return View(listaPacientes);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }       

        // GET: Paciente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]Paciente paciente)
        {
            try
            {
                _repository.Insert(paciente);

                TempData["Sucesso"] = "Paciente inserido com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Paciente/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Id inválido!");
                }

                Paciente paciente = _repository.GetById(id);

                if (paciente == null)
                {
                    throw new Exception("Paciente não encontrado!");
                }

                return View(paciente);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // POST: Paciente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [FromForm]Paciente paciente)
        {
            try
            {
                paciente.Id = id;

                _repository.Update(paciente);

                TempData["Sucesso"] = "Paciente atualizado com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Paciente/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Id inválido!");
                }

                Paciente paciente = _repository.GetById(id);

                if (paciente == null)
                {
                    throw new Exception("Paciente não encontrado!");
                }

                if (_repositoryConsulta.GetAll().Any(x => x.Paciente.Id == id))
                {
                    ViewData["Aviso"] = "Paciente está com consulta marcada!";
                }

                return View(paciente);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // POST: Paciente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                List<Consulta> consultasExistentes =_repositoryConsulta.GetAll().Where(x => x.Paciente.Id == id).ToList();

                consultasExistentes.ForEach(x => _repositoryConsulta.Delete(x.Id));

                _repository.Delete(id);

                TempData["Sucesso"] = "Paciente excluído com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }
    }
}