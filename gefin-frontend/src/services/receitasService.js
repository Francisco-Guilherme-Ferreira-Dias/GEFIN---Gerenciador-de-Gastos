import API_URL from './api'

// GET - Lista todas as receitas
export async function buscarReceitas() {
  const resposta = await fetch(
    `${API_URL}/api/Receitas`
  )

  if (!resposta.ok) {
    throw new Error('Erro ao buscar receitas.')
  }

  return await resposta.json()
}

// POST - Cadastra uma nova receita
export async function criarReceita(receita) {
  const resposta = await fetch(
    `${API_URL}/api/Receitas`,
    {
      method: 'POST',

      headers: {
        'Content-Type': 'application/json',
      },

      body: JSON.stringify(receita),
    }
  )

  if (!resposta.ok) {
    throw new Error('Erro ao cadastrar receita.')
  }

  return await resposta.json()
}

// PUT - Atualiza uma receita
export async function atualizarReceita(id, receita) {
  const resposta = await fetch(
    `${API_URL}/api/Receitas/${id}`,
    {
      method: 'PUT',

      headers: {
        'Content-Type': 'application/json',
      },

      body: JSON.stringify(receita),
    }
  )

  if (!resposta.ok) {
    throw new Error('Erro ao atualizar receita.')
  }

  return await resposta.json()
}

// DELETE - Exclui uma receita
export async function excluirReceita(id) {
  const resposta = await fetch(
    `${API_URL}/api/Receitas/${id}`,
    {
      method: 'DELETE',
    }
  )

  if (!resposta.ok) {
    throw new Error('Erro ao excluir receita.')
  }
}   