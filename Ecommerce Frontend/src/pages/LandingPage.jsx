import React from "react";
import logo from "../assets/logo.svg";
import { useNavigate } from "react-router-dom";
import bg3 from "../assets/bg3.avif";

const LandingPage = () => {
  const navigate = useNavigate();

  return (
    <div
      className="relative min-h-screen max-w-full bg-cover bg-center bg-no-repeat font-mont "
      style={{ backgroundImage: `url(${bg3})` }}
    >
      {/* Dark overlay */}
      <div className="absolute inset-0  bg-black/4c0 "></div>

      {/* Content */}
      <div className=" relative z-10 flex min-h-screen flex-col items-center justify-center text-center text-white">
        <img src={logo} alt="Logo" className="mb-6 h-20" />

        <h1 className="text-5xl font-semibold text-shadow-2xs">
          Welcome to Our Store
        </h1>

        <p className="mt-4 text-lg">Discover your perfect fragrance</p>

        <div className="mt-6 flex gap-4">
          <button
            onClick={() => navigate("/login")}
            className="rounded-md bg-black px-8 py-3 font-medium text-white transition hover:bg-gray-800"
          >
            Login
          </button>

          <button
            onClick={() => navigate("/register")}
            className="rounded-md bg-white px-8 py-3 font-medium text-black transition hover:bg-gray-200"
          >
            Register
          </button>
        </div>
      </div>
    </div>
  );
};

export default LandingPage;
