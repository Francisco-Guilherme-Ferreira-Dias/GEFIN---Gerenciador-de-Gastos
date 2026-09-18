<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>

  <div class="app">

    <header class="navbar">

      <div class="navbar-conteudo">

        <RouterLink
          to="/"
          class="logo"
        >
          <span class="logo-icone">$</span>

          GEFIN
        </RouterLink>

        <nav class="menu">

          <RouterLink to="/">
            Dashboard
          </RouterLink>

          <RouterLink to="/gastos">
            Gastos
          </RouterLink>

          <RouterLink to="/receitas">
            Receitas
          </RouterLink>

        </nav>

      </div>

    </header>

    <RouterView />

  </div>

</template>

<style scoped>

/* =========================
   NAVBAR
   ========================= */

.navbar {
  width: 100%;

  background: var(--cor-card);

  border-bottom: 1px solid var(--cor-borda);

  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.04);
}

.navbar-conteudo {
  max-width: 1100px;

  margin: 0 auto;

  padding: 16px 20px;

  display: flex;
  align-items: center;
  justify-content: space-between;
}


/* =========================
   LOGO
   ========================= */

.logo {
  display: flex;
  align-items: center;

  gap: 9px;

  font-size: 22px;
  font-weight: 700;

  text-decoration: none;

  color: var(--cor-texto);
}

.logo-icone {
  width: 36px;
  height: 36px;

  display: flex;
  align-items: center;
  justify-content: center;

  background: var(--cor-primaria);

  color: white;

  border-radius: 9px;

  font-size: 20px;
  font-weight: bold;
}


/* =========================
   MENU
   ========================= */

.menu {
  display: flex;
  align-items: center;

  gap: 8px;
}

.menu a {
  padding: 9px 14px;

  border-radius: 7px;

  text-decoration: none;

  color: var(--cor-texto-secundario);

  font-weight: 500;

  transition:
    background-color 0.2s,
    color 0.2s;
}

.menu a:hover {
  background: #f1f5f9;

  color: var(--cor-texto);
}


/* Página atualmente selecionada */

.menu a.router-link-active {
  background: #dcfce7;

  color: var(--cor-primaria);

  font-weight: 600;
}


/* =========================
   RESPONSIVIDADE
   ========================= */

@media (max-width: 600px) {

  .navbar-conteudo {
    flex-direction: column;

    gap: 14px;
  }

  .menu {
    width: 100%;

    justify-content: center;
  }

  .menu a {
    padding: 8px 10px;
  }

}

</style>