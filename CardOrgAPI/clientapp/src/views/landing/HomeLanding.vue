<template>
  <div class="wrapper">
    <v-card>
      <v-card-title>
        Cards

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
            style="max-width: 150px; padding: 5px"
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
  mounted() {
    //this.$set(this.hideDelimiters, "hide-delimiters", this.isMobile());
  },
  methods: {
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
      this.loading = true;
      const { page, itemsPerPage } = this.options;
      const request = {
        quickSearch: this.search,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
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
    isMobile() {
      if (screen.width <= 760) {
        return true;
      } else {
        return false;
      }
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
</style>
