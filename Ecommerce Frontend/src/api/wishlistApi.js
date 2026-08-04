import axiosInstance from "./axios";

export const getWishlist = async () => {
  const response = await axiosInstance.get("/wishlist");
  return response.data;
};

export const addToWishlist = async (productId) => {
  const response = await axiosInstance.post("/wishlist", {
    productId,
  });

  return response.data;
};

export const removeWishlistItem = async (wishlistItemId) => {
  const response = await axiosInstance.delete(`/wishlist/${wishlistItemId}`);
  return response.data;
};

export const clearWishlist = async () => {
  const response = await axiosInstance.delete("/wishlist");
  return response.data;
};
