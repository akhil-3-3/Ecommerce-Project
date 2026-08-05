import { CheckCircle } from "lucide-react";
import { useNavigate, useParams } from "react-router-dom";

function OrderSuccess() {
  const navigate = useNavigate();
  const { orderId } = useParams();

  return (
    <div className="flex min-h-[80vh] items-center justify-center px-6">
      <div className="w-full max-w-xl rounded-2xl border bg-white p-10 text-center shadow-lg">
        <CheckCircle size={90} className="mx-auto mb-6 text-green-600" />

        <h1 className="text-4xl font-bold">Order Placed Successfully!</h1>

        <p className="mt-4 text-gray-600">Thank you for shopping with us.</p>

        <p className="mt-3 text-lg">
          <span className="font-semibold">Order ID :</span> #{orderId}
        </p>

        <p className="mt-2 text-gray-500">
          Your order is currently
          <span className="ml-1 font-semibold text-orange-600">Pending</span>.
        </p>

        <div className="mt-10 flex flex-col gap-4 sm:flex-row">
          <button
            onClick={() => navigate("/products")}
            className="flex-1 rounded-lg bg-black py-3 text-white transition hover:bg-gray-800"
          >
            Continue Shopping
          </button>

          <button
            onClick={() => navigate("/orders")}
            className="flex-1 rounded-lg border py-3 transition hover:bg-gray-100"
          >
            My Orders
          </button>
        </div>
      </div>
    </div>
  );
}

export default OrderSuccess;
