using NESS_AgendamentoExames.Models;
using NESS_AgendamentoExames.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NESS_AgendamentoExames.Repository
{
    public class ConsultaRepository : Repository, IRepository<Consulta>
    {
        public void Delete(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@id", id));
                base.ProcedureSemRetorno("prDelConsulta");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao apagar dados no banco: {ex.Message}");
            }
        }

        public List<Consulta> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureComRetorno("prSelConsulta");

                List<Consulta> listaConsultas = new List<Consulta>();

                foreach (DataRow row in table.Rows)
                {
                    listaConsultas.Add(ConvertDataRow(row));
                }

                return listaConsultas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public Consulta GetById(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@id", id));
                DataTable table = base.ProcedureComRetorno("prSelConsulta");

                Consulta consulta = ConvertDataRow(table.Rows[0]);

                return consulta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public void Insert(Consulta entity)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@idPaciente", entity.Paciente.Id));
                base.Parameters.Add(new SqlParameter("@idData", entity.DataDisponivel.Id));
                DataTable table = base.ProcedureComRetorno("prInsConsulta");

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

        public void Update(Consulta entity)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter> {
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@idPaciente", entity.Paciente.Id),
                    new SqlParameter("@idData", entity.DataDisponivel.Id)
                };

                base.Parameters.AddRange(parametros);

                base.ProcedureSemRetorno("prUpdConsulta");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar dados no banco: {ex.Message}");
            }
        }

        private Consulta ConvertDataRow(DataRow row)
        {
            try
            {
                Consulta consulta = new Consulta()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Paciente = new Paciente(Convert.ToInt32(row["IdPaciente"]), row["Nome"].ToString()),
                    DataDisponivel = new DataDisponivel(Convert.ToInt32(row["IdData"]), Convert.ToDateTime(row["Data"]))
                };

                return consulta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter dados para objeto: {ex.Message}");
            }
        }
    }
}
