import axiosInstance from "./axios";

export const getCart = async () => {
  const response = await axiosInstance.get("/cart");
  return response.data;
};

export const addToCart = async (productId, quantity = 1) => {
  const response = await axiosInstance.post("/cart", {
    productId,
    quantity,
  });

  return response.data;
};

export const updateCartQuantity = async (cartItemId, quantity) => {
  const response = await axiosInstance.put(`/cart/${cartItemId}`, quantity, {
    headers: {
      "Content-Type": "application/json",
    },
  });

  return response.data;
};

export const removeCartItem = async (cartItemId) => {
  const response = await axiosInstance.delete(`/cart/${cartItemId}`);
  return response.data;
};

export const clearCart = async () => {
  const response = await axiosInstance.delete("/cart");
  return response.data;
};
