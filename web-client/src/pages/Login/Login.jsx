import React, { useState } from "react";

import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";

import axios from "axios";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [username, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleSubmitClick = () => {
    console.log(password);
    axios
      .post("/auth", {
        username,
        password,
      })
      .then((response) => {
        localStorage.setItem("token", response.headers.get("X-Session-Id"));
        console.log(response.headers);
      });
  };

  return (
    <div className="card">
      <div className="flex align-items-center justify-content-center m-4">
        <span className="p-float-label ">
          <InputText
            id="username"
            value={username}
            onChange={(e) => setEmail(e.target.value)}
          />
          <label htmlFor="username">User username</label>
        </span>
      </div>
      <div className="flex align-items-center justify-content-center m-4">
        <span className="p-float-label">
          <Password
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            toggleMask
            feedback={false}
          />
          <label htmlFor="password">Password</label>
        </span>
      </div>
      <div className="flex align-items-center justify-content-center m-4">
        <Button onClick={handleSubmitClick} label="Submit" />
      </div>
    </div>
  );
}
