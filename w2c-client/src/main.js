import Vue from 'vue';
import App from './App.vue';
import vSelect from 'vue-select';
import VueResource from 'vue-resource';


import 'vue-select/dist/vue-select.css';
Vue.use(VueResource);

Vue.http.options.root = 'http://192.168.0.7:5000/';

Vue.component('v-select', vSelect);
Vue.config.productionTip = false;



new Vue({
  render: h => h(App),
}).$mount('#app');
