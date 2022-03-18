<template>
  <div class="wrapper">
    <v-card>
      <v-card-title>
        Cards
        <v-expansion-panels>
          <v-expansion-panel>
            <v-expansion-panel-header> Search </v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-form>
                <v-container>
                  <v-row>
                    <v-text-field
                      label="Card Description"
                      outlined
                      v-model="txtCardDescription"
                    ></v-text-field>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="isGraded"
                      :label="'Is Graded'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="yearsSelected"
                      :headers="yearsHeaders"
                      :page="page"
                      :items="years"
                      :options.sync="yearsOptions"
                      :sort-by.sync="sortBy"
                      :sort-desc.sync="sortDesc"
                      :server-items-length="totalYears"
                      :loading="yearsLoading"
                      :items-per-page="5"
                      :search="yearsSearch"
                      class="elevation-1 tblYears"
                      show-select
                      :single-select="false"
                      item-key="yearId"
                    >
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="margin-top: 30px">
                    <v-col cols="12" sm="6" md="3">
                      <v-btn depressed elevation="2" @click="readDataFromAPI()"
                        >Search</v-btn
                      >
                    </v-col>
                  </v-row>
                </v-container>
              </v-form>
            </v-expansion-panel-content>
          </v-expansion-panel>
          <v-expansion-panel>
            <v-expansion-panel-header> Sort </v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-form>
                <v-container>
                  <v-row>
                    <h5>Player Name Sort</h5>
                    <v-radio-group
                      v-model="playerNameSort"
                      row
                      style="width: 100%"
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Team Sort</h5>
                    <v-radio-group v-model="teamSort" row style="width: 100%">
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="margin-top: 30px">
                    <v-col cols="12" sm="6" md="3">
                      <v-btn depressed elevation="2" @click="readDataFromAPI()"
                        >Sort</v-btn
                      >
                    </v-col>
                  </v-row>
                </v-container>
              </v-form>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
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
        :items="cards"
        :options.sync="options"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        :server-items-length="totalCards"
        :loading="loading"
        :items-per-page="10"
        :search="search"
        class="elevation-1"
      >
        <template v-slot:[`item.pictures`]="{ item }">
          <img
            v-bind:src="showImage(item.frontCardThumbnailImagePath)"
            style="max-width: 100px; padding: 5px"
            @click="onClickFrontImage(item)"
          />

          <img
            v-bind:src="showImage(item.backCardThumbnailImagePath)"
            style="max-width: 150px; padding: 5px"
          />
          <br />
          <p style="text-align: left">
            {{ showFullNames(item.players) }} <br />
            {{ showTeamsNames(item.teams) }}
          </p>
        </template>
        <template v-slot:[`item.details`]="{ item }"
          ><div>
            {{ item.year.year }} {{ item.cardDescription }}
            {{ item.cardNumber }}
            <span v-html="showSerialNumber(item.serialNumber)"></span>
            <span v-html="showGradeInformation(item)"></span>
            <span v-html="showRookie(item.isRookie)"></span>
            <span v-html="showAutograph(item)"></span>
            <span v-html="showPatch(item)"></span>
            <v-expansion-panels>
              <v-expansion-panel>
                <v-expansion-panel-header>
                  More Info...
                </v-expansion-panel-header>
                <v-expansion-panel-content>
                  <span v-html="showMoreInfo(item)"></span>
                </v-expansion-panel-content>
              </v-expansion-panel>
            </v-expansion-panels>
          </div>
        </template>
      </v-data-table>
    </v-card>
    <v-dialog v-model="frontPictureDialog" width="85%" persistent>
      <v-card>
        <v-card-title class="text-h5 text-center block">Front</v-card-title>
        <v-img v-bind:src="showFrontImage"> </v-img>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closefrontPicture"
            >Close</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "HomeLanding",
  data() {
    return {
      totalCards: 0,
      page: 1,
      cards: [],
      loading: true,
      options: {},
      numberOfPages: 0,
      sortBy: "",
      sortDesc: false,
      search: "",
      headers: [
        {
          text: "Images",
          align: "start",
          sortable: false,
          value: "pictures",
        },
        {
          text: "Details",
          align: "start",
          sortable: false,
          value: "details",
        },
      ],
      frontPictureDialog: false,
      showFrontImage: "",
      frontImage: {},
      images: [],
      txtCardDescription: "",
      yearsLoading: true,
      yearsOptions: {},
      yearsNumberOfPages: 0,
      yearsSearch: "",
      years: [],
      totalYears: 0,
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
      yearsSelected: [],
      isGraded: false,
      playerNameSort: "0",
      teamSort: "0",
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
    yearsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForYearsFromAPI();
        }
      },
      deep: true,
    },
  },

  mounted() {
    //this.$set(this.hideDelimiters, "hide-delimiters", this.isMobile());
  },
  methods: {
    inited(viewer) {
      this.$viewer = viewer;
    },
    showFullNames: function (playerList) {
      var names = "";
      for (var i = 0; i < playerList.length; i++) {
        if (names.length > 0) {
          names += ",";
        }
        names += playerList[i].fullName;
      }
      return names;
    },
    showTeamsNames: function (teamList) {
      var teams = "";
      for (var i = 0; i < teamList.length; i++) {
        if (teams.length > 0) {
          teams += ",";
        }
        teams += teamList[i].team;
      }
      return teams;
    },
    showImage: function (path) {
      return "/Uploads/Thumb/" + path;
    },
    showSerialNumber: function (serialNumber) {
      if (serialNumber > 0) {
        return "<b>#/" + serialNumber + "</b>";
      }
    },
    showGradeInformation: function (item) {
      if (item.isGraded && item.gradeCompany.name !== "None") {
        return "<br/ >" + item.gradeCompany.name + " - " + item.grade;
      }
    },
    showRookie: function (isRookie) {
      if (isRookie) {
        return "<br/ ><b>Rookie</b>";
      }
    },
    showAutograph: function (item) {
      if (item.isAutograph && !item.isOnCardAutograph) {
        return "<br/ ><b>Autograph - not on card</b>";
      } else if (item.isAutograph && item.isOnCardAutograph) {
        return "<br/ ><b>On card autograph</b>";
      } else {
        return "";
      }
    },
    showPatch: function (item) {
      if (item.isPatch && !item.isGameWornJersey) {
        return "<br/ ><b>Patch - not game worn</b>";
      } else if (item.isPatch && item.isGameWornJersey) {
        return "<br/ ><b>Game worn patch</b>";
      } else {
        return "";
      }
    },
    showMoreInfo: function (item) {
      return (
        "Location: " +
        item.location.name +
        "<br />Copies: " +
        item.copies +
        "<br />Low Beckett: " +
        this.formatMoney(item.lowestBeckettPrice) +
        "<br />High Beckett: " +
        this.formatMoney(item.highestBeckettPrice) +
        "<br />Lowest COMC: " +
        this.formatMoney(item.lowestCOMCPrice) +
        "<br />Ebay: " +
        this.formatMoney(item.ebayPrice) +
        "<br />Price Paid: " +
        this.formatMoney(item.pricePaid)
      );
    },
    formatMoney: function (number) {
      return number.toLocaleString("en-US", {
        style: "currency",
        currency: "USD",
      });
    },
    readDataFromAPI() {
      this.readDataForYearsFromAPI();
      console.log(this.txtCardDescription);
      console.log(this.yearsSelected);
      this.loading = true;
      const { page, itemsPerPage } = this.options;
      const request = {
        quickSearch: this.search,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        searchSortRequest: {
          cardDescription: this.txtCardDescription,
          yearIds: this.getYearIds(this.yearsSelected),
          isGraded: this.isGraded,
          playerNameSort: parseInt(this.playerNameSort),
          teamSort: parseInt(this.teamSort),
        },
      };

      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "public/cards", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.cards = response.data.value.cards;
            this.totalCards = response.data.value.totalCards;
            this.loading = false;
          }
        });
    },
    onClickFrontImage(item) {
      this.showFrontImage = "/Uploads/Mid/" + item.frontCardMainImagePath;
      this.showFrontImageZoom = "/Uploads/Large/" + item.frontCardMainImagePath;
      this.frontPictureDialog = true;
    },
    closefrontPicture() {
      this.frontPictureDialog = false;
    },
    isMobile() {
      if (screen.width <= 760) {
        return true;
      } else {
        return false;
      }
    },
    readDataForYearsFromAPI() {
      this.yearsLoading = true;
      console.log(this.yearsOptions);
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
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/years", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.years = response.data.value.years;
            this.totalYears = response.data.value.totalYears;
          }

          this.yearsLoading = false;
        });
    },
    getYearIds(yearArray) {
      var ids = "";
      for (var i = 0; i < yearArray.length; i++) {
        if (ids.length > 0) {
          ids += ",";
        }
        ids += yearArray[i].yearId.toString();
      }
      return ids;
    },
  },
};
</script>

<style>
.v-data-table__mobile-row {
  justify-content: left !important;
}
.v-application--is-ltr .v-data-table__mobile-row__cell {
  text-align: left !important;
}
.v-data-table__wrapper > table > tbody > tr:hover {
  background: inherit !important;
}
.v-data-table__mobile-row__cell {
  width: 100%;
}
.block {
  display: block !important;
}
.tblYears .v-data-table__mobile-row__header {
  min-width: 100px;
}
.v-input--radio-group--row {
  margin-top: 0px;
}
.elevation-1 {
  margin-top: 30px;
}
</style>
