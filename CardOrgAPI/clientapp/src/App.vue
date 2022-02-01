<template>
    <v-app>
        <router-view></router-view>
         <div class="hello">
           Test
           </div>
         
    </v-app>
   
</template>

<script>
//import Home from './views/Home.vue';
//import axios from 'axios';

export default {
  name: 'App',
  components: {
  },
  data () {
    return {
      info: null
    }
  },
  // mounted () {
  //   axios
  //     .get('http://localhost:54421/api/admin/years/1')
  //     .then(response => (this.info = response))
  // }
};
</script>

<style>
</style>
