import React from "react";
import Filter from "./Filter";
import Rating from "./Rating";
import Availability from "./Availability";
import Brands from "./Brands";

const Sidebar = ({
  onFilterChange,
  onRatingChange,
  onBrandChange,
  includeOutOfStock,
  setIncludeOutOfStock,
}) => {
  return (
    <div>
      <Filter onFilterChange={onFilterChange} />

      <Rating onRatingChange={onRatingChange} />

      <Availability
        includeOutOfStock={includeOutOfStock}
        setIncludeOutOfStock={setIncludeOutOfStock}
      />

      <Brands onBrandChange={onBrandChange} />
    </div>
  );
};

export default Sidebar;
