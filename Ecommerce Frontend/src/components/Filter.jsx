import { useEffect, useState } from "react";
import { getCategories } from "../api/categoryApi";

function Filter({ onFilterChange }) {
  const [categories, setCategories] = useState([]);
  const [selectedFilters, setSelectedFilters] = useState([]);

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    try {
      const data = await getCategories();
      setCategories(data);
    } catch (err) {
      console.log(err);
    }
  };

  const handleChange = (categoryName) => {
    const updatedFilters = selectedFilters.includes(categoryName)
      ? selectedFilters.filter((item) => item !== categoryName)
      : [...selectedFilters, categoryName];

    setSelectedFilters(updatedFilters);

    if (onFilterChange) {
      onFilterChange(updatedFilters);
    }
  };

  return (
    <div className="w-70 mt-4 h-fit space-y-3 rounded-lg border border-gray-50 bg-white p-6 text-sm shadow-2xl">
      <h2 className="mb-6 text-2xl font-semibold">FRAGRANCE</h2>

      {categories.map((category) => (
        <label
          key={category.categoryId}
          className="flex cursor-pointer items-center gap-2"
        >
          <input
            type="checkbox"
            checked={selectedFilters.includes(category.categoryName)}
            onChange={() => handleChange(category.categoryName)}
          />

          <span>{category.categoryName}</span>
        </label>
      ))}
    </div>
  );
}

export default Filter;
