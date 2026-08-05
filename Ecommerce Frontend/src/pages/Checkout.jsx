import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { getProductById } from "../api/productApi";
import { createOrder } from "../api/orderApi";

function Checkout() {
  const location = useLocation();
  const navigate = useNavigate();

  const { productId, quantity } = location.state || {};

  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);

  const [address, setAddress] = useState("");
  const [paymentMethod, setPaymentMethod] = useState("COD");

  useEffect(() => {
    if (!productId) {
      navigate("/");
      return;
    }

    loadProduct();
  }, []);

  const loadProduct = async () => {
    try {
      const data = await getProductById(productId);
      setProduct(data);
    } catch (err) {
      console.log(err);
      alert("Unable to load product.");
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="flex min-h-[500px] items-center justify-center">
        Loading...
      </div>
    );
  }

  if (!product) {
    return (
      <div className="flex min-h-[500px] items-center justify-center">
        Product not found.
      </div>
    );
  }

  const discountedPrice =
    product.price - (product.price * product.discount) / 100;

  const total = discountedPrice * quantity;

  const placeOrder = async () => {
    if (!address.trim()) {
      alert("Please enter your delivery address.");
      return;
    }

    try {
      const order = {
        shippingAddress: address,
        items: [
          {
            productId: product.productId,
            quantity: quantity,
            unitPrice: discountedPrice,
            discount: product.discount,
          },
        ],
      };
      const response = await createOrder(order);

      alert(response.message);

      navigate(`/order-success/${response.orderId}`);
    } catch (err) {
      console.log(err);
      alert("Failed to place order.");
    }
  };
  return (
    <div className="mx-auto max-w-6xl px-6 py-10">
      <h1 className="mb-10 text-4xl font-bold">Checkout</h1>

      <div className="grid grid-cols-1 gap-10 lg:grid-cols-2">
        {/* Product */}

        <div className="rounded-xl border p-6">
          <h2 className="mb-6 text-2xl font-semibold">Order Summary</h2>

          <div className="flex gap-6">
            <img
              src={product.images?.[0]?.imageUrl}
              alt={product.productName}
              className="h-36 w-36 rounded-lg object-contain"
            />

            <div className="flex-1">
              <h3 className="text-xl font-semibold">{product.productName}</h3>

              <p className="mt-1 text-gray-500">{product.brandName}</p>

              <p className="mt-3">
                Quantity : <strong>{quantity}</strong>
              </p>

              <p className="mt-2">
                Price :
                <span className="ml-2 font-semibold text-green-700">
                  ₹{discountedPrice.toFixed(2)}
                </span>
              </p>

              <p className="mt-4 text-2xl font-bold">
                Total : ₹{total.toFixed(2)}
              </p>
            </div>
          </div>
        </div>

        {/* Checkout */}

        <div className="rounded-xl border p-6">
          <h2 className="mb-6 text-2xl font-semibold">Delivery Details</h2>

          <label className="mb-2 block font-medium">Delivery Address</label>

          <textarea
            rows={5}
            value={address}
            onChange={(e) => setAddress(e.target.value)}
            className="w-full rounded-lg border p-3 outline-none focus:border-black"
            placeholder="Enter your complete delivery address"
          />

          <label className="mt-6 mb-2 block font-medium">Payment Method</label>

          <select
            value={paymentMethod}
            onChange={(e) => setPaymentMethod(e.target.value)}
            className="w-full rounded-lg border p-3"
          >
            <option value="COD">Cash On Delivery</option>

            <option value="Online">Online Payment</option>
          </select>

          <button
            onClick={placeOrder}
            className="mt-8 w-full rounded-lg bg-black py-4 text-white transition hover:bg-gray-800"
          >
            Place Order
          </button>
        </div>
      </div>
    </div>
  );
}

export default Checkout;
