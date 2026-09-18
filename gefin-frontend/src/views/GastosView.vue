<script setup>
import { onMounted, ref } from 'vue'
import {
  buscarGastos,
  criarGasto,
  excluirGasto,
  atualizarGasto,
} from '../services/gastosService'

// Lista de gastos
const gastos = ref([])

// Controle da tela
const carregando = ref(true)
const erro = ref(null)

// Controla se o formulário aparece
const mostrarFormulario = ref(false)

// Guarda o ID quando estivermos editando
const gastoEditandoId = ref(null)

// Dados do formulário
const novoGasto = ref({
  descricao: '',
  valor: null,
  categoria: 0,
  data: '',
})

// Busca os gastos da API
async function carregarGastos() {
  try {
    carregando.value = true
    erro.value = null

    gastos.value = await buscarGastos()
  } catch (error) {
    erro.value = error.message
  } finally {
    carregando.value = false
  }
}

// Abre o formulário para cadastrar
function abrirNovoGasto() {
  gastoEditandoId.value = null

  novoGasto.value = {
    descricao: '',
    valor: null,
    categoria: 0,
    data: '',
  }

  mostrarFormulario.value = true
}

// Abre o formulário preenchido para edição
function editarGasto(gasto) {
  gastoEditandoId.value = gasto.id

  novoGasto.value = {
    descricao: gasto.descricao,
    valor: gasto.valor,
    categoria: gasto.categoria,
    data: gasto.data.substring(0, 10),
  }

  mostrarFormulario.value = true
}

// Salva um gasto novo ou uma edição
async function salvarGasto() {
  try {
    erro.value = null

    const gasto = {
      descricao: novoGasto.value.descricao,
      valor: Number(novoGasto.value.valor),
      categoria: Number(novoGasto.value.categoria),
      data: novoGasto.value.data,
    }

    // Se existe um ID, estamos editando
    if (gastoEditandoId.value !== null) {
      await atualizarGasto(
        gastoEditandoId.value,
        gasto
      )
    } else {
      // Caso contrário, estamos cadastrando
      await criarGasto(gasto)
    }

    limparFormulario()

    await carregarGastos()

  } catch (error) {
    erro.value = error.message
  }
}

// Limpa e fecha o formulário
function limparFormulario() {
  novoGasto.value = {
    descricao: '',
    valor: null,
    categoria: 0,
    data: '',
  }

  gastoEditandoId.value = null
  mostrarFormulario.value = false
}

// Cancela cadastro ou edição
function cancelarCadastro() {
  limparFormulario()
}

// Exclui um gasto
async function removerGasto(gasto) {
  const confirmou = window.confirm(
    `Deseja realmente excluir o gasto "${gasto.descricao}"?`
  )

  if (!confirmou) {
    return
  }

  try {
    erro.value = null

    await excluirGasto(gasto.id)

    await carregarGastos()

  } catch (error) {
    erro.value = error.message
  }
}

// Formata dinheiro
function formatarMoeda(valor) {
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  })
}

// Formata data
function formatarData(data) {
  return new Date(data).toLocaleDateString('pt-BR', {
    timeZone: 'UTC',
  })
}

onMounted(() => {
  carregarGastos()
})
</script>

