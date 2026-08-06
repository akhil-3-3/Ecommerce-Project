import axiosInstance from "./axios";

export const getCategories = async () => {
  const response = await axiosInstance.get("/category");
  return response.data;
};
