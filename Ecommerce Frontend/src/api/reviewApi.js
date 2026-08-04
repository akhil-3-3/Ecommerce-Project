import api from "./axios";

export const getReviews = async () => {
  const responce = await api.get("/Review");
  return responce.data;
};

export const searchReviews = async (keyword) => {
  const responce = await api.get(`/Review/search?keyword=${keyword}`);
  return responce.data;
};
