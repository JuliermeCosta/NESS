using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NESS_AgendamentoExames.Models;
using NESS_AgendamentoExames.Repository;
using NESS_AgendamentoExames.Repository.Interfaces;

namespace NESS_AgendamentoExames.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IRepository<Consulta> _repository;
        private readonly IRepository<Paciente> _repositoryPaciente;
        private readonly DataDisponivelRepository _repositoryDataDisponivel;

        public ConsultaController(IRepository<Consulta> repository, IRepository<Paciente> repositoryPaciente, IRepository<DataDisponivel> repositoryDataDisponivel)
        {
            _repository = repository;
            _repositoryPaciente = repositoryPaciente;
            _repositoryDataDisponivel = (DataDisponivelRepository)repositoryDataDisponivel;
        }

        // GET: Consulta
        public IActionResult Index()
        {
            try
            {
                List<Consulta> listaConsultas = _repository.GetAll();

                ViewData["Informacao"] = "Acesso ao banco dados realizado com sucesso";

                if (!listaConsultas.Any())
                {
                    ViewData["Aviso"] = "Não há dados disponíveis!";
                }                

                return View(listaConsultas);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Consulta/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Id inválido!");
                }

                Consulta consulta = _repository.GetById(id);

                if (consulta == null)
                {
                    throw new Exception("Consulta não encontrada!");
                }

                ViewData["Informacao"] = "Acesso ao banco dados realizado com sucesso";

                return View(consulta);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Consulta/Create
        public IActionResult Create()
        {
            try
            {
                List<SelectListItem> listaPacientes = _repositoryPaciente.GetAll().OrderBy(x => x.Nome).Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
                listaPacientes.Insert(0, new SelectListItem("Selecione um paciente", string.Empty));
                ViewBag.Pacientes = listaPacientes;

                List<SelectListItem> listaDatas = _repositoryDataDisponivel.GetDisponiveis().OrderBy(x => x.Data).Select(x => new SelectListItem(x.Data.ToString("dd/MM/yyyy"), x.Id.ToString())).ToList();
                listaDatas.Insert(0, new SelectListItem("Selecione uma data disponível", string.Empty));
                ViewBag.Datas = listaDatas;

                ViewData["Informacao"] = "Acesso ao banco dados realizado com sucesso";

                return View();
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Consulta consulta)
        {
            try
            {
                _repository.Insert(consulta);

                TempData["Sucesso"] = "Consulta inserida com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Consulta/Edit/5
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Id inválido!");
            }

            Consulta consulta = _repository.GetById(id);

            if (consulta == null)
            {
                throw new Exception("Consulta não encontrada!");
            }

            List<SelectListItem> listaPacientes = _repositoryPaciente.GetAll().OrderBy(x => x.Nome).Select(x => new SelectListItem(x.Nome, x.Id.ToString())).ToList();
            listaPacientes.Insert(0, new SelectListItem("Selecione um paciente", string.Empty));
            ViewBag.Pacientes = listaPacientes;

            List<DataDisponivel> listaDatas = _repositoryDataDisponivel.GetDisponiveis();
            listaDatas.Add(consulta.DataDisponivel);

            List<SelectListItem> listaDatasCompleta = listaDatas.OrderBy(x => x.Data).Select(x => new SelectListItem(x.Data.ToString("dd/MM/yyyy"), x.Id.ToString())).ToList();
            listaDatasCompleta.Insert(0, new SelectListItem("Selecione uma data disponível", string.Empty));
            ViewBag.Datas = listaDatasCompleta;

            ViewData["Informacao"] = "Acesso ao banco dados realizado com sucesso";

            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [FromForm] Consulta consulta)
        {
            try
            {
                _repository.Update(consulta);

                TempData["Sucesso"] = "Consulta atualizada com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // GET: Consulta/Delete/5
        public IActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Id inválido!");
                }

                Consulta consulta = _repository.GetById(id);

                if (consulta == null)
                {
                    throw new Exception("Consulta não encontrada!");
                }

                return View(consulta);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        // POST: Consulta/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _repository.Delete(id);

                TempData["Sucesso"] = "Consulta excluída com sucesso!";

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