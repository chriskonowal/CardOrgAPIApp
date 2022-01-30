<template>
  <div class="wrapper">
      <h2>This is from the api:</h2>
      <p>{{ info }}</p>
    <router-link to="/page10">Testing Link</router-link>
  </div>
</template>

<script>
import axios from "axios"
export default {
  name: 'Home',
  data () {
    return {
      info: ''
    }
  },
  mounted () {
    axios
      .get('http://localhost:54421/api/admin/years/1')
      .then(response => (this.info = response))
  }
};
</script>


<style>

</style>