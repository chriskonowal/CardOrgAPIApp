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
                        <span
                          class="text-caption selected-search"
                          v-html="showPlayersSelected(playersSelected)"
                        ></span>
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
                        <span
                          class="text-caption selected-search"
                          v-html="showTeamsSelected(teamsSelected)"
                        ></span>
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
                        <span
                          class="text-caption selected-search"
                          v-html="showNamesSelected(sportsSelected)"
                        ></span>
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
                        <span
                          class="text-caption selected-search"
                          v-html="showYearsSelected(yearsSelected)"
                        ></span>
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
                        <span
                          class="text-caption selected-search"
                          v-html="showNamesSelected(setsSelected)"
                        ></span>
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
                        <span
                          class="text-caption selected-search"
                          v-html="showNamesSelected(gradeCompaniesSelected)"
                        ></span>
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
                        <span
                          class="text-caption selected-search"
                          v-html="showNamesSelected(locationsSelected)"
                        ></span>
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
                        prefix=""
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="gradeHigh"
                        prefix=""
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
                        prefix=""
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="copiesHigh"
                        prefix=""
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
                        prefix=""
                        type="number"
                      ></v-text-field>
                    </v-col>
                    <v-col>
                      <v-text-field
                        label="High"
                        outlined
                        v-model="serialNumberHigh"
                        prefix=""
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
                        @click="readDataFromAPI(false, false)"
                        >Search</v-btn
                      >
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                      <v-btn
                        depressed
                        elevation="2"
                        @click="readDataFromAPI(true, false)"
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
                        @click="readDataFromAPI(false, false)"
                        >Sort</v-btn
                      >
                    </v-col>
                    <v-col cols="12" sm="6" md="3">
                      <v-btn
                        depressed
                        elevation="2"
                        @click="readDataFromAPI(true, false)"
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
                  <div class="d-block pa-2">Search Sorts</div>
                  <v-text-field
                    v-model="searchSortsSearch"
                    label="Search"
                    class="mx-4"
                    @blur="readDataForSearchSortsFromAPI()"
                    @keyup="readDataForSearchSortsFromAPI()"
                  ></v-text-field>
                  <template v-slot:[`item.actions`]="{ item }">
                    <v-icon
                      small
                      class="mr-2"
                      @click="editSearchSortItem(item)"
                    >
                      mdi-pencil
                    </v-icon>
                    <v-icon small @click="deleteSearchSortItem(item)">
                      mdi-delete
                    </v-icon>
                  </template>
                </v-data-table>
              </v-row>
              <v-divider></v-divider>
              <v-row style="margin-top: 30px">
                <v-col cols="12" sm="6" md="3">
                  <v-btn
                    depressed
                    elevation="2"
                    @click="readDataFromAPI(false, true)"
                    >Load Search</v-btn
                  >
                </v-col>
                <v-col cols="12" sm="6" md="3">
                  <v-btn
                    depressed
                    elevation="2"
                    @click="readDataFromAPI(true, true)"
                    >Clear Search</v-btn
                  >
                </v-col>
                <v-col cols="12" sm="6" md="3">
                  <v-btn
                    depressed
                    elevation="2"
                    @click="openSaveSearchSortDialog()"
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
          @blur="readDataFromAPI(false, false)"
          @keyup="readDataFromAPI(false, false)"
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
    <v-dialog v-model="editSearchSortDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="text-h5">{{ editSearchSortTitle }}</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-form
              ref="editSearchSortForm"
              v-model="editSearchSortValid"
              lazy-validation
            >
              <v-row>
                <v-col cols="12" sm="6" md="4">
                  <v-text-field
                    v-model="editedSearchSortItem.name"
                    label="Name"
                    type="text"
                    name="editedItem.Name"
                    :rules="[rules.required]"
                    @blur="clearSearchSortAddMessage()"
                    @keyup="clearSearchSortAddMessage()"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-textarea
                  v-model="editedSearchSortItem.description"
                  solo
                  label="Description"
                ></v-textarea>
              </v-row>
              <v-row>
                <span style="color: red" v-show="hasSearchSortAddError">
                  <v-icon color="red"> mdi-alert-rhombus-outline </v-icon>
                  {{ addSearchSortErrorMessage }}
                </span>
              </v-row>
            </v-form>
          </v-container>
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn color="blue darken-1" text @click="editSearchSortClose">
            Cancel
          </v-btn>
          <v-btn color="blue darken-1" text @click="editSearchSortSave">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialogSearchSortDelete" max-width="500px">
      <v-card>
        <v-card-title class="text-h5"
          >Are you sure you want to delete this item?</v-card-title
        >
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="closeSearchSortDelete"
            >Cancel</v-btn
          >
          <v-btn color="blue darken-1" text @click="deleteSearchSortItemConfirm"
            >OK</v-btn
          >
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="infoSearchSortDialog" max-width="500px">
      <v-card>
        <v-card-title>
          <span class="text-h5">{{ infoSearchSortDialogTitleMessage }}</span>
        </v-card-title>
        <v-card-text>
          <span style="color: green">
            <v-icon color="green"> mdi-alarm-light-outline </v-icon>
            {{ infoSearchSortDialogMessage }}
          </span>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              color="blue darken-1"
              text
              @click="infoSearchSortDialogClose"
            >
              Ok
            </v-btn>
          </v-card-actions>
        </v-card-text>
      </v-card>
    </v-dialog>
    <v-dialog v-model="dialogSearchSortErrorDelete" max-width="500px">
      <v-card>
        <v-card-title class="text-h5"
          >There was a problem deleting item.</v-card-title
        >
        <p style="color: red; text-align: center">
          {{ deleteSearchSortErrorMessage }}
        </p>
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
        { text: "Actions", value: "actions", sortable: false },
      ],
      searchSortName: "",
      searchSortDescription: "",
      isSaveSearchButtonDisabled: false,
      hasSearchSortInfoMessage: false,
      searchSortInfoMessage: "",
      editedSearchSortItem: {
        searchSortId: 0,
        name: "",
        description: "",
      },
      rules: {
        required: (value) => !!value || "Required.",
      },
      editSearchSortTitle: "",
      editSearchSortValid: true,
      hasSearchSortAddError: false,
      addSearchSortErrorMessage: false,
      dialogSearchSortDelete: false,
      searchSortId: 0,
      dialogSearchSortErrorDelete: false,
      deleteSearchSortErrorMessage: "",
      editSearchSortDialog: false,
      infoSearchSortDialog: false,
      infoSearchSortDialogMessage: "",
      infoSearchSortDialogTitleMessage: "",
    };
  },
  watch: {
    options: {
      handler(newVal, oldVal) {
        if (newVal != oldVal) {
          this.readDataFromAPI(false, false);
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
    readDataFromAPI(clear, isLoadSearch) {
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
        this.fetchI;
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
        this.lowestCOMCPriceSort = searchSort.lowestComcpriceSort;
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
      } else {
        if (isLoadSearch) {
          clear = true;
          this.clearFilters();
        }
      }

      this.loading = true;
      if (clear) {
        this.clearFilters();
      }
      const { page, itemsPerPage } = this.options;
      let request = {};
      let searchSortRequest = this.buildSearchSortRequest();
      request = {
        quickSearch: this.search,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        searchSortRequest: searchSortRequest,
      };
      console.log("request:");
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "public/cards", request)
        .then((response) => {
          //console.log("response.data");
          //console.log(response.data);
          if (response.data.isSuccessful) {
            this.cards = response.data.value.cards;
            this.totalCards = response.data.value.totalCards;
            this.loading = false;
          }
        });
      console.log("end card request:");
    },
    clearFilters() {
      this.txtCardDescription = "";
      this.yearsSelected = [];
      this.isGraded = false;
      this.playerNameSort = 0;
      this.teamSort = 0;
      this.isRookie = false;
      this.isAutograph = false;
      this.isPatch = false;
      this.isOnCardAutograph = false;
      this.isGameWornJersey = false;
      this.teamsSelected = [];
      this.playersSelected = [];
      this.sportsSelected = [];
      this.setsSelected = [];
      this.gradeCompaniesSelected = [];
      this.locationsSelected = [];
      this.highestBeckettPriceSort = 0;
      this.lowestBeckettPriceLow = 0;
      this.lowestBeckettPriceHigh = 0;
      this.highestBeckettPriceLow = 0;
      this.highestBeckettPriceHigh = 0;
      this.lowestCOMCPriceLow = 0;
      this.lowestCOMCPriceHigh = 0;
      this.ebayPriceLow = 0;
      this.ebayPriceHigh = 0;
      this.pricePaidLow = 0;
      this.pricePaidHigh = 0;
      this.gradeLow = 0;
      this.gradeHigh = 0;
      this.copiesLow = 0;
      this.copiesHigh = 0;
      this.serialNumberLow = 0;
      this.serialNumberHigh = 0;
      this.hasImage = false;
      this.isSerialNumbered = false;
      this.cardDescriptionSort = 0;
      this.lowestBeckettPriceSort = 0;
      this.lowestCOMCPriceSort = 0;
      this.ebayPriceSort = 0;
      this.pricePaidSort = 0;
      this.hasImageSort = 0;
      this.isGradedSort = 0;
      this.copiesSort = 0;
      this.serialNumberSort = 0;
      this.gradeSort = 0;
      this.isRookieSort = 0;
      this.isAutographSort = 0;
      this.isPatchSort = 0;
      this.isOnCardAutographSort = 0;
      this.isGameWornJerseySort = 0;
      this.sportSort = 0;
      this.yearSort = 0;
      this.setSort = 0;
      this.gradeCompanySort = 0;
      this.locationSort = 0;
      this.timeStampSort = 0;
      this.searchSortsSelected = [];
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
    buildSearchSortRequest() {
      let request = {};
      request = {
        name: this.searchSortName,
        description: this.searchSortDescription,
        cardDescription: this.txtCardDescription,
        yearIds: this.yearsSelected,
        isGraded: this.isGraded,
        playerNameSort: parseInt(this.playerNameSort),
        teamSort: parseInt(this.teamSort),
        isRookie: this.isRookie,
        isAutograph: this.isAutograph,
        isPatch: this.isPatch,
        isOnCardAutograph: this.isOnCardAutograph,
        isGameWornJersey: this.isGameWornJersey,
        teamIds: this.teamsSelected,
        playerIds: this.playersSelected,
        sportIds: this.sportsSelected,
        setIds: this.setsSelected,
        gradeCompanyIds: this.gradeCompaniesSelected,
        locationIds: this.locationsSelected,
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
      };
      return request;
    },
    openSaveSearchSortDialog() {
      this.editSearchSortTitle = "Add Search Sort";
      this.editedSearchSortItem = {};
      this.editSearchSortDialog = true;
    },
    editSearchSortItem(item) {
      console.log(item);
      this.editSearchSortTitle = "Edit Search Sort";
      this.editedSearchSortItem = item;
      this.editSearchSortDialog = true;
    },
    editSearchSortSave() {
      this.clearSearchSortAddMessage();
      var isEdit = this.editedSearchSortItem.searchSortId > 0;
      var validEdit = this.$refs.editSearchSortForm.validate();
      if (!validEdit) {
        return;
      }
      this.hasSearchSortInfoMessage = false;
      let request = {};
      let searchSortRequest = this.buildSearchSortRequest();
      searchSortRequest.searchSortId = this.editedSearchSortItem.searchSortId;
      searchSortRequest.name = this.editedSearchSortItem.name;
      searchSortRequest.description = this.editedSearchSortItem.description;

      request = {
        searchSortRequest: searchSortRequest,
      };
      console.log("request:");
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "public/search_sort/save", request)
        .then((response) => {
          //console.log("response.data");
          //console.log(response.data);
          if (!response.data.isSuccessful) {
            this.hasAddError = true;
            this.addErrorMessage = response.data.errorMessage;
          } else {
            this.readDataForSearchSortsFromAPI();
            this.editSearchSortDialog = false;
            this.infoSearchSortDialog = true;
            if (isEdit) {
              this.infoSearchSortDialogMessage = "Edit successful!";
              this.infoSearchSortDialogTitleMessage = "Edit Search Sort";
            } else {
              this.infoSearchSortDialogMessage = "Add successful!";
              this.infoSearchSortDialogTitleMessage = "Add Search Sort";
            }
          }
        });
    },
    editSearchSortClose() {
      this.searchSortName = "";
      this.searchSortDescription = "";
      this.hasSearchSortInfoMessage = false;
      this.isSaveSearchButtonDisabled = false;
      this.editSearchSortDialog = false;
      this.searchSortInfoMessage = "";
      this.editSearchSortDialog = false;
    },
    infoSearchSortDialogClose() {
      this.infoSearchSortDialog = false;
    },
    deleteSearchSortItem(item) {
      this.searchSortId = item.searchSortId;
      this.dialogSearchSortDelete = true;
    },
    closeSearchSortDelete() {
      this.dialogSearchSortDelete = false;
    },
    deleteSearchSortItemConfirm() {
      var request = {
        id: this.searchSortId,
      };
      console.log("delete request");
      console.log(request);
      axios({
        method: "post", //you can set what request you want to be
        url: process.env.VUE_APP_ROOT_API + "public/search_sort/delete",
        data: request,
      }).then((response) => {
        console.log(response.data);
        if (!response.data.isSuccessful) {
          this.closeSearchSortDelete();
          this.dialogSearchSortErrorDelete = true;
          this.deleteSearchSortErrorMessage = response.data.errorMessage;
        } else {
          this.readDataFromAPI();
          this.editSearchSortDialog = false;
          this.infoSearchSortDialog = true;
          this.infoSearchSortDialogMessage = "Delete successful!";
          this.infoSearchSortDialogTitleMessage = "Delete Search Sort";
          this.closeSearchSortDelete();
        }
      });
    },
    clearSearchSortAddMessage() {
      this.hasSearchSortAddError = false;
      this.addSearchSortErrorMessage = "";
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
            this.totalPlayers = response.data.value.totalPlayers;
          }

          this.playersLoading = false;
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
    readDataForSearchSortsFromAPI() {
      this.searchSortsLoading = true;
      //console.log(this.searchSortsOptions);
      const { sortBy, sortDesc, page, itemsPerPage } = this.searchSortsOptions;

      const request = {
        searchTerm: this.searchSortSearch,
        rowsPerPage: itemsPerPage,
        pageNumber: page,
        sortByField: sortBy != null ? sortBy[0] : "",
        isSortDesc: sortDesc != null ? sortDesc[0] : false,
      };
      //console.log(request);
      axios
        .post(
          process.env.VUE_APP_ROOT_API + "public/search_sort/search",
          request
        )
        .then((response) => {
          //console.log(response.data);
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
.selected-search {
  padding: 15px;
  word-break: break-word;
}
</style>
