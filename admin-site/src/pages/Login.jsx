import React from "react";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const navigate = useNavigate();

  const onSubmit = (e) => {
    e.preventDefault();
    navigate("/dashboard");
  };
  return (
    <div style={{ padding: "40px 0px" }}>
      <form style={{ margin: "0 auto", width: "300px" }} onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="username" className="form-label">
            Username
          </label>
          <input
            type="email"
            className="form-control"
            id="username"
            placeholder=""
          ></input>
        </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">
            Password
          </label>
          <input
            type="password"
            className="form-control"
            id="password"
            autoComplete="off"
          ></input>
        </div>
        <div class="row mb-3">
          <div class="col-sm-12">
            <div class="form-check">
              <input class="form-check-input" type="checkbox" id="gridCheck1" />
              <label class="form-check-label" for="gridCheck1">
                Remember me
              </label>
            </div>
          </div>
        </div>
        <button
          type="submit"
          className="btn btn-primary mb-3"
          style={{ width: "100%" }}
        >
          Login
        </button>
      </form>
    </div>
  );
}
