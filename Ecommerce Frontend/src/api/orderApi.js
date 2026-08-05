import axiosInstance from "./axios";

export const createOrder = async (order) => {
  const response = await axiosInstance.post("/order", order);
  return response.data;
};
