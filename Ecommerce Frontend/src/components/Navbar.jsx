import { useEffect, useState } from "react";
import {
  Search,
  User,
  Heart,
  ShoppingCart,
  ChevronDown,
  LogOut,
} from "lucide-react";

import logo from "../assets/logo.svg";
import { authApi } from "../api/authApi";
import { useNavigate } from "react-router-dom";

function Navbar({ search, setSearch }) {
  const [userName, setUserName] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const loadUser = async () => {
      try {
        const user = await authApi.getCurrentUser();
        setUserName(user.userName);
      } catch (error) {
        setUserName(null);
      }
    };

    loadUser();
  }, []);

  const handleLogout = async () => {
    try {
      await authApi.logout();

      setUserName(null);

      // Send user to login page
      window.location.href = "/login";
    } catch (error) {
      console.error("Logout failed:", error);
    }
  };

  return (
    <header className="border-b bg-[#f8f8f8] font.mont">
      <div className="mx-auto flex max-w-350 items-center px-6 py-4 font.mont">
        {/* Logo */}
        <div className="shrink-0">
          <img src={logo} alt="Logo" className="h-14 w-auto" />
        </div>

        {/* Center */}
        <div className="mx-6 flex-1 rounded-lg border bg-white px-3 py-2 font.mont">
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
          <nav className="mt-2 flex items-center gap-7 px-2 text-sm font.mont">
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
            <Heart size={20} />
          </div>

          {/* Cart */}
          <div className="border-r pr-4">
            <ShoppingCart size={20} onClick={() => navigate("/cart")} />
          </div>

          {/* Logout */}
          {userName && (
            <button
              type="button"
              onClick={handleLogout}
              className="flex items-center gap-1 rounded-md px-2 py-1 text-sm text-red-600 transition hover:bg-red-50"
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
