import Vue from "vue";
import VueRouter, { RouteConfig } from "vue-router";
import Home from "../views/Home.vue";

import Admin from "../views/Admin.vue";
import Export from "../views/Export.vue";
import Register from "../views/Register.vue";
import store from '@/store';
import { User } from "@/types/DTO/user";

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {
        requiresAuth: true
    }
  },
  {
    path: '/admin',
    name: 'Admin',
    component: Admin,
    meta: {
        requiresAuth: true,
        isAdmin: true
    }
  },
  {
    path: '/export',
    name: 'Export',
    component: Export,
    meta: {
      requiresAuth: true
    }
  },
  {
    path: '/register',
    name: 'Register',
    component: Register,
    meta: {
      requiresAuth: false,
      requiresRegister: true,
    }
  },
  {
    path: "/*",
    redirect: { name: 'Home' },
    meta: {
      requiresAuth: true
    }
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
  linkActiveClass: "active"
});

router.beforeEach((to, from, next) => {
  const currentUser: User = store.getters.getCurrentUser as User  
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!currentUser || !currentUser.profil) {
      next({ name: 'Register' })
    } else {
          if (to.matched.some(record => record.meta.isAdmin)) {
              // route requires admin, check if user is admin
              // if not, redirect to home page.
              if(currentUser.profil === 'Administrateur') {
                next();
              }
              else {
                next({ name: 'Home' });
              }                  
          } else {
              next();
          }      
    }
  }
  else if (to.matched.some(record => record.meta.requiresRegister)) {
    if (currentUser && currentUser.profil) {
      next({ name: 'Home' })
    }
    else {
      next();
    }
  }
  else {
      next();
  }
});
export default router;
