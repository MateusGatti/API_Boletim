using API_Boletim.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Boletim.Interfaces
{
    interface IAluno
    {
        Aluno Cadastrar(Aluno aluno);
        List<Aluno> LerAlunos();
        Aluno BuscarPorId(int id);
        Aluno Alterar(Aluno aluno);
        Aluno Excluir(Aluno aluno);


    }
}
