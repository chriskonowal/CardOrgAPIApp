<template>
  <v-app>
    <Navigation></Navigation>
    <router-view></router-view>
  </v-app>
</template>

<script>
import Navigation from "./components/Navigation.vue";
//import Home from './views/Home.vue';
//import axios from 'axios';

export default {
  name: "App",
  components: {
    Navigation,
  },
  data() {
    return {
      info: null,
    };
  },
  // mounted () {
  //   axios
  //     .get('http://localhost:54421/api/admin/years/1')
  //     .then(response => (this.info = response))
  // }
};
</script>

<style></style>
