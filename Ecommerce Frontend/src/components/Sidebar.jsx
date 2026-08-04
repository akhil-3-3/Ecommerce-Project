import React from "react";
import Filter from "./Filter";
import Rating from "./Rating";
import Availability from "./Availability";
import Brands from "./Brands";

const Sidebar = ({ onFilterChange, onRatingChange, onBrandChange }) => {
  return (
    <div>
      <Filter onFilterChange={onFilterChange} />

      <Rating onRatingChange={onRatingChange} />

      <Availability />

      <Brands onBrandChange={onBrandChange} />
    </div>
  );
};

export default Sidebar;
