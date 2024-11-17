using RotaViagemModel.Model;

namespace RotaViagem.Interface
{
    public interface IRotaBusiness
    {
        TabRota Incluir(TabRota rota);
        TabRota BuscarPorId(int id);
        List<TabRota> BuscarOrigem(string origem);
        TabRota Atualizar(int id, TabRota rota);
        void Excluir(int id);
        string MontarRotas(string origem, string destino);
    }
}
