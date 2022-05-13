using Blaze.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blaze
{
    public class Database
    {
        private readonly string connectionString = "Data Source=INSPIRON5458\\SQLEXPRESS;Initial Catalog=Blaze;Persist Security Info=True;User ID=sa;Password=123456;MultipleActiveResultSets=True";
        private readonly SqlConnection conexao;
        public Database()
        {
            if (conexao ==null)
                conexao = new(connectionString);
        }

        public async Task InserirWaiting(Payload objeto)
        {
            try
            {
                if (conexao.State != ConnectionState.Open && conexao.State != ConnectionState.Connecting)
                    conexao.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO [dbo].[double_waiting] ");
                builder.Append("([id],[color],[roll],[createdAt],[updatedAt],[status])");
                builder.Append("VALUES");
                builder.Append('(');
                builder.Append("'"+objeto.Id+"',");
                builder.Append(objeto.Color == null ? "''," : objeto.Color+",");
                builder.Append(objeto.Roll== null ? "''," : objeto.Roll+",");
                builder.Append("'"+objeto.CreatedAt+"',");
                builder.Append("'"+objeto.UpdatedAt+"','");
                builder.Append(objeto.Status+"')");

                string sql = builder.ToString();

                using var cmd = new SqlCommand(sql, conexao);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InserirRolling(Payload objeto)
        {
            try
            {
                if (conexao.State != ConnectionState.Open && conexao.State != ConnectionState.Connecting)
                    conexao.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO [dbo].[double_rolling] ");
                builder.Append("([id],[color],[roll],[createdAt],[updatedAt],[status])");
                builder.Append("VALUES");
                builder.Append('(');
                builder.Append("'"+objeto.Id+"',");
                builder.Append(objeto.Color == null ? "''," : objeto.Color+",");
                builder.Append(objeto.Roll== null ? "''," : objeto.Roll+",");
                builder.Append("'"+objeto.CreatedAt+"',");
                builder.Append("'"+objeto.UpdatedAt+"','");
                builder.Append(objeto.Status+"')");

                string sql = builder.ToString();

                using var cmd = new SqlCommand(sql, conexao);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InserirComplete(Payload objeto)
        {
            try
            {
                if (conexao.State != ConnectionState.Open && conexao.State != ConnectionState.Connecting)
                    conexao.Open();

                StringBuilder builder = new StringBuilder();
                builder.Append("INSERT INTO [dbo].[double_complete] ");
                builder.Append("([id],[color],[roll],[createdAt],[updatedAt],[status])");
                builder.Append("VALUES");
                builder.Append('(');
                builder.Append("'"+objeto.Id+"',");
                builder.Append(objeto.Color == null ? "''," : objeto.Color+",");
                builder.Append(objeto.Roll== null ? "''," : objeto.Roll+",");
                builder.Append("'"+objeto.CreatedAt+"',");
                builder.Append("'"+objeto.UpdatedAt+"','");
                builder.Append(objeto.Status+"')");

                string sql = builder.ToString();

                using var cmd = new SqlCommand(sql, conexao);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void FecharConexao()
        {
            conexao.Close();
            conexao.Dispose();
        }
    }
}
