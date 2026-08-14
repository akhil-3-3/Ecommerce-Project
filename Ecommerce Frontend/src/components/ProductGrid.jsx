import ProductCard from "./ProductCard";

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
    </>
  );
};

export default ProductGrid;
