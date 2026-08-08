import React, { useState } from "react";
import { authApi } from "../api/authApi";
import { useNavigate } from "react-router-dom";
import logo from "../assets/logo.svg";

function Register() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    setError("");

    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    setLoading(true);

    try {
      await authApi.register(username, email, password);

      // Registration is NOT complete yet.
      // User must verify the email first.
      navigate("/verify-email", {
        state: {
          email: email,
        },
      });
    } catch (err) {
      setError(
        err.response?.data?.message ||
          err.response?.data ||
          "Unable to create account. Please try again.",
      );
    } finally {
      setLoading(false);
    }
  };

  const handleGoogleLogin = () => {
    authApi.googleLogin();
  };

  const handleFacebookLogin = () => {
    authApi.facebookLogin();
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-50 px-4">
      <div className="w-full max-w-md rounded-2xl bg-white p-8 shadow-lg">
        {/* Logo */}
        <div className="flex flex-col items-center">
          <img src={logo} alt="Logo" className="h-16" />

          <h1 className="mt-5 text-3xl font-semibold">Create Account</h1>

          <p className="mt-2 text-center text-gray-500">
            Sign up to start shopping
          </p>
        </div>

        {/* Register Form */}
        <form onSubmit={handleSubmit} className="mt-8 space-y-5">
          {/* Username */}
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => {
              setUsername(e.target.value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none focus:border-black"
            required
          />

          {/* Email */}
          <input
            type="email"
            placeholder="Email Address"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none focus:border-black"
            required
          />

          {/* Password */}
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none focus:border-black"
            required
          />

          {/* Confirm Password */}
          <input
            type="password"
            placeholder="Confirm Password"
            value={confirmPassword}
            onChange={(e) => {
              setConfirmPassword(e.target.value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none focus:border-black"
            required
          />

          {/* Error */}
          {error && (
            <div className="rounded-lg bg-red-100 p-3 text-center text-sm text-red-600">
              {error}
            </div>
          )}

          {/* Register */}
          <button
            type="submit"
            disabled={loading}
            className="w-full rounded-xl bg-black py-3 text-white transition hover:bg-gray-800 disabled:opacity-50"
          >
            {loading ? "Sending Verification Code..." : "Create Account"}
          </button>
        </form>

        {/* Divider */}
        <div className="my-6 flex items-center">
          <div className="h-px flex-1 bg-gray-300"></div>

          <span className="mx-4 text-sm text-gray-500">OR</span>

          <div className="h-px flex-1 bg-gray-300"></div>
        </div>

        {/* Google / Facebook */}
        <div className="space-y-3">
          <button
            type="button"
            onClick={handleGoogleLogin}
            className="flex w-full items-center justify-center gap-3 rounded-xl border border-gray-300 py-3 transition hover:bg-gray-100"
          >
            <img
              src="https://www.svgrepo.com/show/475656/google-color.svg"
              alt="Google"
              className="h-5"
            />
            Continue with Google
          </button>

          <button
            type="button"
            onClick={handleFacebookLogin}
            className="flex w-full items-center justify-center gap-3 rounded-xl border border-gray-300 py-3 transition hover:bg-gray-100"
          >
            <img
              src="https://upload.wikimedia.org/wikipedia/commons/5/51/Facebook_f_logo_%282019%29.svg"
              alt="Facebook"
              className="h-5"
            />
            Continue with Facebook
          </button>
        </div>

        {/* Login */}
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

export default Register;
