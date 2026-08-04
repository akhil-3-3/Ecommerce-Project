import { useEffect, useState } from "react";
import WishlistItem from "../components/WishlistItem";
import {
  getWishlist,
  removeWishlistItem,
  clearWishlist,
} from "../api/wishlistApi";

function Wishlist() {
  const [wishlist, setWishlist] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadWishlist();
  }, []);

  const loadWishlist = async () => {
    try {
      setLoading(true);

      const data = await getWishlist();

      setWishlist(data);
    } catch (err) {
      console.log(err);
    } finally {
      setLoading(false);
    }
  };

  const remove = async (wishlistItemId) => {
    try {
      await removeWishlistItem(wishlistItemId);
      loadWishlist();
    } catch (err) {
      console.log(err);
    }
  };

  const clear = async () => {
    try {
      await clearWishlist();
      loadWishlist();
    } catch (err) {
      console.log(err);
    }
  };

  if (loading) {
    return <div className="py-20 text-center text-xl">Loading Wishlist...</div>;
  }

  if (wishlist.length === 0) {
    return (
      <div className="flex h-[70vh] flex-col items-center justify-center">
        <h1 className="text-4xl font-bold">Your Wishlist is Empty</h1>

        <p className="mt-3 text-gray-500">
          Save your favourite fragrances here.
        </p>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-7xl px-6 py-10">
      <div className="mb-10 flex items-center justify-between">
        <h1 className="text-4xl font-bold">My Wishlist</h1>

        <button
          onClick={clear}
          className="rounded-lg border border-red-500 px-5 py-3 text-red-500 hover:bg-red-500 hover:text-white"
        >
          Clear Wishlist
        </button>
      </div>

      <div className="space-y-6">
        {wishlist.map((item) => (
          <WishlistItem
            key={item.wishlistItemId}
            item={item}
            onRemove={remove}
          />
        ))}
      </div>
    </div>
  );
}

export default Wishlist;
