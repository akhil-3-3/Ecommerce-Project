import { useEffect, useState } from "react";
import { getCategories } from "../api/categoryApi";

function Filter({ onFilterChange }) {
  const [categories, setCategories] = useState([]);

  // Restore previously selected filters
  const [selectedFilters, setSelectedFilters] = useState(() => {
    const savedFilters = sessionStorage.getItem("selectedFilters");

    return savedFilters ? JSON.parse(savedFilters) : [];
  });

  useEffect(() => {
    loadCategories();
  }, []);

  // Tell Products.jsx about the restored filters
  useEffect(() => {
    if (onFilterChange) {
      onFilterChange(selectedFilters);
    }
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
    let updatedFilters;

    if (selectedFilters.includes(categoryName)) {
      updatedFilters = selectedFilters.filter((item) => item !== categoryName);
    } else {
      updatedFilters = [...selectedFilters, categoryName];
    }

    // Update React state
    setSelectedFilters(updatedFilters);

    // Save filters
    sessionStorage.setItem("selectedFilters", JSON.stringify(updatedFilters));

    // Send filters to Products.jsx
    if (onFilterChange) {
      onFilterChange(updatedFilters);
    }
  };

  return (
    <div>
      <h3 className="mb-4 font-semibold">FRAGRANCE</h3>

      {categories.map((category) => (
        <label
          key={category.categoryId}
          className="mb-3 flex cursor-pointer items-center gap-2"
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
