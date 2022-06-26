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
                    <v-switch
                      v-model="isRookie"
                      :label="'Is Rookie'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="isAutograph"
                      :label="'Is Autograph'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="isOnCardAutograph"
                      :label="'Is On Card Autograph'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch v-model="isPatch" :label="'Is Patch'"> </v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="isGameWornJersey"
                      :label="'Is Game Worn Jersey'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="hasImage"
                      :label="'Has Image'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-switch
                      v-model="isSerialNumbered"
                      :label="'Is Serial Numbered'"
                    ></v-switch>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="playersSelected"
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
                        <div class="d-block pa-2">Players</div>
                        <v-text-field
                          v-model="playersSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForPlayersFromAPI()"
                          @keyup="readDataForPlayersFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="teamsSelected"
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
                        <div class="d-block pa-2">Teams</div>
                        <v-text-field
                          v-model="teamsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForTeamsFromAPI()"
                          @keyup="readDataForTeamsFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="sportsSelected"
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
                      :single-select="false"
                      item-key="sportId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">Sport</div>
                        <v-text-field
                          v-model="sportsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForSportsFromAPI()"
                          @keyup="readDataForSportsFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="yearsSelected"
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
                      :single-select="false"
                      item-key="yearId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">Year</div>
                        <v-text-field
                          v-model="yearsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForYearsFromAPI()"
                          @keyup="readDataForYearsFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="setsSelected"
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
                      :single-select="false"
                      item-key="setId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">Set</div>
                        <v-text-field
                          v-model="setsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForSetsFromAPI()"
                          @keyup="readDataForSetsFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="gradeCompaniesSelected"
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
                      :single-select="false"
                      item-key="gradeCompanyId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">Grade Company</div>
                        <v-text-field
                          v-model="gradeCompaniesSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForGradeCompaniesFromAPI()"
                          @keyup="readDataForGradeCompaniesFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row>
                    <v-data-table
                      v-model="locationsSelected"
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
                      :single-select="false"
                      item-key="locationId"
                    >
                      <template v-slot:top>
                        <div class="d-block pa-2">Location</div>
                        <v-text-field
                          v-model="locationsSearch"
                          label="Search"
                          class="mx-4"
                          @blur="readDataForLocationsFromAPI()"
                          @keyup="readDataForLocationsFromAPI()"
                        ></v-text-field>
                      </template>
                    </v-data-table>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Lowest Beckett Price</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="lowestBeckettPriceLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="lowestBeckettPriceHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Highest Beckett Price</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="highestBeckettPriceLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="highestBeckettPriceHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Lowest COMC Price</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="lowestCOMCPriceLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="lowestCOMCPriceHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Ebay Price</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="ebayPriceLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="ebayPriceHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Price Paid</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="pricePaidLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="pricePaidHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Grade</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="gradeLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="gradeHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Copies</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="copiesLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="copiesHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="padding-top: 30px">
                    <h5>Serial Number</h5>
                  </v-row>
                  <v-row>
                    <v-col>
                      <v-text-field
                        label="Low"
                        outlined
                        v-model="serialNumberLow"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="serialNumberHigh"
                        prefix="$"
                        type="number"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="margin-top: 30px">
                    <v-col cols="12" sm="6" md="3">
                      <v-btn
                        depressed
                        elevation="2"
                        @click="readDataFromAPI(false)"
                        >Search</v-btn
                      >
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                      <v-btn
                        depressed
                        elevation="2"
                        @click="readDataFromAPI(true)"
                        >Clear Search</v-btn
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
                  <v-row>
                    <h5>Card Description Sort</h5>
                    <v-radio-group
                      v-model="cardDescriptionSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Lowest Beckett Price Sort</h5>
                    <v-radio-group
                      v-model="lowestBeckettPriceSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Highest Beckett High Price</h5>
                    <v-radio-group
                      v-model="highestBeckettPriceSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Lowest COMC Price</h5>
                    <v-radio-group
                      v-model="lowestCOMCPriceSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Ebay Price</h5>
                    <v-radio-group
                      v-model="ebayPriceSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Price Paid</h5>
                    <v-radio-group
                      v-model="pricePaidSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Has Image</h5>
                    <v-radio-group
                      v-model="hasImageSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Is Graded</h5>
                    <v-radio-group
                      v-model="isGradedSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Copies</h5>
                    <v-radio-group v-model="copiesSort" style="width: 100%" row>
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Serial Number</h5>
                    <v-radio-group
                      v-model="serialNumberSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Grade</h5>
                    <v-radio-group v-model="gradeSort" style="width: 100%" row>
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Is Rookie</h5>
                    <v-radio-group
                      v-model="isRookieSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Is Autograph</h5>
                    <v-radio-group
                      v-model="isAutographSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Is On Card Autograph</h5>
                    <v-radio-group
                      v-model="isOnCardAutographSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Is Patch</h5>
                    <v-radio-group
                      v-model="isPatchSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Is Game Worn Jersey</h5>
                    <v-radio-group
                      v-model="isGameWornJerseySort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Sport</h5>
                    <v-radio-group v-model="sportSort" style="width: 100%" row>
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Year</h5>
                    <v-radio-group v-model="yearSort" style="width: 100%" row>
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Set</h5>
                    <v-radio-group v-model="setSort" style="width: 100%" row>
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Grade Company</h5>
                    <v-radio-group
                      v-model="gradeCompanySort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Location</h5>
                    <v-radio-group
                      v-model="locationSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-row>
                    <h5>Time Stamp</h5>
                    <v-radio-group
                      v-model="timeStampSort"
                      style="width: 100%"
                      row
                    >
                      <v-radio label="None" value="0"></v-radio>
                      <v-radio label="Asc" value="1"></v-radio>
                      <v-radio label="Desc" value="2"></v-radio>
                    </v-radio-group>
                  </v-row>
                  <v-divider></v-divider>
                  <v-row style="margin-top: 30px">
                    <v-col cols="12" sm="6" md="3">
                      <v-btn
                        depressed
                        elevation="2"
                        @click="readDataFromAPI(false)"
                        >Sort</v-btn
                      >
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                      <v-btn
                        depressed
                        elevation="2"
                        @click="readDataFromAPI(true)"
                        >Clear Sort</v-btn
                      >
                    </v-col>
                  </v-row>
                </v-container>
              </v-form>
            </v-expansion-panel-content>
          </v-expansion-panel>
          <v-expansion-panel>
            <v-expansion-panel-header>
              Save Search Sort
            </v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-row>
                <v-data-table
                  v-model="searchSortsSelected"
                  :headers="searchSortsHeaders"
                  :page="page"
                  :items="searchSorts"
                  :options.sync="searchSortsOptions"
                  :sort-by.sync="searchSortsSortBy"
                  :sort-desc.sync="searchSortsSortDesc"
                  :server-items-length="totalSearchSorts"
                  :loading="searchSortsLoading"
                  :items-per-page="5"
                  :search="searchSortsSearch"
                  class="elevation-1 tblSearch"
                  show-select
                  :single-select="true"
                  item-key="searchSortId"
                >
                  <template v-slot:top>
                    <div class="d-block pa-2">Search Sorts</div>
                    <v-text-field
                      v-model="searchSortsSearch"
                      label="Search"
                      class="mx-4"
                      @blur="readDataForSearchSortsFromAPI()"
                      @keyup="readDataForSearchSortsFromAPI()"
                    ></v-text-field>
                  </template>
                </v-data-table>
              </v-row>
              <v-divider></v-divider>
              <v-row style="margin-top: 30px">
                <v-col cols="12" sm="6" md="3">
                  <v-btn depressed elevation="2" @click="readDataFromAPI(false)"
                    >Load Search</v-btn
                  >
                </v-col>
                <v-col cols="12" sm="6" md="3">
                  <v-btn depressed elevation="2" @click="readDataFromAPI(true)"
                    >Clear Search</v-btn
                  >
                </v-col>
                <v-col cols="12" sm="6" md="3">
                  <v-btn depressed elevation="2" @click="readDataFromAPI(true)"
                    >Save Search</v-btn
                  >
                </v-col>
              </v-row>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
        <v-spacer></v-spacer>
        <v-text-field
          v-model="search"
          append-icon="mdi-magnify"
          label="Quick Search"
          single-line
          hide-details
          @blur="readDataFromAPI(false)"
          @keyup="readDataFromAPI(false)"
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
            @click="onClickBackImage(item)"
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
        <div class="justify-center" style="text-align: center">
          <img v-bind:src="showFrontImage" width="75%" />
        </div>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closeFrontPicture"
            >Close</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="backPictureDialog" width="85%" persistent>
      <v-card>
        <v-card-title class="text-h5 text-center block">Back</v-card-title>
        <div class="justify-center" style="text-align: center">
          <img v-bind:src="showBackImage" width="75%" />
        </div>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closeBackPicture"
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
      frontBackDialog: false,
      showBackImage: "",
      backPictureDialog: false,
      backImage: {},
      images: [],
      txtCardDescription: "",
      teamsSelected: [],
      teamsLoading: true,
      teamsOptions: {},
      teamsNumberOfPages: 0,
      teamsSearch: "",
      teams: [],
      totalTeams: 0,
      teamsSortBy: "",
      teamsSortDesc: false,
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
      playersSelected: [],
      playersLoading: true,
      playersOptions: {},
      playersNumberOfPages: 0,
      playersSearch: "",
      players: [],
      totalPlayers: 0,
      playersSortBy: "",
      playersSortDesc: false,
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
      sportsSelected: [],
      sportsLoading: true,
      sportsOptions: {},
      sportsNumberOfPages: 0,
      sportsSearch: "",
      sports: [],
      totalSports: 0,
      sportsSortBy: "",
      sportsSortDesc: false,
      sportsHeaders: [
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
      setsSelected: [],
      setsLoading: true,
      setsOptions: {},
      setsNumberOfPages: 0,
      setsSearch: "",
      sets: [],
      totalSets: 0,
      setsSortBy: "",
      setsSortDesc: false,
      setsHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
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
      locationsHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
      ],
      isGraded: false,
      playerNameSort: "0",
      teamSort: "0",
      isRookie: false,
      isAutograph: false,
      isPatch: false,
      isOnCardAutograph: false,
      isGameWornJersey: false,
      highestBeckettPriceSort: "0",
      lowestBeckettPriceLow: 0,
      lowestBeckettPriceHigh: 0,
      highestBeckettPriceLow: 0,
      highestBeckettPriceHigh: 0,
      lowestCOMCPriceLow: 0,
      lowestCOMCPriceHigh: 0,
      ebayPriceHigh: 0,
      ebayPriceLow: 0,
      pricePaidLow: 0,
      pricePaidHigh: 0,
      gradeLow: 0,
      gradeHigh: 0,
      copiesLow: 0,
      copiesHigh: 0,
      serialNumberLow: 0,
      serialNumberHigh: 0,
      hasImage: false,
      isSerialNumbered: false,
      cardDescriptionSort: "0",
      lowestBeckettPriceSort: "0",
      lowestCOMCPriceSort: "0",
      ebayPriceSort: "0",
      pricePaidSort: "0",
      hasImageSort: "0",
      isGradedSort: "0",
      copiesSort: "0",
      serialNumberSort: "0",
      gradeSort: "0",
      isRookieSort: "0",
      isAutographSort: "0",
      isPatchSort: "0",
      isOnCardAutographSort: "0",
      isGameWornJerseySort: "0",
      sportSort: "0",
      yearSort: "0",
      setSort: "0",
      gradeCompanySort: "0",
      locationSort: "0",
      timeStampSort: "0",
      searchSortsSelected: [],
      searchSortsLoading: true,
      searchSortsOptions: {},
      searchSortsNumberOfPages: 0,
      searchSortsSearch: "",
      searchSorts: [],
      totalSearchSorts: 0,
      searchSortsSortBy: "",
      searchSortsSortDesc: false,
      searchSortsHeaders: [
        {
          text: "Name",
          align: "start",
          sortable: true,
          value: "name",
        },
        {
          text: "Description",
          align: "start",
          sortable: true,
          value: "description",
        },
      ],
    };
  },
  watch: {
    options: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataFromAPI(false);
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
    yearsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForYearsFromAPI();
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
    searchSortsOptions: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataForSearchSortsFromAPI();
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
    loadSearchSort() {},
    readDataFromAPI(clear) {
      console.log("this.searchSortsSelected");
      console.log(this.searchSortsSelected);
      this.readDataForPlayersFromAPI();
      this.readDataForTeamsFromAPI();
      this.readDataForSportsFromAPI();
      this.readDataForYearsFromAPI();
      this.readDataForSetsFromAPI();
      this.readDataForGradeCompaniesFromAPI();
      this.readDataForLocationsFromAPI();
      this.readDataForSearchSortsFromAPI();
      if (
        this.searchSortsSelected != null &&
        this.searchSortsSelected.length > 0
      ) {
        let searchSort = this.searchSortsSelected[0];
        this.txtCardDescription = searchSort.cardDescription;
        this.yearsSelected = searchSort.yearIds;
        this.isGraded = searchSort.isGraded;
        this.playerNameSort = searchSort.playerNameSort;
        this.teamSort = searchSort.teamSort;
        this.isRookie = searchSort.isRookie;
        this.isAutograph = searchSort.isAutograph;
        this.isPatch = searchSort.isPatch;
        this.isOnCardAutograph = searchSort.isOnCardAutograph;
        this.isGameWornJersey = searchSort.isGameWornJersey;
        this.teamsSelected = searchSort.teamIds;
        this.playersSelected = searchSort.playerIds;
        this.sportsSelected = searchSort.sportIds;
        this.setsSelected = searchSort.setIds;
        this.gradeCompaniesSelected = searchSort.gradeCompanyIds;
        this.locationsSelected = searchSort.locationIds;
        this.highestBeckettPriceSort = searchSort.highestBeckettPriceSort;
        this.lowestBeckettPriceLow = searchSort.lowestBeckettPriceLow;
        this.lowestBeckettPriceHigh = searchSort.lowestBeckettPriceHigh;
        this.highestBeckettPriceLow = searchSort.highestBeckettPriceLow;
        this.highestBeckettPriceHigh = searchSort.highestBeckettPriceHigh;
        this.lowestCOMCPriceLow = searchSort.lowestComcpriceLow;
        this.lowestCOMCPriceHigh = searchSort.lowestComcPriceHigh;
        this.ebayPriceLow = searchSort.ebayPriceLow;
        this.ebayPriceHigh = searchSort.ebayPriceHigh;
        this.pricePaidLow = searchSort.pricePaidLow;
        this.pricePaidHigh = searchSort.pricePaidHigh;
        this.gradeLow = searchSort.gradeLow;
        this.gradeHigh = searchSort.gradeHigh;
        this.copiesLow = searchSort.copiesLow;
        this.copiesHigh = searchSort.copiesHigh;
        this.serialNumberLow = searchSort.serialNumberLow;
        this.serialNumberHigh = searchSort.serialNumberHigh;
        this.hasImage = searchSort.hasImage;
        this.isSerialNumbered = searchSort.isSerialNumbered;
        this.cardDescriptionSort = searchSort.cardDescriptionSort;
        this.lowestBeckettPriceSort = searchSort.lowestBeckettPriceSort;
        this.lowestCOMCPriceSort = searchSort.lowestCOMCPriceSort;
        this.ebayPriceSort = searchSort.ebayPriceSort;
        this.pricePaidSort = searchSort.pricePaidSort;
        this.hasImageSort = searchSort.hasImageSort;
        this.isGradedSort = searchSort.isGradedSort;
        this.copiesSort = searchSort.copiesSort;
        this.serialNumberSort = searchSort.serialNumberSort;
        this.gradeSort = searchSort.gradeSort;
        this.isRookieSort = searchSort.isRookieSort;
        this.isAutographSort = searchSort.isAutographSort;
        this.isPatchSort = searchSort.isPatchSort;
        this.isOnCardAutographSort = searchSort.isOnCardAutographSort;
        this.isGameWornJerseySort = searchSort.isGameWornJerseySort;
        this.sportSort = searchSort.sportSort;
        this.yearSort = searchSort.yearSort;
        this.setSort = searchSort.setSort;
        this.gradeCompanySort = searchSort.gradeCompanySort;
        this.locationSort = searchSort.locationSort;
        this.timeStampSort = searchSort.timeStampSort;
      }

      this.loading = true;
      const { page, itemsPerPage } = this.options;
      let request = {};
      if (clear) {
        request = {
          quickSearch: "",
          rowsPerPage: itemsPerPage,
          pageNumber: page,
          searchSortRequest: {
            cardDescription: "",
            yearIds: [],
            isGraded: false,
            playerNameSort: 0,
            teamSort: 0,
            isRookie: false,
            isAutograph: false,
            isPatch: false,
            isOnCardAutograph: false,
            isGameWornJersey: false,
            teamIds: [],
            playerIds: [],
            sportIds: [],
            setIds: [],
            gradeCompanyIds: [],
            locationIds: [],
            highestBeckettPriceSort: 0,
            lowestBeckettPriceLow: 0,
            lowestBeckettPriceHigh: 0,
            highestBeckettPriceLow: 0,
            highestBeckettPriceHigh: 0,
            lowestCOMCPriceLow: 0,
            lowestCOMCPriceHigh: 0,
            ebayPriceLow: 0,
            ebayPriceHigh: 0,
            pricePaidLow: 0,
            pricePaidHigh: 0,
            gradeLow: 0,
            gradeHigh: 0,
            copiesLow: 0,
            copiesHigh: 0,
            serialNumberLow: 0,
            serialNumberHigh: 0,
            hasImage: false,
            isSerialNumbered: false,
            cardDescriptionSort: 0,
            lowestBeckettPriceSort: 0,
            lowestCOMCPriceSort: 0,
            ebayPriceSort: 0,
            pricePaidSort: 0,
            hasImageSort: 0,
            isGradedSort: 0,
            copiesSort: 0,
            serialNumberSort: 0,
            gradeSort: 0,
            isRookieSort: 0,
            isAutographSort: 0,
            isPatchSort: 0,
            isOnCardAutographSort: 0,
            isGameWornJerseySort: 0,
            sportSort: 0,
            yearSort: 0,
            setSort: 0,
            gradeCompanySort: 0,
            locationSort: 0,
            timeStampSort: 0,
          },
        };
      } else {
        request = {
          quickSearch: this.search,
          rowsPerPage: itemsPerPage,
          pageNumber: page,
          searchSortRequest: {
            cardDescription: this.txtCardDescription,
            yearIds: this.getYearIds(this.yearsSelected),
            isGraded: this.isGraded,
            playerNameSort: parseInt(this.playerNameSort),
            teamSort: parseInt(this.teamSort),
            isRookie: this.isRookie,
            isAutograph: this.isAutograph,
            isPatch: this.isPatch,
            isOnCardAutograph: this.isOnCardAutograph,
            isGameWornJersey: this.isGameWornJersey,
            teamIds: this.getTeamsIds(this.teamsSelected),
            playerIds: this.getPlayersIds(this.playersSelected),
            sportIds: this.getSportIds(this.sportsSelected),
            setIds: this.getSetIds(this.setsSelected),
            gradeCompanyIds: this.getGradeCompanyIds(
              this.gradeCompaniesSelected
            ),
            locationIds: this.getLocationIds(this.locationsSelected),
            highestBeckettPriceSort: parseInt(this.highestBeckettPriceSort),
            lowestBeckettPriceLow: this.lowestBeckettPriceLow,
            lowestBeckettPriceHigh: this.lowestBeckettPriceHigh,
            highestBeckettPriceLow: this.highestBeckettPriceLow,
            highestBeckettPriceHigh: this.highestBeckettPriceHigh,
            lowestCOMCPriceLow: this.lowestCOMCPriceLow,
            lowestCOMCPriceHigh: this.lowestCOMCPriceHigh,
            ebayPriceLow: this.ebayPriceLow,
            ebayPriceHigh: this.ebayPriceHigh,
            pricePaidLow: this.pricePaidLow,
            pricePaidHigh: this.pricePaidHigh,
            gradeLow: this.gradeLow,
            gradeHigh: this.gradeHigh,
            copiesLow: this.copiesLow,
            copiesHigh: this.copiesHigh,
            serialNumberLow: this.serialNumberLow,
            serialNumberHigh: this.serialNumberHigh,
            hasImage: this.hasImage,
            isSerialNumbered: this.isSerialNumbered,
            cardDescriptionSort: parseInt(this.cardDescriptionSort),
            lowestBeckettPriceSort: parseInt(this.lowestBeckettPriceSort),
            lowestCOMCPriceSort: parseInt(this.lowestCOMCPriceSort),
            ebayPriceSort: parseInt(this.ebayPriceSort),
            pricePaidSort: parseInt(this.pricePaidSort),
            hasImageSort: parseInt(this.hasImageSort),
            isGradedSort: parseInt(this.isGradedSort),
            copiesSort: parseInt(this.copiesSort),
            serialNumberSort: parseInt(this.serialNumberSort),
            gradeSort: parseInt(this.gradeSort),
            isRookieSort: parseInt(this.isRookieSort),
            isAutographSort: parseInt(this.isAutographSort),
            isPatchSort: parseInt(this.isPatchSort),
            isOnCardAutographSort: parseInt(this.isOnCardAutographSort),
            isGameWornJerseySort: parseInt(this.isGameWornJerseySort),
            sportSort: parseInt(this.sportSort),
            yearSort: parseInt(this.yearSort),
            setSort: parseInt(this.setSort),
            gradeCompanySort: parseInt(this.gradeCompanySort),
            locationSort: parseInt(this.locationSort),
            timeStampSort: parseInt(this.timeStampSort),
          },
        };
      }

      console.log("request:");
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
    onClickBackImage(item) {
      this.showBackImage = "/Uploads/Mid/" + item.backCardMainImagePath;
      this.showBackImageZoom = "/Uploads/Large/" + item.backCardMainImagePath;
      this.backPictureDialog = true;
    },
    closeFrontPicture() {
      this.frontPictureDialog = false;
    },
    closeBackPicture() {
      this.backPictureDialog = false;
    },
    isMobile() {
      if (screen.width <= 760) {
        return true;
      } else {
        return false;
      }
    },
    readDataForPlayersFromAPI() {
      this.teamsLoading = true;
      console.log(this.playersOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.playersOptions;

      const request = {
        searchTerm: this.playersSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/players", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.players = response.data.value.players;
            this.totalPlayers = response.data.value.totalPlayers;
          }

          this.playersLoading = false;
        });
    },
    readDataForTeamsFromAPI() {
      this.teamsLoading = true;
      console.log(this.teamsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.teamsOptions;

      const request = {
        searchTerm: this.teamsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/teams", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.teams = response.data.value.teams;
            this.totalTeams = response.data.value.total;
          }

          this.teamsLoading = false;
        });
    },
    readDataForSportsFromAPI() {
      this.sportsLoading = true;
      console.log(this.sportsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.sportsOptions;

      const request = {
        searchTerm: this.sportsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/sports", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.sports = response.data.value.sports;
            this.totalSports = response.data.value.total;
          }

          this.sportsLoading = false;
        });
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
    readDataForSetsFromAPI() {
      this.setsLoading = true;
      console.log(this.setsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.setsOptions;
      const request = {
        searchTerm: this.setsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/sets", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.sets = response.data.value.sets;
            this.totalSets = response.data.value.total;
          }

          this.setsLoading = false;
        });
    },
    readDataForGradeCompaniesFromAPI() {
      this.gradeCompaniesLoading = true;
      console.log(this.gradeCompaniesOptions);
      const { sortBy, sortDesc, page, itemsPerPage } =
        this.gradeCompaniesOptions;
      const request = {
        searchTerm: this.gradeCompaniesSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/grade_companies", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.gradeCompanies = response.data.value.gradeCompanies;
            this.totalGradeCompanies = response.data.value.total;
          }

          this.gradeCompaniesLoading = false;
        });
    },
    readDataForLocationsFromAPI() {
      this.locationsLoading = true;
      console.log(this.locationsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.locationsOptions;
      const request = {
        searchTerm: this.locationsSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "admin/locations", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.locations = response.data.value.locations;
            this.totalLocations = response.data.value.total;
          }

          this.locationsLoading = false;
        });
    },
    readDataForSearchSortsFromAPI() {
      this.searchSortsLoading = true;
      console.log(this.searchSortsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.searchSortsOptions;

      const request = {
        searchTerm: this.searchSortSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      console.log(request);
      axios
        .post(
          process.env.VUE_APP_ROOT_API + "public/search_sort/search",
          request
        )
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.searchSorts = response.data.value.searchSorts;
            this.totalSearchSorts = response.data.value.total;
          }

          this.searchSortsLoading = false;
        });
    },
    validInt: function (text) {
      return text % 1 === 0;
    },
    getPlayersIds(playerArray) {
      var ids = new Array();
      for (var i = 0; i < playerArray.length; i++) {
        ids.push(parseInt(playerArray[i].playerId));
      }
      return ids;
    },
    getTeamsIds(teamArray) {
      var ids = new Array();
      for (var i = 0; i < teamArray.length; i++) {
        ids.push(parseInt(teamArray[i].teamId));
      }
      return ids;
    },
    getSportIds(sportArray) {
      var ids = new Array();
      for (var i = 0; i < sportArray.length; i++) {
        ids.push(parseInt(sportArray[i].sportId));
      }
      return ids;
    },
    getYearIds(yearArray) {
      var ids = new Array();
      for (var i = 0; i < yearArray.length; i++) {
        ids.push(parseInt(yearArray[i].yearId));
      }
      return ids;
    },
    getSetIds(setArray) {
      var ids = new Array();
      for (var i = 0; i < setArray.length; i++) {
        ids.push(parseInt(setArray[i].setId));
      }
      return ids;
    },
    getGradeCompanyIds(gradeCompanyArray) {
      var ids = new Array();
      for (var i = 0; i < gradeCompanyArray.length; i++) {
        ids.push(parseInt(gradeCompanyArray[i].gradeCompanyId));
      }
      return ids;
    },
    getLocationIds(locationArray) {
      var ids = new Array();
      for (var i = 0; i < locationArray.length; i++) {
        ids.push(parseInt(locationArray[i].locationId));
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
.tblSearch .v-data-table__mobile-row__header {
  min-width: 100px;
}
.v-input--radio-group--row {
  margin-top: 0px;
}
.elevation-1 {
  margin-top: 30px;
}
</style>
