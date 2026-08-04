import api, { API_URL } from "./axios";

export const authApi = {
  // ==========================
  // REGISTER
  // ==========================
  register: async (username, email, password) => {
    const response = await api.post("/Auth/register", {
      username,
      email,
      password,
    });

    return response.data;
  },

  // ==========================
  // VERIFY EMAIL
  // ==========================
  verifyEmail: async (email, code) => {
    const response = await api.post("/Auth/verify-email", {
      email,
      code,
    });

    return response.data;
  },

  // ==========================
  // LOGIN
  // ==========================
  login: async (email, password) => {
    const response = await api.post("/Auth/login", {
      email,
      password,
    });

    return response.data;
  },

  // ==========================
  // CURRENT USER
  // ==========================
  getCurrentUser: async () => {
    const response = await api.get("/Auth/me");
    return response.data;
  },

  // ==========================
  // LOGOUT
  // ==========================
  logout: async () => {
    const response = await api.post("/Auth/logout");
    return response.data;
  },

  // ==========================
  // GOOGLE LOGIN
  // ==========================
  googleLogin: () => {
    window.location.href = `${API_URL}/Auth/google-login`;
  },

  // ==========================
  // FACEBOOK LOGIN
  // ==========================
  facebookLogin: () => {
    window.location.href = `${API_URL}/Auth/facebook-login`;
  },
};
