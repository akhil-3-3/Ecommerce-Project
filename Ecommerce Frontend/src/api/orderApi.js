import axiosInstance from "./axios";

export const createOrder = async (order) => {
  const response = await axiosInstance.post("/order", order);
  return response.data;
};
export const getMyOrders = async () => {
  const response = await axiosInstance.get("/order/my-orders");
  return response.data;
};
export const cancelOrder = async (orderId) => {
  const response = await axiosInstance.put(`/order/cancel/${orderId}`);
  return response.data;
};