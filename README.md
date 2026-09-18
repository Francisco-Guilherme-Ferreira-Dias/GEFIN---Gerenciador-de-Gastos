# GeFinbeta 💰

Um gerenciador de finanças pessoais que comecei enquanto estudava orientação a objetos em C#. A ideia nasceu de um problema bem real: eu queria controlar meus próprios gastos, então resolvi construir a ferramenta em vez de usar um app pronto.

Por enquanto ele roda no console, mas o plano é fazer ele crescer — no fim das contas, meu objetivo é ter uma API de verdade rodando por trás e um front-end web mostrando tudo isso em gráficos.

## O que ele já faz

Hoje dá pra fazer o básico de um controle de gastos:

- Cadastrar um gasto (descrição, valor, data e categoria)
- Listar todos os gastos cadastrados
- Atualizar um gasto existente
- Excluir um gasto
- Tudo isso navegando por um menu simples no terminal

## Como o código está organizado

Enquanto aprendia sobre boas práticas, decidi não deixar tudo jogado dentro do `Program.cs` (como fiz nos primeiros exercícios do curso). Separei o projeto em camadas, cada uma com uma responsabilidade clara:

```
GeFinbeta/
├── Entities/          → as classes que representam os dados (Gasto, enum Categoria)
├── Repository/         → tudo que mexe na lista de gastos (criar, listar, editar, excluir)
├── UI/                 → o menu e a interação com o usuário
└── Program.cs          → só "liga" as peças e inicia o programa
```

A ideia por trás dessa separação: o `Program.cs` não precisa saber *como* um gasto é salvo ou atualizado, só precisa chamar quem sabe fazer isso. Isso deixou o código bem mais fácil de mexer depois — quando eu quis trocar a lógica de edição, por exemplo, só precisei alterar um arquivo, sem quebrar o resto.

## Rodando o projeto

Precisa do .NET instalado. Depois é só:

```bash
dotnet run
```

E seguir o menu que aparece no terminal.

## Próximos passos

Esse projeto ainda está bem no início. A lista do que quero fazer, em ordem:

- [✔️] Persistir os dados de verdade com Entity Framework Core + SQLite (hoje, fechar o programa apaga tudo)
- [✔️] Transformar em uma API com ASP.NET Core 
- [✔️] Conectar com um front-end
- [ ] Gráfico de pizza mostrando gastos por categoria
- [ ] Filtro de gastos por mês

## Por que esse projeto existe

Sou iniciante em C# e decidi que, em vez de só fazer exercícios soltos do curso, valia mais a pena aplicar o que fui aprendendo (herança, polimorfismo, coleções, enums) em algo que eu realmente usaria. Esse repositório é basicamente o registro dessa jornada — vai ficar evoluindo conforme eu for aprendendo mais.
