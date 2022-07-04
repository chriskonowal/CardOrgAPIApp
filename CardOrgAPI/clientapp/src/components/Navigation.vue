<template>
  <v-toolbar max-height="64">
    <v-menu
      v-model="menu"
      :close-on-content-click="false"
      top
      :offset-x="offset"
      style="min-width: 45px"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-icon v-bind="attrs" v-on="on"> mdi-menu </v-icon>
      </template>
      <v-list>
        <v-list-item>
          <v-list-item-icon>
            <v-icon>mdi-home</v-icon>
          </v-list-item-icon>
          <v-list-item-title
            ><router-link
              to="/home"
              style="text-decoration: none; color: inherit"
              >Home</router-link
            ></v-list-item-title
          >
        </v-list-item>
        <v-list-item>
          <v-list-item-icon>
            <v-icon>mdi-basketball</v-icon>
          </v-list-item-icon>
          <v-list-item-title
            ><router-link
              to="/landing"
              style="text-decoration: none; color: inherit"
              >Landing Page</router-link
            ></v-list-item-title
          >
        </v-list-item>
        <v-list-group :value="true" no-action sub-group>
          <template v-slot:activator>
            <v-list-item-content>
              <v-list-item-title>Admin</v-list-item-title>
            </v-list-item-content>
          </template>
          <v-list-item>
            <v-list-item-title>
              <router-link
                to="/years-admin"
                style="text-decoration: none; color: inherit"
                >Years</router-link
              ></v-list-item-title
            >
          </v-list-item>
          <v-list-item>
            <v-list-item-title>
              <router-link
                to="/grade-company-admin"
                style="text-decoration: none; color: inherit"
                >Grade Company</router-link
              ></v-list-item-title
            >
          </v-list-item>
          <v-list-item>
            <v-list-item-title>
              <router-link
                to="/location-admin"
                style="text-decoration: none; color: inherit"
                >Location</router-link
              ></v-list-item-title
            >
          </v-list-item>
          <v-list-item>
            <v-list-item-title>
              <router-link
                to="/player-admin"
                style="text-decoration: none; color: inherit"
                >Player</router-link
              ></v-list-item-title
            >
          </v-list-item>
          <v-list-item>
            <v-list-item-title>
              <router-link
                to="/set-admin"
                style="text-decoration: none; color: inherit"
                >Set</router-link
              ></v-list-item-title
            >
          </v-list-item>
        </v-list-group>
      </v-list>
    </v-menu>
    <v-toolbar-title style="margin-left: 15px">Card Org</v-toolbar-title>
  </v-toolbar>
</template>

<script>
export default {
  name: "Navigation",
  data: () => ({
    menu: false,
    offset: true,
  }),
};
</script>
