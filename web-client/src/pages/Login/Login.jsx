import React, { useState } from "react";

import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";

import { useNavigate } from "react-router-dom";

import { useUserStore } from "../../store";

export default function Login() {
  const [username, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const { login } = useUserStore(({ login }) => ({ login }));

  const handleSubmitClick = () => {
    login({ username, password });
  };

  return (
    <div className="card flex justify-content-center">
      <div className="card-container w-min">
        <div className="flex align-items-center justify-content-center mt-4 p-inputtext-lg">
          <span className="p-float-label">
            <InputText
              id="username"
              value={username}
              onChange={(e) => setEmail(e.target.value)}
            />
            <label htmlFor="username">User username</label>
          </span>
        </div>
        <div className="flex align-items-center justify-content-center mt-4 p-inputtext-lg">
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
        <div className="flex justify-content-end mt-3">
          <Button
            className="border-round-xl"
            onClick={handleSubmitClick}
            label="Login"
          />
        </div>
      </div>
    </div>
  );
}
