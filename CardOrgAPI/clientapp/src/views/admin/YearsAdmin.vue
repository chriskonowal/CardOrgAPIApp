<template>
  <div class="wrapper">
    <router-link to="/home">Testing Link</router-link>
      <h1>Years</h1>
      <h2>This is from the api:</h2>
      <div>
        <v-card>
      <v-card-title>
        Years
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Search"
          single-line
          hide-details
          @blur="readDataFromAPI()"
          @keyup="readDataFromAPI()"
        ></v-text-field>
      </v-card-title>
      <v-data-table
        :headers="headers"
        :page="page"
        :items="years"
        :options.sync="options"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        :server-items-length="totalYears"
        :loading="loading"
        :items-per-page="10"
        :search="search"
        class="elevation-1"
      ></v-data-table>
    </v-card>
    </div>
  </div>
</template>

<script>
import axios from "axios"
export default {
  name: 'YearsAdmin',
  data () {
    return {
      totalYears: 0,
      page: 1,
      years: [],
      loading: true,
      options: {},
      numberOfPages: 0,
      sortBy: '',
      sortDesc: false,
      search: '',
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
      handler(newVal, oldVal){
          if(newVal != oldVal){
            this.readDataFromAPI()
          }
        },
      deep: true,
    },
  },
  methods: {
    readDataFromAPI () {
      this.loading = true;
      const { sortBy, sortDesc, page, itemsPerPage } = this.options;
      console.log(this.search);
      var searchYear = 0;
      if (this.search.toString().length == 4  && isInt(this.search))
      {
        searchYear = parseInt(this.search); 
      }
     


      const request = { 
        searchYear: searchYear,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy[0],
        isSortDesc: sortDesc[0]
        };
        console.log(request);
      axios
        .post(
          "http://localhost:54421/api/admin/years", request
        )
        .then((response) => {
          //Then injecting the result to datatable parameters.
          console.log(response.data);
          this.loading = false;
          this.years = response.data.years;
          this.totalYears = response.data.totalYears;
          //this.totalPassengers = response.data.totalPassengers;
          //this.numberOfPages = response.data.totalPages;
        });

        function isInt(n) {
          return n % 1 === 0;
        }
    }
  },
  mounted() {
    this.readDataFromAPI();
  },
};
</script>


<style>

</style>