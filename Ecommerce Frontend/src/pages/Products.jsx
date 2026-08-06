import { useEffect, useMemo, useState } from "react";
import Sidebar from "../components/Sidebar";
import ProductGrid from "../components/ProductGrid";
import Footer from "../components/Footer";
import { getProducts, searchProducts } from "../api/productApi";
import { getStocks } from "../api/stockApi";
import { useLocation } from "react-router-dom";

function Products({ search }) {
  const [products, setProducts] = useState([]);
  const [includeOutOfStock, setIncludeOutOfStock] = useState(false);
  useEffect(() => {
    if (products.length > 0) {
    }
  }, [products]);
  const location = useLocation();

  const selectedGender = location.state?.gender || null;

  const [selectedFilters, setSelectedFilters] = useState([]);
  const [selectedRating, setSelectedRating] = useState(null);
  const [selectedBrands, setSelectedBrands] = useState([]);

  const [sortOption, setSortOption] = useState("Default Sorting");

  // ==========================
  // LOAD / SEARCH PRODUCTS
  // ==========================
  useEffect(() => {
    if (search?.trim()) {
      handleSearch();
    } else {
      loadProducts();
    }
  }, [search]);

  const loadProducts = async () => {
    try {
      const productsData = await getProducts();
      const stocksData = await getStocks();

      const productsWithStock = productsData.map((product) => {
        const stock = stocksData.find((s) => s.productId === product.productId);

        return {
          ...product,
          stockQuantity: stock ? stock.quantity : 0,
        };
      });

      setProducts(productsWithStock);
    } catch (err) {
      console.error(err);
      setProducts([]);
    }
  };
  const handleSearch = async () => {
    try {
      const productsData = await searchProducts(search.trim());

      const stocksData = await getStocks();

      const productsWithStock = productsData.map((product) => {
        const stock = stocksData.find((s) => s.productId === product.productId);

        return {
          ...product,
          stockQuantity: stock ? stock.quantity : 0,
        };
      });

      setProducts(productsWithStock);
    } catch (err) {
      console.error(err);
      setProducts([]);
    }
  };

  // ==========================
  // CATEGORY FILTER
  // ==========================

  const handleFilterChange = (filters) => {
    setSelectedFilters(filters);
  };

  // ==========================
  // RATING FILTER
  // ==========================

  const handleRatingChange = (rating) => {
    setSelectedRating(rating);
  };

  // ==========================
  // BRAND FILTER
  // ==========================

  const handleBrandChange = (brands) => {
    setSelectedBrands(brands);
  };

  // ==========================
  // FILTER + SORT
  // ==========================

  const filteredProducts = useMemo(() => {
    const result = products.filter((product) => {
      // Category Filter
      const categoryMatch =
        selectedFilters.length === 0 ||
        selectedFilters.includes(product.categoryName);

      // Rating Filter
      const rating = Number(product.rating ?? 0);

      const ratingMatch =
        selectedRating === null ||
        (rating >= selectedRating && rating < selectedRating + 1);

      // Brand Filter
      const brandMatch =
        selectedBrands.length === 0 ||
        selectedBrands.includes(product.brandName);

      // Stock Filter
      const stockMatch = includeOutOfStock || product.stockQuantity > 0;

      // Gender Filter
      const genderMatch =
        !selectedGender ||
        product.gender?.toLowerCase() === selectedGender.toLowerCase();

      return (
        categoryMatch && ratingMatch && brandMatch && stockMatch && genderMatch
      );
    });

    switch (sortOption) {
      case "Price High To Low":
        result.sort((a, b) => b.price - a.price);
        break;

      case "Price Low To High":
        result.sort((a, b) => a.price - b.price);
        break;

      case "Customer Rating":
        result.sort((a, b) => Number(b.rating ?? 0) - Number(a.rating ?? 0));
        break;

      case "A to Z":
        result.sort((a, b) => a.productName.localeCompare(b.productName));
        break;

      case "Z to A":
        result.sort((a, b) => b.productName.localeCompare(a.productName));
        break;

      case "Default Sorting":
      default:
        break;
    }

    return result;
  }, [
    products,
    selectedFilters,
    selectedRating,
    selectedBrands,
    includeOutOfStock,
    selectedGender,
    sortOption,
  ]);
  return (
    <div className="font-actor mx-auto px-6 py-2">
      {/* ==========================
          HEADER
      ========================== */}

      <div className="flex items-end justify-between">
        <div>
          <h1 className="text-[35px]">Fragrance</h1>

          <div className="mt-1 flex flex-wrap items-center gap-3 text-sm text-gray-400">
            <span className="border-r border-gray-400 pr-10">
              Showing {filteredProducts.length} Items
            </span>

            {/* Category filters */}
            {selectedFilters.map((filter) => (
              <span key={filter} className="border border-gray-300 px-3 py-1">
                × {filter}
              </span>
            ))}

            {/* Rating filter */}
            {selectedRating !== null && (
              <span className="border border-gray-300 px-3 py-1">
                × {selectedRating} Star
              </span>
            )}

            {/* Brand filters */}
            {selectedBrands.map((brand) => (
              <span key={brand} className="border border-gray-300 px-3 py-1">
                × {brand}
              </span>
            ))}
          </div>
        </div>

        {/* Sorting */}
        <select
          value={sortOption}
          onChange={(e) => setSortOption(e.target.value)}
          className="w-56 rounded-xl border border-gray-300 bg-white px-4 py-2 text-sm shadow-sm outline-none"
        >
          <option value="Default Sorting">Default Sorting</option>

          <option value="Price High To Low">Price High To Low</option>

          <option value="Price Low To High">Price Low To High</option>

          <option value="Customer Rating">Customer Rating</option>

          <option value="A to Z">A to Z</option>

          <option value="Z to A">Z to A</option>
        </select>
      </div>

      {/* ==========================
          CONTENT
      ========================== */}

      <div className="mt-6 flex items-start gap-6">
        <Sidebar
          onFilterChange={handleFilterChange}
          onRatingChange={handleRatingChange}
          onBrandChange={handleBrandChange}
          includeOutOfStock={includeOutOfStock}
          setIncludeOutOfStock={setIncludeOutOfStock}
        />

        <div className="flex-1">
          <ProductGrid products={filteredProducts} />
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default Products;
