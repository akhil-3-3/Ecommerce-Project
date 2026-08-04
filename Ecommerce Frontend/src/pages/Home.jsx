import React, { useState } from "react";
import Navbar from "../components/Navbar";
import Products from "./Products";

const Home = () => {
  const [search, setSearch] = useState("");

  return (
    <div>
      <Navbar search={search} setSearch={setSearch} />
      <Products search={search} />
    </div>
  );
};

export default Home;
