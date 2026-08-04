import React from "react";

const Availability = () => {
  return (
    <div className="w-70 rounded-lg border bg-white p-6 space-y-3 text-sm h-fit mt-4 shadow-2xl border-gray-50">
      {/* Header */}
      <h2 className="mb-6 text-2xl font-semibold">AVAILABILITY</h2>

      <label className="flex items-center gap-2">
        <input type="checkbox" />
        <span className="text-base">Include out of Stock(16)</span>
      </label>
    </div>
  );
};

export default Availability;
