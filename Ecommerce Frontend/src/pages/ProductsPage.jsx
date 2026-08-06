    import { useState } from "react";
    import Navbar from "../components/Navbar";
    import Products from "./Products";

    function ProductsPage() {
    const [search, setSearch] = useState("");

    return (
        <>
        <Navbar search={search} setSearch={setSearch} />
        <Products search={search} />
        </>
    );
    }

    export default ProductsPage;
