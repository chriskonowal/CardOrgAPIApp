import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'
import VueGoogleCharts from 'vue-google-charts'
import './assets/style.css';

Vue.config.productionTip = false

new Vue({
  vuetify,
  router,
  VueGoogleCharts,
  render: h => h(App)
}).$mount('#app')

