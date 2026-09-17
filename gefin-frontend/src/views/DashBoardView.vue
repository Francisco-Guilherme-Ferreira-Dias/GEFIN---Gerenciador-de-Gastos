<script setup>
import { onMounted, ref } from 'vue'
import { buscarResumo } from '../services/gastosService'

const resumo = ref(null)
const carregando = ref(true)
const erro = ref(null)

async function carregarResumo() {
  try {
    const hoje = new Date()

    const mes = hoje.getMonth() + 1
    const ano = hoje.getFullYear()

    resumo.value = await buscarResumo(mes, ano)
  } catch (error) {
    erro.value = error.message
  } finally {
    carregando.value = false
  }
}

onMounted(() => {
  carregarResumo()
})
</script>

<template>
  <main class="dashboard">
    <h1>GEFIN</h1>
    <p class="subtitulo">Gerenciador Financeiro</p>

    <p v-if="carregando">
      Carregando dados...
    </p>

    <p v-else-if="erro">
      {{ erro }}
    </p>

    <template v-else>
      <section class="cards">

        <div class="card">
          <h2>Receitas</h2>
          <p>R$ {{ resumo.totalReceitas.toFixed(2) }}</p>
        </div>

        <div class="card">
          <h2>Gastos</h2>
          <p>R$ {{ resumo.totalGasto.toFixed(2) }}</p>
        </div>

        <div class="card">
          <h2>Saldo</h2>
          <p>R$ {{ resumo.saldo.toFixed(2) }}</p>
        </div>

      </section>

      <section class="maior-categoria">
        <h2>Categoria com maior gasto</h2>

        <p v-if="resumo.maiorCategoria">
          {{ resumo.maiorCategoria }}
          — R$ {{ resumo.valorMaiorCategoria.toFixed(2) }}
        </p>

        <p v-else>
          Nenhum gasto registrado neste mês.
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

.maior-categoria {
  margin-top: 30px;
  padding: 25px;
  border: 1px solid #ddd;
  border-radius: 10px;
}

@media (max-width: 700px) {
  .cards {
    flex-direction: column;
  }
}
</style>