<template>
  <main class="gastos">

    <!-- CABEÇALHO -->
    <div class="cabecalho">

      <div>
        <h1>Gastos</h1>

        <p>
          Gerencie seus gastos cadastrados.
        </p>
      </div>

      <button
        class="botao-novo"
        @click="abrirNovoGasto"
      >
        + Novo gasto
      </button>

    </div>


    <!-- FORMULÁRIO -->
    <section
      v-if="mostrarFormulario"
      class="formulario"
    >

      <h2>
        {{ gastoEditandoId !== null ? 'Editar gasto' : 'Novo gasto' }}
      </h2>

      <form @submit.prevent="salvarGasto">

        <!-- DESCRIÇÃO -->
        <div class="campo">
          <label for="descricao">
            Descrição
          </label>

          <input
            id="descricao"
            v-model="novoGasto.descricao"
            type="text"
            required
          />
        </div>


        <!-- VALOR -->
        <div class="campo">
          <label for="valor">
            Valor
          </label>

          <input
            id="valor"
            v-model="novoGasto.valor"
            type="number"
            step="0.01"
            min="0.01"
            required
          />
        </div>


        <!-- CATEGORIA -->
        <div class="campo">
          <label for="categoria">
            Categoria
          </label>

          <select
            id="categoria"
            v-model="novoGasto.categoria"
            required
          >
            <option :value="0">Moradia</option>
            <option :value="1">Alimentação</option>
            <option :value="2">Transporte</option>
            <option :value="3">Outros</option>
          </select>
        </div>


        <!-- DATA -->
        <div class="campo">
          <label for="data">
            Data
          </label>

          <input
            id="data"
            v-model="novoGasto.data"
            type="date"
            required
          />
        </div>


        <!-- BOTÕES -->
        <div class="botoes-formulario">

          <button
            type="submit"
            class="botao-salvar"
          >
            {{ gastoEditandoId !== null ? 'Salvar alterações' : 'Salvar' }}
          </button>

          <button
            type="button"
            @click="cancelarCadastro"
          >
            Cancelar
          </button>

        </div>

      </form>

    </section>


    <!-- ERRO -->
    <p
      v-if="erro"
      class="erro"
    >
      {{ erro }}
    </p>


    <!-- CARREGAMENTO -->
    <p v-if="carregando">
      Carregando gastos...
    </p>


    <!-- SEM GASTOS -->
    <p v-else-if="gastos.length === 0">
      Nenhum gasto cadastrado.
    </p>


    <!-- TABELA -->
    <div
      v-else
      class="tabela-container"
    >

      <table>

        <thead>
          <tr>
            <th>Descrição</th>
            <th>Categoria</th>
            <th>Data</th>
            <th>Valor</th>
            <th>Ações</th>
          </tr>
        </thead>

        <tbody>

          <tr
            v-for="gasto in gastos"
            :key="gasto.id"
          >

            <td>
              {{ gasto.descricao }}
            </td>

            <td>
              {{ gasto.categoria }}
            </td>

            <td>
              {{ formatarData(gasto.data) }}
            </td>

            <td>
              {{ formatarMoeda(gasto.valor) }}
            </td>

            <td class="acoes">

              <button
                @click="editarGasto(gasto)"
              >
                Editar
              </button>

              <button
                class="botao-excluir"
                @click="removerGasto(gasto)"
              >
                Excluir
              </button>

            </td>

          </tr>

        </tbody>

      </table>

    </div>

  </main>
</template>

<style scoped>
.gastos {
  max-width: 1100px;
  margin: 0 auto;
  padding: 40px 20px;
}


/* CABEÇALHO */

.cabecalho {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
}

.cabecalho h1 {
  margin-bottom: 5px;
}

.cabecalho p {
  margin-top: 0;
}

.botao-novo {
  padding: 10px 18px;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}


/* FORMULÁRIO */

.formulario {
  margin-bottom: 30px;
  padding: 25px;
  border: 1px solid #ddd;
  border-radius: 10px;
}

.formulario h2 {
  margin-top: 0;
}

.campo {
  display: flex;
  flex-direction: column;
  gap: 5px;
  margin-bottom: 15px;
}

.campo input,
.campo select {
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 16px;
}

.botoes-formulario {
  display: flex;
  gap: 10px;
  margin-top: 20px;
}

.botoes-formulario button {
  padding: 10px 18px;
  border-radius: 6px;
  cursor: pointer;
}

.botao-salvar {
  border: none;
}

.erro {
  margin-bottom: 20px;
}


/* TABELA */

.tabela-container {
  overflow-x: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th,
td {
  padding: 15px;
  text-align: left;
  border-bottom: 1px solid #ddd;
}

.acoes {
  display: flex;
  gap: 10px;
}

.acoes button {
  padding: 6px 10px;
  cursor: pointer;
}


/* RESPONSIVIDADE */

@media (max-width: 700px) {

  .cabecalho {
    flex-direction: column;
    align-items: flex-start;
    gap: 15px;
  }

}
</style>