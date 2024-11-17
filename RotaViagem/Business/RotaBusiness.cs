using RotaViagem.Interface;
using RotaViagem.Repositorys;
using RotaViagem.Util;
using RotaViagemModel.Model;
using System.Collections.Generic;

namespace RotaViagem.Business
{
    public class RotaBusiness : IRotaBusiness

    {
        private readonly IRotaImplementation _rota;
        public RotaBusiness(IRotaImplementation rota)
        {
            _rota = rota;
        }
        public TabRota Atualizar(int id, TabRota rota)
        {
            return _rota.Atualizar(id, rota);
        }

        public List<TabRota> BuscarOrigem(string origem)
        {
            return _rota.BuscarOrigem(origem);
        }

        public TabRota BuscarPorId(int id)
        {
            return _rota.BuscarPorId(id);
             
        }

        public void Excluir(int id)
        {
            _rota.Excluir(id);
        }

        public TabRota Incluir(TabRota rota)
        {
            return _rota.Incluir(rota);           
        }

        public string MontarRotas(string origem, string destino)
        {
            List<RotaMaisBarata> listRotaMaisBarata = new List<RotaMaisBarata>();
            listRotaMaisBarata = MontarListaDeRota(origem, destino, listRotaMaisBarata);
            return RetornarRotaMelhorPreco(origem, destino, listRotaMaisBarata);

        }
        private List<RotaMaisBarata> MontarListaDeRota(string origem, string destino
                                                        , List<RotaMaisBarata> listRotaMaisBarata
                                                        , string descricaoRota = "", decimal valorRota = 0)
        {
            List<TabRota> rotas = _rota.BuscarOrigem(origem);
            RotaMaisBarata rotaMaisBarata = new RotaMaisBarata();
            string descricaoAnt = descricaoRota;
            foreach (var rota in rotas)
            {
                if (rota.Destino.ToUpper() == destino.ToUpper())
                {
                    descricaoRota = descricaoRota + origem + " - " + rota.Destino ;
                    rotaMaisBarata.Descricao = descricaoRota;
                    rotaMaisBarata.Valor = rota.Valor + valorRota;
                    listRotaMaisBarata.Add(rotaMaisBarata);                    
                    descricaoRota = "";
                }
                else
                {
                    valorRota += rota.Valor;
                    if (descricaoRota != rota.Origem + " - ")
                        descricaoRota = descricaoRota + rota.Origem + " - ";
                    listRotaMaisBarata = MontarListaDeRota(rota.Destino, destino, listRotaMaisBarata, descricaoRota, valorRota);
                    valorRota -= rota.Valor;
                    descricaoRota = descricaoAnt;

                }

            }
            return listRotaMaisBarata;
        }

        private string RetornarRotaMelhorPreco(string origem, string destino,
                                                List<RotaMaisBarata> listRotaMaisBarata)
        {
            RotaMaisBarata rotaMaisBarata = new RotaMaisBarata();
            string retorno = $"Rota não encontada para a origem {origem} e destino {destino}.";
            if (listRotaMaisBarata.Count == 0)
            {
                return retorno;
            }
            if (listRotaMaisBarata.Count == 1)
            {
                retorno = "A rota com o melhor preço é  (" + listRotaMaisBarata[0].Descricao.ToUpper() + ") ao custo de $" + listRotaMaisBarata[0].Valor.ToString() + ".";
                return retorno;
            }
            rotaMaisBarata = listRotaMaisBarata[0];
            for (int i = 1; listRotaMaisBarata.Count - 1 >= i; i++)
            {
                if (rotaMaisBarata.Valor > listRotaMaisBarata[i].Valor)
                {
                    rotaMaisBarata = listRotaMaisBarata[i];
                }
            }
            retorno = "A rota com o melhor preço é (" + rotaMaisBarata.Descricao.ToUpper() + ") ao custo de $" + rotaMaisBarata.Valor.ToString() + ".";

            return retorno;
        }


    }
}
