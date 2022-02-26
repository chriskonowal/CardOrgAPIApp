<template>
  <div class="wrapper">
    <v-app id="inspire">
      <v-carousel
        v-bind="hideDelimiters"
        cycle
        hide-delimiter-background
        show-arrows-on-hover
      >
        <v-carousel-item v-for="(slide, i) in slides" :key="i">
          <v-sheet color="deep-purple accent-4" height="100%">
            <v-row align="center" justify="center">
              <v-banner two-line color="white" class="mt-5 mb-4">
                <v-avatar slot="icon" color="deep-purple accent-4" size="40">
                  <v-icon icon="mdi-basketball" color="white">
                    mdi-basketball
                  </v-icon>
                </v-avatar>
                {{ showFullNames(slide.players) }}
                {{ slide.year.year }} {{ slide.cardDescription }}
              </v-banner>
            </v-row>
            <v-row align="center" justify="center">
              <img
                v-bind:src="showImage(slide.frontCardMainImagePath)"
                style="max-width: 250px"
              />
            </v-row>
          </v-sheet>
        </v-carousel-item>
      </v-carousel>
    </v-app>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "Home",
  data() {
    return {
      slides: [],
      hideDelimiters: {},
    };
  },
  created() {
    this.readDataFromAPI();
  },
  mounted() {
    this.$set(this.hideDelimiters, "hide-delimiters", this.isMobile());
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
    showImage: function (path) {
      return "/Uploads/Mid/" + path;
    },
    readDataFromAPI() {
      const request = {
        count: 25,
      };
      console.log(request);
      axios
        .post(process.env.VUE_APP_ROOT_API + "public/card_carousel", request)
        .then((response) => {
          console.log(response.data);
          if (response.data.isSuccessful) {
            this.slides = response.data.value;
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

<style></style>
