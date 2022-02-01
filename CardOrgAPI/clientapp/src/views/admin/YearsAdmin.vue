<template>
  <div class="wrapper">
    <router-link to="/home">Testing Link</router-link>
      <h1>Years</h1>
      <h2>This is from the api:</h2>
      <div>
      <v-data-table
        :headers="headers"
        :page="page"
        :items="years"
        :options.sync="options"
        :server-items-length="totalYears"
        :loading="loading"
        :items-per-page="10" 
        class="elevation-1"
      ></v-data-table>
    </div>
    
  </div>
</template>

<script>
import axios from "axios"
export default {
  name: 'YearsAdmin',
  data () {
    return {
      totalYears: 10,
      page: 0,
      years: [],
      loading: true,
      options: {},
      headers: [
        {
          text: 'Beginning Year',
          align: 'start',
          sortable: true,
          value: 'beginningYear',
        },
        { text: 'End Year', value: 'endingYear' }
      ],
    }
  },
  watch: {
    options: {
      handler () {
        this.readDataFromAPI()
      },
      deep: true,
    },
  },
  methods: {
    readDataFromAPI () {
      this.loading = true;
      const { page } = this.options;
      let pageNumber = page + 1;
      axios
        .get(
          "http://localhost:54421/api/admin/years/" +
            pageNumber
        )
        .then((response) => {
          //Then injecting the result to datatable parameters.
          console.log(response.data);
          this.loading = false;
          this.years = response.data;
          //this.totalPassengers = response.data.totalPassengers;
          //this.numberOfPages = response.data.totalPages;
        });
    }
  },
  mounted() {
    this.readDataFromAPI();
  },
};
</script>


<style>

</style>