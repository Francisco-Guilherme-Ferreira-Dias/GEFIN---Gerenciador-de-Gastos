import API_URL from './api'

export async function buscarResumo(mes, ano) {
  const resposta = await fetch(
    `${API_URL}/api/Gastos/resumo?mes=${mes}&ano=${ano}`
  )

  if (!resposta.ok) {
    throw new Error('Erro ao buscar resumo financeiro.')
  }

  return await resposta.json()
}

export async function buscarGastosPorCategoria(mes, ano) {
  const resposta = await fetch(
    `${API_URL}/api/Gastos/por-categoria?mes=${mes}&ano=${ano}`
  )

  if (!resposta.ok) {
    throw new Error('Erro ao buscar gastos por categoria.')
  }

  return await resposta.json()
}

export async function buscarGastos() {
  const resposta = await fetch(
    `${API_URL}/api/Gastos`
  )

  if (!resposta.ok) {
    throw new Error('Erro ao buscar gastos.')
  }

  return await resposta.json()
}

export async function criarGasto(gasto) {
  const resposta = await fetch(
    `${API_URL}/api/Gastos`,
    {
      method: 'POST',

      headers: {
        'Content-Type': 'application/json',
      },

      body: JSON.stringify(gasto),
    }
  )

  if (!resposta.ok) {
    throw new Error('Erro ao cadastrar gasto.')
  }

  return await resposta.json()
}

export async function excluirGasto(id) {
  const resposta = await fetch(
    `${API_URL}/api/Gastos/${id}`,
    {
      method: 'DELETE',
    }
  )

  if (!resposta.ok) {
    throw new Error('Erro ao excluir gasto.')
  }
}

export async function atualizarGasto(id, gasto) {
  const resposta = await fetch(
    `${API_URL}/api/Gastos/${id}`,
    {
      method: 'PUT',

      headers: {
        'Content-Type': 'application/json',
      },

      body: JSON.stringify(gasto),
    }
  )

  if (!resposta.ok) {
    throw new Error('Erro ao atualizar gasto.')
  }

  return await resposta.json()
}
