import { Trash2 } from "lucide-react";
import { useNavigate } from "react-router-dom";

function WishlistItem({ item, onRemove }) {
  const navigate = useNavigate();

  const price = item.price - (item.price * item.discount) / 100;
  const handleProductClick = () => {
    navigate(`/product/${item.productId}`);
  };
  return (
    <div
      className="flex items-center gap-6 rounded-xl border p-5 shadow-sm cursor-pointer hover:shadow-md transition"
      onClick={handleProductClick}
    >
      <img
        src={item.images?.[0]?.imageUrl}
        alt={item.productName}
        className="h-28 w-28 rounded-lg object-cover"
      />

      <div className="flex-1">
        <h2 className="text-xl font-semibold">{item.productName}</h2>

        <div className="mt-2 flex items-center gap-3">
          <span className="text-2xl font-bold">₹{price.toFixed(0)}</span>

          {item.discount > 0 && (
            <>
              <span className="text-gray-400 line-through">₹{item.price}</span>

              <span className="text-green-600">{item.discount}% OFF</span>
            </>
          )}
        </div>
      </div>

      <button
        onClick={(e) => {
          e.stopPropagation(); // prevents opening product page
          onRemove(item.wishlistItemId);
        }}
        className="rounded-lg p-3 text-red-500 transition hover:bg-red-50"
      >
        <Trash2 />
      </button>
    </div>
  );
}

export default WishlistItem;
