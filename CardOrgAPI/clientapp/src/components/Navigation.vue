<template>
  <v-toolbar max-height="64">
    <v-menu
      v-model="menu"
      :close-on-content-click="false"
      top
      :offset-x="offset"
    >
      <template v-slot:activator="{ on, attrs }">
        <v-icon v-bind="attrs" v-on="on"> mdi-menu </v-icon>
      </template>
      <v-list>
        <v-list-item>
          <v-list-item-title
            ><router-link to="/home">Home</router-link></v-list-item-title
          >
        </v-list-item>

        <v-list-item>
          <v-list-item-title>
            <router-link to="/years-admin"
              >Years Admin</router-link
            ></v-list-item-title
          >
        </v-list-item>

        <v-list-item>
          <v-list-item-title>Fizz</v-list-item-title>
        </v-list-item>

        <v-list-item>
          <v-list-item-title>Buzz</v-list-item-title>
        </v-list-item>
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
