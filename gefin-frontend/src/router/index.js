import { createRouter, createWebHistory } from 'vue-router'

import DashboardView from '../views/DashboardView.vue'
import GastosView from '../views/GastosView.vue'
import ReceitasView from '../views/ReceitasView.vue'

const router = createRouter({

  history: createWebHistory(import.meta.env.BASE_URL),

  routes: [

    {
      path: '/',
      name: 'dashboard',
      component: DashboardView,
    },

    {
      path: '/gastos',
      name: 'gastos',
      component: GastosView,
    },

    {
      path: '/receitas',
      name: 'receitas',
      component: ReceitasView,
    },

  ],

})

export default router