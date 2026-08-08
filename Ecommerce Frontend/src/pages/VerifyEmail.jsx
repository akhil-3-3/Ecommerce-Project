import React, { useState } from "react";
import { authApi } from "../api/authApi";
import { useLocation, useNavigate } from "react-router-dom";
import logo from "../assets/logo.svg";

function VerifyEmail() {
  const location = useLocation();
  const navigate = useNavigate();

  const [code, setCode] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const email = location.state?.email;

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");

    if (!email) {
      setError("Email information is missing. Please register again.");
      return;
    }

    if (code.length !== 6) {
      setError("Please enter the 6-digit verification code.");
      return;
    }

    setLoading(true);

    try {
      await authApi.verifyEmail(email, code);

      alert("Email verified successfully. Please login.");

      navigate("/login");
    } catch (err) {
      setError(
        err.response?.data?.message ||
          err.response?.data ||
          "Invalid or expired verification code.",
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-50 px-4">
      <div className="w-full max-w-md rounded-2xl bg-white p-8 shadow-lg">
        {/* Logo */}
        <div className="flex flex-col items-center">
          <img src={logo} alt="Logo" className="h-16" />

          <h1 className="mt-5 text-3xl font-semibold">Verify Your Email</h1>

          <p className="mt-2 text-center text-gray-500">
            Enter the verification code sent to
          </p>

          <p className="mt-1 text-center font-medium text-gray-800">
            {email || "your email address"}
          </p>
        </div>

        {/* Verification Form */}
        <form onSubmit={handleSubmit} className="mt-8 space-y-5">
          <input
            type="text"
            inputMode="numeric"
            maxLength={6}
            placeholder="Enter 6-digit code"
            value={code}
            onChange={(e) => {
              const value = e.target.value.replace(/\D/g, "");

              setCode(value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 text-center text-xl tracking-[0.5em] outline-none focus:border-black"
          />

          {/* Error */}
          {error && (
            <div className="rounded-lg bg-red-100 p-3 text-center text-sm text-red-600">
              {error}
            </div>
          )}

          {/* Verify */}
          <button
            type="submit"
            disabled={loading}
            className="w-full rounded-xl bg-black py-3 text-white transition hover:bg-gray-800 disabled:opacity-50"
          >
            {loading ? "Verifying..." : "Verify Email"}
          </button>
        </form>

        {/* Back to Login */}
        <p className="mt-8 text-center text-sm text-gray-600">
          Already have an account?{" "}
          <button
            type="button"
            onClick={() => navigate("/login")}
            className="font-semibold text-black hover:underline"
          >
            Login
          </button>
        </p>
      </div>
    </div>
  );
}

export default VerifyEmail;
