import ProductCard from "./ProductCard";
import ad from "../assets/ad.svg";

const ProductGrid = ({ products, reviews }) => {
  return (
    <>
      <div className="grid grid-cols-4 gap-5">
        {products.map((product) => (
          <ProductCard
            key={product.productId}
            product={product}
            reviews={reviews}
          />
        ))}
      </div>

      <img src={ad} alt="Banner" className="rounded-2xl w-full mt-5 mb-5" />
    </>
  );
};

export default ProductGrid;
