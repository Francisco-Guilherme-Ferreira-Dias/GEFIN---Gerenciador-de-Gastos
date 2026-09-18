# 💰 GEFIN — Gerenciador Financeiro

O **GEFIN** é uma aplicação web para gerenciamento de finanças pessoais, desenvolvida com o objetivo de colocar em prática conceitos de desenvolvimento Full Stack.

O sistema permite cadastrar, visualizar, editar e excluir gastos e receitas, além de apresentar um dashboard com um resumo financeiro mensal e a distribuição dos gastos por categoria.

> 🚧 Projeto em desenvolvimento. Novas funcionalidades e melhorias visuais ainda serão adicionadas.

---

## 📊 Funcionalidades

### Dashboard

- Visualização do total de receitas do mês
- Visualização do total de gastos do mês
- Cálculo automático do saldo
- Identificação da categoria com maior gasto
- Filtro por mês e ano
- Gráfico de gastos por categoria
- Formatação dos valores em Real (BRL)

### Gastos

- Cadastro de gastos
- Listagem de gastos
- Edição de gastos
- Exclusão de gastos
- Classificação por categoria

Categorias disponíveis atualmente:

- Moradia
- Alimentação
- Transporte
- Outros

### Receitas

- Cadastro de receitas
- Listagem de receitas
- Edição de receitas
- Exclusão de receitas

### Navegação

A aplicação utiliza Vue Router para navegação entre:

- Dashboard
- Gastos
- Receitas

---

## 🛠️ Tecnologias utilizadas

### Backend

- C#
- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

### Frontend

- Vue.js
- JavaScript
- Vite
- Vue Router
- Chart.js
- HTML
- CSS

### Versionamento

- Git
- GitHub

---

## 🏗️ Arquitetura

O projeto é dividido entre backend e frontend.

```text
GEFIN
│
├── GeFinbeta.Api
│   ├── Controllers
│   ├── DTOs
│   └── API REST
│
├── GeFinbeta
│   ├── Data
│   ├── Entities
│   └── SQLite
│
└── gefin-frontend
    └── src
        ├── assets
        ├── router
        ├── services
        └── views
```

O frontend Vue se comunica com a API ASP.NET Core através de requisições HTTP.

```text
Vue.js
   ↓
Services
   ↓
HTTP / JSON
   ↓
ASP.NET Core Web API
   ↓
Entity Framework Core
   ↓
SQLite
```

---

## 🔌 API

A aplicação possui endpoints para gerenciamento de gastos e receitas.

### Gastos

```http
GET    /api/Gastos
GET    /api/Gastos/{id}
POST   /api/Gastos
PUT    /api/Gastos/{id}
DELETE /api/Gastos/{id}
```

Também existem endpoints utilizados pelo dashboard:

```http
GET /api/Gastos/resumo?mes={mes}&ano={ano}

GET /api/Gastos/por-categoria?mes={mes}&ano={ano}
```

### Receitas

```http
GET    /api/Receitas
GET    /api/Receitas/{id}
POST   /api/Receitas
PUT    /api/Receitas/{id}
DELETE /api/Receitas/{id}
```

---

## 📈 Dashboard

O dashboard apresenta informações financeiras de acordo com o mês e ano selecionados.

Entre os dados apresentados estão:

- Receitas
- Gastos
- Saldo
- Categoria com maior gasto
- Distribuição dos gastos por categoria

O gráfico é construído utilizando **Chart.js** a partir dos dados fornecidos pela API.

---

## ▶️ Executando o projeto

### Pré-requisitos

Para executar o projeto localmente é necessário ter instalado:

- .NET SDK
- Node.js
- npm

### Backend

Acesse o projeto da API:

```bash
cd GeFinbeta.Api
```

Execute:

```bash
dotnet run
```

A API será iniciada localmente.

O Swagger pode ser utilizado para visualizar e testar os endpoints.

### Frontend

Em outro terminal, acesse:

```bash
cd gefin-frontend
```

Instale as dependências:

```bash
npm install
```

Execute:

```bash
npm run dev
```

Depois abra no navegador o endereço informado pelo Vite.

---

## 🗺️ Próximas melhorias

Algumas funcionalidades planejadas para as próximas versões:

- Melhorias na identidade visual
- Melhorias de responsividade
- Categorias personalizadas
- Melhorias de validação e experiência do usuário
- Novos gráficos e relatórios
- Autenticação de usuários
- Organização e componentização do frontend

---

## 🎯 Objetivo do projeto

O GEFIN está sendo desenvolvido como projeto de estudo e portfólio, aplicando na prática conceitos como:

- Desenvolvimento de APIs REST
- CRUD
- Integração entre frontend e backend
- Requisições HTTP
- Manipulação de JSON
- Entity Framework Core
- Persistência de dados
- Vue.js
- Roteamento em SPA
- Visualização de dados
- Git e GitHub
