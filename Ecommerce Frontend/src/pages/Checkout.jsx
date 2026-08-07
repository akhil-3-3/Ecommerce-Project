import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { getProductById } from "../api/productApi";
import { createOrder } from "../api/orderApi";
import { getCart } from "../api/cartApi";
function Checkout() {
  const location = useLocation();
  const navigate = useNavigate();

  const { productId, quantity } = location.state || {};

  const [product, setProduct] = useState(null);
  const [cartItems, setCartItems] = useState([]);
  const [loading, setLoading] = useState(true);

  const [address, setAddress] = useState("");
  const [paymentMethod, setPaymentMethod] = useState("COD");

  useEffect(() => {
    if (productId) {
      loadProduct();
    } else {
      loadCart();
    }
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
  const loadCart = async () => {
    try {
      const data = await getCart();

      if (data.length === 0) {
        navigate("/cart");
        return;
      }

      setCartItems(data);
    } catch (err) {
      console.log(err);
    } finally {
      setLoading(false);
    }
  };
  if (loading) {
    return (
      <div className="flex min-h-125 items-center justify-center">
        Loading...
      </div>
    );
  }

  if (productId && !product) {
    return (
      <div className="flex min-h-125 items-center justify-center">
        Product not found.
      </div>
    );
  }

  let discountedPrice = 0;
  let total = 0;

  if (productId) {
    discountedPrice = product.price - (product.price * product.discount) / 100;

    total = discountedPrice * quantity;
  } else {
    total = cartItems.reduce((sum, item) => {
      const price = item.price - (item.price * item.discount) / 100;

      return sum + price * item.quantity;
    }, 0);
  }

  const placeOrder = async () => {
    if (!address.trim()) {
      alert("Please enter your delivery address.");
      return;
    }

    try {
      const order = {
        shippingAddress: address,
        items: productId
          ? [
              {
                productId: product.productId,
                quantity,
                unitPrice: discountedPrice,
                discount: product.discount,
              },
            ]
          : cartItems.map((item) => ({
              productId: item.productId,
              quantity: item.quantity,
              unitPrice: item.price - (item.price * item.discount) / 100,
              discount: item.discount,
            })),
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

          {productId ? (
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
          ) : (
            <>
              {cartItems.map((item) => {
                const price = item.price - (item.price * item.discount) / 100;

                return (
                  <div
                    key={item.cartItemId}
                    className="mb-4 flex items-center gap-4 border-b pb-4"
                  >
                    <img
                      src={item.imageUrl}
                      alt={item.productName}
                      className="h-20 w-20 rounded object-contain"
                    />

                    <div className="flex-1">
                      <h3 className="font-semibold">{item.productName}</h3>

                      <p>Qty : {item.quantity}</p>

                      <p>₹{price.toFixed(2)}</p>
                    </div>
                  </div>
                );
              })}

              <div className="mt-4 text-right text-2xl font-bold">
                Total : ₹{total.toFixed(2)}
              </div>
            </>
          )}
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
