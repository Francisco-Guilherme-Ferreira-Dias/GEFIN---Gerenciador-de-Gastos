using GeFinbeta.Entities.Enums;
using GeFinbeta.Repositories;

namespace GeFinbeta.UI
{
    internal class MenuEscolha
    {
        private readonly GastosRepository _repositorio;

        public MenuEscolha(GastosRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void Iniciar()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine();
                Console.WriteLine("=== MENU ===");
                Console.WriteLine("1 - Adicionar gasto");
                Console.WriteLine("2 - Listar gastos");
                Console.WriteLine("3 - Atualizar gasto");
                Console.WriteLine("4 - Excluir gasto");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": AdicionarGasto(); break;
                    case "2": _repositorio.ListarTodos(); break;
                    case "3": AtualizarGasto(); break;
                    case "4": ExcluirGasto(); break;
                    case "5":
                        continuar = false;
                        Console.WriteLine("Até mais!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private void AdicionarGasto()
        {
            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Valor: ");
            decimal valor = decimal.Parse(Console.ReadLine());

            Console.Write("Data (dd/MM/yyyy): ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            Console.Write("Categoria (Moradia, Alimentacao, Transporte, Outros): ");
            Categoria categoria = (Categoria)Enum.Parse(typeof(Categoria), Console.ReadLine(), true);

            _repositorio.Adicionar(descricao, valor, data, categoria);
            Console.WriteLine("Gasto adicionado com sucesso!");
        }

        private void AtualizarGasto()
        {
            _repositorio.ListarTodos();

            Console.Write("Digite o Id do gasto que deseja atualizar: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Novo valor: ");
            decimal novoValor = decimal.Parse(Console.ReadLine());

            Console.Write("Nova data (dd/MM/yyyy): ");
            DateTime novaData = DateTime.Parse(Console.ReadLine());

            Console.Write("Nova categoria: ");
            Categoria novaCategoria = (Categoria)Enum.Parse(typeof(Categoria), Console.ReadLine(), true);

            _repositorio.AtualizarGasto(id, novoValor, novaData, novaCategoria);
        }

        private void ExcluirGasto()
        {
            _repositorio.ListarTodos();

            Console.Write("Digite o Id do gasto que deseja excluir: ");
            int id = int.Parse(Console.ReadLine());

            _repositorio.ExcluirGasto(id);
        }
    }
}