function Availability({ includeOutOfStock, setIncludeOutOfStock }) {
  return (
    <div className="w-70 mt-4 h-fit rounded-lg border border-gray-50 bg-white p-6 shadow-2xl">
      <h2 className="mb-6 text-2xl font-semibold">AVAILABILITY</h2>

      <label className="flex items-center gap-2 cursor-pointer">
        <input
          type="checkbox"
          checked={includeOutOfStock}
          onChange={(e) => setIncludeOutOfStock(e.target.checked)}
          className="h-4 w-4 accent-blue-600"
        />

        <span className="text-base">Include Out Of Stock</span>
      </label>
    </div>
  );
}

export default Availability;
