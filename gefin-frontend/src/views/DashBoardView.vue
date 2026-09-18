<script setup>
import { onMounted, ref, nextTick } from 'vue'
import Chart from 'chart.js/auto'

import {
  buscarResumo,
  buscarGastosPorCategoria,
} from '../services/gastosService'

// Data atual
const hoje = new Date()

// Período selecionado
const mesSelecionado = ref(hoje.getMonth() + 1)
const anoSelecionado = ref(hoje.getFullYear())

// Dados da API
const resumo = ref(null)
const gastosPorCategoria = ref([])

// Controle da tela
const carregando = ref(true)
const erro = ref(null)

// Referência para o <canvas> do gráfico
const graficoCanvas = ref(null)

// Guarda a instância atual do Chart.js
let grafico = null

// Formata valores como Real brasileiro
function formatarMoeda(valor) {
  return valor.toLocaleString('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  })
}

// Cria ou recria o gráfico
async function criarGrafico() {
  // Espera o Vue terminar de atualizar o HTML
  await nextTick()

  // Se não existir o canvas, não tenta criar o gráfico
  if (!graficoCanvas.value) {
    return
  }

  // Destrói o gráfico antigo antes de criar outro
  if (grafico) {
    grafico.destroy()
  }

  // Se não houver gastos, não cria gráfico
  if (gastosPorCategoria.value.length === 0) {
    grafico = null
    return
  }

  grafico = new Chart(graficoCanvas.value, {
    type: 'pie',

    data: {
      labels: gastosPorCategoria.value.map(
        item => item.categoria
      ),

      datasets: [
        {
          label: 'Gastos',
          data: gastosPorCategoria.value.map(
            item => item.total
          ),
        },
      ],
    },

    options: {
      responsive: true,
      maintainAspectRatio: false,

      plugins: {
        legend: {
          position: 'bottom',
        },

        tooltip: {
          callbacks: {
            label: function (context) {
              return `${context.label}: ${formatarMoeda(context.raw)}`
            },
          },
        },
      },
    },
  })
}

// Busca todos os dados necessários para o dashboard
async function carregarDashboard() {
  try {
    carregando.value = true
    erro.value = null

    // Busca o resumo e os gastos por categoria
    // ao mesmo tempo
    const [dadosResumo, dadosCategorias] = await Promise.all([
      buscarResumo(
        mesSelecionado.value,
        anoSelecionado.value
      ),

      buscarGastosPorCategoria(
        mesSelecionado.value,
        anoSelecionado.value
      ),
    ])

    resumo.value = dadosResumo
    gastosPorCategoria.value = dadosCategorias

  } catch (error) {
    erro.value = error.message

  } finally {
    carregando.value = false
  }

  // Atualiza o gráfico depois dos dados
  await criarGrafico()
}

// Executa quando o Dashboard é aberto
onMounted(() => {
  carregarDashboard()
})
</script>

<template>
  <main class="dashboard">

    <h1>GEFIN</h1>

    <p class="subtitulo">
      Gerenciador Financeiro
    </p>

    <!-- FILTROS -->
    <section class="filtros">

      <div>
        <label for="mes">Mês</label>

        <select
          id="mes"
          v-model="mesSelecionado"
          @change="carregarDashboard"
        >
          <option :value="1">Janeiro</option>
          <option :value="2">Fevereiro</option>
          <option :value="3">Março</option>
          <option :value="4">Abril</option>
          <option :value="5">Maio</option>
          <option :value="6">Junho</option>
          <option :value="7">Julho</option>
          <option :value="8">Agosto</option>
          <option :value="9">Setembro</option>
          <option :value="10">Outubro</option>
          <option :value="11">Novembro</option>
          <option :value="12">Dezembro</option>
        </select>
      </div>

      <div>
        <label for="ano">Ano</label>

        <select
          id="ano"
          v-model="anoSelecionado"
          @change="carregarDashboard"
        >
          <option :value="2024">2024</option>
          <option :value="2025">2025</option>
          <option :value="2026">2026</option>
        </select>
      </div>

    </section>

    <!-- CARREGAMENTO -->
    <p v-if="carregando">
      Carregando dados...
    </p>

    <!-- ERRO -->
    <p v-else-if="erro">
      {{ erro }}
    </p>

    <!-- CONTEÚDO -->
    <template v-else>

      <!-- CARDS -->
      <section class="cards">

        <div class="card">
          <h2>Receitas</h2>

          <p>
            {{ formatarMoeda(resumo.totalReceitas) }}
          </p>
        </div>

        <div class="card">
          <h2>Gastos</h2>

          <p>
            {{ formatarMoeda(resumo.totalGasto) }}
          </p>
        </div>

        <div class="card">
          <h2>Saldo</h2>

          <p>
            {{ formatarMoeda(resumo.saldo) }}
          </p>
        </div>

      </section>

      <!-- MAIOR CATEGORIA -->
      <section class="maior-categoria">

        <h2>Categoria com maior gasto</h2>

        <p v-if="resumo.maiorCategoria">
          {{ resumo.maiorCategoria }}
          —
          {{ formatarMoeda(resumo.valorMaiorCategoria) }}
        </p>

        <p v-else>
          Nenhum gasto registrado neste mês.
        </p>

      </section>

      <!-- GRÁFICO -->
      <section class="grafico-container">

        <h2>Gastos por categoria</h2>

        <div
          v-if="gastosPorCategoria.length > 0"
          class="grafico"
        >
          <canvas ref="graficoCanvas"></canvas>
        </div>

        <p v-else>
          Nenhum gasto para exibir no gráfico.
        </p>

      </section>

    </template>

  </main>
</template>

<style scoped>

.dashboard {
  max-width: 1100px;
  margin: 0 auto;
  padding: 40px 20px;
}

.dashboard h1 {
  margin-bottom: 0;
}

.subtitulo {
  margin-top: 5px;
  margin-bottom: 30px;
}


/* FILTROS */

.filtros {
  display: flex;
  gap: 20px;
  margin-bottom: 30px;
}

.filtros div {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.filtros select {
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 16px;
}


/* CARDS */

.cards {
  display: flex;
  gap: 20px;
}

.card {
  flex: 1;
  padding: 25px;
  border: 1px solid #ddd;
  border-radius: 10px;
}

.card h2 {
  margin-top: 0;
}

.card p {
  font-size: 24px;
  font-weight: bold;
}


/* MAIOR CATEGORIA */

.maior-categoria {
  margin-top: 30px;
  padding: 25px;
  border: 1px solid #ddd;
  border-radius: 10px;
}


/* GRÁFICO */

.grafico-container {
  margin-top: 30px;
  padding: 25px;
  border: 1px solid #ddd;
  border-radius: 10px;
}

.grafico-container h2 {
  margin-top: 0;
}

.grafico {
  position: relative;
  height: 400px;
  max-width: 600px;
  margin: 0 auto;
}


/* RESPONSIVIDADE */

@media (max-width: 700px) {

  .cards {
    flex-direction: column;
  }

  .filtros {
    flex-direction: column;
  }

  .grafico {
    height: 300px;
  }

}

</style>