import API_URL from './api'

export async function buscarResumo(mes, ano) {
  const resposta = await fetch(
    `${API_URL}/api/Gastos/resumo?mes=${mes}&ano=${ano}`,
  )

  if (!resposta.ok) {
    throw new Error('Erro ao buscar resumo financeiro.')
  }

  return await resposta.json()
}