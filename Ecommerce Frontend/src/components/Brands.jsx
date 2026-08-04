import { useState } from "react";

const Brands = ({ onBrandChange }) => {
  const [selectedBrands, setSelectedBrands] = useState([]);

  const brands = [
    "Creed",
    "Dior",
    "Tom Ford",
    "Guerlain",
    "Jo Malone",
    "Hermès",
    "Burberry",
    "Bvlgari",
  ];

  const handleChange = (brand) => {
    const updatedBrands = selectedBrands.includes(brand)
      ? selectedBrands.filter((item) => item !== brand)
      : [...selectedBrands, brand];

    setSelectedBrands(updatedBrands);

    if (onBrandChange) {
      onBrandChange(updatedBrands);
    }
  };

  return (
    <div className="w-70 rounded-lg border bg-white p-6 space-y-3 text-sm h-fit mt-4 shadow-2xl border-gray-50">
      <h2 className="mb-6 text-2xl font-semibold">Brands</h2>

      {brands.map((brand) => (
        <label key={brand} className="flex items-center gap-2 cursor-pointer">
          <input
            type="checkbox"
            checked={selectedBrands.includes(brand)}
            onChange={() => handleChange(brand)}
          />

          {brand}
        </label>
      ))}

      <button type="button" className="mt-2 text-sm font-medium">
        View More +
      </button>
    </div>
  );
};

export default Brands;
