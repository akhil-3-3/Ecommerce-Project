import axiosInstance from "./axios";

export const getStocks = async () => {
  const response = await axiosInstance.get("/stock");
  return response.data;
};
