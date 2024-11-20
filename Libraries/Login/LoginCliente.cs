using MySqlX.XDevAPI;
using Newtonsoft.Json;
using ProjetoLogin.Models;

namespace ProjetoLogin.Libraries.Login
{
    public class LoginCliente
    {
        private string Key = "Login Cliente";
        private Sessao.Sessao _sessao;

        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }
        public void Login(Cliente cliente)
        {
            //Serializar
            string clienteJSONString = JsonConvert.SerializeObject(cliente);
           
            _sessao.Cadastrar(Key, clienteJSONString);
        }
        //Reverte Json para o Objeto Cliente
        public Cliente GetCliente()
        {
            //Deserializar
            if (_sessao.Existe(Key))
            {
                string clienteJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJSONString);
            }
            else
            {
                return null;
            }
        }
        //Remover a sessao e deslogar o Cliente
        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
