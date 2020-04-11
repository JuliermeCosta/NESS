using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NESS_AgendamentoExames.Models;
using NESS_AgendamentoExames.Repository.Interfaces;

namespace NESS_AgendamentoExames.Controllers
{
    public class DataDisponivelController : Controller
    {
        private readonly IRepository<DataDisponivel> _repository;
        private readonly IRepository<Consulta> _repositoryConsulta;

        public DataDisponivelController(IRepository<DataDisponivel> repository, IRepository<Consulta> repositoryConsulta)
        {
            _repository = repository;
            _repositoryConsulta = repositoryConsulta;
        }

        // GET: Paciente
        public IActionResult Index()
        {
            try
            {
                List<DataDisponivel> listaDatas = _repository.GetAll();

                ViewData["Informacao"] = "Acesso ao banco dados realizado com sucesso";

                if (!listaDatas.Any())
                {
                    ViewData["Aviso"] = "Não há dados disponíveis!";
                }

                return View(listaDatas);
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
        public IActionResult Create([FromForm]DataDisponivel dataDisponivel)
        {
            try
            {
                dataDisponivel.Data = dataDisponivel.Data.Date;

                if (_repository.GetAll().Any(x => x.Data == dataDisponivel.Data))
                {
                    throw new Exception("Data já existente!");
                }

                _repository.Insert(dataDisponivel);

                TempData["Sucesso"] = "Data inserida com sucesso!";

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

                DataDisponivel data = _repository.GetById(id);

                if (data == null)
                {
                    throw new Exception("Data não encontrada!");
                }

                if (_repositoryConsulta.GetAll().Any(x => x.DataDisponivel.Id == id))
                {
                    ViewData["Aviso"] = "Data está em uso numa consulta!";
                }

                return View(data);
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
        public IActionResult Edit(int id, [FromForm]DataDisponivel dataDisponivel)
        {
            try
            {
                if (_repositoryConsulta.GetAll().Any(x => x.DataDisponivel.Id == id))
                {
                    throw new Exception("Data está em uso numa consulta!");
                }

                dataDisponivel.Id = id;
                dataDisponivel.Data = dataDisponivel.Data.Date;

                if (_repository.GetAll().Any(x => x.Data == dataDisponivel.Data))
                {
                    throw new Exception("Data já existente!");
                }

                _repository.Update(dataDisponivel);

                TempData["Sucesso"] = "Data atualizada com sucesso!";

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

                DataDisponivel paciente = _repository.GetById(id);

                if (paciente == null)
                {
                    throw new Exception("Data não encontrada!");
                }

                if (_repositoryConsulta.GetAll().Any(x => x.DataDisponivel.Id == id))
                {
                    ViewData["Aviso"] = "Data está em uso numa consulta!";
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
                if (_repositoryConsulta.GetAll().Any(x => x.DataDisponivel.Id == id))
                {
                    throw new Exception("Não é possível apagar uma data em uso!");
                }

                _repository.Delete(id);

                TempData["Sucesso"] = "Data excluída com sucesso!";

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