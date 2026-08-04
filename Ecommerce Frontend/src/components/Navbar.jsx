import { useEffect, useState } from "react";
import {
  Search,
  User,
  Heart,
  ShoppingCart,
  ChevronDown,
  LogOut,
} from "lucide-react";
import { Link, useNavigate } from "react-router-dom";

import logo from "../assets/logo.svg";
import { authApi } from "../api/authApi";
import { getCart } from "../api/cartApi";
import { getWishlist } from "../api/wishlistApi";

function Navbar({ search, setSearch }) {
  const [userName, setUserName] = useState(null);
  const [cartCount, setCartCount] = useState(0);
  const [wishlistCount, setWishlistCount] = useState(0);

  const navigate = useNavigate();

  useEffect(() => {
    loadUser();
    loadCartCount();
    loadWishlistCount();
  }, []);

  const loadUser = async () => {
    try {
      const user = await authApi.getCurrentUser();
      setUserName(user.userName);
    } catch (error) {
      setUserName(null);
    }
  };

  const loadCartCount = async () => {
    try {
      const cart = await getCart();

      // Total quantity in cart
      const total = cart.reduce((sum, item) => sum + item.quantity, 0);

      setCartCount(total);

      // If you want only different products instead:
      // setCartCount(cart.length);
    } catch (error) {
      console.error(error);
      setCartCount(0);
    }
  };

  const loadWishlistCount = async () => {
    try {
      const wishlist = await getWishlist();
      setWishlistCount(wishlist.length);
    } catch (error) {
      console.error(error);
      setWishlistCount(0);
    }
  };

  const handleLogout = async () => {
    try {
      await authApi.logout();
      setUserName(null);
      window.location.href = "/login";
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <header className="border-b bg-[#f8f8f8]">
      <div className="mx-auto flex max-w-[1700px] items-center px-6 py-4">
        {/* Logo */}
        <div className="shrink-0">
          <img src={logo} alt="Logo" className="h-14 w-auto" />
        </div>

        {/* Center */}
        <div className="mx-6 flex-1 rounded-lg border bg-white px-3 py-2">
          {/* Search */}
          <div className="flex items-center rounded-md border px-3 py-2">
            <Search size={18} className="text-gray-500" />

            <input
              type="text"
              placeholder="Search for Perfumes"
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="ml-2 w-full text-sm outline-none"
            />
          </div>

          {/* Navigation */}
          <nav className="mt-2 flex items-center gap-7 px-2 text-sm">
            <div className="flex cursor-pointer items-center gap-1">
              For Him
              <ChevronDown size={14} />
            </div>

            <div className="flex cursor-pointer items-center gap-1">
              For Her
              <ChevronDown size={14} />
            </div>

            <div className="flex cursor-pointer items-center gap-1">
              Products
              <ChevronDown size={14} />
            </div>

            <a href="#">Story</a>
            <a href="#">About</a>
            <a href="#">Help</a>
          </nav>
        </div>

        {/* Right */}
        <div className="flex shrink-0 items-center gap-4 text-sm">
          {/* User */}
          <div className="flex items-center gap-2 border-r pr-4">
            <User size={18} />
            <span className="font-medium">{userName || "Guest"}</span>
          </div>

          {/* Wishlist */}
          <div className="border-r pr-4">
            <Link to="/wishlist" className="relative">
              <Heart size={22} />

              {wishlistCount > 0 && (
                <span className="absolute -right-2 -top-2 flex h-4 w-4 items-center justify-center rounded-full bg-red-600 text-xs font-bold text-white">
                  {wishlistCount}
                </span>
              )}
            </Link>
          </div>

          {/* Cart */}
          <div className="border-r pr-4">
            <div
              className="relative cursor-pointer"
              onClick={() => navigate("/cart")}
            >
              <ShoppingCart size={22} />

              {cartCount > 0 && (
                <span className="absolute -right-2 -top-2 flex h-4 w-4 items-center justify-center rounded-full bg-black text-xs font-bold text-white">
                  {cartCount}
                </span>
              )}
            </div>
          </div>

          {/* Logout */}
          {userName && (
            <button
              type="button"
              onClick={handleLogout}
              className="flex items-center gap-1 rounded-md px-2 py-1 text-red-600 transition hover:bg-red-50"
            >
              <LogOut size={18} />
              Logout
            </button>
          )}
        </div>
      </div>
    </header>
  );
}

export default Navbar;
