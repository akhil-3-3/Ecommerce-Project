import { Minus, Plus, Trash2 } from "lucide-react";

const CartItem = ({ item, onIncrease, onDecrease, onRemove }) => {
  const price = item.price - (item.price * item.discount) / 100;

  return (
    <div className="flex items-center gap-6 rounded-xl border p-5">
      <img
        src={item.imageUrl}
        alt={item.productName}
        className="h-28 w-28 rounded-lg object-cover"
      />

      <div className="flex-1">
        <h2 className="text-xl font-semibold">{item.productName}</h2>

        <p className="mt-2 text-lg font-bold">₹{price.toFixed(0)}</p>

        <div className="mt-4 flex items-center gap-2">
          <button
            onClick={() => onDecrease(item)}
            className="rounded border p-2"
          >
            <Minus size={16} />
          </button>

          <span className="w-10 text-center">{item.quantity}</span>

          <button
            onClick={() => onIncrease(item)}
            className="rounded border p-2"
          >
            <Plus size={16} />
          </button>
        </div>
      </div>

      <button
        onClick={() => onRemove(item.cartItemId)}
        className="text-red-500"
      >
        <Trash2 />
      </button>
    </div>
  );
};

export default CartItem;
