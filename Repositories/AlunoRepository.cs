using API_Boletim.Context;
using API_Boletim.Domains;
using API_Boletim.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API_Boletim.Repositories
{
    public class AlunoRepository : IAluno
    {
        //Chamando a classe de conexão do banco
        BoletimContext conexao = new BoletimContext();

        //Chamando o objeto que receberá e executará os comandos do banco
        SqlCommand cmd = new SqlCommand();
        
        public Aluno Alterar(Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "UPDATE Aluno SET Nome= @nome, RA = @ra, Idade= @idade WHERE IdAluno= @id";

            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);
            cmd.Parameters.AddWithValue("@id", a.IdAluno);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return a;
        }

        public Aluno BuscarPorId(int id)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "SELECT * FROM Aluno WHERE IdAluno = @id";

            //Atribuindo as variaveis
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dados = cmd.ExecuteReader();

            Aluno a = new Aluno();

            while (dados.Read())
            {
                a.IdAluno   = Convert.ToInt32(dados.GetValue(0));
                a.Nome      = dados.GetValue(1).ToString();
                a.RA        = dados.GetValue(2).ToString();
                a.Idade = Convert.ToInt32(dados.GetValue(3));
            }


            conexao.Desconectar();

            return a;
        }

        public Aluno Cadastrar(Aluno a)
        {

            cmd.Connection = conexao.Conectar();

            cmd.CommandText =
                "INSERT INTO Aluno (Nome, RA, Idade) " +
                "VALUES" +
                "(@nome, @ra, @idade)";
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@ra", a.RA);
            cmd.Parameters.AddWithValue("@idade", a.Idade);

            //comando o responsável para injetar os dados no banco efetivamente
            cmd.ExecuteNonQuery();

            //-------------------------
            // USAR cmd.ExecuteNonQuery(); QUANDO FOR DML
            //-------------------------

            conexao.Desconectar();

            return a;
        }

        public Aluno Excluir(Aluno a)
        {
            cmd.Connection = conexao.Conectar();

            cmd.CommandText = "DELETE FROM Aluno WHERE IdAluno= @id";

            cmd.Parameters.AddWithValue("@id", a.IdAluno);

            cmd.ExecuteNonQuery();

            conexao.Desconectar();

            return a;
        }

        public List<Aluno> LerAlunos()
        {
            //Abrindo conexao
            cmd.Connection = conexao.Conectar();

            //Preparando query
            cmd.CommandText = "SELECT * FROM Aluno";

            SqlDataReader dados = cmd.ExecuteReader();

            //Criando lista para guardar os alunos
            List<Aluno> alunos = new List<Aluno>();

            while (dados.Read())
            {
                alunos.Add(
                    new Aluno()
                    { 
                        IdAluno     = Convert.ToInt32(dados.GetValue(0)),
                        Nome        = dados.GetValue(1).ToString(),
                        RA          = dados.GetValue(2).ToString(),
                        Idade       = Convert.ToInt32(dados.GetValue(3)),
                    }
                );
            }

            //Fechando conexao
            conexao.Desconectar();

            return alunos;

        }
    }
}
