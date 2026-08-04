import React, { useState } from "react";
import { authApi } from "../api/authApi";
import { useNavigate } from "react-router-dom";
import logo from "../assets/logo.svg";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    setLoading(true);
    setError("");

    try {
      await authApi.login(email, password);
      navigate("/home");
    } catch (err) {
      setError(err.response?.data?.message || "Invalid email or password.");
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
    <div className="flex min-h-screen items-center justify-center bg-[#f8f8f8] px-4 font-actor">
      <div className="w-full max-w-md rounded-3xl bg-white p-10 shadow-xl">
        {/* Logo */}
        <div className="mb-8 flex flex-col items-center">
          <img src={logo} alt="Logo" className="h-16" />

          <h1 className="mt-5 text-3xl font-semibold">Welcome Back</h1>

          <p className="mt-2 text-center text-gray-500">
            Sign in to continue shopping
          </p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-5">
          <input
            type="email"
            placeholder="Email Address"
            value={email}
            onChange={(e) => {
              setEmail(e.target.value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none focus:border-black"
          />

          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => {
              setPassword(e.target.value);
              setError("");
            }}
            className="w-full rounded-xl border border-gray-300 px-4 py-3 outline-none focus:border-black"
          />

          {error && (
            <div className="rounded-lg bg-red-100 p-3 text-center text-sm text-red-600">
              {error}
            </div>
          )}

          <button
            type="submit"
            disabled={loading}
            className="w-full rounded-xl bg-black py-3 text-white transition hover:bg-gray-800"
          >
            {loading ? "Signing In..." : "Sign In"}
          </button>
        </form>

        <div className="my-6 flex items-center">
          <div className="h-px flex-1 bg-gray-300"></div>
          <span className="mx-4 text-sm text-gray-500">OR</span>
          <div className="h-px flex-1 bg-gray-300"></div>
        </div>

        <div className="space-y-3">
          <button
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

        <p className="mt-8 text-center text-sm text-gray-600">
          Don't have an account?{" "}
          <button
            onClick={() => navigate("/register")}
            className="font-semibold text-black hover:underline"
          >
            Register
          </button>
        </p>
      </div>
    </div>
  );
}

export default Login;
