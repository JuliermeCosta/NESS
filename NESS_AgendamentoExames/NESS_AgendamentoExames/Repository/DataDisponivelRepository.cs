using NESS_AgendamentoExames.Models;
using NESS_AgendamentoExames.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NESS_AgendamentoExames.Repository
{
    public class DataDisponivelRepository : Repository, IRepository<DataDisponivel>
    {
        public void Delete(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@id", id));
                base.ProcedureSemRetorno("prDelData");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao apagar dados no banco: {ex.Message}");
            }            
        }

        public List<DataDisponivel> GetAll()
        {
            try
            {
                DataTable table = base.ProcedureComRetorno("prSelData");

                List<DataDisponivel> listaDatas = new List<DataDisponivel>();

                foreach (DataRow row in table.Rows)
                {
                    listaDatas.Add(ConvertDataRow(row));
                }

                return listaDatas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public List<DataDisponivel> GetDisponiveis()
        {
            try
            {
                DataTable table = base.ProcedureComRetorno("prSelDisponiveis");

                List<DataDisponivel> listaDatas = new List<DataDisponivel>();

                foreach (DataRow row in table.Rows)
                {
                    listaDatas.Add(ConvertDataRow(row));
                }

                return listaDatas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public DataDisponivel GetById(int id)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@id", id));
                DataTable table = base.ProcedureComRetorno("prSelData");

                DataDisponivel data = ConvertDataRow(table.Rows[0]);

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao trazer os dados no banco: {ex.Message}");
            }
        }

        public void Insert(DataDisponivel entity)
        {
            try
            {
                base.Parameters.Add(new SqlParameter("@data", entity.Data));
                DataTable table = base.ProcedureComRetorno("prInsData");

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

        public void Update(DataDisponivel entity)
        {
            try
            {
                List<SqlParameter> parametros = new List<SqlParameter> { 
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@data", entity.Data)
                };

                base.Parameters.AddRange(parametros);

                base.ProcedureSemRetorno("prUpdData");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar dados no banco: {ex.Message}");
            }
        }

        private DataDisponivel ConvertDataRow(DataRow row)
        {
            try
            {
                DataDisponivel data = new DataDisponivel(Convert.ToInt32(row["Id"]), Convert.ToDateTime(row["Data"]));

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao converter dados para objeto: {ex.Message}");
            }
        }
    }
}
