using Microsoft.EntityFrameworkCore;
using RotaViagem.Context;
using RotaViagemModel.Model;

namespace RotaViagem.Repositorys
{
    public class RotaImplementation: IRotaImplementation
    {
        private readonly DataContext _context;
        public RotaImplementation(DataContext context)
        {
            _context = context;
        }

        public TabRota Atualizar(int id, TabRota rota)
        {
            if (!Exists(rota.Id))
            {
                return null;
            }

            _context.Entry(rota).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.RotasViagem.Any(e => e.Id == id))
                    throw new Exception($"Não encontrado o id {id} requisitado.");
                throw;
            }

            return rota;
        }

        public TabRota BuscarPorId(int id)
        {
            var rota =  _context.RotasViagem.Find(id);
            if (rota == null)
                throw new Exception($"Não encontrado o id {id} requisitado.");

            return (rota);
        }

        public List<TabRota> BuscarOrigem(string origem)
        {
            // Busca as rotas que têm a origem fornecida
            var rotasComOrigem =  _context.RotasViagem
                .Where(r => r.Origem == origem)
                .ToList();

            return rotasComOrigem;
        }

        public void Excluir(int id)
        {
            var rota =  _context.RotasViagem.Find(id);
            if (rota == null)
                throw new Exception($"Não encontrado o id {id} requisitado.");

            _context.RotasViagem.Remove(rota);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.RotasViagem.Any(p => p.Id.Equals(id));
        }

        public TabRota Incluir(TabRota rota)
        {
            try
            {
                _context.RotasViagem.Add(rota);
                _context.SaveChanges();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return rota;
        }
    }
}
