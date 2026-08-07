import { useEffect, useState } from "react";
import CartItem from "../components/CartItem";
import { useNavigate } from "react-router-dom";
import {
  getCart,
  updateCartQuantity,
  removeCartItem,
  clearCart,
} from "../api/cartApi";

function Cart() {
  const [cart, setCart] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  useEffect(() => {
    loadCart();
  }, []);

  const loadCart = async () => {
    try {
      setLoading(true);
      const data = await getCart();
      setCart(data);
    } catch (err) {
      console.log(err);
    } finally {
      setLoading(false);
    }
  };

  const increase = async (item) => {
    try {
      await updateCartQuantity(item.cartItemId, item.quantity + 1);
      loadCart();
    } catch (err) {
      console.log(err);
    }
  };

  const decrease = async (item) => {
    if (item.quantity <= 1) return;

    try {
      await updateCartQuantity(item.cartItemId, item.quantity - 1);
      loadCart();
    } catch (err) {
      console.log(err);
    }
  };

  const remove = async (cartItemId) => {
    try {
      await removeCartItem(cartItemId);
      loadCart();
    } catch (err) {
      console.log(err);
    }
  };

  const clear = async () => {
    try {
      await clearCart();
      loadCart();
    } catch (err) {
      console.log(err);
    }
  };

  if (loading) {
    return <div className="py-20 text-center text-xl">Loading Cart...</div>;
  }

  if (cart.length === 0) {
    return (
      <div className="flex h-[70vh] flex-col items-center justify-center">
        <h1 className="text-4xl font-bold">Your Cart is Empty</h1>

        <p className="mt-3 text-gray-500">
          Add some fragrances to continue shopping.
        </p>
      </div>
    );
  }

  const subtotal = cart.reduce((sum, item) => {
    const price = item.price - (item.price * item.discount) / 100;
    return sum + price * item.quantity;
  }, 0);

  const totalDiscount = cart.reduce((sum, item) => {
    return sum + ((item.price * item.discount) / 100) * item.quantity;
  }, 0);

  const totalItems = cart.reduce((sum, item) => sum + item.quantity, 0);

  return (
    <div className="mx-auto max-w-7xl px-6 py-10">
      <h1 className="mb-10 text-4xl font-bold">Shopping Cart</h1>

      <div className="grid gap-10 lg:grid-cols-3">
        <div className="space-y-6 lg:col-span-2">
          {cart.map((item) => (
            <CartItem
              key={item.cartItemId}
              item={item}
              onIncrease={increase}
              onDecrease={decrease}
              onRemove={remove}
            />
          ))}
        </div>

        <div className="h-fit rounded-xl border p-6 shadow-sm">
          <h2 className="mb-6 text-2xl font-bold">Order Summary</h2>

          <div className="mb-3 flex justify-between">
            <span>Items</span>
            <span>{totalItems}</span>
          </div>

          <div className="mb-3 flex justify-between">
            <span>Discount</span>
            <span className="text-green-600">-₹{totalDiscount.toFixed(0)}</span>
          </div>

          <hr className="my-5" />

          <div className="mb-6 flex justify-between text-xl font-bold">
            <span>Total</span>
            <span>₹{subtotal.toFixed(0)}</span>
          </div>

          <button
            onClick={() => navigate("/checkout")}
            className="mb-3 w-full rounded-lg bg-black py-4 text-white hover:bg-gray-800"
          >
            Proceed To Checkout
          </button>

          <button
            onClick={clear}
            className="w-full rounded-lg border border-red-500 py-4 text-red-500 hover:bg-red-500 hover:text-white"
          >
            Clear Cart
          </button>
        </div>
      </div>
    </div>
  );
}

export default Cart;
