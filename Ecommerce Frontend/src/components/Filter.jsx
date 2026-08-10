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
    let updatedFilters;

    if (selectedFilters.includes(categoryName)) {
      updatedFilters = selectedFilters.filter((item) => item !== categoryName);
    } else {
      updatedFilters = [...selectedFilters, categoryName];
    }

    setSelectedFilters(updatedFilters);

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
