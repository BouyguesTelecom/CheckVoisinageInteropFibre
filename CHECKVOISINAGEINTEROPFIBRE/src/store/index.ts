import Vue from 'vue'
import Vuex from 'vuex'
import { Intervention } from '@/types/DTO/intervention';
import { User } from '@/types/DTO/user';
import HomeService from '@/services/HomeService';

import AuthService from '@/services/AuthService';
const $HomeService = new HomeService();
const $AuthService = new AuthService();

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    currentIntervention: {} as Intervention,
    newIntervention: {} as Intervention,    
    currentTab: '' as string,
    newInterventionName: '' as string,
    currentProfile: { ID: 1, Name: 'Administrateur' } as any,
    currentUser: {} as User,
    isAuthenticated: true,
    toUpdateAdminBlocks: [],
    commentsClientKO: [],
    mutualisationsPM: [],
    historyInfo: {} as any
  },
  getters: {
    getCurrentTab: state => state.currentTab,
    getCurrentIntervention: state => state.currentIntervention,
    getNewIntervention: state => state.newIntervention,
    getNewInterventionName: state => state.newInterventionName,
    getCurrentProfile: state => state.currentProfile,
    getCurrentUser: state => state.currentUser,
    getToUpdateAdminBlocks: state => state.toUpdateAdminBlocks,
    getCommentsClientKO: state => state.commentsClientKO,
    getMutualisationPoints: state => state.mutualisationsPM,
    getHistoryInfo: state => state.historyInfo 
  },
  mutations: {
    'CURRENT_MAIN_TAB': (state, tab) => {
      state.currentTab = tab;
    },
    'CURRENT_INTERVENTION': (state, intervention) => {
      state.currentIntervention = intervention;
    },
    'NEW_INTERVENTION_ID': (state, intervention) => {
      state.newInterventionName = intervention;
    },
    'CURRENT_PROFILE': (state, profile) => {
      state.currentProfile = profile;
    },
    'NEW_INTERVENTION':(state, intervention) => {
      state.newIntervention = intervention;
    },
    'SET_CURRENT_USER':(state, user) => {
      state.currentUser = user;
    },
    'SET_TO_UPDATE_ADMIN_BLOCK':(state, blocks) => {
      state.toUpdateAdminBlocks = blocks;
    },
    'COMMENTS_CLIENT_KO':(state, rows) => {
      state.commentsClientKO = rows;
    },
    'SET_ALL_PMS':(state, rows) => {
      state.mutualisationsPM = rows;
    },
    'HISTORY_INFO':(state, obj) => {
      state.historyInfo = obj;
    }
  },
  actions: {
    async currentUser(context) {
      const resp = await $AuthService.GetLoggedUser();
      context.state.currentUser = resp.object;
      
    },
    async allPms(context) {
      const resp = await $HomeService.GetMutualisationPoints();
      context.state.mutualisationsPM = Object.freeze(resp.object);      
    }
  },
  modules: {
  }
})

export interface Getters {
  getCommentsClientKO: {};
  getCurrentIntervention: Intervention;
  getCurrentProfile: {};
  getCurrentTab: string;
  getCurrentUser: {};
  getHistoryInfo: {};
  getMutualisationPoints: [];
  getNewIntervention: Intervention;
  getNewInterventionName: string;
  getToUpdateAdminBlocks: string[];
}