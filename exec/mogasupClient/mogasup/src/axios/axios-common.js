import axios from "axios";

// axios 공통 설정
export default axios.create({
  //baseURL: "http://localhost:8080",
  baseURL: "http://k4a102.p.ssafy.io:8080/",
  headers: {
    "Content-type": "application/json;charset=utf-8",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Headers": "*",
    "Access-Control-Allow-Credentials": true
  }
  //withCredentials: true
});
