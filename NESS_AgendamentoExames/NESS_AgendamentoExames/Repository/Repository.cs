using Microsoft.Extensions.Configuration;
using NESS_AgendamentoExames.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NESS_AgendamentoExames.Repository
{
    public abstract class Repository
    {
        //String de conexão no appsettings.json
        private readonly string _stringConnection = ConfigurationManager.AppSetting.GetConnectionString("DB_NESS");

        public List<SqlParameter> Parameters { get; private set; } = new List<SqlParameter>();

        private DataTable AcessoAoBanco(string procedure, bool comRetorno, bool usarProcedure)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(_stringConnection))
            {
                try
                {
                    SqlCommand command = new SqlCommand(procedure, connection);
                    command.CommandType = usarProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    if (Parameters.Any())
                    {
                        command.Parameters.AddRange(Parameters.ToArray());
                        Parameters.Clear();
                    }

                    command.Connection.Open();

                    if (comRetorno)
                    {
                        using (SqlDataReader sdr = command.ExecuteReader())
                        {
                            table.Load(sdr);
                        }
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return table;
        }

        public void ProcedureSemRetorno(string procedure)
        {
            try
            {
                AcessoAoBanco(procedure, false, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ProcedureComRetorno(string procedure)
        {
            try
            {
                return AcessoAoBanco(procedure, true, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SqlSemRetorno(string sql)
        {
            try
            {
                AcessoAoBanco(sql, false, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SqlComRetorno(string sql)
        {
            try
            {
                return AcessoAoBanco(sql, true, false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
