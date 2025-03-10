//Conexão com o Backend
import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7130",
});

export default api;
