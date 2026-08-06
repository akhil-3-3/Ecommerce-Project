import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getMyOrders } from "../api/orderApi";
import { cancelOrder } from "../api/orderApi";

function Orders() {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);

  const navigate = useNavigate();

  useEffect(() => {
    loadOrders();
  }, []);

  const loadOrders = async () => {
    try {
      const data = await getMyOrders();
      setOrders(data);
    } catch (err) {
      console.log(err);
      alert("Unable to load orders.");
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
  const handleCancelOrder = async (orderId) => {
    const confirmCancel = window.confirm(
      "Are you sure you want to cancel this order?",
    );

    if (!confirmCancel) return;

    try {
      await cancelOrder(orderId);

      alert("Order cancelled successfully.");

      loadOrders();
    } catch (err) {
      console.log(err);
      alert("Unable to cancel order.");
    }
  };
  return (
    <div className="mx-auto max-w-5xl px-6 py-10">
      <h1 className="mb-8 text-4xl font-bold">My Orders</h1>

      {orders.length === 0 ? (
        <p>You have not placed any orders yet.</p>
      ) : (
        <div className="space-y-5">
          {orders.map((order) => (
            <div
              key={order.orderId}
              className="rounded-xl border p-6 shadow-sm"
            >
              <div className="flex items-center justify-between">
                <div>
                  <h2 className="text-xl font-semibold">
                    Order #{order.orderId}
                  </h2>

                  <p className="text-gray-500">
                    {new Date(order.orderDate).toLocaleDateString()}
                  </p>
                </div>

                <span className="rounded bg-yellow-100 px-3 py-1 text-sm font-medium text-yellow-700">
                  {order.status}
                </span>
              </div>

              <div className="mt-5 flex items-center justify-between">
                <p className="text-2xl font-bold">
                  ₹{Number(order.totalAmount).toFixed(2)}
                </p>

                <div className="flex gap-3">
                  <button
                    onClick={() => navigate(`/orders/${order.orderId}`)}
                    className="rounded-lg bg-black px-5 py-2 text-white hover:bg-gray-800"
                  >
                    View Details
                  </button>

                  {order.status === "Pending" && (
                    <button
                      onClick={() => handleCancelOrder(order.orderId)}
                      className="rounded-lg bg-red-600 px-5 py-2 text-white hover:bg-red-700"
                    >
                      Cancel Order
                    </button>
                  )}
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default Orders;
