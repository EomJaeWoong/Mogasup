<template>
  <v-container>
    <v-row class="text-center">
      <v-col cols="12">
        <v-img src="@/assets/mylogo.png" contain height="300" />
      </v-col>

      <v-col class="mb-4">
        <h1 class="display-2 font-weight-bold mb-3">DownLoad Our Game</h1>
      </v-col>
    </v-row>
    <v-row class="text-center">
      <v-col>
        <v-btn x-large color="primary" icon class="my-10" @click="download()">
          <v-img
            height="200px"
            width="200px"
            contain
            src="@/assets/download.png"
          ></v-img>
        </v-btn>
      </v-col>
    </v-row>

    <v-snackbar centered :color="color" v-model="snackbar" timeout="3000">
      {{ msg }}
    </v-snackbar>
  </v-container>
</template>

<script>
import axios from "../axios/axios-common";
//import axios from "axios";
export default {
  data() {
    return {
      snackbar: false,
      msg: "",
      color: ""
    };
  },
  methods: {
    download() {
      return axios
        .get("/download")
        .then(response => {
          this.msg = "다운로드 성공!";
          this.color = "success";
          this.snackbar = true;
          console.log(response.data);
          //const url = window.URL.createObjectURL(new Blob([response.data]));
          //const url ="http://k4a102.p.ssafy.io/home/ubuntu/backend/MoGasup.zip";
          const url = response.data.result;
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "MoGaSup.zip");
          document.body.appendChild(link);
          link.click();
          console.log(link);
        })
        .catch(err => {
          this.msg = "인터넷 연결을 확인 해 주세요";
          this.color = "error";
          this.snackbar = true;

          console.log(err + "다운 실패");
        });
    }
  }
};
</script>
