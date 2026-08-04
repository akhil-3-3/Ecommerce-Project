import api from "./axios";

export const getProducts = async () => {
  const response = await api.get("/Product");
  return response.data;
};

export const getProductById = async (id) => {
  const response = await api.get(`/Product/${id}`);
  return response.data;
};

export const searchProducts = async (keyword) => {
  const response = await api.get(`/Product/search?keyword=${keyword}`);
  return response.data;
};
