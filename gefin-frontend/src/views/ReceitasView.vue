<script setup>
import { onMounted, ref } from 'vue'

import {
  buscarReceitas,
  criarReceita,
  atualizarReceita,
  excluirReceita,
} from '../services/receitasService'

// Lista das receitas
const receitas = ref([])

// Controle da tela
const carregando = ref(true)
const erro = ref(null)

// Formulário
const mostrarFormulario = ref(false)

// ID da receita sendo editada
const receitaEditandoId = ref(null)

// Dados do formulário
const novaReceita = ref({
  descricao: '',
  valor: null,
  data: '',
})


// -------------------------
// BUSCAR RECEITAS
// -------------------------

async function carregarReceitas() {
  try {
    carregando.value = true
    erro.value = null

    receitas.value = await buscarReceitas()

  } catch (error) {
    erro.value = error.message

  } finally {
    carregando.value = false
  }
}


// -------------------------
// NOVA RECEITA
// -------------------------

function abrirNovaReceita() {
  receitaEditandoId.value = null

  novaReceita.value = {
    descricao: '',
    valor: null,
    data: '',
  }

  mostrarFormulario.value = true
}


// -------------------------
// EDITAR RECEITA
// -------------------------

function editarReceita(receita) {
  receitaEditandoId.value = receita.id

  novaReceita.value = {
    descricao: receita.descricao,
    valor: receita.valor,
    data: receita.data.substring(0, 10),
  }

  mostrarFormulario.value = true
}


// -------------------------
// SALVAR
// -------------------------

async function salvarReceita() {
  try {
    erro.value = null

    const receita = {
      descricao: novaReceita.value.descricao,
      valor: Number(novaReceita.value.valor),
      data: novaReceita.value.data,
    }

    // Se existe ID, estamos editando
    if (receitaEditandoId.value !== null) {

      await atualizarReceita(
        receitaEditandoId.value,
        receita
      )

    } else {

      // Caso contrário, estamos cadastrando
      await criarReceita(receita)
    }

    limparFormulario()

    await carregarReceitas()

  } catch (error) {
    erro.value = error.message
  }
}


// -------------------------
// EXCLUIR
// -------------------------

async function removerReceita(receita) {

  const confirmou = window.confirm(
    `Deseja realmente excluir a receita "${receita.descricao}"?`
  )

  if (!confirmou) {
    return
  }

  try {
    erro.value = null

    await excluirReceita(receita.id)

    await carregarReceitas()

  } catch (error) {
    erro.value = error.message
  }
}


// -------------------------
// FORMULÁRIO
// -------------------------

function limparFormulario() {

  novaReceita.value = {
    descricao: '',
    valor: null,
    data: '',
  }

  receitaEditandoId.value = null
  mostrarFormulario.value = false
}

function cancelarCadastro() {
  limparFormulario()
}


// -------------------------
// FORMATAÇÃO
// -------------------------

function formatarMoeda(valor) {
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  })
}

function formatarData(data) {
  return new Date(data).toLocaleDateString('pt-BR', {
    timeZone: 'UTC',
  })
}


// Carrega quando a página abre
onMounted(() => {
  carregarReceitas()
})
</script>


<template>

  <main class="receitas">

    <!-- CABEÇALHO -->

    <div class="cabecalho">

      <div>

        <h1>Receitas</h1>

        <p>
          Gerencie suas receitas cadastradas.
        </p>

      </div>

      <button
        class="botao-novo"
        @click="abrirNovaReceita"
      >
        + Nova receita
      </button>

    </div>


    <!-- FORMULÁRIO -->

    <section
      v-if="mostrarFormulario"
      class="formulario"
    >

      <h2>
        {{
          receitaEditandoId !== null
            ? 'Editar receita'
            : 'Nova receita'
        }}
      </h2>

      <form @submit.prevent="salvarReceita">

        <!-- DESCRIÇÃO -->

        <div class="campo">

          <label for="descricao">
            Descrição
          </label>

          <input
            id="descricao"
            v-model="novaReceita.descricao"
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
            v-model="novaReceita.valor"
            type="number"
            step="0.01"
            min="0.01"
            required
          />

        </div>


        <!-- DATA -->

        <div class="campo">

          <label for="data">
            Data
          </label>

          <input
            id="data"
            v-model="novaReceita.data"
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
            {{
              receitaEditandoId !== null
                ? 'Salvar alterações'
                : 'Salvar'
            }}
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
      Carregando receitas...
    </p>


    <!-- SEM RECEITAS -->

    <p v-else-if="receitas.length === 0">
      Nenhuma receita cadastrada.
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
            <th>Data</th>
            <th>Valor</th>
            <th>Ações</th>
          </tr>

        </thead>

        <tbody>

          <tr
            v-for="receita in receitas"
            :key="receita.id"
          >

            <td>
              {{ receita.descricao }}
            </td>

            <td>
              {{ formatarData(receita.data) }}
            </td>

            <td>
              {{ formatarMoeda(receita.valor) }}
            </td>

            <td class="acoes">

              <button
                @click="editarReceita(receita)"
              >
                Editar
              </button>

              <button
                @click="removerReceita(receita)"
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

.receitas {
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

.campo input {
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