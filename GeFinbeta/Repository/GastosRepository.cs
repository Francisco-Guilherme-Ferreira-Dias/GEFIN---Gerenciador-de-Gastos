using GeFinbeta.Entities;
using GeFinbeta.Entities.Enums;

namespace GeFinbeta.Repositories
{
    internal class GastosRepository
    {
        private List<Gastos> _gastos = new List<Gastos>();
        private int nextId = 1;

        public void Adicionar(string descricao, double valor, DateTime data, Categoria categoria)
        {
            _gastos.Add(new Gastos(descricao, valor, data, categoria, nextId));
            nextId++;
        }

        public void AtualizarGasto(int id, double novoValor, DateTime novaData, Categoria novaCategoria)
        {
            var gastoParaAtualizar = _gastos.FirstOrDefault(g => g.Id == id);
            if (gastoParaAtualizar != null)
            {
                gastoParaAtualizar.Valor = novoValor;
                gastoParaAtualizar.Data = novaData;
                gastoParaAtualizar.categoria = novaCategoria;
                Console.WriteLine("Gasto atualizado com sucesso!");
            } else
            {
                Console.WriteLine("Gasto não encontrado.");
            }
        }

        public void ExcluirGasto(int id)
        {
            var gastoParaExcluir = _gastos.FirstOrDefault(g => g.Id == id);
            if (gastoParaExcluir != null)
            {
                _gastos.Remove(gastoParaExcluir);
                Console.WriteLine("Gasto excluído com sucesso!");
            } else
            {
                Console.WriteLine("Gasto não encontrado.");
            }
        }

        public void ListarTodos()
        {
            if (_gastos.Count == 0)
            {
                Console.WriteLine("Nenhum gasto cadastrado.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== LISTA DE GASTOS ===");
            foreach (var gasto in _gastos)
            {
                Console.WriteLine(gasto);
            }
        }
    }
}