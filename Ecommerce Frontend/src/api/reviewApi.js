import api from "./axios";

// ==========================
// GET ALL REVIEWS
// =========================

export const getReviews = async () => {
  const response = await api.get("/Review");
  return response.data;
};

// ==========================
// GET REVIEWS BY PRODUCT
// ==========================

export const getReviewsByProduct = async (productId) => {
  const response = await api.get(`/Review/product/${productId}`);
  return response.data;
};

// ==========================
// ADD REVIEW
// ==========================

export const addReview = async (productId, userId, rating, reviewText) => {
  const response = await api.post("/Review", {
    productId,
    userId,
    rating,
    reviewText,
  });

  return response.data;
};

// ==========================
// UPDATE REVIEW
// ==========================

export const updateReview = async (reviewId, rating, reviewText) => {
  const response = await api.put("/Review", {
    reviewId,
    rating,
    reviewText,
  });

  return response.data;
};

// ==========================
// DELETE REVIEW
// ==========================

export const deleteReview = async (reviewId) => {
  const response = await api.delete(`/Review/${reviewId}`);
  return response.data;
};
