<template>
  <v-footer app color="primary" dark>
    <v-row justify="center" no-gutters>
      <v-btn
        v-for="link in links"
        :key="link.name"
        color="white"
        text
        rounded
        class="my-2 mx-10"
        :href="link.url"
      >
        <h3>
          {{ link.name }}
        </h3>
      </v-btn>
      <v-col class="primary lighten-2 py-4 text-center white--text" cols="12">
        {{ new Date().getFullYear() }} — <strong>MoGaSup Team A102</strong>
      </v-col>
    </v-row>
  </v-footer>
</template>

<script>
export default {
  data: () => ({
    links: [
      {
        name: "박성우",
        url: "https://github.com/uaio88"
      },
      {
        name: "백태훈",
        url: "https://github.com/whiteburnout"
      },
      {
        name: "신인섭",
        url: "https://github.com/InSub-Shin"
      },
      {
        name: "엄재웅",
        url: "https://github.com/eomjaewoong"
      }
    ]
  })
};
</script>

<style></style>
