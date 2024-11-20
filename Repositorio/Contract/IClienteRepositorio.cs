using ProjetoLogin.Models;

namespace ProjetoLogin.Repositorio.Contract
{
    public interface IClienteRepositorio
    {
        // Logim Cliente
        Cliente Login(string Email, string Senha);

        //CRUD
        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);

        void Ativar(int id);
        void Desativar(int id);

        void Excluir(int Id);
        Cliente ObterCliente(int Id);

        Cliente BuscaCpfCliente(string CPF);

        Cliente BuscaEmailCliente(string email);

        IEnumerable<Cliente> ObterTodosClientes();
    }
}
