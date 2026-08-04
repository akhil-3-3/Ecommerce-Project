import axios from "axios";

export const API_URL = "https://localhost:7236/api";

const api = axios.create({
  baseURL: API_URL,
  withCredentials: true,
});

export default api;
