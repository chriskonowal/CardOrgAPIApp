<template>
  <div class="wrapper">
    <div>
      <v-card>
        <v-card-title>
          Team

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
        <v-dialog v-model="infoDialog" max-width="500px">
          <v-card>
            <v-card-title>
              <span class="text-h5">{{ infoDialogTitleMessage }}</span>
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
          :items="items"
          :options.sync="options"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          :server-items-length="total"
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
              <span class="text-h5">{{ editTitle }}</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-form ref="editForm" v-model="editValid" lazy-validation>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.city"
                        label="City"
                        type="text"
                        name="editedItem.city"
                        :rules="[rules.required]"
                        @blur="clearAddMessage()"
                        @keyup="clearAddMessage()"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedItem.name"
                        label="Name"
                        type="text"
                        name="editedItem.name"
                        :rules="[rules.required]"
                        @blur="clearAddMessage()"
                        @keyup="clearAddMessage()"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
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
        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5"
              >Are you sure you want to delete this item?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="blue darken-1" text @click="closeDelete"
                >Cancel</v-btn
              >
              <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                >OK</v-btn
              >
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogErrorDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5"
              >There was a problem deleting item.</v-card-title
            >
            <p style="color: red; text-align: center">
              {{ deleteErrorMessage }}
            </p>
          </v-card>
        </v-dialog>
      </v-card>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "TeamAdmin",
  data() {
    return {
      total: 0,
      page: 1,
      items: [],
      loading: true,
      options: {},
      numberOfPages: 0,
      sortBy: "",
      sortDesc: false,
      search: "",
      headers: [
        {
          text: "City",
          align: "start",
          sortable: true,
          value: "city",
        },
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
        { text: "Actions", value: "actions", sortable: false },
      ],
      dialog: false,
      infoDialog: false,
      infoDialogMessage: "",
      infoDialogTitleMessage: "",
      editedItem: {
        teamId: 0,
        name: 0,
      },
      hasAddError: false,
      addErrorMessage: "",
      rules: {
        required: (value) => !!value || "Required.",
      },
      valid: true,
      editDialog: false,
      editValid: true,
      editTitle: "",
      dialogDelete: false,
      id: 0,
      dialogErrorDelete: false,
      deleteErrorMessage: "",
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

      const request = {
        searchTerm: this.search,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy[0],
        isSortDesc: sortDesc[0],
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/teams", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.items = response.data.value.teams;
            this.total = response.data.value.total;
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
    clearAddMessage() {
      this.hasAddError = false;
      this.addErrorMessage = "";
    },
    openNewItemDialog() {
      this.editTitle = "Add Team";
      this.editedItem = {};
      this.editDialog = true;
    },
    editItem(item) {
      console.log(item);
      this.editTitle = "Edit Team";
      this.editedItem = item;
      this.editDialog = true;
    },
    editSave() {
      this.clearAddMessage();
      var isEdit = this.editedItem.teamId > 0;
      var validEdit = this.$refs.editForm.validate();
      if (!validEdit) {
        return;
      }
      var request = {
        id: this.editedItem.teamId,
        city: this.editedItem.city,
        name: this.editedItem.name,
      };
      console.log("save request");
      console.log(request);
      axios({
        method: "post", //you can set what request you want to be
        url: process.env.VUE_APP_ROOT_API + "admin/teams/save",
        data: request,
      }).then((response) => {
        console.log(response.data);
        if (!response.data.isSuccessful) {
          this.hasAddError = true;
          this.addErrorMessage = response.data.errorMessage;
        } else {
          this.readDataFromAPI();
          this.editDialog = false;
          this.infoDialog = true;
          if (isEdit) {
            this.infoDialogMessage = "Edit successful!";
            this.infoDialogTitleMessage = "Edit Team";
          } else {
            this.infoDialogMessage = "Add successful!";
            this.infoDialogTitleMessage = "Add Team";
          }
        }
      });
    },
    editClose() {
      this.editDialog = false;
    },
    deleteItem(item) {
      this.id = item.teamId;
      this.dialogDelete = true;
    },
    closeDelete() {
      this.dialogDelete = false;
    },
    deleteItemConfirm() {
      var request = {
        id: this.id,
      };
      console.log(request);
      axios({
        method: "post", //you can set what request you want to be
        url: process.env.VUE_APP_ROOT_API + "admin/teams/delete",
        data: request,
      }).then((response) => {
        console.log(response.data);
        if (!response.data.isSuccessful) {
          this.closeDelete();
          this.dialogErrorDelete = true;
          this.deleteErrorMessage = response.data.errorMessage;
        } else {
          this.readDataFromAPI();
          this.editDialog = false;
          this.infoDialog = true;
          this.infoDialogMessage = "Delete successful!";
          this.infoDialogTitleMessage = "Delete Team";
          this.closeDelete();
        }
      });
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
