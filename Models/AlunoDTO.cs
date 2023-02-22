using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using mvc.Controllers ;


namespace mvc.Models
{
    public partial class Alunos
    {
        
        #region instancia
        public double CalcularMedia()
        {
            var somaNotas = 0.0;
            foreach(var nota in this.Nota)
            {
                somaNotas += nota;
            }
            return somaNotas / this.Nota.Count;
        }

        public string Situacao()
        {
            return this.CalcularMedia() >= 7 ? "Aprovado" : "Reprovado";
        }
        public void Apagar()
        {
            Alunos.DeletePorId(this.Id);
        }

        public void Salvar()
        {
            if(this.Id > 0)
            {
                Alunos.Atualizar(this);
            }
            else
            {
                Alunos.IncluirAluno(this);
            }
        }

        #endregion
        #region "Metodos de classe"
        private static string connectionString()
        {
            return "Server=JUNIODEV\\SQLEXPRESS;Database=desafio21diasapi;Trusted_Connection=True;";
        }
        
        public static void IncluirAluno(Alunos aluno)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();            
            SqlCommand sqlCommand = new SqlCommand($"insert into alunos(nome, matricula, notas) values (@nome, @matricula, @notas)", sqlConn);
            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula); 
            sqlCommand.Parameters.AddWithValue("@notas", string.Join(",", aluno.Nota.ToArray()));

            var reader = sqlCommand.ExecuteNonQuery();       

            sqlConn.Close();
            sqlConn.Dispose();
        }
        public static void Atualizar(Alunos aluno)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"update alunos set nome=@nome, matricula=@matricula, notas=@notas Where id=@id", sqlConn);
            sqlCommand.Parameters.AddWithValue("@id", aluno.Id);
            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula); 
            sqlCommand.Parameters.AddWithValue("@notas", string.Join(",", aluno.Nota.ToArray()));

            var reader = sqlCommand.ExecuteNonQuery();       

            sqlConn.Close();
            sqlConn.Dispose();
        }

        
        public static void DeletePorId(int id)
        {
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"delete from alunos where id={id}", sqlConn);
            var reader = sqlCommand.ExecuteNonQuery();       

            sqlConn.Close();
            sqlConn.Dispose();  

        }

        public static List<Alunos> BuscarAlunoId(int id)
        {
            var alunos = new List<Alunos>();
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"select * from alunos where id={id}", sqlConn);
            var reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                var notas = new List<double>();
                string strNotas = reader["notas"].ToString();
                foreach(var nota in strNotas.Split(','))
                {
                    notas.Add(Convert.ToDouble(nota));
                }

                var aluno = new Alunos()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    Nota = notas,
                };

                alunos.Add(aluno);
            }

            sqlConn.Close();
            sqlConn.Dispose();

            return alunos;
        }
        public static List<Alunos> Todos()
        {      
            var alunos = new List<Alunos>();
            SqlConnection sqlConn = new SqlConnection(connectionString());
            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand("select * from alunos", sqlConn);
            var reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                var notas = new List<double>();
                string strNotas = reader["notas"].ToString();
                foreach(var nota in strNotas.Split(','))
                {
                    notas.Add(Convert.ToDouble(nota));
                    
                }

                var aluno = new Alunos()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    Nota = notas,
                };

                alunos.Add(aluno);
            }

            sqlConn.Close();
            sqlConn.Dispose();

            return alunos;
        }      
        #endregion    
    }         
}