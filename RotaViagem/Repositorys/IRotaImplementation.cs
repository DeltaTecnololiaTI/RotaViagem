using RotaViagemModel.Model;

namespace RotaViagem.Repositorys
{
    public interface IRotaImplementation
    {
        TabRota Incluir(TabRota rota);
        TabRota BuscarPorId(int id);
        List<TabRota> BuscarOrigem(string origem);
        TabRota Atualizar(int id, TabRota rota);
        void Excluir(int id);
        bool Exists(int id);
    }
}
