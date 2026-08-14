import { useState } from "react";
import star from "../assets/star.svg";
import dullstar from "../assets/dullstar.svg";

const Rating = ({ onRatingChange }) => {
  // Restore selected rating when coming back to Products page
  const [selectedRating, setSelectedRating] = useState(() => {
    const savedRating = sessionStorage.getItem("selectedRating");

    return savedRating ? Number(savedRating) : null;
  });

  const ratings = [5, 4, 3, 2, 1];

  const handleChange = (rating) => {
    const newRating = selectedRating === rating ? null : rating;

    // Update React state
    setSelectedRating(newRating);

    // Save / remove from sessionStorage
    if (newRating === null) {
      sessionStorage.removeItem("selectedRating");
    } else {
      sessionStorage.setItem("selectedRating", newRating);
    }

    // Send to Products.jsx
    if (onRatingChange) {
      onRatingChange(newRating);
    }
  };

  return (
    <div className="w-70 rounded-lg border bg-white p-6 space-y-3 text-sm h-fit mt-4 shadow-2xl border-gray-50">
      <h2 className="mb-6 text-2xl font-semibold">FILTER BY RATING</h2>

      {ratings.map((rating) => (
        <label key={rating} className="flex items-center gap-2 cursor-pointer">
          <input
            type="checkbox"
            checked={selectedRating === rating}
            onChange={() => handleChange(rating)}
          />

          {[1, 2, 3, 4, 5].map((starNumber) => (
            <img
              key={starNumber}
              src={starNumber <= rating ? star : dullstar}
              alt=""
              className="w-4 h-4"
            />
          ))}

          <span className="text-base">({rating} Star)</span>
        </label>
      ))}
    </div>
  );
};

export default Rating;
