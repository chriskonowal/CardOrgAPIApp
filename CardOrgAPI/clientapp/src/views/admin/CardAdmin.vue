<template>
  <div class="wrapper">
    <div>
      <v-card>
        <v-card-title>
          Card

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
        <v-dialog v-model="infoDialog" max-width="800px">
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
          <template v-slot:[`item.pictures`]="{ item }">
            <img
              v-bind:src="showImage(item.frontCardThumbnailImagePath)"
              style="max-width: 100px; padding: 5px"
            />
            <br />
            <p style="text-align: left">
              {{ showFullNames(item.players) }} <br />
              {{ showTeamsNames(item.teams) }}
            </p>
          </template>
          <template v-slot:[`item.year`]="{ item }">
            <p style="text-align: left">
              {{ item.year.year }}
            </p>
          </template>
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
                <v-form
                  ref="editForm"
                  v-model="editValid"
                  lazy-validation
                  @submit.prevent="onSubmit"
                >
                  <v-row>
                    <v-text-field
                      v-model="editedItem.cardDescription"
                      label="Card Description"
                      type="text"
                      name="editedItem.cardDescription"
                      :rules="[rules.required]"
                      @blur="clearAddMessage()"
                      @keyup="clearAddMessage()"
                    ></v-text-field>
                  </v-row>
                  <v-row>
                    <v-text-field
                      v-model="editedItem.cardNumber"
                      label="Card Number"
                      type="text"
                      name="editedItem.cardNumber"
                      :rules="[rules.required]"
                      @blur="clearAddMessage()"
                      @keyup="clearAddMessage()"
                    ></v-text-field>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.playersSelected"
                      :headers="playersHeaders"
                      :page="page"
                      :items="players"
                      :options.sync="playersOptions"
                      :sort-by.sync="playersSortBy"
                      :sort-desc.sync="playersSortDesc"
                      :server-items-length="totalPlayers"
                      :loading="playersLoading"
                      :items-per-page="5"
                      :search="playersSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="false"
                      item-key="playerId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Players
                          <span style="color: red" v-show="playerHasAddError">
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="playersSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForPlayersFromAPI()"
                          @keyup="readDataForPlayersFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="
                            showPlayersSelected(editedItem.playersSelected)
                          "
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.setsSelected"
                      :headers="setsHeaders"
                      :page="page"
                      :items="sets"
                      :options.sync="setsOptions"
                      :sort-by.sync="setsSortBy"
                      :sort-desc.sync="setsSortDesc"
                      :server-items-length="totalSets"
                      :loading="setsLoading"
                      :items-per-page="5"
                      :search="setsSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="true"
                      item-key="setId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Set
                          <span style="color: red" v-show="setHasAddError">
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="setsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForSetsFromAPI()"
                          @keyup="readDataForSetsFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="showNamesSelected(editedItem.setsSelected)"
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.yearsSelected"
                      :headers="yearsHeaders"
                      :page="page"
                      :items="years"
                      :options.sync="yearsOptions"
                      :sort-by.sync="yearsSortBy"
                      :sort-desc.sync="yearsSortDesc"
                      :server-items-length="totalYears"
                      :loading="yearsLoading"
                      :items-per-page="5"
                      :search="yearsSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="true"
                      item-key="yearId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Year
                          <span style="color: red" v-show="yearHasAddError">
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="yearsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForYearsFromAPI()"
                          @keyup="readDataForYearsFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="showYearsSelected(editedItem.yearsSelected)"
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.teamsSelected"
                      :headers="teamsHeaders"
                      :page="page"
                      :items="teams"
                      :options.sync="teamsOptions"
                      :sort-by.sync="teamsSortBy"
                      :sort-desc.sync="teamsSortDesc"
                      :server-items-length="totalTeams"
                      :loading="teamsLoading"
                      :items-per-page="5"
                      :search="teamsSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="false"
                      item-key="teamId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Team
                          <span style="color: red" v-show="teamHasAddError">
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="teamsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForTeamsFromAPI()"
                          @keyup="readDataForTeamsFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="showTeamsSelected(editedItem.teamsSelected)"
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.gradeCompaniesSelected"
                      :headers="gradeCompaniesHeaders"
                      :page="page"
                      :items="gradeCompanies"
                      :options.sync="gradeCompaniesOptions"
                      :sort-by.sync="gradeCompaniesSortBy"
                      :sort-desc.sync="gradeCompaniesSortDesc"
                      :server-items-length="totalGradeCompanies"
                      :loading="gradeCompaniesLoading"
                      :items-per-page="5"
                      :search="gradeCompaniesSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="true"
                      item-key="gradeCompanyId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Grade Company
                          <span
                            style="color: red"
                            v-show="gradeCompanyHasAddError"
                          >
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="gradeCompaniesSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForGradeCompaniesFromAPI()"
                          @keyup="readDataForGradeCompaniesFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="
                            showNamesSelected(editedItem.gradeCompaniesSelected)
                          "
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.locationsSelected"
                      :headers="locationsHeaders"
                      :page="page"
                      :items="locations"
                      :options.sync="locationsOptions"
                      :sort-by.sync="locationsSortBy"
                      :sort-desc.sync="locationsSortDesc"
                      :server-items-length="totalLocations"
                      :loading="locationsLoading"
                      :items-per-page="5"
                      :search="locationsSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="true"
                      item-key="locationId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Location
                          <span style="color: red" v-show="locationHasAddError">
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="locationsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForLocationsFromAPI()"
                          @keyup="readDataForLocationsFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="
                            showNamesSelected(editedItem.locationsSelected)
                          "
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-data-table
                      v-model="editedItem.sportsSelected"
                      :headers="sportsHeaders"
                      :page="page"
                      :items="sports"
                      :options.sync="sportsOptions"
                      :sort-by.sync="sportsSortBy"
                      :sort-desc.sync="sportsSortDesc"
                      :server-items-length="totalSports"
                      :loading="sportsLoading"
                      :items-per-page="5"
                      :search="sportsSearch"
                      class="elevation-1 tblSearch"
                      show-select
                      :single-select="true"
                      item-key="sportId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">
                          Sport
                          <span style="color: red" v-show="sportHasAddError">
                            <v-icon color="red">
                              mdi-alert-rhombus-outline
                            </v-icon>
                            Required!
                          </span>
                        </div>
                        <v-text-field
                          v-model="sportsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForSportsFromAPI()"
                          @keyup="readDataForSportsFromAPI()"
                        ></v-text-field>
                        <span
                          class="text-caption selected-search"
                          v-html="showNamesSelected(editedItem.sportsSelected)"
                        ></span>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-row>
                    <v-file-input
                      accept="image/*"
                      label="Front Image"
                      v-model="editedItem.frontImage"
                    ></v-file-input>
                  </v-row>
                  <v-row>
                    <v-file-input
                      accept="image/*"
                      label="Back Image"
                      v-model="editedItem.backImage"
                    ></v-file-input>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="editedItem.isGraded"
                      :label="'Is Graded'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="editedItem.isRookie"
                      :label="'Is Rookie'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="editedItem.isAutograph"
                      :label="'Is Autograph'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="editedItem.isOnCardAutograph"
                      :label="'Is On Card Autograph'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch v-model="editedItem.isPatch" :label="'Is Patch'">
                    </v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="editedItem.isGameWornJersey"
                      :label="'Is Game Worn Jersey'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Lowest Beckett Price"
                        outlined
                        v-model="editedItem.lowestBeckettPrice"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Highest Beckett Price"
                        outlined
                        v-model="editedItem.highestBeckettPrice"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Lowest COMC Price"
                        outlined
                        v-model="editedItem.lowestCOMCPrice"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Ebay Price"
                        outlined
                        v-model="editedItem.ebayPrice"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Price Paid"
                        outlined
                        v-model="editedItem.pricePaid"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Grade"
                        outlined
                        v-model="editedItem.grade"
                        prefix=""
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Copies"
                        outlined
                        v-model="editedItem.copies"
                        prefix=""
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Serial Number"
                        outlined
                        v-model="editedItem.serialNumber"
                        prefix=""
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
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
  name: "CardAdmin",
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
        { text: "Card", value: "pictures", sortable: true },
        { text: "Year", value: "year", sortable: true },
        {
          text: "Card Description",
          align: "start",
          sortable: true,
          value: "cardDescription",
        },
        {
          text: "Card Number",
          align: "start",
          sortable: true,
          value: "cardNumber",
        },
        { text: "Actions", value: "actions", sortable: false },
      ],
      dialog: false,
      infoDialog: false,
      infoDialogMessage: "",
      infoDialogTitleMessage: "",
      editedItem: {
        cardId: 0,
        players: [],
        teams: [],
        gradeCompany: {},
        location: {},
        set: {},
        sport: {},
        year: {},
        cardDescription: "",
        cardNumber: "",
        lowestBeckettPrice: "0",
        highestBeckettPrice: 0,
        LowestComcprice: 0,
        ebayPrice: 0,
        pricePaid: 0,
        isGraded: false,
        copies: 0,
        serialNumber: 0,
        grade: 0,
        isRookie: false,
        isAutograph: false,
        isPatch: false,
        isOnCardAutograph: false,
        isGameWornJersey: false,
        frontCardMainImagePath: "",
        frontCardThumbnailImagePath: "",
        backCardMainImagePath: "",
        backCardThumbnailImagePath: "",
        frontImage: {},
        backImage: {},
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
      playersSelected: [],
      playersLoading: true,
      playersOptions: {},
      playersNumberOfPages: 0,
      playersSearch: "",
      players: [],
      totalPlayers: 0,
      playersSortBy: "",
      playersSortDesc: false,
      playerHasAddError: false,
      playersHeaders: [
        {
          text: "First Name",
          align: "start",
          sortable: true,
          value: "firstName",
        },
        {
          text: "Last Name",
          align: "start",
          sortable: true,
          value: "lastName",
        },
      ],
      setsSelected: [],
      setsLoading: true,
      setsOptions: {},
      setsNumberOfPages: 0,
      setsSearch: "",
      sets: [],
      totalSets: 0,
      setsSortBy: "",
      setsSortDesc: false,
      setHasAddError: false,
      setsHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
      ],
      yearsSelected: [],
      yearsLoading: true,
      yearsOptions: {},
      yearsNumberOfPages: 0,
      yearsSearch: "",
      years: [],
      totalYears: 0,
      yearsSortBy: "",
      yearsSortDesc: false,
      yearHasAddError: false,
      yearsHeaders: [
        {
          text: "Year",
          align: "start",
          sortable: false,
          value: "year",
        },
        {
          text: "Beginning Year",
          align: "start",
          sortable: true,
          value: "beginningYear",
        },
        { text: "End Year", value: "endingYear" },
      ],
      gradeCompaniesSelected: [],
      gradeCompaniesLoading: true,
      gradeCompaniesOptions: {},
      gradeCompaniesNumberOfPages: 0,
      gradeCompaniesSearch: "",
      gradeCompanies: [],
      totalGradeCompanies: 0,
      gradeCompaniesSortBy: "",
      gradeCompaniesSortDesc: false,
      gradeCompanyHasAddError: false,
      gradeCompaniesHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
      ],
      locationsSelected: [],
      locationsLoading: true,
      locationsOptions: {},
      locationsNumberOfPages: 0,
      locationsSearch: "",
      locations: [],
      totalLocations: 0,
      locationsSortBy: "",
      locationsSortDesc: false,
      locationHasAddError: false,
      locationsHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
      ],
      teamsSelected: [],
      teamsLoading: true,
      teamsOptions: {},
      teamsNumberOfPages: 0,
      teamsSearch: "",
      teams: [],
      totalTeams: 0,
      teamsSortBy: "",
      teamsSortDesc: false,
      teamHasAddError: false,
      teamsHeaders: [
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
      ],
      sportsSelected: [],
      sportsLoading: true,
      sportsOptions: {},
      sportsNumberOfPages: 0,
      sportsSearch: "",
      sports: [],
      totalSports: 0,
      sportsSortBy: "",
      sportsSortDesc: false,
      sportHasAddError: false,
      sportsHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
      ],
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
    playersOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForPlayersFromAPI();
        }
      },
      deep: true,
    },
    setsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForSetsFromAPI();
        }
      },
      deep: true,
    },
    yearsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForYearsFromAPI();
        }
      },
      deep: true,
    },
    gradeCompaniesOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForGradeCompaniesFromAPI();
        }
      },
      deep: true,
    },
    locationsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForLocationsFromAPI();
        }
      },
      deep: true,
    },
    teamsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForTeamsFromAPI();
        }
      },
      deep: true,
    },
    sportsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForSportsFromAPI();
        }
      },
      deep: true,
    },
  },
  methods: {
    onSubmit() {
      // Do something...
    },
    readDataFromAPI() {
      this.readDataForPlayersFromAPI();
      this.readDataForSetsFromAPI();
      this.readDataForYearsFromAPI();
      this.readDataForGradeCompaniesFromAPI();
      this.readDataForLocationsFromAPI();
      this.readDataForTeamsFromAPI();
      this.readDataForSportsFromAPI();
      this.loading = true;
      const { sortBy, sortDesc, page, itemsPerPage } = this.options;

      const request = {
        quickSearch: this.search,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy[0],
        isSortDesc: sortDesc[0],
      };
      console.log("public/cards request");
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "public/cards", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.items = response.data.value.cards;
            this.total = response.data.value.totalCards;
          }

          this.loading = false;
        });
    },
    validInt: function (text) {
      return text % 1 === 0;
    },
    showImage: function (path) {
      return "/Uploads/Thumb/" + path;
    },
    showFullNames: function (playerList) {
      var names = "";
      for (var i = 0; i < playerList.length; i++) {
        if (names.length > 0) {
          names += ", ";
        }
        names += playerList[i].fullName;
      }
      return names;
    },
    showTeamsNames: function (teamList) {
      var teams = "";
      for (var i = 0; i < teamList.length; i++) {
        if (teams.length > 0) {
          teams += ", ";
        }
        teams += teamList[i].team;
      }
      return teams;
    },
    showYearsSelected(selectedArray) {
      let years = "";
      if (selectedArray != null) {
        for (var i = 0; i < selectedArray.length; i++) {
          if (years.length > 0) {
            years += ", ";
          }
          years += selectedArray[i].year;
        }
      }
      if (years.length > 0) {
        years = "Selected: " + years;
      }
      return years;
    },
    readDataForPlayersFromAPI() {
      this.playersLoading = true;
      //console.log(this.playersOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.playersOptions;

      const request = {
        searchTerm: this.playersSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/players", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.players = response.data.value.players;
            this.totalPlayers = response.data.value.total;
          }

          this.playersLoading = false;
        });
    },
    showPlayersSelected(selectedArray) {
      let players = "";
      if (selectedArray != null) {
        for (var i = 0; i < selectedArray.length; i++) {
          if (players.length > 0) {
            players += ", ";
          }
          players += selectedArray[i].fullName;
        }
      }
      if (players.length > 0) {
        players = "Selected: " + players;
      }
      return players;
    },
    showNamesSelected(selectedArray) {
      let names = "";
      if (selectedArray != null) {
        for (var i = 0; i < selectedArray.length; i++) {
          if (names.length > 0) {
            names += ", ";
          }
          names += selectedArray[i].name;
        }
      }
      if (names.length > 0) {
        names = "Selected: " + names;
      }
      return names;
    },
    showTeamsSelected(selectedArray) {
      let teams = "";
      if (selectedArray != null) {
        for (var i = 0; i < selectedArray.length; i++) {
          if (teams.length > 0) {
            teams += ", ";
          }
          teams += selectedArray[i].team;
        }
      }
      if (teams.length > 0) {
        teams = "Selected: " + teams;
      }
      return teams;
    },
    readDataForSetsFromAPI() {
      this.setsLoading = true;
      //console.log(this.setsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.setsOptions;
      const request = {
        searchTerm: this.setsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/sets", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.sets = response.data.value.sets;
            this.totalSets = response.data.value.total;
          }

          this.setsLoading = false;
        });
    },
    readDataForYearsFromAPI() {
      this.yearsLoading = true;
      //console.log(this.yearsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.yearsOptions;
      var searchYear = 0;
      if (this.yearsSearch.length == 4 && this.validInt(this.yearsSearch)) {
        searchYear = parseInt(this.yearsSearch);
      }

      const request = {
        searchYear: searchYear,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/years", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.years = response.data.value.years;
            this.totalYears = response.data.value.totalYears;
          }

          this.yearsLoading = false;
        });
    },
    readDataForGradeCompaniesFromAPI() {
      this.gradeCompaniesLoading = true;
      //console.log(this.gradeCompaniesOptions);
      const { sortBy, sortDesc, page, itemsPerPage } =
        this.gradeCompaniesOptions;
      const request = {
        searchTerm: this.gradeCompaniesSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/grade_companies", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.gradeCompanies = response.data.value.gradeCompanies;
            this.totalGradeCompanies = response.data.value.total;
          }

          this.gradeCompaniesLoading = false;
        });
    },
    readDataForLocationsFromAPI() {
      this.locationsLoading = true;
      //console.log(this.locationsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.locationsOptions;
      const request = {
        searchTerm: this.locationsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/locations", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.locations = response.data.value.locations;
            this.totalLocations = response.data.value.total;
          }

          this.locationsLoading = false;
        });
    },
    readDataForTeamsFromAPI() {
      this.teamsLoading = true;
      //console.log(this.teamsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.teamsOptions;

      const request = {
        searchTerm: this.teamsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/teams", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.teams = response.data.value.teams;
            this.totalTeams = response.data.value.total;
          }

          this.teamsLoading = false;
        });
    },
    readDataForSportsFromAPI() {
      this.sportsLoading = true;
      //console.log(this.sportsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.sportsOptions;

      const request = {
        searchTerm: this.sportsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/sports", request)
        .then((response) => {
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.sports = response.data.value.sports;
            this.totalSports = response.data.value.total;
          }

          this.sportsLoading = false;
        });
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
      this.playerHasAddError = false;
      this.setHasAddError = false;
      this.yearHasAddError = false;
      this.gradeCompanyHasAddError = false;
      this.locationHasAddError = false;
      this.teamHasAddError = false;
      this.sportHasAddError = false;
    },
    openNewItemDialog() {
      this.editTitle = "Add Card";
      this.editedItem = {};
      this.editDialog = true;
      this.editedItem.lowestBeckettPrice = 0;
      this.editedItem.highestBeckettPrice = 0;
      this.editedItem.lowestCOMCPrice = 0;
      this.editedItem.ebayPrice = 0;
      this.editedItem.pricePaid = 0;
      this.editedItem.grade = 0;
      this.editedItem.copies = 0;
      this.editedItem.serialNumber = 0;
      this.editedItem.cardId = 0;
    },
    editItem(item) {
      this.clearAddMessage();
      console.log(item);
      this.editTitle = "Edit Card";
      this.editedItem = item;
      this.editedItem.playersSelected = item.players;
      this.editedItem.setsSelected = [
        {
          setId: item.set.setId,
          name: item.set.name,
        },
      ];
      this.editedItem.yearsSelected = [
        {
          yearId: item.year.yearId,
          year: item.year.year,
        },
      ];
      this.editedItem.gradeCompaniesSelected = [
        {
          gradeCompanyId: item.gradeCompany.gradeCompanyId,
          name: item.gradeCompany.name,
        },
      ];
      this.editedItem.locationsSelected = [
        {
          locationId: item.location.locationId,
          name: item.location.name,
        },
      ];
      this.editedItem.sportsSelected = [
        {
          sportId: item.sport.sportId,
          name: item.sport.name,
        },
      ];
      this.editedItem.teamsSelected = item.teams;
      this.editDialog = true;
      this.editedItem.frontCardMainImagePath = item.frontCardMainImagePath;
      this.editedItem.frontCardThumbnailImagePath =
        item.frontCardThumbnailImagePath;
      this.editedItem.backCardMainImagePath = item.backCardMainImagePath;
      this.backCardThumbnailImagePath = item.backCardThumbnailImagePath;
    },
    editSave() {
      var isEdit = this.editedItem.cardId > 0;
      this.clearAddMessage();
      var validEdit = this.$refs.editForm.validate();

      if (
        this.editedItem.playersSelected == null ||
        this.editedItem.playersSelected.length == 0
      ) {
        this.playerHasAddError = true;
        validEdit = false;
      }

      if (
        this.editedItem.setsSelected == null ||
        this.editedItem.setsSelected.length == 0
      ) {
        this.setHasAddError = true;
        validEdit = false;
      }

      if (
        this.editedItem.yearsSelected == null ||
        this.editedItem.yearsSelected.length == 0
      ) {
        this.yearHasAddError = true;
        validEdit = false;
      }

      if (
        this.editedItem.gradeCompaniesSelected == null ||
        this.editedItem.gradeCompaniesSelected.length == 0
      ) {
        this.gradeCompanyHasAddError = true;
        validEdit = false;
      }

      if (
        this.editedItem.locationsSelected == null ||
        this.editedItem.locationsSelected.length == 0
      ) {
        this.locationHasAddError = true;
        validEdit = false;
      }

      if (
        this.editedItem.teamsSelected == null ||
        this.editedItem.teamsSelected.length == 0
      ) {
        this.teamHasAddError = true;
        validEdit = false;
      }

      if (
        this.editedItem.sportsSelected == null ||
        this.editedItem.sportsSelected.length == 0
      ) {
        this.sportHasAddError = true;
        validEdit = false;
      }

      if (!validEdit) {
        return;
      }

      console.log("this.editedItem.gradeCompaniesSelected");
      console.log(this.editedItem.gradeCompaniesSelected);
      var request = {
        cardId: this.editedItem.cardId,
        cardDescription: this.editedItem.cardDescription,
        cardNumber: this.editedItem.cardNumber,
        lowestBeckettPrice: parseInt(this.editedItem.lowestBeckettPrice),
        highestBeckettPrice: parseInt(this.editedItem.highestBeckettPrice),
        frontCardMainImagePath: this.editedItem.frontCardMainImagePath,
        frontCardThumbnailImagePath:
          this.editedItem.frontCardThumbnailImagePath,
        backCardMainImagePath: this.editedItem.backCardMainImagePath,
        backCardThumbnailImagePath: this.backCardThumbnailImagePath,
        lowestComcprice: parseInt(this.editedItem.lowestCOMCPrice),
        ebayPrice: parseInt(this.editedItem.ebayPrice),
        pricePaid: parseInt(this.editedItem.pricePaid),
        isGraded: this.editedItem.isGraded,
        copies: parseInt(this.editedItem.copies),
        serialNumber: parseInt(this.editedItem.serialNumber),
        grade: parseInt(this.editedItem.grade),
        isRookie: this.editedItem.isRookie,
        isAutograph: this.editedItem.isAutograph,
        isPatch: this.editedItem.isPatch,
        isOnCardAutograph: this.editedItem.isOnCardAutograph,
        isGameWornJersey: this.editedItem.isGameWornJersey,
        sportId: this.editedItem.sportsSelected[0].sportId,
        yearId: this.editedItem.yearsSelected[0].yearId,
        setId: this.editedItem.setsSelected[0].setId,
        gradeCompanyId:
          this.editedItem.gradeCompaniesSelected[0].gradeCompanyId,
        locationId: this.editedItem.locationsSelected[0].locationId,
        players: this.editedItem.playersSelected,
        teams: this.editedItem.teamsSelected,
      };

      let formData = new FormData();
      formData.append("file", this.editedItem.frontImage);
      formData.append("file", this.editedItem.backImage);
      formData.append("cardRequest", JSON.stringify(request));
      console.log("save request");

      console.log(request);
      axios({
        method: "post", //you can set what request you want to be
        url: process.env.VUE_APP_ROOT_API + "public/cards/save",

        data: formData,
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
            this.infoDialogTitleMessage = "Edit Card";
          } else {
            this.infoDialogMessage = "Add successful!";
            this.infoDialogTitleMessage = "Add Card";
          }
        }
      });
    },
    editClose() {
      this.editDialog = false;
    },
    deleteItem(item) {
      this.id = item.cardId;
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
        url: process.env.VUE_APP_ROOT_API + "public/cards/delete",
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
          this.infoDialogTitleMessage = "Delete Card";
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
.selected-search {
  color: green;
}
</style>
