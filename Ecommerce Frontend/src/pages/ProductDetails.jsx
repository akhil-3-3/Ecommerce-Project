import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import {
  Heart,
  ShoppingCart,
  Minus,
  Plus,
  Star,
  ShoppingBag,
} from "lucide-react";
import { getProductById } from "../api/productApi";
import { addToCart } from "../api/cartApi";
import { addToWishlist } from "../api/wishlistApi";
import { useNavigate } from "react-router-dom";
import { getReviewsByProduct } from "../api/reviewApi";

function ProductDetails() {
  const { id } = useParams();

  const [product, setProduct] = useState(null);
  const [quantity, setQuantity] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [selectedImage, setSelectedImage] = useState(0);

  const navigate = useNavigate();
  const [reviews, setReviews] = useState([]);
  useEffect(() => {
    loadReviews();
  }, [id]);

  const loadReviews = async () => {
    try {
      const data = await getReviewsByProduct(id);
      setReviews(data);
    } catch (error) {
      console.error("Failed to load reviews:", error);
    }
  };
  useEffect(() => {
    loadProduct();
  }, [id]);

  const loadProduct = async () => {
    try {
      setLoading(true);
      setError("");

      const data = await getProductById(id);

      setProduct(data);
      setSelectedImage(0);
    } catch (err) {
      console.error(err);
      setError("Unable to load product.");
    } finally {
      setLoading(false);
    }
  };

  const increaseQuantity = () => {
    setQuantity((prev) => prev + 1);
  };

  const decreaseQuantity = () => {
    setQuantity((prev) => (prev > 1 ? prev - 1 : 1));
  };

  if (loading) {
    return (
      <div className="flex min-h-[500px] items-center justify-center">
        <p className="text-gray-500">Loading product...</p>
      </div>
    );
  }

  if (error || !product) {
    return (
      <div className="flex min-h-[500px] items-center justify-center">
        <p className="text-red-500">{error || "Product not found."}</p>
      </div>
    );
  }

  const discountedPrice =
    product.price - (product.price * product.discount) / 100;

  return (
    <div className="mx-auto max-w-7xl px-6 py-10">
      <div className="grid grid-cols-1 gap-12 md:grid-cols-2">
        {/* ==========================
            PRODUCT IMAGE
        ========================== */}

        <div className="flex flex-col">
          {/* Main Image */}
          <div className="flex items-center justify-center rounded-xl bg-[#f8f8f8] p-10">
            {product.images?.length > 0 ? (
              <img
                src={product.images[selectedImage].imageUrl}
                alt={product.productName}
                className="h-[500px] w-full object-contain"
              />
            ) : (
              <div className="flex h-[500px] w-full items-center justify-center text-gray-400">
                No Image Available
              </div>
            )}
          </div>

          {/* Thumbnails */}
          {product.images?.length > 1 && (
            <div className="mt-5 flex flex-wrap gap-3">
              {product.images.map((img, index) => (
                <button
                  key={img.imageId}
                  type="button"
                  onClick={() => setSelectedImage(index)}
                  className={`overflow-hidden rounded-lg border-2 transition-all duration-200 ${
                    selectedImage === index
                      ? "border-black scale-105"
                      : "border-gray-300 hover:border-gray-500"
                  }`}
                >
                  <img
                    src={img.imageUrl}
                    alt={`Thumbnail ${index + 1}`}
                    className="h-20 w-20 object-cover"
                  />
                </button>
              ))}
            </div>
          )}
        </div>
        {/* ==========================
            PRODUCT INFO
        ========================== */}

        <div className="flex flex-col">
          {/* Category */}
          <p className="mb-2 text-sm uppercase tracking-wide text-gray-500">
            {product.categoryName}
          </p>

          {/* Product Name */}
          <h1 className="text-4xl font-semibold">{product.productName}</h1>

          {/* Brand */}
          <p className="mt-2 text-lg text-gray-500">{product.brandName}</p>

          {/* Rating */}
          <div className="mt-5 flex items-center gap-3">
            <div className="flex items-center gap-1">
              <Star size={20} fill="currentColor" />

              <span className="font-medium">
                {Number(product.rating ?? 0).toFixed(1)}
              </span>
            </div>

            <span className="text-sm text-gray-400">Customer Rating</span>
          </div>

          {/* Description */}
          <p className="mt-6 leading-7 text-gray-600">{product.description}</p>

          {/* Details */}
          <div className="mt-6 space-y-2 text-sm">
            <p>
              <span className="font-semibold">Volume:</span> {product.volumeMl}{" "}
              ml
            </p>

            <p>
              <span className="font-semibold">Gender:</span> {product.gender}
            </p>
          </div>

          {/* Price */}
          <div className="mt-8 flex items-center gap-4">
            <span className="text-3xl font-semibold">
              ₹{discountedPrice.toFixed(2)}
            </span>

            {product.discount > 0 && (
              <>
                <span className="text-lg text-gray-400 line-through">
                  ₹{Number(product.price).toFixed(2)}
                </span>

                <span className="text-sm font-medium text-green-600">
                  {product.discount}% OFF
                </span>
              </>
            )}
          </div>

          {/* Quantity */}
          <div className="mt-8 flex items-center gap-4">
            <span className="font-medium">Quantity</span>

            <div className="flex items-center border">
              <button
                type="button"
                onClick={decreaseQuantity}
                className="p-3 hover:bg-gray-100"
              >
                <Minus size={16} />
              </button>

              <span className="w-12 text-center">{quantity}</span>

              <button
                type="button"
                onClick={increaseQuantity}
                className="p-3 hover:bg-gray-100"
              >
                <Plus size={16} />
              </button>
            </div>
          </div>

          {/* Buttons */}
          <div className="mt-8 flex items-center gap-3">
            <button
              type="button"
              onClick={() =>
                navigate("/checkout", {
                  state: {
                    productId: product.productId,
                    quantity,
                  },
                })
              }
              className="flex flex-1 items-center justify-center gap-2 rounded-lg bg-green-600 px-5 py-3 text-white font-medium hover:bg-green-700"
            >
              <ShoppingBag size={18} />
              Buy Now
            </button>

            <button
              type="button"
              onClick={async () => {
                try {
                  await addToCart(product.productId, quantity);
                  alert("Added to cart");
                } catch (err) {
                  console.log(err);
                  alert("Unable to add to cart");
                }
              }}
              className="flex flex-1 items-center justify-center gap-2 rounded-lg bg-black px-5 py-3 text-white font-medium hover:bg-gray-800"
            >
              <ShoppingCart size={18} />
              Add To Cart
            </button>

            <button
              type="button"
              onClick={async () => {
                try {
                  await addToWishlist(product.productId);
                  alert("Added to Wishlist");
                } catch (err) {
                  console.log(err);
                }
              }}
              className="rounded-full border p-3 hover:bg-gray-100"
            >
              <Heart size={18} />
            </button>
          </div>

          {/* Customer Reviews */}
          <div className="mt-10">
            <h2 className="mb-5 text-2xl font-semibold">Customer Reviews</h2>

            {reviews.length === 0 ? (
              <p className="text-gray-500">No reviews yet.</p>
            ) : (
              <div className="space-y-4">
                {reviews.map((review) => (
                  <div
                    key={review.reviewId}
                    className="rounded-lg border border-gray-200 p-4"
                  >
                    <div className="flex items-center justify-between">
                      <h3 className="font-semibold">{review.userName}</h3>

                      <span className="text-yellow-500">
                        {"⭐".repeat(review.rating)}
                      </span>
                    </div>

                    <p className="mt-3 text-gray-600">{review.reviewText}</p>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductDetails;
