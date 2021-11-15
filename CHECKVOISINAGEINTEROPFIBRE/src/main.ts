import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from './store';

import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "bootstrap/dist/css/bootstrap.css";

import 'devextreme/dist/css/dx.common.css';
import 'devextreme/dist/css/dx.darkmoon.css';

import { locale, loadMessages } from "devextreme/localization";
const frMessages = require("devextreme/localization/messages/fr.json");

import { library } from '@fortawesome/fontawesome-svg-core';
import { faCamera, faFileExport, faCog, faPlus, faHistory, faTimes, faCheck, faFlag, faPen, faSave, faCopy, faUserPlus, faExpandAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import moment from 'moment';
import VueMoment from 'vue-moment';
library.add(faCamera);
library.add(faFileExport);
library.add(faCog);
library.add(faPlus);
library.add(faHistory);
library.add(faTimes);
library.add(faCheck);
library.add(faFlag);
library.add(faPen);
library.add(faSave);
library.add(faCopy);
library.add(faUserPlus);
library.add(faExpandAlt);

Vue.use(VueMoment, { moment });

Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.use(BootstrapVue);
Vue.use(IconsPlugin);
Vue.config.productionTip = false;

var vm = new Vue({

  router,
  store,
  render: h => h(App),
  beforeMount(){
    store.dispatch('currentUser');
    store.dispatch('allPms');
  },
  created() {
    loadMessages(frMessages);
    locale(navigator.language);
    window.onbeforeunload = function () {
      store.commit('SET_CURRENT_USER', {});
      store.commit('SET_ALL_PMS', []);
    };
  },
}).$mount("#app");
