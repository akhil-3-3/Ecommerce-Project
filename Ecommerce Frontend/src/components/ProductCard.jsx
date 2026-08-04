import { Heart, Star } from "lucide-react";
import perfume from "../assets/perfume.svg";
import money from "../assets/money.svg";
import { getReviews } from "../api/reviewApi";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { addToCart } from "../api/cartApi";

function ProductCard({ product }) {
  const [reviews, setReviews] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    loadReviews();
  }, []);

  const loadReviews = async () => {
    try {
      const data = await getReviews();

      const map_obj = new Map();
      const map_obj2 = new Map();

      for (let i = 0; i < data.length; i++) {
        const { productId, rating } = data[i];

        if (!map_obj.has(productId)) {
          map_obj.set(productId, rating);
          continue;
        }

        map_obj.set(productId, map_obj.get(productId) + rating);
      }

      for (let i = 0; i < data.length; i++) {
        const { productId } = data[i];

        if (!map_obj2.has(productId)) {
          map_obj2.set(productId, 1);
          continue;
        }

        map_obj2.set(productId, map_obj2.get(productId) + 1);
      }

      setReviews(
        data.map((d) => {
          return {
            ...d,
            totalrating: map_obj2.get(d.productId),
            avg_rating: Math.round(
              map_obj.get(d.productId) / map_obj2.get(d.productId),
            ),
          };
        }),
      );
    } catch (err) {
      console.log(err);
    }
  };

  const review = reviews.find((r) => r.productId === product.productId);

  const handleProductClick = () => {
    navigate(`/product/${product.productId}`);
  };

  return (
    <div className="rounded-3xl border border-gray-300 bg-white p-3 font.mont transition hover:shadow-lg">
      {/* Clickable Product Area */}
      <div onClick={handleProductClick} className="cursor-pointer">
        {/* Image */}
        <div className="relative overflow-hidden rounded-3xl border border-gray-200 bg-gray-100">
          <img
            src={product.images?.[0]?.imageUrl || perfume}
            alt={product.productName}
            className="mx-auto h-64 w-full object-cover"
          />

          {/* Heart should NOT trigger card navigation */}
          <button
            type="button"
            onClick={(e) => {
              e.stopPropagation();
            }}
            className="absolute right-3 top-3 rounded-full bg-white p-2 shadow-md"
          >
            <Heart size={18} />
          </button>
        </div>

        {/* Rating */}
        <div className="mt-4 flex items-center gap-1">
          {[...new Array(review?.avg_rating || 0)].map((_, index) => (
            <Star
              key={index}
              size={16}
              className="fill-yellow-400 text-yellow-400"
            />
          ))}

          <span className="ml-1 text-sm text-gray-500">
            ({review?.totalrating || 0})
          </span>
        </div>

        {/* Brand */}
        <p className="mt-3 text-sm uppercase tracking-wide text-gray-500">
          {product?.brandName || "DIOR"}
        </p>

        {/* Name */}
        <h3 className="mt-1 text-base font-medium text-gray-900">
          {product?.productName || "Dior Sauvage Eau De Parfum"}
        </h3>

        {/* Price */}
        <div className="mt-3 flex items-center gap-3">
          <div className="flex items-center gap-1 text-gray-400 line-through">
            <img src={money} alt="" className="h-4 w-4" />
            <span>{product.price}</span>
          </div>

          <div className="flex items-center gap-1 font-semibold text-green-600">
            <img src={money} alt="" className="h-4 w-4" />

            <span>
              {Math.round(
                product.price - (product.price * product.discount) / 100,
              )}
            </span>
          </div>
        </div>
      </div>

      {/* Add To Cart stays outside clickable area */}
      <button
        type="button"
        onClick={async (e) => {
          e.stopPropagation();

          try {
            await addToCart(product.productId, 1);

            alert("Added to cart");
          } catch (err) {
            console.error(err);
            alert("Unable to add to cart");
          }
        }}
        className="mt-5 w-full rounded-full border border-gray-400 py-3 font-medium transition hover:bg-black hover:text-white"
      >
        Add to Cart
      </button>
    </div>
  );
}

export default ProductCard;
