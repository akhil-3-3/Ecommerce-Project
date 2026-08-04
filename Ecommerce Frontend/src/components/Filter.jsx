import { useState } from "react";

function Filter({ onFilterChange }) {
  const [selectedFilters, setSelectedFilters] = useState([]);

  const filters = [
    "Niche Perfumes",
    "Indian Perfumes",
    "Oudh And Bhakdoors",
    "Oils",
    "Body Mist",
    "Hair Mist",
    "Home Frgrance",
    "Gift Set",
    "Best Sellers",
    "Newest Arrival",
  ];

  const handleChange = (filter) => {
    const updatedFilters = selectedFilters.includes(filter)
      ? selectedFilters.filter((item) => item !== filter)
      : [...selectedFilters, filter];

    setSelectedFilters(updatedFilters);

    if (onFilterChange) {
      onFilterChange(updatedFilters);
    }
  };

  return (
    <div className="w-70 rounded-lg border bg-white p-6 space-y-3 text-sm h-fit mt-4 shadow-2xl border-gray-50">
      <h2 className="mb-6 text-2xl font-semibold">FRAGRANCE</h2>

      {filters.map((filter) => (
        <label key={filter} className="flex items-center gap-2 cursor-pointer">
          <input
            type="checkbox"
            checked={selectedFilters.includes(filter)}
            onChange={() => handleChange(filter)}
          />

          <span>{filter}</span>
        </label>
      ))}
    </div>
  );
}

export default Filter;
