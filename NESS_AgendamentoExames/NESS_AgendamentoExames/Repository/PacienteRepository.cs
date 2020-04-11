using NESS_AgendamentoExames.Models;
using NESS_AgendamentoExames.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NESS_AgendamentoExames.Repository
{
    public class PacienteRepository : Repository, IRepository<Paciente>
    {
        public void Delete(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@id", id));
                base.ProcedureSemRetorno("prDelPaciente");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao apagar dados no banco: {ex.Message}");
            }            
        }

        public List<Paciente> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureComRetorno("prSelPaciente");

                List<Paciente> listaPacientes = new List<Paciente>();

                foreach (DataRow row in table.Rows)
                {
                    listaPacientes.Add(ConvertDataRow(row));
                }

                return listaPacientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public Paciente GetById(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@id", id));
                DataTable table = base.ProcedureComRetorno("prSelPaciente");

                Paciente paciente = ConvertDataRow(table.Rows[0]);

                return paciente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public void Insert(Paciente entity)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@nome", entity.Nome));
                DataTable table = base.ProcedureComRetorno("prInsPaciente");

                if (table == null)
                {
                    new Exception("Resultado não obtido!");
                }

                int novoID = Convert.ToInt32(table.Rows[0]["Id"] ?? 0);

                if (novoID == 0)
                {
                    new Exception("Novo Id não encontrado!");
                }

                entity.Id = novoID;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inserir dados no banco: {ex.Message}");
            }
        }

        public void Update(Paciente entity)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter> { 
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@nome", entity.Nome)
                };

                base.Parameters.AddRange(parametros);

                base.ProcedureSemRetorno("prUpdPaciente");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar dados no banco: {ex.Message}");
            }
        }

        private Paciente ConvertDataRow(DataRow row)
        {
            try
            {
                Paciente paciente = new Paciente(Convert.ToInt32(row["Id"]), row["Nome"].ToString());

                return paciente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter dados para objeto: {ex.Message}");
            }
        }
    }
}
