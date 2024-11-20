using ProjetoLogin.Models;

namespace ProjetoLogin.Repositorio.Contract
{
    public interface IColaboradorRepositorio
    {
        // Logim Cliente
        Colaborador Login(string Email, string Senha);
        //CRUD
        void Cadastrar(Colaborador colaborador);
        void Atualizar(Colaborador colaborador);
        void AtualizarSenha(int Id);
        void Excluir(int Id);
        Colaborador ObterColaborador(int Id);
        List<Colaborador> ObterColaboradorPorEmail(string Email);
        IEnumerable<Colaborador> ObterTodosColaboradores();
    }
}

