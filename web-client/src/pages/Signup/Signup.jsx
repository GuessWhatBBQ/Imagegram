import React, { useState } from "react";

import { InputText } from "primereact/inputtext";
import { Password } from "primereact/password";
import { Button } from "primereact/button";

import { useNavigate } from "react-router-dom";

import { useUserStore } from "../../store";

export default function Signup() {
  const [username, setEmail] = useState("");
  const [fullname, setFullName] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const { signup, error, login } = useUserStore(({ signup, error, login }) => ({
    signup,
    error,
    login,
  }));

  const handleSubmitClick = async () => {
    const onLoginSuccess = () => {
      navigate("/feed");
    };
    const onSignupSuccess = async () => {
      await login({ username, password }, onLoginSuccess);
    };
    await signup({ username, fullname, password }, onSignupSuccess);
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
            <InputText
              id="fullname"
              value={fullname}
              onChange={(e) => setFullName(e.target.value)}
            />
            <label htmlFor="fullname">Full name</label>
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
        <p>{error}</p>
        <div className="flex justify-content-end mt-3">
          <Button
            className="border-round-xl"
            onClick={handleSubmitClick}
            label="Signup"
          />
        </div>
      </div>
    </div>
  );
}
