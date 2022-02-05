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
          <v-btn
            color="primary"
            dark
            class="mb-2 btnAdd"
            @click="openNewItemDialog"
          >
            New Item
          </v-btn>
        </v-card-title>
        <v-dialog v-model="dialog" max-width="500px">
          <v-card>
            <v-card-title>
              <span class="text-h5">Add</span>
            </v-card-title>

            <v-card-text>
              <v-container>
                <v-form ref="form" v-model="valid" lazy-validation>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="addItem.beginningYear"
                        label="Beginning Year"
                        type="number"
                        name="addItem.beginningYear"
                        :rules="[rules.required, rules.integer, rules.length]"
                        @blur="clearAddMessage()"
                        @keyup="clearAddMessage()"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="addItem.endingYear"
                        label="Ending Year"
                        type="number"
                        name="addItem.endingYear"
                        :rules="[rules.required, rules.integer, rules.length]"
                        @blur="clearAddMessage()"
                        @keyup="clearAddMessage()"
                      ></v-text-field>
                    </v-col>
                    <span style="color: red" v-show="hasAddError">
                      <v-icon color="red"> mdi-alert-rhombus-outline </v-icon>
                      {{ addErrorMessage }}
                    </span>
                  </v-row>
                </v-form>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>

              <v-btn color="blue darken-1" text @click="close"> Cancel </v-btn>
              <v-btn color="blue darken-1" text @click="save"> Save </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="infoDialog" max-width="500px">
          <v-card>
            <v-card-title>
              <span class="text-h5">Add</span>
            </v-card-title>
            <v-card-text>
              <span style="color: green">
                <v-icon color="green"> mdi-alarm-light-outline </v-icon>
                {{ infoDialogMessage }}
              </span>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="infoDialogClose">
                  Ok
                </v-btn>
              </v-card-actions>
            </v-card-text>
          </v-card>
        </v-dialog>

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
        >
          <template v-slot:[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              mdi-pencil
            </v-icon>
            <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
          </template>
        </v-data-table>

        <v-dialog v-model="editDialog" max-width="500px">
          <v-card>
            <v-card-title>
              <span class="text-h5">Edit</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-form ref="form" v-model="editValid" lazy-validation>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.beginningYear"
                        label="Beginning Year"
                        type="number"
                        name="editedItem.beginningYear"
                        :rules="[rules.required, rules.integer, rules.length]"
                        @blur="clearAddMessage()"
                        @keyup="clearAddMessage()"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.endingYear"
                        label="Ending Year"
                        type="number"
                        name="editedItem.endingYear"
                        :rules="[rules.required, rules.integer, rules.length]"
                        @blur="clearAddMessage()"
                        @keyup="clearAddMessage()"
                      ></v-text-field>
                    </v-col>
                    <span style="color: red" v-show="hasAddError">
                      <v-icon color="red"> mdi-alert-rhombus-outline </v-icon>
                      {{ addErrorMessage }}
                    </span>
                  </v-row>
                </v-form>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>

              <v-btn color="blue darken-1" text @click="editClose">
                Cancel
              </v-btn>
              <v-btn color="blue darken-1" text @click="editSave"> Save </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-card>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "YearsAdmin",
  data() {
    return {
      totalYears: 0,
      page: 1,
      years: [],
      loading: true,
      options: {},
      numberOfPages: 0,
      sortBy: "",
      sortDesc: false,
      search: "",
      headers: [
        {
          text: "Beginning Year",
          align: "start",
          sortable: true,
          value: "beginningYear",
        },
        { text: "End Year", value: "endingYear" },
        { text: "Actions", value: "actions", sortable: false },
      ],
      dialog: false,
      infoDialog: false,
      infoDialogMessage: "",
      addItem: {
        beginningYear: 0,
        endingYear: 0,
      },
      editedItem: {
        beginningYear: 0,
        endingYear: 0,
      },
      hasAddError: false,
      addErrorMessage: "",
      rules: {
        required: (value) => !!value || "Required.",
        length: (value) => {
          return (
            (value !== null && value.toString().length === 4) || "Invalid year."
          );
        },
        integer: (value) => {
          return value % 1 === 0 || "Invalid year.";
        },
      },
      valid: true,
      editDialog: false,
      editValid: true,
    };
  },
  watch: {
    options: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataFromAPI();
        }
      },
      deep: true,
    },
  },
  methods: {
    readDataFromAPI() {
      this.loading = true;
      const { sortBy, sortDesc, page, itemsPerPage } = this.options;
      var searchYear = 0;
      if (this.search.length == 4 && this.validInt(this.search)) {
        searchYear = parseInt(this.search);
      }

      const request = {
        searchYear: searchYear,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy[0],
        isSortDesc: sortDesc[0],
      };
      console.log(request);
      axios
        .post("http://localhost:54421/api/admin/years", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.years = response.data.value.years;
            this.totalYears = response.data.value.totalYears;
          }

          this.loading = false;
        });
    },
    validInt: function (text) {
      return text % 1 === 0;
    },
    close() {
      this.dialog = false;
    },
    infoDialogClose() {
      this.infoDialog = false;
    },
    save() {
      this.clearAddMessage();
      this.valid = this.$refs.form.validate();
      if (!this.valid) {
        return;
      }
      const request = {
        beginningYear: this.addItem.beginningYear,
        endingYear: this.addItem.endingYear,
      };
      console.log(request);
      axios
        .post("http://localhost:54421/api/admin/year/add", request)
        .then((response) => {
          console.log(response.data);
          if (!response.data.isSuccessful) {
            this.hasAddError = true;
            this.addErrorMessage = response.data.errorMessage;
          } else {
            this.readDataFromAPI();
            this.dialog = false;
            this.infoDialog = true;
            this.infoDialogMessage = "Add successful!";
          }
        });
    },
    clearAddMessage() {
      this.hasAddError = false;
      this.addErrorMessage = "";
    },
    openNewItemDialog() {
      this.dialog = true;
    },
    editItem() {
      this.editDialog = true;
    },
    editClose() {
      this.editDialog = false;
    },
    deleteItem(item) {
      this.editedIndex = this.desserts.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.desserts.splice(this.editedIndex, 1);
      this.closeDelete();
    },
  },
  mounted() {
    this.readDataFromAPI();
  },
};
</script>

<style>
.btnAdd {
  margin-left: 15px !important;
}
</style>
