import React from "react";
import { Link } from "react-router-dom";
export default function Dashboard() {
  return (
    <div>
      <h1>Dashboard</h1>
      <Link to="/">
        <a href="/#">Logout</a>
      </Link>
    </div>
  );
}
